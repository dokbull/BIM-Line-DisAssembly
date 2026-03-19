using DevAge.Windows.Forms;
using lib.plc;
using Lib.UI.Generic.DarkMode;
using Lib.UI.Generic.DarkMode.Forms;
using Lib.UI.Generic.Icons;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Linq;
using static bim_base.CSTATION;
using static bim_base.data.CIM.CIMEnumeric;
using static CIMRead;
using static CIMWrite;
using static LSFenet.FNET;
using static System.Windows.Forms.AxHost;

namespace bim_base.data.CIM
{

    // TODO LHJ : check ui guide spec. and need make function UI of ETC sheet in scenario spec (data menu - function)
    internal class Automation
    {
        DateTime startTimeAlive;
        DateTime startTimeFDC;

        #region Delegate

        public delegate void OnResetSignalTowerBuzzorEventHandler();
        public delegate void OnReceivedOperatorCallEventHandler(int _OpCallNum, string _OpCallText);
        public delegate void OnReceivedInterlockEventHandler(string _ID, string _Message, EnumInterlockRCMD _RCMD);
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

        private const int HANDSHAKE_TIMEOUT_MILLISECONDS = 30000;
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

        private MessageData m_ReceivedTerminalDisplayData = new MessageData();
        public DarkMessageBox MessageBoxTerminalDisplay = DarkMessageBox.CreateMessageBox(
            "Terminal Dispaly",
            EnumMessageBoxIcons.Information,
            string.Empty,
            EnumMessageBoxButtons.OK);

        private MessageData m_ReceivedOpcallData = new MessageData();
        public DarkMessageBox MessageBoxOpcall = DarkMessageBox.CreateMessageBox(
            "Operator Call",
            EnumMessageBoxIcons.Warning,
            string.Empty,
            EnumMessageBoxButtons.OK);

        private MessageData m_ReceivedInterlockData = new MessageData();
        public DarkMessageBox MessageBoxInterlock = DarkMessageBox.CreateMessageBox(
            "Interlock",
            EnumMessageBoxIcons.Error,
            string.Empty,
            EnumMessageBoxButtons.OK);

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

        public EnumAlarmState AlarmState { get; private set; } = EnumAlarmState.None;

        public EnumEqControlMode EqControlMode { get; set; } = EnumEqControlMode.Manual;

        public List<HistoryItem> OperatorCallHistory { get; private set; } = new List<HistoryItem>();
        public List<HistoryItem> InterlockHistory { get; private set; } = new List<HistoryItem>();

        #endregion

        #region Event

        public event OnResetSignalTowerBuzzorEventHandler OnResetSignalTowerBuzzorEvent; 
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
            int[] readDataW = new int[0x5FFF];
            int[] readDataW32 = new int[0x5FFF / 2];

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

        public void SleepWithDoEvent(int _milliseconds)
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

            CElaspedTimer m_timeout = new CElaspedTimer(_timeoutMilliseconds);
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(_addr) == _waitValue)
                    return true;

                if (m_timeout.isElasped() == true)
                    return false;

