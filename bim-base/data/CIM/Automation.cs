using lib.plc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bim_base.CSTATION;
using static bim_base.data.CIM.CIMEnumeric;
using static CIMRead;
using static CIMWrite;

namespace bim_base.data.CIM
{


    internal class Automation
    {
        #region Delegate

        public delegate void OnReceivedTerminalDisplayEventHandler(int _MessageNum, string _MessageText);
        public delegate void OnReceivedOperatorCallEventHandler(int _OpCallNum, string _OpCallText);
        public delegate bool OnReceivedInterlockEventHandler(int _ID, string _Message, EnumInterlockRCMD _RCMD);
        public delegate bool OnRequestAutoNormalModeEventHandler();

        #endregion

        #region Constructor

        public Automation()
        {
        }

        #endregion

        #region Constants

        private const int HANDSHAKE_TIMEOUT_SECONDS = 5;

        #endregion

        #region Private Member

        private MelsecCCLink m_ccLink = null;
        private CIMRead m_Reader = new CIMRead();
        private CIMWrite m_Writer = new CIMWrite();

        //private RMS m_RMS = new RMS();


        #endregion

        #region public Properties

        public static Automation Instance = new Automation();

        public bool IsInitialized { get; private set; } = false;
        public bool IsRun { get; private set; } = false;


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

        public List<EnumRequestProcState> RequestProcStateList { get; private set; } = new List<EnumRequestProcState>();


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
        public event OnRequestAutoNormalModeEventHandler RequestAutoNormalModeEvent;
        #endregion

        #region Private Method

