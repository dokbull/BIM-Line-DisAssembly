using lib.plc;
using Lib.UI.Generic.DarkMode.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static bim_base.CSTATION;
using static bim_base.data.CIM.CIMEnumeric;
using static CIMRead;
using static CIMWrite;
using static System.Windows.Forms.AxHost;

namespace bim_base.data.CIM
{


    internal class Automation
    {
        DateTime startTime;

        #region Delegate

        public delegate void OnReceivedTerminalDisplayEventHandler(int _MessageNum, string _MessageText);
        public delegate void OnReceivedOperatorCallEventHandler(int _OpCallNum, string _OpCallText);
        public delegate void OnReceivedInterlockEventHandler(int _ID, string _Message, EnumInterlockRCMD _RCMD);
        public delegate void OnReleaseInterlockEventHandler(int _ID, EnumInterlockRCMD _RCMD, string _LogMessage);
        public delegate void OnAutomationAlarmEventHandler();
        public delegate bool OnGetSampleExistEventHandler();
        public delegate (Dictionary<INPUT, bool> Inputs, Dictionary<OUTPUT, bool> Outputs, List<long> TackTime) OnGetMonitoringDataEventHandler();

        #endregion

        #region Constructor

        public Automation()
        {
        }

        #endregion

        #region Constants

        private const int HANDSHAKE_TIMEOUT_MILLISECONDS = 5000;
        private const int BIT_COUNT = 16;
        private const int HISTORY_MAX_COUNT = 100;

        #endregion

        #region Private Member

        private MelsecCCLink m_ccLink = null;
        private CIMRead m_Reader = new CIMRead();
        private CIMWrite m_Writer = new CIMWrite();

        //private RMS m_RMS = new RMS();

        private Thread mTh_IntervalRun = null;
        private object mLock_IntervalRun = new object();

        #endregion

        #region public Properties

        public static Automation Instance = new Automation();

        public bool IsInitialized { get; private set; } = false;

        public CIMRead CCIE_Reader
        {
            get { return this.m_Reader; }
            private set { this.m_Reader = value; }
        }

        public CIMWrite CCIE_Writer
        {
            get { return this.m_Writer; }
            private set { this.m_Writer = value; }
        }

        /// <summary>
        /// TODO CHECK LHJ -> HJP : Recipe 또는 모드 변경에서 연동 필요
        /// </summary>
        public bool UseTrackInValidationCheckMode { get; set; } = false;

        public EnumAlarmState AlarmState { get; private set; } = EnumAlarmState.None;

        public EnumEqControlMode EqControlMode { get; set; } = EnumEqControlMode.Manual;

        public List<HistoryItem> OperatorCallHistory { get; private set; } = new List<HistoryItem>();
        public List<HistoryItem> InterlockHistory { get; private set; } = new List<HistoryItem>();

        #endregion

        #region Event

        public event OnReceivedTerminalDisplayEventHandler ReceivedTerminalDisplayEvent;
        public event OnReceivedOperatorCallEventHandler ReceivedOperatorCallEvent;
        /// <summary>
        /// 설비 가동 정지, 인터락 메세지 팝업
        /// </summary>
        public event OnReceivedInterlockEventHandler ReceivedInterlockEvent;
        /// <summary>
        /// 인터락 모드 해제 및 AUTO로 정상 가등 모드로 운영 요청
        /// </summary>
        public event OnReleaseInterlockEventHandler ReleaseInterlockEvent;

        public event OnAutomationAlarmEventHandler AutomationAlarmEvent;

        public event OnGetSampleExistEventHandler GetSampleExistEvent;

        public event OnGetMonitoringDataEventHandler GetMonitoringDataEvent;

        #endregion

        #region Private Method

        //private void SyncCommCCIE()
        public void SyncCommCCIE()
        {
            bool ret = true;

            int[] readDataB = new int[512 / 32];
            int[] readDataW = new int[0x12FFF];
            int[] readDataW32 = new int[0x12FFF / 2];

            ret = m_ccLink.read(Addr.B, 0x1000, readDataB.Length, ref readDataB);
            ret &= m_ccLink.read(Addr.W, 0xD000, readDataW32.Length, ref readDataW32);

            if (ret == false)
            {
                // Debug.warning("ProcessMain::run cclink read failed");
            }

            for (int i = 0; i < readDataW32.Length; i++)
            {
                readDataW[i * 2] = readDataW32[i] & 0xFFFF;
                readDataW[i * 2 + 1] = readDataW32[i] >> 16;
            }

            m_Reader.toBitArray(readDataB);
            m_Reader.toWordArray(readDataW);

            int[] writeDataB = new int[512 / 32];
            int[] writeDataW = new int[0xCFFF];

            m_Writer.toArrayB(ref writeDataB);
            m_Writer.toArrayW(ref writeDataW);

            ret = m_ccLink.write(Addr.B, 0x0000, writeDataB.Length * 4, writeDataB);
            ret &= m_ccLink.write(Addr.W, 0x0000, writeDataW.Length * 2, writeDataW);


            if (ret)
            {
                // this.ResetSignals();
            }
            else
            {
                throw new Exception("failed to run cclink write failed");
            }
        }

        private void SleepWithDoEvent(int _milliseconds)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan ts = DateTime.Now  - startTime;

            while (ts.TotalMilliseconds < _milliseconds)
            {
                Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();
                ts = DateTime.Now - startTime;  
            }
        }

        private bool WaitBitSignal(CIMRead.READ_B _addr, bool _waitValue, int _timeoutMilliseconds = 0)
        {
            bool val = this.ReadBit(_addr);

            if (val == _waitValue) return true;

            DateTime startTime = DateTime.Now;
            TimeSpan ts = DateTime.Now - startTime;

            while (val == _waitValue)
            {
                if (_timeoutMilliseconds > 0 && ts.TotalMilliseconds >= _timeoutMilliseconds)
                {
                    //throw new Exception("WaitBitSignal Timeout Occurred. Addr: " + _addr.ToString() + ", WaitValue: " + _waitValue.ToString());
                    return false;
                }

                val = this.ReadBit(_addr);

                if (val == _waitValue) { return true; }

                this.SleepWithDoEvent(1);
                ts = DateTime.Now - startTime;
            }

            return false;
        }

        private bool HandShakeSignal(CIMWrite.WRITE_B _addrWrite, bool _writeValue, CIMRead.READ_B _addrRead, bool _readValue, int _timeoutSeconds = 0, bool _isOnError = false)
        {
            // TODO CHECK LHJ : H/S 진입시 중복 실행되지 않는지 확인 필요

            m_Writer.setBit(_addrWrite, _writeValue);
            //m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);

            CElaspedTimer m_timeout = new CElaspedTimer(_timeoutSeconds * 1000);

            bool ret = false;
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(_addrRead) == true)
                {
                    m_Writer.setBit(_addrWrite, !_writeValue);
                    ret = true;
                    break;
                }

                if (m_timeout.isElasped() == true)
                    break;