                Util.waitTick(100);
            }
        }

        private bool HandShakeSignal(CIMWrite.WRITE_B _addrWrite, bool _writeValue, CIMRead.READ_B _addrRead, bool _readValue, int _timeoutMilliseconds = 0, bool _isOnError = false)
        {
            m_Writer.setBit(_addrWrite, _writeValue);
            //m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);

            CElaspedTimer m_timeout = new CElaspedTimer(_timeoutMilliseconds);

            bool ret = false;
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(_addrRead) == _readValue)
                {
                    m_Writer.setBit(_addrWrite, !_writeValue);
                    ret = true;
                    break;
                }

                if (m_timeout.isElasped() == true)
                    break;

                Util.waitTick(100);
            }


            return ret;
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

        // Invoke to set local system time. Requires the process to have the appropriate privileges (usually admin).
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


                TimeSpan ts = DateTime.Now - startTimeAlive;

                if (ts.TotalSeconds > 5)
                {
                    this.WriteBit(WRITE_B.ALIVEBIT_1, !alive);
                    startTimeAlive = DateTime.Now;
                }
            }
            catch
            {
            }
        }


        private void RunScan()
        {
            if (this.OpenCCIE() == false)
                return;

            if (this.ReportInitializeCIM() == false)
                return;


            startTimeAlive = DateTime.Now;
            startTimeFDC = DateTime.Now;

            PpidChange(Conf.CURR_MODEL_IDX);

            while (true)
            {
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

                this.EquipConstantQuery();
                
                Thread.Sleep(1);
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

                string msgSummery = $"{messageNum} : {msgText}";

                // TODO LHJ : Need manage History

                if(this.MessageBoxTerminalDisplay.Visible == false)
                {
                    // 맨 처음에 받은 하나만 보존
                    this.m_ReceivedTerminalDisplayData.ID = $"{messageNum}";
                    this.m_ReceivedTerminalDisplayData.Message = msgText;
                }


                this.MessageBoxTerminalDisplay.Message = msgSummery;
                this.MessageBoxTerminalDisplay.TopMost = true;
                this.MessageBoxTerminalDisplay.MaximumSize = new System.Drawing.Size(1024, 768);
                this.MessageBoxTerminalDisplay.WindowState = FormWindowState.Maximized;
                this.MessageBoxTerminalDisplay.Refresh();

                Task.Run(async () => this.MessageBoxTerminalDisplay.ShowDialog());

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

                if (this.OperatorCallHistory.Count > HISTORY_MAX_COUNT)
                {
                    this.OperatorCallHistory.RemoveAt(0);
                }


                if (this.MessageBoxOpcall.Visible == false)
                {
                    // 맨 처음에 받은 하나만 보존
                    this.m_ReceivedOpcallData.ID = $"{opCallNum}";
                    this.m_ReceivedOpcallData.Message = strOpCallText;
                }

                this.WriteBit(WRITE_B.OPERATORCALL_4, true);

                this.MessageBoxOpcall.Message = $"{opCallNum} : {strOpCallText}";
                this.MessageBoxOpcall.TopMost = true;
                this.MessageBoxOpcall.MaximumSize = new System.Drawing.Size(1024, 768);
                this.MessageBoxOpcall.WindowState = FormWindowState.Maximized;
                this.MessageBoxOpcall.Refresh();

                Task.Run(async () => this.MessageBoxOpcall.ShowDialog());

                // TODO CHECK LHJ to HJP : 설비 정지 필요, 터치판넬에서 메세지 팝업 확인
                ReceivedOperatorCallEvent?.Invoke(opCallNum, strOpCallText);

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

                if (this.MessageBoxInterlock.Visible == false)
                {
                    // 맨 처음에 받은 하나만 보존
                    this.m_ReceivedInterlockData.ID = $"{interlockID}";
                    this.m_ReceivedInterlockData.Message = strMessage;
                    this.m_ReceivedInterlockData.Option = (int)rcmd;
                }

                // Set Interlock
                this.InterlockHistory.Add(new HistoryItem(DateTime.Now, $"{interlockID}", strMessage));
                if (this.InterlockHistory.Count > HISTORY_MAX_COUNT)
                {
                    this.InterlockHistory.RemoveAt(0);
                }

                // TODO CHECK LHJ to HJP : 알람이 발생하더라도 인터락 요청 상태이면 정상가동 불가
                this.WriteWord(WRITE_W.ASCII_10_1040_InterlockIDComfirm, $"{interlockID}");
                this.WriteWord(WRITE_W.ASCII_60_104A_InterlockMessageConfirm, strMessage);

                this.WriteBit(WRITE_B.INTERLOCK_5, true);
                this.SetEqState(EnumInterlockState.On);
                this.SetEqState(EnumMoveState.Pause);

                this.SleepWithDoEvent(500);


                if (this.ReceivedInterlockEvent == null)
                    throw new Exception("ReceivedInterlockEvent is not set.");

                string logMessage = $"{interlockID} : {strMessage}";
                this.MessageBoxInterlock.Message = logMessage;
                this.MessageBoxInterlock.TopMost = true;
                this.MessageBoxInterlock.MaximumSize = new System.Drawing.Size(1024, 768);
                this.MessageBoxInterlock.WindowState = FormWindowState.Maximized;
                this.MessageBoxInterlock.Refresh();

                Task.Run(async () => this.MessageBoxInterlock.ShowDialog());

                // Show Popup + Signal Tower ON + Buzzor ON 
                ReceivedInterlockEvent.Invoke($"{interlockID}", strMessage, rcmd);

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
                if (m_Reader.readBit(CIMRead.READ_B.CURRENTEQUIPPPIDLISTREQUEST_56) == false)
                    return;

                UpdateModelList();

                m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, true);

                this.SleepWithDoEvent(100);
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
                if (m_Reader.readBit(CIMRead.READ_B.FORMATTEDPROCESSPROGRAMSEND2_52) == true)
                {
                    string text = m_Reader.wordData(READ_W.ASCII_3_EF20_FormattedProcessProgramSendRecipeNumber).text;

                    string cleaned = new string(text.TakeWhile(char.IsDigit).ToArray());
                    nSelectedIdx = Util.toInt32(cleaned);

                    char ch;

                    if (text.Contains("N"))
                        ch = 'N';
                    else if (text.Contains("O"))
                        ch = 'O';
                    else if (text.Contains("D"))
                        ch = 'D';
                    else if (text.Contains("G"))
                        ch = 'G';
                    else if (text.Contains("C"))
                        ch = 'C';
                    else
                        return;

                    sRecipeName = m_Reader.wordData(READ_W.ASCII_20_DF82_PPID).text.Replace("\0", "");
                    int PPID_MODE = Convert.ToInt32(m_Reader.wordData(READ_W.ASCII_2_DF7E_FormattedProcessProgramSendCCode).text);

                    //정상이면 0
                    //EQP already exist PPID : Nak 8
                    //other case : Nak 7

                    //(cmd == "N" || cmd == "O" || cmd == "D" || cmd == "G" || cmd == "C")
                    //{
                    //    // N make new teach
                    //    // O make without teach
                    //    // D delete all teach
                    //    // G delete without teach
                    //    // C fix
                    //}

                    if(nSelectedIdx == Conf.CURR_MODEL_IDX)
                    {
                        SendRecipeDownloadReply(8);
                        return;
                    }
                        

                    if ( (ch == 'N') || (ch == 'O') )
                    {
                        if (PPID_MODE == 1)
                        {
                            ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx);

                            if( (nSelectedIdx <= 90 && !sRecipeName.StartsWith("TT_")) ||
                                (nSelectedIdx > 90 && sRecipeName.StartsWith("TT_")))
                            {
                                if (INFO.modelName() == "")
                                {
                                    var p = ReadTeachPos();
                                    ModelInfo newInfo = Common.MODEL_INFO(nSelectedIdx);

                                    newInfo.setTeachPos(p);
                                    newInfo.saveModelName(m_Reader.wordData(READ_W.ASCII_20_DF82_PPID).text);

                                    SendRecipeDownloadReply(0);

                                    PpidCreate(nSelectedIdx, ch, true);
                                }
                                else
                                {
                                    SendRecipeDownloadReply(8);
                                }

                            }
                            else
                            {
                                SendRecipeDownloadReply(7);
                            }
                        }
                        else
                        {
                            SendRecipeDownloadReply(8);
                        }

                    }
                    else if ((ch == 'D') || (ch == 'G'))
                    {
                        if (PPID_MODE == 2)
                        {
                            ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx);

                            //if(true)
                            //{
                                if (INFO.modelName() != "")
                                {
                                    SendRecipeDownloadReply(0);
                                    PpidDelete(nSelectedIdx, ch);
                                }
                                else
                                {
                                    SendRecipeDownloadReply(8);
                                }
                            //}
                            //else
                            //{
                            //    SendRecipeDownloadReply(7);
                            //}
                        }
                        else
                        {
                            SendRecipeDownloadReply(8);
                        }
                    }
                    else if (ch == 'C')
                    {
                        ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx);

                        string sNewName = INFO.modelName().Replace("\0", "");
                        if (sNewName == sRecipeName) 
                        {
                            var p = ReadTeachPos();
                            ModelInfo newInfo = Common.MODEL_INFO(nSelectedIdx);

                            newInfo.setTeachPos(p);
                            SendRecipeDownloadReply(0);

                            ParameterChange(nSelectedIdx);
                        }
                        else
                        {
                            SendRecipeDownloadReply(8);
                        }
                    }
                    else
                    {
                        SendRecipeDownloadReply(7);
                    }
                }
            }
            catch
            {

            }
            finally
            {
                m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND2_52, false);
            }
        }

        private void SendRecipeDownloadReply(int nNum)
        {
            if(nNum == 0)
                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_1_125D_FormattedProcessProgramAck).text = "0";
            else
                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_1_125D_FormattedProcessProgramAck).text = Convert.ToString(nNum);

            this.SleepWithDoEvent(500);
            m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND2_52, true);

            if (this.WaitBitSignal(READ_B.FORMATTEDPROCESSPROGRAMSEND2_52, true, 10000) == false)
                return;

            m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND2_52, false);

            /* 검수때 소스
            //this.SleepWithDoEvent(2000);

            //if(m_Reader.readBit(READ_B.FORMATTEDPROCESSPROGRAMSEND2_52) == true)
            //m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND2_52, false); */
        }

        private void RequestParameterQuery()
        {
            string sReqPPID = "";
            int nSelectedIdx = -1;

            try
            {
                if (m_Reader.readBit(CIMRead.READ_B.FORMATTEDPROCESSPROGRAMREQUEST_55) == true)
                {
                    nSelectedIdx = m_Reader.wordData(CIMRead.READ_W.DEC_1_D206_ReqPPIDINDEX).value;

                    if (nSelectedIdx < 0 && nSelectedIdx > 99)
                        return;

                    ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx - 1);
                    m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

                    m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 0;
                    m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

                    WriteTeachPos(INFO);

                    m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMREQUEST_55, true);

                    this.SleepWithDoEvent(100);
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
                TimeSpan ts = DateTime.Now - startTimeFDC; ;

                if (ts.TotalSeconds > 1)
                {
                    startTimeFDC = DateTime.Now;
                }
                else
                    return;

                var fdc = GetMonitoringDataEvent.Invoke();

                int Index = (int)WRITE_B.PlugRemove_CV_In_Sensor;

                Index = (int)WRITE_B.Detach_Transfer_Grip_Detect_Sensor;

                m_Writer.wordData(WRITE_W.ASCII_20_AFD4_PlasmaCellID).text = "PLASMA_CELL_ID";
                m_Writer.wordData(WRITE_W.DEC_2_AFE8_PlasmaStepID).value = 1;
                m_Writer.wordData(WRITE_W.DEC_2_AFEA_PlasmaSpeed).value = 2;
                m_Writer.wordData(WRITE_W.DEC_2_AFEC_PlasmaPass).value = 1;

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

                // TODO CHECK LHJ -> HJP : Need Recovery after CIM Qual 
                //this.SetEqState(isExist ? EnumRunState.Run : EnumRunState.Idle);
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

            if (data.type == CIMWrite.WRITE_TYPE.DEC || data.type == WRITE_TYPE.BIT)
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
                case WRITE_TYPE.BIT:
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

        #endregion

        #region Public Method : CIM Initialize

        
        public async void Initialize()
        {
            this.mTh_IntervalRun = new Thread(new ThreadStart(this.RunScan));
            this.mTh_IntervalRun.IsBackground = true;
            //this.mTh_IntervalRun.ApartmentState = ApartmentState.STA;
            this.mTh_IntervalRun.Start();

            await Task.Run(() =>
            {
                DateTime start = DateTime.Now;
                TimeSpan ts = DateTime.Now - start;

                while (this.IsInitialized == false)
                {
                    this.SleepWithDoEvent(1000);

                    ts = DateTime.Now - start;
                    if (ts.TotalMilliseconds >= HANDSHAKE_TIMEOUT_MILLISECONDS)
                        break;
                }
            }).ConfigureAwait(true);


            this.MessageBoxTerminalDisplay.TitleButtonClickEvent += MessageBoxTerminalDisplay_TitleButtonClickEvent;
            this.MessageBoxOpcall.TitleButtonClickEvent += MessageBoxOpcall_TitleButtonClickEvent;
            this.MessageBoxInterlock.TitleButtonClickEvent += MessageBoxInterlock_TitleButtonClickEvent;
        }

        private void MessageBoxInterlock_TitleButtonClickEvent(Lib.UI.Generic.DarkMode.Controls.EnumTitleButton button)
        {
            switch (button)
            {
                case Lib.UI.Generic.DarkMode.Controls.EnumTitleButton.Button2:

                    // Interlock Released (=Clear Popup)

                    this.OnResetSignalTowerBuzzorEvent?.Invoke();
                    this.MessageBoxInterlock.Hide();

                    // Confirm 보고는 제일 처음에 수신된 메세지로 보고
                    this.WriteWord(WRITE_W.ASCII_10_1040_InterlockIDComfirm, $"{this.m_ReceivedInterlockData.ID}");
                    this.WriteWord(WRITE_W.ASCII_60_104A_InterlockMessageConfirm, this.m_ReceivedInterlockData.Message);

                    this.SleepWithDoEvent(500);

                    this.WriteBit(WRITE_B.INTERLOCK_5, false);
                    this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, true);

                    this.WaitBitSignal(READ_B.INTERLOCKCONFIRM_42, true, HANDSHAKE_TIMEOUT_MILLISECONDS);
                    this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, false);


                    this.InterlockHistory.Add(new HistoryItem(DateTime.Now, this.m_ReceivedInterlockData.ID, this.m_ReceivedInterlockData.Message));
                    if (this.InterlockHistory.Count > HISTORY_MAX_COUNT)
                    {
                        this.InterlockHistory.RemoveAt(0);
                    }

                    if (this.ReleaseInterlockEvent == null)
                        throw new Exception("RequestAutoNormalModeEvent is not set.");

                    this.ReleaseInterlockEvent?.Invoke(
                        int.Parse(this.m_ReceivedInterlockData.ID), 
                        (EnumInterlockRCMD)this.m_ReceivedInterlockData.Option, 
                        this.m_ReceivedInterlockData.Message);

                    // TODO LHJ->HJP : Change State to Auto Start
                    //this.SetEqState(EnumInterlockState.Off);
                    //this.SetEqState(EnumMoveState.Runnning);

                    break;
                case Lib.UI.Generic.DarkMode.Controls.EnumTitleButton.Button1:
                default:
                    break;
            }
        }

        private void MessageBoxOpcall_TitleButtonClickEvent(Lib.UI.Generic.DarkMode.Controls.EnumTitleButton button)
        {
            switch (button)
            {
                case Lib.UI.Generic.DarkMode.Controls.EnumTitleButton.Button1:
                default:
                    break;
                case Lib.UI.Generic.DarkMode.Controls.EnumTitleButton.Button2:

                    this.OnResetSignalTowerBuzzorEvent?.Invoke();
                    this.MessageBoxOpcall.Hide();

                    // Confirm 보고는 제일 처음에 수신된 메세지로 보고

                    this.WriteWord(WRITE_W.ASCII_10_0F80_OPCallIDComfirm, $"{this.m_ReceivedOpcallData.ID}");
                    this.WriteWord(WRITE_W.ASCII_60_0F8A_OPCallMessageConfirm, this.m_ReceivedOpcallData.Message);

                    this.SleepWithDoEvent(500);

                    this.WriteBit(WRITE_B.OPCALLCONFIRM_41, true);
                    this.SleepWithDoEvent(1000);
                    this.WriteBit(WRITE_B.OPERATORCALL_4, false);
                    this.WriteBit(WRITE_B.OPCALLCONFIRM_41, false);

                    break;
            }
        }

        private void MessageBoxTerminalDisplay_TitleButtonClickEvent(Lib.UI.Generic.DarkMode.Controls.EnumTitleButton button)
        {
            switch (button)
            {
                case Lib.UI.Generic.DarkMode.Controls.EnumTitleButton.Button2:

                    this.MessageBoxTerminalDisplay.Hide();

                    // Confirm 보고는 제일 처음에 수신된 메세지로 보고

                    this.WriteBit(WRITE_B.TERMINALDISPLAY_3, true);
                    this.SleepWithDoEvent(1000);
                    this.WriteBit(WRITE_B.TERMINALDISPLAY_3, false);

                    break;
                case Lib.UI.Generic.DarkMode.Controls.EnumTitleButton.Button1:
                default:
                    break;
            }
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
            this.SleepWithDoEvent(1000);
            this.WriteBit(WRITE_B.DATETIMESET_2, false);

            return true;
        }



        #endregion

        #region Public Method : CIM Equipment State


        public void SetEqState(CIMEnumeric.EnumAvailabilityState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002C_EQPAvailability, $"{(int)_state}");
        }


        // TODO LHJ -> HJP : Not release at AlarmClear. Must AlarmReleased at Auto Start state
        public void SetEqState(CIMEnumeric.EnumInterlockState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002D_EQPInterlock, $"{(int)_state}");
        }
        public void SetEqState(CIMEnumeric.EnumMoveState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002E_EQPMove, $"{(int)_state}");
        }
        public void SetEqState(CIMEnumeric.EnumRunState _state)
        {
            if (this.IsInitialized == false)
                return;

            this.WriteWord(WRITE_W.ASCII_1_002F_EQPRun, $"{(int)_state}");
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public void SendReplyTerminalDisplay()
        {
            if (this.IsInitialized == false)
                return;

            try
            {
                this.WriteWord(WRITE_W.ASCII_60_1086_TerminalDisplaySnd, this.m_ReceivedTerminalDisplayData.ToString());
                this.HandShakeSignal(WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, HANDSHAKE_TIMEOUT_MILLISECONDS); 

            }
            catch
            {
            }
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public void SendReplyOperatorCall(string _message)
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

            // TODO LHJ : Alarm area enum list
            switch (_alarmLevel)
            {
                case EnumAlarmLevel.HeavyAlarm:
                    this.WriteWord(WRITE_W.BIT_200_C55C_HeavyAlarm, $"{writeValue}");//

                    this.SleepWithDoEvent(500);

                    this.AlarmState = EnumAlarmState.HeavyAlarm;
                    this.SetEqState(EnumAvailabilityState.Down);
                    this.SetEqState(EnumMoveState.Pause);
                    break;
                case EnumAlarmLevel.LightAlarm:
                default:
                    this.WriteWord(WRITE_W.BIT_200_C624_LightAlarm, $"{writeValue}");
                    this.AlarmState = EnumAlarmState.LightAlarm;
                    break;
            }

        }

        // TODO LHJ -> HJP : Not release at AlarmClear. Must AlarmReleased at Auto Start state
        public void AlarmReleased(int _alarmID, string _description)
        {
            if (this.IsInitialized == false)
                return;

            // word 상의 모든 bit를 0으로 reset
            this.WriteWord(WRITE_W.BIT_200_C55C_HeavyAlarm, $"{0}");
            this.WriteWord(WRITE_W.BIT_200_C624_LightAlarm, $"{0}");

            this.AlarmState = EnumAlarmState.None;

            // TODO LHJ->HJP : Change State to Auto Start
            //this.SetEqState(EnumAvailabilityState.Up);
            //this.SetEqState(EnumMoveState.Runnning);
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

                // TODO LHJ : Need change PopupUI of ETC Sheet in Scenario Doc

                FormTpmLossCode form = new FormTpmLossCode();
                form.TopMost = true;
                form.ShowDialog();


                this.WriteWord(WRITE_W.DEC_2_120F_TMPLossCode, $"{(int)form.SelectedTpCode}");
                this.WriteWord(WRITE_W.ASCII_20_1211_TMPLossDescp, form.SelectedTpDescription);

                form.Close();
                form.Dispose();
                form = null;

                this.SleepWithDoEvent(200);

                this.SetEqState(EnumMoveState.Pause);
                this.WriteBit(WRITE_B.TPMLOSS_15, true);
                this.WaitBitSignal(READ_B.TPMLOSS_15, true, HANDSHAKE_TIMEOUT_MILLISECONDS);
                this.WriteBit(WRITE_B.TPMLOSS_15, false);

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
        public (EnumJobProcessType Result, CellDataInfo trackInCellData) TrackInLoadingCell(string _jigBarcode)
        {
            CellDataInfo trackInCellData = new CellDataInfo();

            try
            {
                if (this.IsInitialized == false)
                    throw new Exception("Error TrackInLoadingCell. Not Initialized");

                // TODO 사용되는 WORD DATA는 변경될 수 있음

                #region S6F203 Specipic Valid Request

                // Fix
                this.WriteWord(WRITE_W.ASCII_5_2980_SpecificValidationRequest_OptionCode1, "PREBIMJIG");

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
                    return (EnumJobProcessType.Timeout, trackInCellData);
                    //throw new Exception("Error TrackInLoadingCell. Not Received S3F103");

                this.WriteBit(WRITE_B.SPECIFICVALIDATIONDATASEND1_223, true);

                string specDataJigID =  this.ReadWord(READ_W.ASCII_20_11256_SpecificValidationCarrierID1);
                string specDataCellID = this.ReadWord(READ_W.ASCII_40_1126A_SpecificValidationCellID1);
                string specDataUiqueType = this.ReadWord(READ_W.ASCII_10_11292_SpecificValidationUniqueType1);
                string specDataProductID = this.ReadWord(READ_W.ASCII_20_1129C_SpecificValidationProductID1);
                string specDataStepID = this.ReadWord(READ_W.ASCII_20_112B0_SpecificValidationStepID1);
                string specDataReplyStatus = this.ReadWord(READ_W.ASCII_2_112C4_SpecificValidationReplyStatus1);
                string specDataReplyText = this.ReadWord(READ_W.ASCII_60_112C6_SpecificValidationReplyText1);

                trackInCellData.CellID = specDataCellID;
                trackInCellData.ProductID = specDataProductID;
                trackInCellData.StepID = specDataStepID;

                // Pass or Fail
                if (specDataReplyStatus.ToUpper() != EnumJobProcessType.Pass.ToString().ToUpper())
                    return (EnumJobProcessType.Fail, trackInCellData);
                    //throw new Exception("Error TrackInLoadingCell. Specification Reply Status is fail");

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
                    return (EnumJobProcessType.Timeout, trackInCellData);
                    //throw new Exception("Error TrackInLoadingCell. Not Received S2F43(RCMD=21)");

                this.WriteBit(WRITE_B.CELLSTARTPORT1_28, false);
                string jobPorcRCMD = this.ReadWord(READ_W.ASCII_2_D339_CellJobProcessRCMD1);
                string jobPorcJobID =  this.ReadWord(READ_W.ASCII_20_D33B_CellJobProcessJobID1);
                string jobPorcCellID =  this.ReadWord(READ_W.ASCII_40_D34F_CellJobProcessCellID1);
                string jobPorcProductID =  this.ReadWord(READ_W.ASCII_20_D377_CellJobProcessProductID1);
                string jobPorcStepID =  this.ReadWord(READ_W.ASCII_20_D38B_CellJobProcessStepID1);
                string jobPorcActionType =  this.ReadWord(READ_W.ASCII_10_D39F_CellJobProcessActionType1);


                if (int.TryParse(jobPorcRCMD, out int jobProcRCMDValue) == false)
                    return (EnumJobProcessType.Fail, trackInCellData);
                    //throw new Exception($"Error TrackInLoadingCell. S2F43 RCMD is not integer");

                //S2F43 RCMD 21~24
                //  21 : Cell Job Process Start (Pass)
                //  22 : Cell Job Process Cancel(Fail)
                //  23 : Cell Job Process Pause (미사용)
                //  24 : Cell Job Process Resume (미사용)
                switch (jobProcRCMDValue)
                {
                    case 21:
                        this.HandShakeSignal(WRITE_B.CELLSTARTPORT1_28, true, READ_B.CELLSTARTPORT1_28, true, HANDSHAKE_TIMEOUT_MILLISECONDS);
                        return (EnumJobProcessType.Pass, trackInCellData);
                    default:
                        return (EnumJobProcessType.Fail, trackInCellData);
                        //throw new Exception($"Error TrackInLoadingCell. S2F43 RCMD={jobProcRCMDValue}");
                }
                #endregion

                // TODO LHJ->HJP : FAIL = Occure Light Alarm
            }
            catch (Exception ex)
            {
                return (EnumJobProcessType.Fail, trackInCellData);
            }
            finally
            {
                this.WriteBit(WRITE_B.SPECIFICVALIDATIONREQUEST1_218, false);
                this.WriteBit(WRITE_B.SPECIFICVALIDATIONDATASEND1_223, false);
                this.WriteBit(WRITE_B.CELLSTARTPORT1_28, false);

            }

        }


        // TODO CHECK LHJ -> HJP : CELL 및 지그 배출 전 시점에 본 함수 호출
        public EnumJobProcessType TrackOutUnloadingCell(string _jigBarcode, EnumJobProcessType trackIn, CellDataInfo trackInCellData)
        {
            if (this.IsInitialized == false)
                return EnumJobProcessType.Fail;

            try
            {
                // TODO 사용되는 WORD DATA는 변경될 수 있음


                if (trackIn == EnumJobProcessType.Pass)
                {
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
                        throw new Exception($"Error TrackOutUnloadingCell. Not Received S3F103");

                    this.WriteBit(WRITE_B.SPECIFICVALIDATIONDATASEND1_223, true);

                    #endregion
                }

                /// Use TrackIn Cell Data
                //string specDataJigID = this.ReadWord(READ_W.ASCII_20_11256_SpecificValidationCarrierID1);
                //string specDataCellID = this.ReadWord(READ_W.ASCII_40_1126A_SpecificValidationCellID1);
                //string specDataUiqueType = this.ReadWord(READ_W.ASCII_10_11292_SpecificValidationUniqueType1);
                //string specDataProductID = this.ReadWord(READ_W.ASCII_20_1129C_SpecificValidationProductID1);
                //string specDataStepID = this.ReadWord(READ_W.ASCII_20_112B0_SpecificValidationStepID1);
                string specDataReplyStatus = this.ReadWord(READ_W.ASCII_2_112C4_SpecificValidationReplyStatus1);
                string specDataReplyText = this.ReadWord(READ_W.ASCII_60_112C6_SpecificValidationReplyText1);

                // Pass or Fail
                if (specDataReplyStatus.ToUpper() != EnumJobProcessType.Pass.ToString().ToUpper())
                    throw new Exception($"Error TrackOutUnloadingCell. Specification Reply Status is not PASS");


                #region S6F11 CEID=406 Process Complete


                this.WriteWord(WRITE_W.ASCII_40_0760_TrackOutCellID1, trackInCellData.CellID);
                this.WriteWord(WRITE_W.ASCII_20_0788_TrackOutProductID1, trackInCellData.ProductID);
                this.WriteWord(WRITE_W.ASCII_20_079C_TrackOutStepID1, trackInCellData.StepID);
                this.WriteWord(WRITE_W.ASCII_20_07B0_TrackOutProcessJobID1, "");
                this.WriteWord(WRITE_W.DEC_2_07C4_TrackOutPlanQuantity1, "");
                this.WriteWord(WRITE_W.DEC_2_07C6_TrackOutProcessQuantity1, "");
                this.WriteWord(WRITE_W.ASCII_1_07C8_TrackOutReaderID1, "");//OUT MCR 없으면 / MCR 미사용 : Null처리
                this.WriteWord(WRITE_W.ASCII_1_07C9_TrackOutRRC1, "2");//0 : OK , 1 : Error, 2 : Reader 기능 미사용
                this.WriteWord(WRITE_W.ASCII_10_07CA_TrackOutOperatorID1_1, "");
                this.WriteWord(WRITE_W.ASCII_10_07D4_TrackOutOperatorID1_2, "");
                this.WriteWord(WRITE_W.ASCII_10_07DE_TrackOutOperatorID1_3, "");

                switch (trackIn)
                {
                    case EnumJobProcessType.Pass:
                        this.WriteWord(WRITE_W.ASCII_1_07E8_TrackOutJudge1, "G");
                        this.WriteWord(WRITE_W.ASCII_10_07F3_TrackOutReasonCode1, "");
                        this.WriteWord(WRITE_W.ASCII_20_07FD_TrackOutDescription1, "");
                        break;
                    case EnumJobProcessType.Timeout:
                        this.WriteWord(WRITE_W.ASCII_1_07E8_TrackOutJudge1, "O");
                        this.WriteWord(WRITE_W.ASCII_10_07F3_TrackOutReasonCode1, "");
                        this.WriteWord(WRITE_W.ASCII_20_07FD_TrackOutDescription1, "CELL_VALIDATION_TIMEOUT");
                        break;
                    case EnumJobProcessType.Fail:
                    default:
                        this.WriteWord(WRITE_W.ASCII_1_07E8_TrackOutJudge1, "O");
                        this.WriteWord(WRITE_W.ASCII_10_07F3_TrackOutReasonCode1, "");
                        this.WriteWord(WRITE_W.ASCII_20_07FD_TrackOutDescription1, "CELL_VALIDATION_FAIL");
                        break;
                }
                this.WriteBit(WRITE_B.CELLCOMPPORT1_34, true);

                this.SleepWithDoEvent(2000);

                #endregion

                return EnumJobProcessType.Pass;
            }
            catch (Exception ex)
            {
                return EnumJobProcessType.Fail;
            }
            finally
            {
                this.WriteBit(WRITE_B.SPECIFICVALIDATIONREQUEST1_218, false);
                this.WriteBit(WRITE_B.SPECIFICVALIDATIONDATASEND1_223, false);
                this.WriteBit(WRITE_B.CELLCOMPPORT1_34, false);


            }
        }


        #endregion
        #region Public Method : CIM RMS

        public bool PpidChange(int index = 0)
        {
            Conf.CURR_MODEL_IDX = index;
            ModelInfo info = Common.MODEL_INFO(index);

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = info.modelName();

            m_Writer.setBit(WRITE_B.PPIDCHANGE_21, true);

            if (this.WaitBitSignal(READ_B.PPIDCHANGE_21, true, 10000) == false)
                return false;

            /* //  검수때 소스
            CElaspedTimer m_timeout = new CElaspedTimer(10000);

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
            // */

            m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);

            return true;
        }

        // PPID 생성
        public bool PpidCreate(int index = 0, char ch = 'O', bool bRemote = false)
        {
            if (!bRemote)
            {
                if (index < 91)
                    Common.MODEL[index].saveModelName("MODEL_" + (index).ToString());
                else
                    Common.MODEL[index].saveModelName("TT_MODEL_" + (index).ToString());
            }

            ModelInfo INFO = Common.MODEL_INFO(index);

            m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 1;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            //ParameterChange2RecipeNumber    PPID Number룰 데로 추가 필요.
            //1-90 -> 상위에서 받는거. TT_ X
            //91-99-> 내부에서 만드는거 TT_로 시작
            //string sName = (Conf.CURR_MODEL_IDX + 1).ToString() + "O";

            string sName = (index).ToString("00") + ch;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_3_23D9_ParameterChange2RecipeNumber).text = sName;

            WriteTeachPos(INFO);
            UpdateModelList();

            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, true);

            if (this.WaitBitSignal(READ_B.PARAMETERCHANGE2_23, true, 10000) == false)
                return false;

            /* //검수 때 소스
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
            // */

            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, false);

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
        public bool PpidDelete(int Index = 0, char ch = 'D')
        {
            ModelInfo INFO = Common.MODEL_INFO(Index);

            m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 2;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            string sName = (Index).ToString("00") + ch;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_3_23D9_ParameterChange2RecipeNumber).text = sName; 

            Common.MODEL[Index].saveModelName("");
            UpdateModelList();

            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, true);

            if (this.WaitBitSignal(READ_B.PARAMETERCHANGE2_23, true, 10000) == false)
                return false;

            /* 검수 때 소스
            //CElaspedTimer m_timeout = new CElaspedTimer(10000);

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
            //} */
            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, false);

            return true;
        }

        POS[] ReadTeachPos()
        {
            int baseEnum = (int)READ_W.ASCII_20_DF96_PICK_PP_WAIT_NAME;
            POS[] p = new POS[(int)TEACH_POS.MAX];

            for (int i = 0; i < (int)TEACH_POS.MAX; i++)
            {
                p[i] = new POS();

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
            ModelInfo INFO = Common.MODEL_INFO(index);

            m_Writer.wordData((WRITE_W)WRITE_W.DEC_2_9224_PPIDMode).value = 3;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            WriteTeachPos(INFO);
            string sName = (index).ToString("00") + "C";

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_3_23D9_ParameterChange2RecipeNumber).text = sName; 

            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, true);

            if (this.WaitBitSignal(READ_B.PARAMETERCHANGE2_23, true, HANDSHAKE_TIMEOUT_MILLISECONDS) == false)
                return false;

            Util.waitTick(500);

            m_Writer.setBit(WRITE_B.PARAMETERCHANGE2_23, false);

            return true;
        }

        #endregion

        #region Public Method : CIM ECM
        public void EquipConstantUpdate()
        {
            if (this.IsInitialized == false)
                return;

            ModelInfo Mc = Common.MC;
            WriteMcPos(Mc);

            m_Writer.setBit(WRITE_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24, true);

            if (this.WaitBitSignal(READ_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24, true, 10000) == false)
                return;

            m_Writer.setBit(WRITE_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24, false);

            /* //검수때 소스
            Thread.Sleep(200);

            CElaspedTimer m_timeout = new CElaspedTimer(10000);

            bool ret = false;
            m_timeout.start();

            while (true)
            {
                if (m_Reader.readBit(READ_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24) == true)
                {
                    m_Writer.setBit(WRITE_B.EQUIPMENTCONSTANTPARAMETERCHANGEEVENT_24, false);
                    ret = true;
                    break;
                }

                if (m_timeout.isElasped() == true)
                    break;

                Util.waitTick(100);
            }
            // */
        }

        public void EquipConstantQuery()
        {
            if (this.IsInitialized == false)
                return;

            
            if (m_Reader.readBit(CIMRead.READ_B.EQUIPCONSTANTNAMELIST_53) == true)
            {
                ModelInfo Mc = Common.MC;

                WriteMcPos(Mc);

                m_Writer.setBit(WRITE_B.EQUIPCONSTANTNAMELIST_53, true);

                Util.waitTick(1000);

                m_Writer.setBit(WRITE_B.EQUIPCONSTANTNAMELIST_53, false);   
            }
        }

        #endregion

    }
}