        private void SyncCommCCIE()
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
                this.InitializeSignals();
            }
            else
            {
                throw new Exception("failed to run cclink write failed");
            }
        }

        private void SleepWithDoEvent(int _seconds)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan ts = DateTime.Now  - startTime;

            while (ts.TotalSeconds < _seconds)
            {
                Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();
                ts = DateTime.Now - startTime;  
            }
        }

        private void WaitBitSignal(CIMRead.READ_B _addr, bool _waitValue, int _timeoutSeconds = 0)
        {
            bool val = this.ReadBit(_addr);

            DateTime startTime = DateTime.Now;
            TimeSpan ts = DateTime.Now - startTime;

            while (val != _waitValue)
            {
                if(_timeoutSeconds > 0 && ts.TotalSeconds >= _timeoutSeconds)
                {
                    throw new Exception("WaitBitSignal Timeout Occurred. Addr: " + _addr.ToString() + ", WaitValue: " + _waitValue.ToString());
                }

                val = this.ReadBit(_addr);

                if (val == _waitValue) { return; }

                this.SleepWithDoEvent(1);
                ts = DateTime.Now - startTime;
            }

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


        private void InitializeSignals()
        {
            foreach (string enumName in Enum.GetNames(typeof(CIMWrite.WRITE_B)))
            {
                CIMWrite.WRITE_B addrBit = (CIMWrite.WRITE_B)Enum.Parse(typeof(CIMWrite.WRITE_B), enumName);
                this.WriteBit(addrBit, false);
            }
        }

        private bool AddRequestProcState(EnumRequestProcState state)
        {
            if (this.RequestProcStateList.Contains(state))
                return false;


            this.RequestProcStateList.Add(state);
            return true;
        }

        private void RemoveRequestProcState(EnumRequestProcState state)
        {
            if (this.RequestProcStateList.Contains(state) == false)
                return;


            this.RequestProcStateList.Remove(state);
        }

        #endregion

        #region Private Method : CIM Request

        private void RequestTerminalDisplay()
        {
            try
            {
                if (this.AddRequestProcState(EnumRequestProcState.TerminalDisplay) == false)
                    return;

                if (this.ReadBit(CIMRead.READ_B.TERMINALDISPLAY_3) == false)
                    return;

                string msgNum = this.ReadWord(CIMRead.READ_W.ASCII_1_D04D_TerminalNumber);
                string msgText = this.ReadWord(CIMRead.READ_W.ASCII_60_D011_TerminalDisplayText);

                if (int.TryParse(msgNum, out int messageNum) == false)
                    return;

                ReceivedTerminalDisplayEvent?.Invoke(messageNum, msgText);

                this.WriteBit(WRITE_B.TERMINALDISPLAY_3, true);
                Task.Run(() => this.SleepWithDoEvent(1)).Wait();
                this.WriteBit(WRITE_B.TERMINALDISPLAY_3, false);


            }
            catch
            {
            }
            finally
            {
                this.RemoveRequestProcState(EnumRequestProcState.TerminalDisplay);
            }
        }

        private void RequestOperatorCall()
        {
            try
            {
                if (this.AddRequestProcState(EnumRequestProcState.OperatorCall) == false)
                    return;

                if (this.ReadBit(CIMRead.READ_B.OPERATORCALL_4) == false)
                    return;

                string strOpCallNum = this.ReadWord(CIMRead.READ_W.ASCII_10_D058_OperatorCallID);
                string strOpCallText = this.ReadWord(CIMRead.READ_W.ASCII_60_D062_OperatorCallText);

                if (int.TryParse(strOpCallNum, out int opCallNum) == false)
                    return;

                // TODO CHECK LHJ : OP CALL 시그널타워 Yellow Blink, Buzzor 발생 필요. 설비 정지 필요, 터치판넬에서 메세지 팝업
                // TODO CHECK LHJ : OP Call 팝업 메세지 확인 시 SendOperatorCall 호출, 팝업/시그널타워/부저 초기화
                ReceivedOperatorCallEvent?.Invoke(opCallNum, strOpCallText);

                this.WriteBit(WRITE_B.OPCALLCONFIRM_41, true);
                Task.Run(() => this.SleepWithDoEvent(1)).Wait();
                this.WriteBit(WRITE_B.OPCALLCONFIRM_41, false);

            }
            catch
            {
            }
            finally
            {
                this.RemoveRequestProcState(EnumRequestProcState.OperatorCall);
            }
        }

        private void RequestInterlcokState()
        {
            try
            {
                if (this.AddRequestProcState(EnumRequestProcState.RequestInterlcokState) == false)
                    return;

                if (this.ReadBit(CIMRead.READ_B.INTERLOCK_5) == false)
                    return;

                string strID = this.ReadWord(CIMRead.READ_W.ASCII_10_D09E_InterlockID);
                string strMessage = this.ReadWord(CIMRead.READ_W.ASCII_60_D0A8_InterlockMessage);
                string strRCMD = this.ReadWord(CIMRead.READ_W.ASCII_1_D0E4_InterlockRCMD);

                if (int.TryParse(strID, out int interlockID) == false)
                    return;

                if (Enum.TryParse<EnumInterlockRCMD>(strRCMD, out EnumInterlockRCMD rcmd) == false)
                    return;

                // TODO CHECK LHJ : 인터락 요청 발생 이력 관리 필요. 인터락 메세지 팝업, 시그널타워 red/yellow blink, 부저 발생
                if (this.ReceivedInterlockEvent == null) 
                    return;

                bool isValid = ReceivedInterlockEvent.Invoke(interlockID, strMessage, rcmd);

                this.WriteBit(WRITE_B.INTERLOCK_5, true);
                Task.Run(() => this.SleepWithDoEvent(1)).Wait();
                this.WriteBit(WRITE_B.INTERLOCK_5, false);

                if (isValid == false) return;

                this.SetEqState(EnumInterlockState.On);
                this.SetEqState(EnumMoveState.Pause);
            }
            catch
            {
            }
            finally
            {
                this.RemoveRequestProcState(EnumRequestProcState.RequestInterlcokState);
            }
        }


        #endregion

        #region Public Method

        public void RunScan()
        {
            if (this.IsRun) return;

            this.IsRun = true;

            this.SyncCommCCIE();

            Task.Run(() => this.RequestTerminalDisplay());
            Task.Run(() => this.RequestOperatorCall());
            Task.Run(() => this.RequestInterlcokState());


            this.IsRun = false;
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


        public bool HandShakeSignal(CIMWrite.WRITE_B _addrWrite, bool _writeValue, CIMRead.READ_B _addrRead, bool _readValue, int _timeoutSeconds = 0, bool _isOnError = false)
        {
            bool function()
            {
                // TODO CHECK LHJ : H/S 진입시 중복 실행되지 않는지 확인 필요
                try
                {
                    this.WriteBit(_addrWrite, _writeValue);
                    this.WaitBitSignal(_addrRead, _readValue, _timeoutSeconds);

                    return true;
                }
                catch (Exception ex)
                {
                    if (_isOnError)
                    {
                        throw new Exception($"HandShakeSignal Error Occurred. {ex.ToString()}");
                    }

                    return false;
                }
                finally
                {
                    this.WriteBit(_addrWrite, !_writeValue);
                }
            }
            ;

            Task<bool> asyncHS = Task.Run(() => function());
            asyncHS.Wait(_timeoutSeconds * 1000);


            return asyncHS.Result;
        }

        #endregion

        #region Public Method : CIM Initialize


        public bool InitializeCIM()
        {
            if (this.IsInitialized) return false;

            this.InitializeSignals();

            // 초기 ALIVE 신호 OFF로 Reset
            Task<bool> asyncHS = Task.Run(() => this.HandShakeSignal(WRITE_B.ALIVEBIT_1, false, CIMRead.READ_B.ALIVEBIT_1, false, HANDSHAKE_TIMEOUT_SECONDS));
            asyncHS.Wait(HANDSHAKE_TIMEOUT_SECONDS);
            if (asyncHS.Result == false) return false;

            // Date Time 동기화 요청 신호 대기
            asyncHS = Task.Run(() =>
            {
                try
                {
                    this.WaitBitSignal(CIMRead.READ_B.DATETIMESET_2, true, HANDSHAKE_TIMEOUT_SECONDS);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
            asyncHS.Wait(HANDSHAKE_TIMEOUT_SECONDS);
            if (asyncHS.Result)
            {
                // Date Time 동기화 처리
                if (this.SetDateTime() == false) return false;
            }

            this.IsInitialized = true;
            Task.Run(() =>
             {
                 bool alive = false;
                 while (this.IsInitialized)
                 {
                     alive = this.ReadBit(CIMRead.READ_B.ALIVEBIT_1);

                     this.HandShakeSignal(WRITE_B.ALIVEBIT_1, !alive, CIMRead.READ_B.ALIVEBIT_1, !alive, HANDSHAKE_TIMEOUT_SECONDS);
                 }
             });

            return true;
        }

        public bool SetDateTime()
        {
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
            Task.Run(() => this.SleepWithDoEvent(1)).Wait();
            this.WriteBit(WRITE_B.DATETIMESET_2, false);

            return true;
        }



#endregion

        #region Public Method : CIM Equipment State


        public void SetEqState(CIMEnumeric.EnumAvailabilityState _state)
        {
            this.WriteWord(WRITE_W.ASCII_1_002C_EQPAvailability, $"{_state}");
        }

        public void SetEqState(CIMEnumeric.EnumInterlockState _state)
        {
            this.WriteWord(WRITE_W.ASCII_1_002D_EQPInterlock, $"{_state}");
        }
        public void SetEqState(CIMEnumeric.EnumMoveState _state)
        {
            this.WriteWord(WRITE_W.ASCII_1_002E_EQPMove, $"{_state}");
        }
        public void SetEqState(CIMEnumeric.EnumRunState _state)
        {
            this.WriteWord(WRITE_W.ASCII_1_002F_EQPRun, $"{_state}");
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public void SendTerminalDisplay(string _message)
        {
            try
            {
                this.WriteWord(WRITE_W.ASCII_60_1086_TerminalDisplaySnd, _message);
                this.HandShakeSignal(WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, HANDSHAKE_TIMEOUT_SECONDS);

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
            try
            {
                // TODO CHECK LHJ : Operator Call은 ID가 존재하는데, ID는 어떻게 관리할지? 일단은 메시지만 전달하는 형태로 구현

                this.WriteWord(WRITE_W.ASCII_60_259C_UnitOPCallConfirmOPCallMessage, _message);
                this.HandShakeSignal(WRITE_B.EQUIPUNITOPCALLSEND_247, true, CIMRead.READ_B.OPCALLCONFIRM_41, true, HANDSHAKE_TIMEOUT_SECONDS);

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
            try
            {
                // TODO CHECK LHJ : 인터락 해제 시 사용할 Word 영역 및 Data 확인 및 수정 필요
                this.WriteWord(WRITE_W.ASCII_60_104A_InterlockMessageConfirm, _message);
                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, true);

                this.WaitBitSignal(READ_B.INTERLOCKCONFIRM_42, true, HANDSHAKE_TIMEOUT_SECONDS);
                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, false);

                if(this.RequestAutoNormalModeEvent == null)
                    throw new Exception("RequestAutoNormalModeEvent is not set.");

                if(this.RequestAutoNormalModeEvent.Invoke() == false)
                    throw new Exception("Failed to changing Auto-Normal Mode");


                this.SetEqState(EnumInterlockState.Off);
                this.SetEqState(EnumMoveState.Runnning);

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

        public void AlarmOccured(ALARM _alarmID)
        {
            //const int BIT_COUNT = 16;
            //int idx = (int)_alarmID;

            //try
            //{
            //    if (idx < 0 || idx > Enum.GetNames(typeof(ALARM)).Length) 
            //        throw new ArgumentOutOfRangeException($"Failed to report alarm. Becauese Invalid AlarmID {_alarmID}.{nameof(_alarmID)}");

            //    this.WriteWord(WRITE_W.alarm)
            //}
            //catch
            //{

            //}

           
            //// 연속된 WRITE_B 열거값의 시작값(실제 이름으로 교체)
            //int baseWriteB = (int)WRITE_B.ALARMBIT_0;

            //// 모든 비트를 false로 설정하고, 해당 인덱스만 true로 설정
            //for (int i = 0; i < BIT_COUNT; i++)
            //{
            //    var bitAddr = (WRITE_B)(baseWriteB + i);
            //    this.WriteBit(bitAddr, i == idx);
            //}

            //this.WriteWord(WRITE_W.ASCII_10_1040_AlarmID, idx.ToString());
            //this.WriteWord(WRITE_W.ASCII_60_104A_AlarmMessage, _alarmID.ToString());

            //this.WriteBit(WRITE_B.ALARMSEND_43, true);
            //Task.Run(() => this.SleepWithDoEvent(1)).Wait();
            //this.WriteBit(WRITE_B.ALARMSEND_43, false);
        }

        #endregion

        #region Public Method : CIM RMS

        //상시 대기
        public void PpidListRequest()
        {
        
            int commandHoldTimeMs = 1000;

            //ModelInfo
            if (m_Reader.readBit(CIMRead.READ_B.CURRENTEQUIPPPIDLISTREQUEST_56) == true)
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

                m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, true);

                _ = Task.Run(async () =>
                {
                    await Task.Delay(commandHoldTimeMs);
                    m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, false);
                });
            }
        }

        // PPID Change (JOB Change)
        public void PpidChange(string ppid)
        {
            //,
            //ASCII_20_9226_PPID,
            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPID_MODE).text = ppid;

            //Parameter WORD(LW영역)에 Update
            //Current PPID WORD 영역에 Write
            //PPID Change BIT

            //PPID Change Reply BIT

            //var addr = (WRITE_W)((int)WRITE_W.ASCII_20_8A54_PPID_1 + i);
            //m_Writer.wordData(addr).text = items[i];

            //m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, true);

        }

        // PPID 생성
        public void PpidCreate(string ppid)
        {
        }

        // PPID 삭제
        public void PpidDelete(string ppid)
        {
        }

        #endregion
    }
}
