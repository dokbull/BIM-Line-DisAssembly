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

        #endregion

        #region Constructor

        public Automation()
        {
        }

        #endregion

        #region Private Member

        private MelsecCCLink m_ccLink = null;
        private CIMRead m_Reader = new CIMRead();
        private CIMWrite m_Writer = new CIMWrite();

        //private RMS m_RMS = new RMS();

        private List<EnumRequestProcState> m_RequestProcStateList = new List<EnumRequestProcState>();

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

        #endregion

        #region Event

        public event OnReceivedTerminalDisplayEventHandler ReceivedTerminalDisplayEvent;
        public event OnReceivedOperatorCallEventHandler ReceivedOperatorCallEvent;
        /// <summary>
        /// 설비 가동 정지
        /// </summary>
        public event OnReceivedInterlockEventHandler ReceivedInterlockEvent;

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

        private void AddRequestProcState(EnumRequestProcState state)
        {
            if (this.m_RequestProcStateList.Contains(state) == false)
            {
                this.m_RequestProcStateList.Add(state);
            }
        }

        #endregion

        #region Private Method : CIM 대응

        private void TerminalDisplay()
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
                Task.Run(() => this.SleepWithDoEvent(1)).Wait();
                this.WriteBit(WRITE_B.TERMINALDISPLAY_3, false);

            }
            catch
            {
            }
        }

        private void OperatorCall()
        {
            try
            {
                if (this.ReadBit(CIMRead.READ_B.OPERATORCALL_4) == false)
                    return;

                string strOpCallNum = this.ReadWord(CIMRead.READ_W.ASCII_10_D058_OperatorCallID);
                string strOpCallText = this.ReadWord(CIMRead.READ_W.ASCII_60_D062_OperatorCallText);

                if (int.TryParse(strOpCallNum, out int opCallNum) == false)
                    return;

                ReceivedOperatorCallEvent?.Invoke(opCallNum, strOpCallText);

                this.WriteBit(WRITE_B.OPCALLCONFIRM_41, true);
                Task.Run(() => this.SleepWithDoEvent(1)).Wait();
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

                if (this.ReceivedInterlockEvent == null) 
                    return;

                bool isValid = ReceivedInterlockEvent.Invoke(interlockID, strMessage, rcmd);

                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, true);
                Task.Run(() => this.SleepWithDoEvent(1)).Wait();
                this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, false);

                if (isValid == false) return;

                this.SetEqState(EnumInterlockState.On);
                this.SetEqState(EnumMoveState.Pause);
            }
            catch
            {
            }
        }

        private void ReleaseInterlcokState()
        {
            try
            {
                //this.WriteWord(WRITE_W.(WRITE_B.INTERLOCKCONFIRM_42, true);
                //this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, true);

                //string strID = this.ReadWord(CIMRead.READ_W.ASCII_10_D09E_InterlockID);
                //string strMessage = this.ReadWord(CIMRead.READ_W.ASCII_60_D0A8_InterlockMessage);
                //string strRCMD = this.ReadWord(CIMRead.READ_W.ASCII_1_D0E4_InterlockRCMD);

                //if (int.TryParse(strID, out int interlockID) == false)
                //    return;

                //if (Enum.TryParse<EnumInterlockRCMD>(strRCMD, out EnumInterlockRCMD rcmd) == false)
                //    return;

                //bool isValid = ReceivedInterlockEvent?.Invoke(interlockID, strMessage, rcmd);

                //this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, true);
                //Task.Run(() => this.SleepWithDoEvent(1)).Wait();
                //this.WriteBit(WRITE_B.INTERLOCKCONFIRM_42, false);

                //if (isValid == false) return;

                //this.SetEqState(EnumInterlockState.On);
                //this.SetEqState(EnumMoveState.Pause);
            }
            catch
            {
            }
        }

        #endregion

        #region Public Method

        public void Run()
        {
            if (this.IsRun) return;

            this.IsRun = true;

            this.SyncCommCCIE();

            Task.Run(() => this.TerminalDisplay());
            Task.Run(() => this.OperatorCall());
            Task.Run(() => this.RequestInterlcokState());
            Task.Run(() => this.ReleaseInterlcokState());


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

            int timeoutSeconds = 5;
            this.InitializeSignals();

            // 초기 ALIVE 신호 OFF로 Reset
            Task<bool> asyncHS = Task.Run(() => this.HandShakeSignal(WRITE_B.ALIVEBIT_1, false, CIMRead.READ_B.ALIVEBIT_1, false, timeoutSeconds));
            asyncHS.Wait(timeoutSeconds);
            if (asyncHS.Result == false) return false;

            // Date Time 동기화 요청 신호 대기
            asyncHS = Task.Run(() =>
            {
                try
                {
                    this.WaitBitSignal(CIMRead.READ_B.DATETIMESET_2, true, timeoutSeconds);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
            asyncHS.Wait(timeoutSeconds);
            if (asyncHS.Result == false) return false;

            // Date Time 동기화 처리
            if (this.SetDateTime() == false) return false;

            this.IsInitialized = true;
            Task.Run(() =>
             {
                 bool alive = false;
                 while (this.IsInitialized)
                 {
                     alive = this.ReadBit(CIMRead.READ_B.ALIVEBIT_1);

                     this.HandShakeSignal(WRITE_B.ALIVEBIT_1, !alive, CIMRead.READ_B.ALIVEBIT_1, !alive, timeoutSeconds);
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


        public void SendTerminalDisplay(string _message)
        {
            try
            {
                this.WriteWord(WRITE_W.ASCII_60_1086_TerminalDisplaySnd, _message);
                this.HandShakeSignal(WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, 5000);

            }
            catch
            {
            }
        }

        public void SendOperatorCall(string _message)
        {
            try
            {
                this.WriteWord(WRITE_W.ASCII_60_1086_TerminalDisplaySnd, _message);
                this.HandShakeSignal(WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, 5000);

            }
            catch
            {
            }
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
