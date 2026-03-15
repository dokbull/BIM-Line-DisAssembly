using lib.plc;
using Lib.UI.Generic.DarkMode.Forms;
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
using System.Xml;
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
        private const int BIT_COUNT = 16;

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

        public EnumAlarmState AlarmState { get; private set; } = EnumAlarmState.None;

        public EnumEqControlMode EqControlMode { get; set; } = EnumEqControlMode.Manual;



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

        private async void Alive()
        {
            try
            {
                if (this.AddRequestProcState(EnumRequestProcState.Alive) == false)
                    return;

                bool alive = this.ReadBit(CIMRead.READ_B.ALIVEBIT_1);

                await this.HandShakeSignal(WRITE_B.ALIVEBIT_1, !alive, CIMRead.READ_B.ALIVEBIT_1, !alive, HANDSHAKE_TIMEOUT_SECONDS).ConfigureAwait(true); ;

            }
            catch
            {
            }
            finally
            {
                this.RemoveRequestProcState(EnumRequestProcState.Alive);
            }
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

        public async void RunScan()
        {
            if (this.IsRun) return;

            this.IsRun = true;

            this.SyncCommCCIE();

            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => this.Alive()));
            tasks.Add(Task.Run(() => this.RequestTerminalDisplay()));
            tasks.Add(Task.Run(() => this.RequestOperatorCall()));
            tasks.Add(Task.Run(() => this.RequestInterlcokState()));

            await Task.WhenAll(tasks).ConfigureAwait(true); ;

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

        public async Task<bool> HandShakeSignal(CIMWrite.WRITE_B _addrWrite, bool _writeValue, CIMRead.READ_B _addrRead, bool _readValue, int _timeoutSeconds = 0, bool _isOnError = false)
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
                    return false;
                }
                finally
                {
                    this.WriteBit(_addrWrite, !_writeValue);
                }
            }
            ;

            bool asyncResult = await Task.Run(() => function()).ConfigureAwait(true);

            if (_isOnError && asyncResult == false)
            {
                throw new Exception($"HandShakeSignal Error Occurred");
            }

            return asyncResult;
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
        public async void SendTerminalDisplay(string _message)
        {
            try
            {
                this.WriteWord(WRITE_W.ASCII_60_1086_TerminalDisplaySnd, _message);
                await this.HandShakeSignal(WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, HANDSHAKE_TIMEOUT_SECONDS).ConfigureAwait(true); 

            }
            catch
            {
            }
        }

        /// <summary>
        /// 터치화면에서 팝업 메세지 확인 및 Clear 시 호출
        /// </summary>
        public async void SendOperatorCall(string _message)
        {
            try
            {
                // TODO CHECK LHJ : Operator Call은 ID가 존재하는데, ID는 어떻게 관리할지? 일단은 메시지만 전달하는 형태로 구현

                this.WriteWord(WRITE_W.ASCII_60_259C_UnitOPCallConfirmOPCallMessage, _message);
                await this.HandShakeSignal(WRITE_B.EQUIPUNITOPCALLSEND_247, true, CIMRead.READ_B.OPCALLCONFIRM_41, true, HANDSHAKE_TIMEOUT_SECONDS).ConfigureAwait(true);

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

        public void AlarmOccured(ALARM _alarmID, EnumAlarmLevel _alarmLevel)
        {

            if ((int)_alarmID < 0 || (int)_alarmID > Enum.GetNames(typeof(ALARM)).Length)
                throw new ArgumentOutOfRangeException($"Failed to report alarm. Becauese Invalid AlarmID {_alarmID}.{nameof(_alarmID)}");

            int wordIDX = (int)_alarmID / BIT_COUNT;
            int bitIDX = (int)_alarmID % BIT_COUNT;
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

        public void AlarmReleased(ALARM _alarmID)
        {
            
            // TODO CHECK LHJ : 주소 확인, 어떤 Data를 작성해야 할지 확인
            this.WriteWord(WRITE_W.BIT_400_CAD4_Alarm, $"{0}");

            this.AlarmState = EnumAlarmState.None;

            this.SetEqState(EnumAvailabilityState.Up);
            this.SetEqState(EnumMoveState.Runnning);
        }

        public void AlarmListRequest()
        {
            // TODO CHECK LHJ : Alarm 조회 기능에 대한 시나리오 구현 필요한지 확인
        }

        /// <summary>
        /// Safety Door Open 등과 같이 설비가 비정상적으로 정지해야 하는 상황에서 터치화면에 팝업 메세지를 띄우고, 설비를 정지시키는 기능
        /// </summary>
        public void EqStopByOperator(EnmumEqStopByOperatorType _stopType)
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
            msgPopup.MaximumSize = new System.Drawing.Size(1024, 628);
            msgPopup.StartPosition = FormStartPosition.CenterScreen;
            msgPopup.TopMost = true;
            DialogResult result = msgPopup.ShowDialog();

            this.WriteWord(WRITE_W.DEC_2_120F_TMPLossCode, msgPopup.SelectedItem.ID);
            this.WriteWord(WRITE_W.ASCII_20_1211_TMPLossDescp, msgPopup.SelectedItem.Text);

            msgPopup.Close();

            this.WaitBitSignal(READ_B.TPMLOSSREADY_19, true, HANDSHAKE_TIMEOUT_SECONDS);

            this.WriteBit(WRITE_B.TPMLOSSREADY_19, false);
        }


        #endregion

        #region Public Method : CIM Sample Processing 

        public async void LoadingCellTrackIn()
        {
            // TODO 어떤 Data를 써야 할지 확인
            //this.WriteWord(WRITE_W.trackin)

            Task<bool> tResult = this.HandShakeSignal(WRITE_B.CELLSTARTPORT1_28, true, READ_B.CELLSTARTPORT1_28, true, HANDSHAKE_TIMEOUT_SECONDS);
            await tResult.ConfigureAwait(true);

        }

        public async void UnloadingCellTrackOut()
        {
            // TODO 어떤 Data를 써야 할지 확인
            //this.WriteWord(WRITE_W.trackin)

            // TODO 어떤 DV Data를 써야 할지 확인

            Task<bool> tResult = this.HandShakeSignal(WRITE_B.CELLCOMPPORT1_34, true, READ_B.CELLCOMPPORT1_34, true, HANDSHAKE_TIMEOUT_SECONDS);
            await tResult.ConfigureAwait(true);
        }

        #endregion

        #region Public Method : CIM RMS
        int CommandHoldTimeMs = 5000;

        // PPID Change (JOB Change)
        public void PpidChange()
        {
            //장비 정지 후 ppid 변경 요청
            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 3;

            ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            Task.Run(() =>
            {
                this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, CommandHoldTimeMs);

                m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
            });
        }

        // PPID 생성
        public void PpidCreate(string ppid)
        {
            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 3;

            ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 1;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            WriteTeachPos(INFO);

            Task.Run(() =>
            {
                this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, CommandHoldTimeMs);

                m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
            });
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

        // PPID 삭제
        public void PpidDelete(string ppid)
        {
            ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 2;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            //WriteTeachPos(INFO);

            Task.Run(() =>
            {
                this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, CommandHoldTimeMs);

                m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
            });
        }


        public void RecipeDownload()
        {
            int nSelectedIdx = -1;
            string sRecipeName;

            //ModelInfo
            if (m_Reader.readBit(CIMRead.READ_B.FORMATTEDPROCESSPROGRAMSEND_54) == true)
            {
                sRecipeName = m_Reader.wordData(READ_W.ASCII_20_DF82_PPID).text?.Trim() ?? "";

                for (int i = 0; i < Common.MODEL.Count(); i++)
                {
                    ModelInfo info = Common.MODEL_INFO(i);

                    if (info.modelName().Trim() == sRecipeName)
                    {
                        nSelectedIdx = i;
                        break;
                    }
                }

                POS[] pos = ReadTeachPos();

                for (int i = 0; i < (int)TEACH_POS.MAX; i++)
                {
                    Common.MODEL[nSelectedIdx].setTeachPos(i, pos[i]);
                }

                m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMSEND_54, true);

                ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 1;
                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

                WriteTeachPos(INFO);

                Task.Run(() =>
                {
                    this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, CommandHoldTimeMs);

                    m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
                });

            }
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

        public void ParameterChange()
        {
            //m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 3;


            ModelInfo INFO = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);// Common.MODEL[0];
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 3;
            m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

            WriteTeachPos(INFO);

            Task.Run(() =>
            {
                this.HandShakeSignal(WRITE_B.PPIDCHANGE_21, true, READ_B.PPIDCHANGE_21, true, CommandHoldTimeMs);

                m_Writer.setBit(WRITE_B.PPIDCHANGE_21, false);
            });
        }

        public void ParameterQuery()
        {
            string sReqPPID = "";
            int nSelectedIdx = -1;

            //Request PPID WORD(W4217) 다른 영역인데..
            if (m_Reader.readBit(CIMRead.READ_B.FORMATTEDPROCESSPROGRAMREQUEST_55) == true)
            {
                sReqPPID = m_Reader.wordData(CIMRead.READ_W.ASCII_20_D1F0_ReqPPID).text;

                for (int i = 0; i < Common.MODEL.Count(); i++)
                {
                    ModelInfo info = Common.MODEL_INFO(i);

                    if (info.modelName().Trim() == sReqPPID)
                    {
                        nSelectedIdx = i;
                        break;
                    }
                }

                ModelInfo INFO = Common.MODEL_INFO(nSelectedIdx);// Common.MODEL[0];
                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_0014_EQPPPID).text = INFO.modelName();

                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_2_9224_PPIDMode).value = 0;
                m_Writer.wordData((WRITE_W)WRITE_W.ASCII_20_9226_PPID).text = INFO.modelName();

                WriteTeachPos(INFO);

                m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMREQUEST_55, true);

                _ = Task.Run(async () =>
                {
                    await Task.Delay(CommandHoldTimeMs).ConfigureAwait(true);
                    m_Writer.setBit(WRITE_B.FORMATTEDPROCESSPROGRAMREQUEST_55, false);
                });
            }
        }

        public void PpidListRequest()
        {
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
                    await Task.Delay(CommandHoldTimeMs).ConfigureAwait(true);
                    m_Writer.setBit(WRITE_B.CURRENTEQUIPPPIDLISTREQUEST_56, false);
                });
            }
        }

        #endregion
    }
}