                Util.waitTick(100);
            }


            //try
            //{
            //    this.WriteBit(_addrWrite, _writeValue);

            //    //this.SleepWithDoEvent(100);

            //    this.WaitBitSignal(_addrRead, _readValue, _timeoutSeconds);


            //    this.SleepWithDoEvent(100);

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    if (_isOnError)
            //    {
            //        throw new Exception($"HandShakeSignal Error Occurred");
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //finally
            //{
            //    //this.WriteBit(_addrWrite, !_writeValue);
            //}

            return true;
        }

        private bool TryParseDateTime(string input, out int year, out int month, out int day, out int hour, out int minute, out int second)
        {
            year = month = day = hour = minute = second = 0;

            if (string.IsNullOrEmpty(input) || input.Length != 14)
                return false;

            // All substrings must be numeric
            if (!int.TryParse(input.Substring(0, 4), out year)) return false; // YYYY
            if (!int.TryParse(input.Substring(4, 2), out month)) return false; // MM
            if (!int.TryParse(input.Substring(6, 2), out day)) return false; // DD
            if (!int.TryParse(input.Substring(8, 2), out hour)) return false; // HH
            if (!int.TryParse(input.Substring(10, 2), out minute)) return false; // mm
            if (!int.TryParse(input.Substring(12, 2), out second)) return false; // ss

            // Validate ranges
            if (month < 1 || month > 12) return false;
            if (day < 1 || day > DateTime.DaysInMonth(year, month)) return false;
            if (hour < 0 || hour > 23) return false;
            if (minute < 0 || minute > 59) return false;
            if (second < 0 || second > 59) return false;

            return true;
        }

        private bool TryParseDateTime(string input, out DateTime dateTime)
        {
            dateTime = default(DateTime);
            if (!TryParseDateTime(input, out int y, out int mo, out int d, out int h, out int mi, out int s))
                return false;

            try
            {
                dateTime = new DateTime(y, mo, d, h, mi, s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // P/Invoke to set local system time. Requires the process to have the appropriate privileges (usually admin).
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetLocalTime(ref SYSTEMTIME st);

        private bool SetSystemLocalTime(DateTime dt)
        {
            var st = new SYSTEMTIME
            {
                Year = (ushort)dt.Year,
                Month = (ushort)dt.Month,
                Day = (ushort)dt.Day,
                Hour = (ushort)dt.Hour,
                Minute = (ushort)dt.Minute,
                Second = (ushort)dt.Second,
                Milliseconds = (ushort)dt.Millisecond,
                DayOfWeek = (ushort)dt.DayOfWeek,
            };

            try
            {
                return SetLocalTime(ref st);
            }
            catch
            {
                return false;
            }
        }


        private void ResetSignals()
        {
            foreach (string enumName in Enum.GetNames(typeof(CIMWrite.WRITE_B)))
            {
                CIMWrite.WRITE_B addrBit = (CIMWrite.WRITE_B)Enum.Parse(typeof(CIMWrite.WRITE_B), enumName);
                this.WriteBit(addrBit, false);
            }
        }

        private int BitsToInt(List<bool> _bits, bool _lsbFirst = true)
        {
            if (_bits == null) throw new ArgumentNullException(nameof(_bits));
            if (_bits.Count != BIT_COUNT) throw new ArgumentException("bits must have length 16", nameof(_bits));

            int value = 0;
            if (_lsbFirst)
            {
                for (int i = 0; i < BIT_COUNT; i++)
                {
                    if (_bits[i]) value |= (1 << i); // index 0 -> bit0(LSB)
                }
            }
            else
            {
                for (int i = 0; i < BIT_COUNT; i++)
                {
                    if (_bits[i]) value |= (1 << ((BIT_COUNT - 1) - i)); // index 0 -> bit15(MSB)
                }
            }
            return value;
        }

        #endregion

        #region Private Method : CIM

        private bool ReportInitializeCIM()
        {
            if (this.IsInitialized) return false;

            this.ResetSignals();

            // 초기 ALIVE 신호 OFF로 Reset
            if (this.HandShakeSignal(WRITE_B.ALIVEBIT_1, true, CIMRead.READ_B.ALIVEBIT_1, false, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                return false;

            try
            {
                this.IsInitialized = true;

                return true;
            }
            catch
            {
                return false;
            }

        }


        private void RequestDateTimeSet()
        {
            // Date Time 동기화 요청 신호 대기
            //if (this.WaitBitSignal(CIMRead.READ_B.DATETIMESET_2, true, HANDSHAKE_TIMEOUT_MILLISECONDS))
            //    return;

            if(m_Reader.readBit(CIMRead.READ_B.DATETIMESET_2) == false)
                return;

            // Date Time 동기화 처리
            //if (this.SetDateTime() == false) 
            this.SetDateTime();

            //m_Writer.setBit(CIMWrite.WRITE_B.DATETIMESET_2, true);

            if (this.HandShakeSignal(WRITE_B.DATETIMESET_2, true, CIMRead.READ_B.DATETIMESET_2, false, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                return;

            m_Writer.setBit(CIMWrite.WRITE_B.DATETIMESET_2, false);
            Thread.Sleep(200);
           
        }

        private void Alive()
        {
            try
            {
                bool alive = this.ReadBit(CIMRead.READ_B.ALIVEBIT_1);

                TimeSpan ts = DateTime.Now - startTime;

                if (ts.TotalSeconds > 5)
                {
                    this.WriteBit(WRITE_B.ALIVEBIT_1, !alive);
                    DateTime startTime = DateTime.Now;
                }
                //DateTime startTime = DateTime.Now;
                

                //while (val != _waitValue)
                //{
                //    if (_timeoutSeconds > 0 && ts.TotalSeconds >= _timeoutSeconds)
                //    {
                //        throw new Exception("WaitBitSignal Timeout Occurred. Addr: " + _addr.ToString() + ", WaitValue: " + _waitValue.ToString());
                //    }

                //    val = this.ReadBit(_addr);

                //    if (val == _waitValue) { return; }

                //    this.SleepWithDoEvent(1);
                //    ts = DateTime.Now - startTime;
                //}


                //this.HandShakeSignal(WRITE_B.ALIVEBIT_1, !alive, CIMRead.READ_B.ALIVEBIT_1, !alive, HANDSHAKE_TIMEOUT_SECONDS);

            }
            catch
            {
            }
        }


        private void RunScan()
        {
            if (this.OpenCCIE() == false)
                return;

            // TODO CHECK LHJ : 사전 테스트 끝나고 다시 복귀 필요
            //this.SyncCommCCIE(); 

            //return;

            //this.WriteBit(WRITE_B.ALIVEBIT_1, true);
            //Thread.Sleep(200);

            if (this.ReportInitializeCIM() == false)
                return;


            startTime = DateTime.Now;

            while (true)
            {
                //this.SyncCommCCIE();        //yj.lee

                //if (this.ReportInitializeCIM() == false)
                //    return;

                //TestAlive();
                this.Alive();
                this.RequestTerminalDisplay();
                this.RequestOperatorCall();
                this.RequestInterlcokState();
                this.RequestPpidList();
                this.RequestDateTimeSet();
                this.RequestRecipeDownload();
                this.RequestParameterQuery();

                this.ReportMonitoringData();
                this.ReportSampleProcessingState();
                Thread.Sleep(100);
            }
        }

        #endregion

        #region Private Method : CIM Request

        private void RequestTerminalDisplay()
        {
            try
            {

                if (this.ReadBit(CIMRead.READ_B.TERMINALDISPLAY_3) == false)
                    return;

                string msgNum = this.ReadWord(CIMRead.READ_W.ASCII_1_D04D_TerminalNumber);
                string msgText = this.ReadWord(CIMRead.READ_W.ASCII_60_D011_TerminalDisplayText);

                if (int.TryParse(msgNum, out int messageNum) == false)
                    return;

                ReceivedTerminalDisplayEvent?.Invoke(messageNum, msgText);

                this.WriteBit(WRITE_B.TERMINALDISPLAY_3, true);
                this.SleepWithDoEvent(1);
                this.WriteBit(WRITE_B.TERMINALDISPLAY_3, false);


            }
            catch
            {
            }
        }

        private void RequestOperatorCall()
        {
            try
            {
                if (this.ReadBit(CIMRead.READ_B.OPERATORCALL_4) == false)
                    return;

                string strOpCallNum = this.ReadWord(CIMRead.READ_W.ASCII_10_D058_OperatorCallID);
                string strOpCallText = this.ReadWord(CIMRead.READ_W.ASCII_60_D062_OperatorCallText);

                if (int.TryParse(strOpCallNum, out int opCallNum) == false)
                    return;

                this.OperatorCallHistory.Add(new HistoryItem(DateTime.Now, $"{opCallNum}", strOpCallText));
                if(this.OperatorCallHistory.Count > HISTORY_MAX_COUNT)
                {
                    this.OperatorCallHistory.RemoveAt(0);
                }

                this.WriteBit(WRITE_B.OPERATORCALL_4, true);

                // TODO CHECK LHJ to HJP : 설비 정지 필요, 터치판넬에서 메세지 팝업 확인
                ReceivedOperatorCallEvent?.Invoke(opCallNum, strOpCallText);

                // TODO CHECK LHJ : 여러개가 중복되어 수신될 경우, UI Popup에는 제일 마지막에 수신된 메세지, Confirm 보고는 제일 처음에 수신된 메세지

                this.WriteWord(WRITE_W.ASCII_10_0F80_OPCallIDComfirm, strOpCallNum);
                this.WriteWord(WRITE_W.ASCII_60_259C_UnitOPCallConfirmOPCallMessage, strOpCallText);

                this.WriteBit(WRITE_B.OPCALLCONFIRM_41, true);
                this.SleepWithDoEvent(1);
                this.WriteBit(WRITE_B.OPERATORCALL_4, false);
                this.WriteBit(WRITE_B.OPCALLCONFIRM_41, false);

            }
            catch
            {
            }
        }

        private void RequestInterlcokState()
        {
            try
            {

                if (this.ReadBit(CIMRead.READ_B.INTERLOCK_5) == false)
                    return;

                string strID = this.ReadWord(CIMRead.READ_W.ASCII_10_D09E_InterlockID);
                string strMessage = this.ReadWord(CIMRead.READ_W.ASCII_60_D0A8_InterlockMessage);
                string strRCMD = this.ReadWord(CIMRead.READ_W.ASCII_1_D0E4_InterlockRCMD);

                if (int.TryParse(strID, out int interlockID) == false)
                    return;

                if (Enum.TryParse<EnumInterlockRCMD>(strRCMD, out EnumInterlockRCMD rcmd) == false)
                    return;

                // Set Interlock
                string logMessage = $"{rcmd} Interlock (RCMD={(int)rcmd}) : {strMessage}";
                this.InterlockHistory.Add(new HistoryItem(DateTime.Now, strID, logMessage));
                if (this.InterlockHistory.Count > HISTORY_MAX_COUNT)
                {
                    this.InterlockHistory.RemoveAt(0);
                }

                this.WriteBit(WRITE_B.INTERLOCK_5, true);
                this.SetEqState(EnumInterlockState.On);
                this.SetEqState(EnumMoveState.Pause);

                if (this.ReceivedInterlockEvent == null)
                    throw new Exception("ReceivedInterlockEvent is not set.");

                // Show Popup + Signal Tower ON + Buzzor ON 
                ReceivedInterlockEvent.Invoke(interlockID, logMessage, rcmd);

                // Interlock Released (=Clear Popup)
                this.WriteBit(WRITE_B.INTERLOCK_5, false);

                // TODO CHECK LHJ to HJP : 알람이 발생하더라도 인터락 요청 상태이면 정상가동 불가
                // TODO CHECK LHJ : 여러개가 중복되어 수신될 경우, UI Popup에는 제일 마지막에 수신된 메세지, Confirm 보고는 제일 처음에 수신된 메세지
                this.WriteWord(WRITE_W.ASCII_10_1040_InterlockIDComfirm, strID);
                this.WriteWord(WRITE_W.ASCII_60_104A_InterlockMessageConfirm, strMessage);
                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, true);

                this.WaitBitSignal(READ_B.INTERLOCKCONFIRM_42, true, HANDSHAKE_TIMEOUT_MILLISECONDS);
                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, false);

                logMessage = $"{rcmd} Release Interlock (RCMD={(int)rcmd}) : {strMessage}";
                this.InterlockHistory.Add(new HistoryItem(DateTime.Now, strID, logMessage));
                if (this.InterlockHistory.Count > HISTORY_MAX_COUNT)
                {
                    this.InterlockHistory.RemoveAt(0);
                }

                if (this.ReleaseInterlockEvent == null)
                    throw new Exception("RequestAutoNormalModeEvent is not set.");

                this.ReleaseInterlockEvent?.Invoke(interlockID, rcmd, logMessage);

                this.SetEqState(EnumInterlockState.Off);
                this.SetEqState(EnumMoveState.Runnning);


            }
            catch
            {
            }
        }


        #endregion

        #region Private Method : CIM Request - RMS

        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        //PPID RULE
        //Manual 일때는 원격제어 변경 불가 NG
        //Auto일때 90 ~ 99으로 변경할려고 하면 NG 
        //0~ 89 원격용
        //TT_ 		//로 시작 
        //90~99
        //TT_     //로 사용
        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        private void RequestPpidList()
        {
            try
            {
                //ModelInfo
                if (m_Reader.readBit(CIMRead.READ_B.CURRENTEQUIPPPIDLISTREQUEST_56) == false)
                    return;

                UpdateModelList();

                m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, true);

                this.SleepWithDoEvent(1);
            }
            catch
            {

            }
            finally
            {
                m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, false);
            }
        }
        
        private void UpdateModelList()
        {
            List<string> items = new List<string>();

            for (int i = 0; i < Common.MODEL.Count; i++)
            {
                ModelInfo INFO = Common.MODEL[i];

                items.Add(INFO.modelName());
            }

            for (int i = 0; i < items.Count; i++)
            {
                var addr = (WRITE_W)((int)WRITE_W.ASCII_20_8A54_PPID_1 + i);
                m_Writer.wordData(addr).text = items[i];
            }
        }

        private void RequestRecipeDownload()
        {
            int nSelectedIdx = -1;
            string sRecipeName;

            try
            {
                //ModelInfo
                if (m_Reader.readBit(CIMRead.READ_B.FORMATTEDPROCESSPROGRAMSEND2_52) == true)
                {
                    string text = m_Reader.wordData(READ_W.ASCII_3_EF20_FormattedProcessProgramSendRecipeNumber).text;

                    nSelectedIdx = Util.toInt32(text.PadLeft(2));
                    string cmd = text.PadRight(1);

                    if (cmd == "N" || cmd == "O" || cmd == "D" || cmd == "G" || cmd == "C")
                    {
                        // N make new teach
                        // O make without teach
                        // D delete all teach
                        // G delete without teach
                        // C fix
                    }

                    //ASCII_1_125D_FormattedProcessProgramAck Cerrect case 0

                    m_Writer.wordData((WRITE_W)WRITE_W.ASCII_1_125D_FormattedProcessProgramAck).text = "0";

                    m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND2_52, true);


                    //sRecipeName = m_Reader.wordData(READ_W.ASCII_20_DF82_PPID).text?.Trim() ?? "";

                    //POS[] pos = ReadTeachPos();

                    //for (int i = 0; i < (int)TEACH_POS.MAX; i++)
                    //{
                    //    Common.MODEL[nSelectedIdx].setTeachPos(i, pos[i]);
                    //}

                    //m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND2_52, true);

                    //ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
                    //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

                    //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 1;
                    //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

                    //WriteTeachPos(INFO);

                    //this.HandShakeSignal(
                    //    WRITE_B.PPIDCHANGE_21,
                    //    true,
                    //    READ_B.PPIDCHANGE_21,
                    //    true,
                    //    HANDSHAKE_TIMEOUT_SECONDS);

                }
            }
            catch
            {

            }
            finally
            {
                //m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
            }

        }

        private void RequestParameterQuery()
        {
            string sReqPPID = "";
            int nSelectedIdx = -1;

            try
            {

                //Request PPID WORD(W4217) 다른 영역인데..
                if (m_Reader.readBit(CIMRead.READ_B.FORMATTEDPROCESSPROGRAMREQUEST_55) == true)
                {
                    //ReqPPIDINDEX  를 읽어서 레시피번호(인덱스)의 파라미터 값을 씀
                    //sReqPPID = m_Reader.wordData(CIMRead.READ_W.ASCII_20_D1F0_ReqPPID).text;
                    nSelectedIdx = m_Reader.wordData(CIMRead.READ_W.DEC_1_D206_ReqPPIDINDEX).value;

                    //for (int i = 0; i < Common.MODEL.Count(); i++)
                    //{
                    //    ModelInfo info = Common.MODEL_INFO(i);

                    //    if (info.modelName().Trim() == sReqPPID)
                    //    {
                    //        nSelectedIdx = i;
                    //        break;
                    //    }
                    //}

                    //알람 발생
                    if (nSelectedIdx < 0 && nSelectedIdx > 99)
                        return;

                    ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx-1);// Common.MODEL[0];
                    m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

                    m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 0;
                    m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

                    WriteTeachPos(INFO);

                    m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMREQUEST_55, true);

                    this.SleepWithDoEvent(1);
                }
            }
            catch
            {

            }
            finally
            {
                m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMREQUEST_55, false);
            }

        }

        #endregion

        #region Private Method : Status

        private void ReportMonitoringData()
        {
            try
            {

                if (GetMonitoringDataEvent == null)
                    return;

                var fdc = GetMonitoringDataEvent.Invoke();

                int Index = (int)WRITE_B.PlugRemove_CV_In_Sensor;

                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_LD_CV_IN]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_LD_CV_MID]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_LD_CV_OUT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.ALIGN_CV_IN]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.ALIGN_CV_OUT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_ULD_CV_IN]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_ULD_CV_OUT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.UB_ULD_CV_IN_1]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.UB_ULD_CV_IN_2]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.UB_ULD_CV_OUT]);

                Index = (int)WRITE_B.Detach_Transfer_Grip_Detect_Sensor;

                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_IN_PP_GRIP]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_IN_REVERSE_DETECT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_SHUTTLE_STAGE_2_DETECT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_OUT_PP_GRIP_1]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_OUT_PP_GRIP_2]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_SHUTTLE_STAGE_3_DETECT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.MOLD_ULD_CV_OUT]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.UB_OUT_PP_VAC]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.UB_OUT_REVERSE_DETECT_1]);
                m_Writer.setBit((WRITE_B)Index++, fdc.Inputs[INPUT.UB_OUT_REVERSE_DETECT_2]);

                for(int i = 0; i < fdc.TackTime.Count -1; i++)
                    m_Writer.wordData((WRITE_W)(WRITE_W.DEC_2_B500_Tack1+1)).value = (int)fdc.TackTime[i];

                m_Writer.wordData((WRITE_W)(WRITE_W.DEC_2_B53E_TackRealTime)).value = (int)fdc.TackTime[fdc.TackTime.Count - 1];
            }
            catch
            {

            }
        }

        private void ReportSampleProcessingState()
        {
            try
            {

                if (this.GetSampleExistEvent == null)
                    return;

                bool isExist= this.GetSampleExistEvent.Invoke();

                this.SetEqState(isExist ? EnumRunState.Run : EnumRunState.Idle);
            }
            catch
            {

            }
            
        }

        #endregion

        #region Public Method : CCIE Comm


        public bool OpenCCIE()
        {
            m_ccLink = new MelsecCCLink();

            m_ccLink.NetworkNo = 1;
            m_ccLink.StationNo = 255;
            m_ccLink.ChannelNo = 151;

            if (!m_ccLink.open())
                return false;

            this.m_Reader = new CIMRead();
            this.m_Writer = new CIMWrite();

            return true;
        }


        public void WriteBit(CIMWrite.WRITE_B addr, bool value)
        {
            m_Writer.setBit(addr, value);
        }

        // TODO KGW : 입력에 대한 테스트로 추가, 테스트후 삭제
        public void WriteBit(CIMRead.READ_B addr, bool value)
        {
            m_Reader.setBit(addr, value);
        }

        public bool ReadBit(CIMWrite.WRITE_B addr)
        {
            return m_Writer.bit(addr);
        }

        public bool ReadBit(CIMRead.READ_B addr)
        {
            return m_Reader.readBit(addr);
        }

        public string ReadWord(CIMRead.READ_W addr)
        {
            CIMRead.WORD_DATA data = m_Reader.wordData(addr);

            string text = "";

            if (data.type == CIMRead.READ_TYPE.DEC)
                text = data.value.ToString();

            if (data.type == CIMRead.READ_TYPE.ASCII)
                text = data.text;

            return text;
        }

        public string ReadWord(CIMWrite.WRITE_W addr)
        {
            CIMWrite.WORD_DATA data = m_Writer.wordData(addr);

            string text = "";

            if (data.type == CIMWrite.WRITE_TYPE.DEC)
                text = data.value.ToString();

            if (data.type == CIMWrite.WRITE_TYPE.ASCII)
                text = data.text;

            return text;
        }

        public void WriteWord(CIMWrite.WRITE_W _addr, string _val)
        {
            CIMWrite.WORD_DATA data = m_Writer.wordData(_addr);

            switch (data.type)
            {
                case WRITE_TYPE.ASCII:
                    data.text = _val;
                    break;
                case WRITE_TYPE.DEC:
                    data.value = Util.toInt32(_val);
                    break;
                case WRITE_TYPE.NONE:
                default:
                    break;
            }
        }

        // TODO KGW : 입력에 대한 테스트로 추가, 테스트후 삭제
        public void WriteWord(CIMRead.READ_W _addr, string _val)
        {
            CIMRead.WORD_DATA data = m_Reader.wordData(_addr);

            switch (data.type)
            {
                case READ_TYPE.ASCII:
                    data.text = _val;
                    break;
                case READ_TYPE.DEC:
                    data.value = Util.toInt32(_val);
                    break;
                case READ_TYPE.NONE:
                default:
                    break;
            }
        }

        // TODO KGW : 입력에 대한 테스트로 추가, 테스트후 삭제
        public void Test_RequestTerminalDisplay()
        {
            this.RequestTerminalDisplay();
        }

        // TODO KGW : 입력에 대한 테스트로 추가, 테스트후 삭제
        public bool Test_ReadTerminalDisplayReplyBit()
        {
            return this.ReadBit(CIMWrite.WRITE_B.TERMINALDISPLAY_3);
        }

        #endregion

        #region Public Method : CIM Initialize

        
        public async void Initialize()
        {
            this.mTh_IntervalRun = new Thread(new ThreadStart(this.RunScan));
            this.mTh_IntervalRun.IsBackground = true;
            this.mTh_IntervalRun.ApartmentState = ApartmentState.STA;
            this.mTh_IntervalRun.Start();

            await Task.Run(() =>
            {
                DateTime start = DateTime.Now;
                TimeSpan ts = DateTime.Now - start;

                while (this.IsInitialized == false)
                {
                    this.SleepWithDoEvent(1);

                    ts = DateTime.Now - start;
                    if (ts.TotalMilliseconds >= HANDSHAKE_TIMEOUT_MILLISECONDS)
                        break;
                }
            }).ConfigureAwait(true);

        }

        public bool SetDateTime()
        {
            if (this.IsInitialized == false)
                return false;

            // TODO CHECK LHJ : PC의 Local 시간에 Date Time이 변경되는지 여부 확인

            this.WriteBit(WRITE_B.DATETIMESET_2, false);

            string strDateTime = this.ReadWord(CIMRead.READ_W.ASCII_7_D000_Datetime);
            if (this.TryParseDateTime(strDateTime, out DateTime setDateTime) == false)
                return false;

            if (this.SetSystemLocalTime(setDateTime) == false)
            {
                return false;
            }

            this.WriteBit(WRITE_B.DATETIMESET_2, true);
            this.SleepWithDoEvent(1);
            this.WriteBit(WRITE_B.DATETIMESET_2, false);

            return true;
        }



        #endregion

        #region Public Method : CIM Equipment State


        public void SetEqState(CIMEnumeric.EnumAvailabilityState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002C_EQPAvailability, $"{_state}");
        }

        public void SetEqState(CIMEnumeric.EnumInterlockState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002D_EQPInterlock, $"{_state}");
        }
        public void SetEqState(CIMEnumeric.EnumMoveState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002E_EQPMove, $"{_state}");
        }
        public void SetEqState(CIMEnumeric.EnumRunState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002F_EQPRun, $"{_state}");
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public void SendTerminalDisplay(string _message)
        {
            if (this.IsInitialized == false)
                return;

            try
            {
                this.WriteWord(WRITE_W.ASCII_60_1086_TerminalDisplaySnd, _message);
                this.HandShakeSignal(WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, HANDSHAKE_TIMEOUT_MILLISECONDS); 

            }
            catch
            {
            }
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public void SendOperatorCall(string _message)
        {
            if (this.IsInitialized == false)
                return;

            try
            {
                // TODO CHECK LHJ : Operator Call은 ID가 존재하는데, ID는 어떻게 관리할지? 일단은 메시지만 전달하는 형태로 구현

                this.WriteWord(WRITE_W.ASCII_60_259C_UnitOPCallConfirmOPCallMessage, _message);
                this.HandShakeSignal(WRITE_B.EQUIPUNITOPCALLSEND_247, true, CIMRead.READ_B.OPCALLCONFIRM_41, true, HANDSHAKE_TIMEOUT_MILLISECONDS);

            }
            catch
            {
            }
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public bool ReleaseInterlcokState(string _message)
        {
            if (this.IsInitialized == false)
                return false;

            try
            {
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, false);
            }
        }

        public void AlarmOccured(EnumAlarmLevel _alarmLevel, int _alarmID, string _description)
        {
            if (this.IsInitialized == false)
                return;

            int wordIDX = _alarmID / BIT_COUNT;
            int bitIDX = _alarmID % BIT_COUNT;
            List<bool> wordToBits = new List<bool>();

            // 모든 비트를 false로 설정하고, 해당 인덱스만 true로 설정
            for (int i = 0; i < BIT_COUNT; i++) //for (int i = BIT_COUNT - 1; i >= 0; i--) 
            {
                wordToBits.Add(i == bitIDX);
            }

            // TODO CHECK LHJ : bit 순서가 역순인지 여부 확인 필요
            int writeValue = this.BitsToInt(wordToBits);

            this.WriteWord(WRITE_W.BIT_400_CAD4_Alarm, $"{writeValue}");

            switch (_alarmLevel)
            {
                case EnumAlarmLevel.HeavyAlarm:
                    this.AlarmState = EnumAlarmState.HeavyAlarm;
                    this.SetEqState(EnumAvailabilityState.Down);
                    this.SetEqState(EnumMoveState.Pause);
                    break;
                case EnumAlarmLevel.LightAlarm:
                default:
                    this.AlarmState = EnumAlarmState.LightAlarm;
                    break;
            }

        }

        public void AlarmReleased(int _alarmID, string _description)
        {
            if (this.IsInitialized == false)
                return;

            // word 상의 모든 bit를 0으로 reset
            this.WriteWord(WRITE_W.BIT_400_CAD4_Alarm, $"{0}");

            this.AlarmState = EnumAlarmState.None;

            this.SetEqState(EnumAvailabilityState.Up);
            this.SetEqState(EnumMoveState.Runnning);
        }

        public void AlarmListRequest()
        {
            if (this.IsInitialized == false)
                return;

            // TODO CHECK LHJ : Alarm 조회 기능에 대한 시나리오 구현 필요한지 확인
        }

        /// <summary>
        /// TPM Loss. Safety Door Open 등과 같이 설비가 비정상적으로 정지해야 하는 상황에서 터치화면에 팝업 메세지를 띄우고, 설비를 정지시키는 기능
        /// </summary>
        public void EqStopByOperator(EnmumEqStopByOperatorType _stopType)
        {
            if (this.IsInitialized == false)
                return;

            try
            {
                this.WriteBit(WRITE_B.TPMLOSSREADY_19, true);

                // Loss Code Popup
                Dictionary<string, string> itemsLossCode = new Dictionary<string, string>();
                foreach (var item in Enum.GetNames(typeof(EnmumEqStopByOperatorType)))
                {
                    EnmumEqStopByOperatorType _type = (EnmumEqStopByOperatorType)Enum.Parse(typeof(EnmumEqStopByOperatorType), item);

                    itemsLossCode.Add($"{(int)_type}", $"{_type}");
                }

                DarkMessageBox msgPopup = DarkMessageBox.CreateMessageBox(
                    "TPM Loss",
                    Lib.UI.Generic.Icons.EnumMessageBoxIcons.Warning,
                    "Loss Code를 선택하세요",
                    Lib.UI.Generic.DarkMode.EnumMessageBoxButtons.OK,
                    itemsLossCode);

                msgPopup.WindowState = FormWindowState.Maximized;
                msgPopup.MaximumSize = new System.Drawing.Size(1024, 768);
                msgPopup.StartPosition = FormStartPosition.CenterScreen;
                msgPopup.TopMost = true;
                DialogResult result = msgPopup.ShowDialog();

                this.WriteWord(WRITE_W.DEC_2_120F_TMPLossCode, msgPopup.SelectedItem.ID);
                this.WriteWord(WRITE_W.ASCII_20_1211_TMPLossDescp, msgPopup.SelectedItem.Text);

                msgPopup.Close();

                this.WaitBitSignal(READ_B.TPMLOSSREADY_19, true, HANDSHAKE_TIMEOUT_MILLISECONDS);

                this.WriteBit(WRITE_B.TPMLOSSREADY_19, false);
            }
            catch
            {

            }
            finally
            {
                this.WriteBit(WRITE_B.TPMLOSSREADY_19, false);
            }
        }


        #endregion

        #region Public Method : CIM Sample Track In/Out Processing 

        // TODO CHECK LHJ -> HJP : CELL 및 지그 투입 전 시점에 본 함수 호출
        public EnumJobProcessType TrackInLoadingCell(string _jigBarcode)
        {
            if (this.IsInitialized == false)
                return EnumJobProcessType.Fail;

            try
            {
                // TODO 사용되는 WORD DATA는 변경될 수 있음

                #region S6F203 Specipic Valid Request

                // Fix
                this.WriteWord(WRITE_W.ASCII_5_2980_SpecificValidationRequest_OptionCode1, "DASSBIMJIG");

                // TODO CHECK LHJ -> HJP: 임시로 _jigBarcode값을 할당. 바코드 리더 연동 시 삭제 필요
                _jigBarcode = "31A-005217-BME-PA-DTHB-UPPP101";
                //Cell ID는 Reading 하지 않으나, Reading 한 지그 바코드 값으로 대체하고 보고 
                this.WriteWord(WRITE_W.ASCII_20_2985_SpecificValidationRequest_CellID1, _jigBarcode);
                
                // OptionInfo는 Cell ID로 보고. 설비에서는 Cell ID를 Reading하지 않으므로 공백으로 보고
                this.WriteWord(WRITE_W.ASCII_20_2999_SpecificValidationRequest_OptionInfo1, "");

                this.WriteBit(WRITE_B.SPECIFICVALIDATIONREQUEST1_218, true);

                #endregion

                #region S3F103 Specific Valid Data

                if (this.WaitBitSignal(READ_B.SPECIFICVALIDATIONDATASEND1_223, true, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                    return EnumJobProcessType.Fail;

                string specDataJigID =  this.ReadWord(READ_W.ASCII_20_1256_SpecificValidationCarrierID1);
                string specDataCellID = this.ReadWord(READ_W.ASCII_40_126A_SpecificValidationCellID1);
                string specDataUiqueType = this.ReadWord(READ_W.ASCII_10_1292_SpecificValidationUniqueType1);
                string specDataProductID = this.ReadWord(READ_W.ASCII_20_129C_SpecificValidationProductID1);
                string specDataStepID = this.ReadWord(READ_W.ASCII_20_12B0_SpecificValidationStepID1);
                string specDataReplyStatus = this.ReadWord(READ_W.ASCII_2_12C4_SpecificValidationReplyStatus1);
                string specDataReplyText = this.ReadWord(READ_W.ASCII_60_12C6_SpecificValidationReplyText1);

                // Pass or Fail
                if(specDataReplyStatus.ToUpper() != EnumJobProcessType.Pass.ToString().ToUpper())
                    return EnumJobProcessType.Fail;

                #endregion

                #region S6F11 CEID=401 Process Start

                this.WriteWord(WRITE_W.ASCII_40_04A0_TrackInCellID1, specDataCellID);
                this.WriteWord(WRITE_W.ASCII_20_04C8_TrackInProductID1, specDataProductID);
                this.WriteWord(WRITE_W.ASCII_20_04DC_TrackInStepID1, specDataStepID);
                this.WriteWord(WRITE_W.ASCII_20_04F0_TrackInProcessJobID1, "");
                this.WriteWord(WRITE_W.DEC_2_0504_TrackInPlanQuantity1, "0");
                this.WriteWord(WRITE_W.DEC_2_0506_TrackInProcessQuantity1, "0");
                this.WriteWord(WRITE_W.ASCII_1_0508_TrackInReaderID1, "");// Cell ID를 Reading하지 않으므로 공백으로 Fix
                this.WriteWord(WRITE_W.ASCII_1_0509_TrackInRRC1, "0"); // 0 = OK, Cell ID를 Reading하지 않으므로 "0""으로 Fix
                this.WriteWord(WRITE_W.ASCII_1_050A_TrackInReasonCode1, "");
                this.WriteBit(WRITE_B.CELLSTARTPORT1_28, true);

                #endregion

                #region S2F43 RCMD=21 Job Process Start


                if (this.WaitBitSignal(READ_B.CELLJOBPROCESS1_74, true, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                    return EnumJobProcessType.Fail;

                this.WriteBit(WRITE_B.CELLSTARTPORT1_28, false);
                string jobPorcRCMD = this.ReadWord(READ_W.ASCII_2_D339_CellJobProcessRCMD1);
                string jobPorcJobID =  this.ReadWord(READ_W.ASCII_20_D33B_CellJobProcessJobID1);
                string jobPorcCellID =  this.ReadWord(READ_W.ASCII_40_D34F_CellJobProcessCellID1);
                string jobPorcProductID =  this.ReadWord(READ_W.ASCII_20_D377_CellJobProcessProductID1);
                string jobPorcStepID =  this.ReadWord(READ_W.ASCII_20_D38B_CellJobProcessStepID1);
                string jobPorcActionType =  this.ReadWord(READ_W.ASCII_10_D39F_CellJobProcessActionType1);


                if (int.TryParse(jobPorcRCMD, out int jobProcRCMDValue) == false)
                    return EnumJobProcessType.Fail;

                //S2F43 RCMD 21~24
                //  21 : Cell Job Process Start (Pass)
                //  22 : Cell Job Process Cancel(Fail)
                //  23 : Cell Job Process Pause (미사용)
                //  24 : Cell Job Process Resume (미사용)
                switch (jobProcRCMDValue)
                {
                    case 21:
                        this.HandShakeSignal(WRITE_B.CELLSTARTPORT1_28, true, READ_B.CELLSTARTPORT1_28, true, HANDSHAKE_TIMEOUT_MILLISECONDS);
                        return EnumJobProcessType.Pass;
                    default:
                        return EnumJobProcessType.Fail;
                }
                #endregion

            }
            catch
            {
                return EnumJobProcessType.Fail;
            }
            finally
            {
                this.WriteBit(WRITE_B.SPECIFICVALIDATIONREQUEST1_218, false);
                this.WriteBit(WRITE_B.CELLSTARTPORT1_28, false);

            }

        }


        // TODO CHECK LHJ -> HJP : CELL 및 지그 배출 전 시점에 본 함수 호출
        public EnumJobProcessType TrackOutUnloadingCell(string _jigBarcode)
        {
            if (this.IsInitialized == false)
                return EnumJobProcessType.Fail;

            try
            {
                // TODO 사용되는 WORD DATA는 변경될 수 있음

                #region s6f203 Specipic Valid Request

                // Fix
                this.WriteWord(WRITE_W.ASCII_5_2980_SpecificValidationRequest_OptionCode1, "DASSBIMJIG");

                // TODO CHECK LHJ -> HJP: 임시로 _jigBarcode값을 할당. 바코드 리더 연동 시 삭제 필요
                _jigBarcode = "31A-005217-BME-PA-DTHB-UPPP101";
                //Cell ID는 Reading 하지 않으나, Reading 한 지그 바코드 값으로 대체하고 보고 
                this.WriteWord(WRITE_W.ASCII_20_2985_SpecificValidationRequest_CellID1, _jigBarcode);

                // OptionInfo는 Cell ID로 보고. 설비에서는 Cell ID를 Reading하지 않으므로 공백으로 보고
                this.WriteWord(WRITE_W.ASCII_20_2999_SpecificValidationRequest_OptionInfo1, "");

                this.WriteBit(WRITE_B.SPECIFICVALIDATIONREQUEST1_218, true);

                #endregion

                #region S3F103 Specific Valid Data

                if (this.WaitBitSignal(READ_B.SPECIFICVALIDATIONDATASEND1_223, true, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                    return EnumJobProcessType.Fail;

                string specDataJigID = this.ReadWord(READ_W.ASCII_20_1256_SpecificValidationCarrierID1);
                string specDataCellID = this.ReadWord(READ_W.ASCII_40_126A_SpecificValidationCellID1);
                string specDataUiqueType = this.ReadWord(READ_W.ASCII_10_1292_SpecificValidationUniqueType1);
                string specDataProductID = this.ReadWord(READ_W.ASCII_20_129C_SpecificValidationProductID1);
                string specDataStepID = this.ReadWord(READ_W.ASCII_20_12B0_SpecificValidationStepID1);
                string specDataReplyStatus = this.ReadWord(READ_W.ASCII_2_12C4_SpecificValidationReplyStatus1);
                string specDataReplyText = this.ReadWord(READ_W.ASCII_60_12C6_SpecificValidationReplyText1);

                // Pass or Fail
                if (specDataReplyStatus.ToUpper() != EnumJobProcessType.Pass.ToString().ToUpper())
                    return EnumJobProcessType.Fail;

                #endregion


                #region S6F11 CEID=406 Process Complete


                this.WriteWord(WRITE_W.ASCII_40_0760_TrackOutCellID1, specDataCellID);
                this.WriteWord(WRITE_W.ASCII_20_0788_TrackOutProductID1, specDataProductID);
                this.WriteWord(WRITE_W.ASCII_20_079C_TrackOutStepID1, specDataStepID);
                this.WriteWord(WRITE_W.ASCII_20_07B0_TrackOutProcessJobID1, "");
                this.WriteWord(WRITE_W.DEC_2_07C4_TrackOutPlanQuantity1, "");
                this.WriteWord(WRITE_W.DEC_2_07C6_TrackOutProcessQuantity1, "");
                this.WriteWord(WRITE_W.ASCII_1_07C8_TrackOutReaderID1, "");//OUT MCR 없으면 / MCR 미사용 : Null처리
                this.WriteWord(WRITE_W.ASCII_1_07C9_TrackOutRRC1, "2");//0 : OK , 1 : Error, 2 : Reader 기능 미사용
                this.WriteWord(WRITE_W.ASCII_10_07CA_TrackOutOperatorID1_1, "");
                this.WriteWord(WRITE_W.ASCII_10_07D4_TrackOutOperatorID1_2, "");
                this.WriteWord(WRITE_W.ASCII_10_07DE_TrackOutOperatorID1_3, "");
                this.WriteWord(WRITE_W.ASCII_1_07E8_TrackOutJudge1, "");
                this.WriteWord(WRITE_W.ASCII_10_07F3_TrackOutReasonCode1, "");
                this.WriteWord(WRITE_W.ASCII_20_07FD_TrackOutDescription1, "");
                this.WriteBit(WRITE_B.CELLCOMPPORT1_34, true);

                #endregion

                return EnumJobProcessType.Pass;
            }
            catch
            {
                return EnumJobProcessType.Fail;
            }
            finally
            {
                this.WriteBit(WRITE_B.SPECIFICVALIDATIONREQUEST1_218, false);
                this.WriteBit(WRITE_B.CELLCOMPPORT1_34, false);


            }
        }


        #endregion

        #region Public Method : CIM RMS
        //int CommandHoldTimeMs = 5000;

        // PPID Change (JOB Change)
        public bool PpidChange()
        {
            //if (this.IsInitialized == false)
            //    return false;

            ModelInfo info = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = info.modelName();

            m_Writer.setBit(WRITE_B.PPIDCHANGE_21, true);

            CElaspedTimer m_timeout = new CElaspedTimer(1000);

            bool ret = false;
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(READ_B.PPIDCHANGE_21) == true)
                {
                    m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
                    ret = true;
                    break;
                }

                if (m_timeout.isElasped() == true)
                    break;

                Util.waitTick(100);
            }

            return ret;


            //return this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, HANDSHAKE_TIMEOUT_SECONDS);
        }

        // PPID 생성
        public bool PpidCreate(int index = 0)
        {
            //if (this.IsInitialized == false)
            //    return false;

            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 3;
            Conf.CURR_MODEL_IDX = index;
            //ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            ModelInfo INFO = Common.MODEL_INFO(index);// Common.MODEL[0];
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 1;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            //ParameterChange2RecipeNumber    PPID Number룰 데로 추가 필요.
            //1-90 -> 상위에서 받는거. TT_ X
            //91-99-> 내부에서 만드는거 TT_로 시작
            //string sName = (Conf.CURR_MODEL_IDX + 1).ToString() + "O";
            string sName = (index).ToString() + "O";
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_3_23D9_ParameterChange2RecipeNumber).text = sName; //추후 문서 보고 수정 필요.
           
            WriteTeachPos(INFO);

            //if(this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, HANDSHAKE_TIMEOUT_SECONDS) == false)
            //    return false;
            Common.MODEL[81].saveModelName(sName);
            UpdateModelList();

            if(this.HandShakeSignal(WRITE_B.PARAMETERCHANGE2_23, true, READ_B.PARAMETERCHANGE2_23, true, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                return false;
            
//yjlee.. Original Source >>
            //m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, true);

            //CElaspedTimer m_timeout = new CElaspedTimer(1000);

            //bool ret = false;
            //m_timeout.start();

            //while (true)
            //{
            //    if (m_Reader.readBit(READ_B.PARAMETERCHANGE2_23) == true)
            //    {
            //        m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, false);
            //        ret = true;
            //        break;
            //    }

            //    if (m_timeout.isElasped() == true)
            //        break;

            //    Util.waitTick(100);
            //}
            //yjlee.. Original Source <<
            return true;
        }

        void WriteTeachPos(ModelInfo info)
        {
            int baseEnum = (int)WRITE_W.ASCII_20_923A_PickPPWaitName;

            for (int i = 0; i < (int)TEACH_POS.MAX; i++)
            {
                POS p = info.teachPos((TEACH_POS)i);

                int idx = baseEnum + i * 8;

                m_Writer.wordData((WRITE_W)(idx + 0)).text = p.name;

                m_Writer.wordData((WRITE_W)(idx + 1)).value = (int)(p.x * 1000);
                m_Writer.wordData((WRITE_W)(idx + 2)).value = (int)(p.y * 1000);
                m_Writer.wordData((WRITE_W)(idx + 3)).value = (int)(p.z * 1000);

                m_Writer.wordData((WRITE_W)(idx + 4)).value = (int)(p.zL * 1000);
                m_Writer.wordData((WRITE_W)(idx + 5)).value = (int)(p.zR * 1000);

                m_Writer.wordData((WRITE_W)(idx + 6)).value = (int)(p.xB * 1000);
                m_Writer.wordData((WRITE_W)(idx + 7)).value = (int)p.vel;
            }
        }

        void WriteMcPos(ModelInfo info)
        {
            int baseEnum = (int)WRITE_W.ASCII_20_A1C4_PickPpWaitName;

            for (int i = 0; i < (int)TEACH_POS.MAX; i++)
            {
                POS p = info.teachPos((TEACH_POS)i);

                int idx = baseEnum + i * 8;

                m_Writer.wordData((WRITE_W)(idx + 0)).text = p.name;

                m_Writer.wordData((WRITE_W)(idx + 1)).value = (int)(p.x * 1000);
                m_Writer.wordData((WRITE_W)(idx + 2)).value = (int)(p.y * 1000);
                m_Writer.wordData((WRITE_W)(idx + 3)).value = (int)(p.z * 1000);

                m_Writer.wordData((WRITE_W)(idx + 4)).value = (int)(p.zL * 1000);
                m_Writer.wordData((WRITE_W)(idx + 5)).value = (int)(p.zR * 1000);

                m_Writer.wordData((WRITE_W)(idx + 6)).value = (int)(p.xB * 1000);
                m_Writer.wordData((WRITE_W)(idx + 7)).value = (int)p.vel;
            }
        }

        // PPID 삭제
        public bool PpidDelete(int Index = 0)
        {
            //if (this.IsInitialized == false)
            //    return false;

            //ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            ModelInfo INFO = Common.MODEL_INFO(Index);// Common.MODEL[0];
            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 2;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            //ParameterChange2RecipeNumber    PPID Number룰 데로 추가 필요.
            //string sName = (Conf.CURR_MODEL_IDX + 1).ToString() + "D";
            string sName = (Index).ToString() + "D";
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_3_23D9_ParameterChange2RecipeNumber).text = sName; //추후 문서 보고 수정 필요.

            Common.MODEL[Index].saveModelName("");
            UpdateModelList();

            //WriteTeachPos(INFO);

            //if (this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, HANDSHAKE_TIMEOUT_SECONDS) == false)
            //    return false;
            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, true);
            //m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);

            CElaspedTimer m_timeout = new CElaspedTimer(10000);

            bool ret = false;
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(READ_B.PARAMETERCHANGE2_23) == true)
                {
                    m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, false);
                    ret = true;
                    break;
                }

                if (m_timeout.isElasped() == true)
                    break;

                Util.waitTick(100);
            }

            return true;
        }

        POS[] ReadTeachPos()
        {
            int baseEnum = (int)READ_W.ASCII_20_DF96_PICK_PP_WAIT_NAME;
            POS[] p = new POS[(int)TEACH_POS.MAX];

            for (int i = 0; i < (int)TEACH_POS.MAX; i++)
            {
                p[i] = new POS();   // ★ 필요 (POS가 class일 경우)

                int idx = baseEnum + i * 8;

                p[i].name = m_Reader.wordData((READ_W)(idx + 0)).text;

                p[i].x = m_Reader.wordData((READ_W)(idx + 1)).value / 1000.0;
                p[i].y = m_Reader.wordData((READ_W)(idx + 2)).value / 1000.0;
                p[i].z = m_Reader.wordData((READ_W)(idx + 3)).value / 1000.0;

                p[i].zL = m_Reader.wordData((READ_W)(idx + 4)).value / 1000.0;
                p[i].zR = m_Reader.wordData((READ_W)(idx + 5)).value / 1000.0;

                p[i].xB = m_Reader.wordData((READ_W)(idx + 6)).value / 1000.0;
                p[i].vel = m_Reader.wordData((READ_W)(idx + 7)).value;
            }

            return p;
        }

        public bool ParameterChange(int index = 0)
        {
            //if (this.IsInitialized == false)
            //    return false;

            //ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            ModelInfo INFO = Common.MODEL_INFO(index);// Common.MODEL[0];
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 3;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            WriteTeachPos(INFO);


            //s
            //string sName = (Conf.CURR_MODEL_IDX + 1).ToString() + "O";
            string sName = (index+1).ToString() + "C";

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_3_23D9_ParameterChange2RecipeNumber).text = sName; //추후 문서 보고 수정 필요.
            //ParameterChange2RecipeNumber    PPID Number룰 데로 추가 필요.

            //if (this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, HANDSHAKE_TIMEOUT_SECONDS) == false)
            //    return false;

            //m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);

            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, true);

            //Common.MODEL[index].saveModelName(INFO.modelName());

            CElaspedTimer m_timeout = new CElaspedTimer(1000);

            bool ret = false;
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(READ_B.PARAMETERCHANGE2_23) == true)
                {
                    m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, false);
                    ret = true;
                    break;
                }

                if (m_timeout.isElasped() == true)
                    break;

                Util.waitTick(100);
            }

            return true;
        }

        #endregion

        #region Public Method : CIM ECM
        public void EquipConstantQuery()
        {
            if (this.IsInitialized == false)
                return;

            //레시피 설정교체시 업데이트 끝...
            //EquipmentConstantParameterChangeEvent 값변경시에 이 필드만 활성화 시켜주면됨. 
            //리플라이 bit 확인후 바로 끔.

            m_Writer.setBit(WRITE_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24, true);

            if (m_Reader.readBit(CIMRead.READ_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24) == true)
            {
                m_Writer.setBit(WRITE_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24, false);
            }
            //if (m_Reader.readBit(CIMRead.READ_B.EQUIPCONSTANTNAMELIST_53) == true)
            {
                //double[] vel = new double[(int)AXIS.MAX];
                //double[] acc = new double[(int)AXIS.MAX];

                //for (int i = 0; i < (int)AXIS.MAX; i++)
                //{
                //    AXIS axis = (AXIS)i;

                //    vel[i] = Conf.vel(axis);
                //    acc[i] = Conf.acc(axis);
                //} 


                //ModelInfo Mc = Common.MC;
                //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = Mc.modelName();

                //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 0;
                //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = Mc.modelName();

                //WriteTeachPos(Mc);
            }
        }

        public void ConstantnameListQuery()
        {
            if (this.IsInitialized == false)
                return;

            if (m_Reader.readBit(CIMRead.READ_B.EQUIPCONSTANTNAMELIST_53) == true)
            {
                ModelInfo Mc = Common.MC;
                //데이터 필드 업데이트 


                //ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx);// Common.MODEL[0];
                //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

                //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 0;
                //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

                WriteMcPos(Mc);
                //속도값 추가

                m_Writer.setBit(WRITE_B.EQUIPCONSTANTNAMELIST_53, true);

                this.SleepWithDoEvent(1);

                m_Writer.setBit(WRITE_B.EQUIPCONSTANTNAMELIST_53, true);    //시퀀스 다이어 그램 화살표 이상.
            }

            
            

        }
        #endregion

        public void TestAlive()
        {
            WriteBit(WRITE_B.ALIVEBIT_1, true);
        }
    }
}
