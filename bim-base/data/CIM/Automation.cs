using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CIMWrite;

namespace bim_base.data.CIM
{


    internal class Automation
    {
        #region Constructor

        public Automation()
        {
        }

        #endregion

        #region Private Member


        private bool m_IsRun = false;

        private CIMRead m_Reader = new CIMRead();
        private CIMWrite m_Writer = new CIMWrite();

        #endregion

        #region public Properties


        public static Automation Instance = new Automation();

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

        #region Private Method


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
            bool val = this.CCIE_Reader.readBit(_addr);

            DateTime startTime = DateTime.Now;
            TimeSpan ts = DateTime.Now - startTime;

            while (val != _waitValue)
            {
                if(_timeoutSeconds > 0 && ts.TotalSeconds >= _timeoutSeconds)
                {
                    throw new Exception("WaitBitSignal Timeout Occurred. Addr: " + _addr.ToString() + ", WaitValue: " + _waitValue.ToString());
                }

                val = this.CCIE_Reader.readBit(_addr);

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


        #endregion

        #region Public Method

        public bool HandShakeSignal(CIMWrite.WRITE_B _addrWrite, bool _writeValue, CIMRead.READ_B _addrRead, bool _readValue, int _timeoutSeconds = 0, bool _isOnError = false)
        {
            bool function()
            {
                // TODO CHECK LHJ : H/S 진입시 중복 실행되지 않는지 확인 필요
                try
                {
                    this.CCIE_Writer.setBit(_addrWrite, _writeValue);
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
            };

            Task<bool> asyncHS = Task.Run(() => function());
            asyncHS.Wait(_timeoutSeconds * 1000);


            return asyncHS.Result;
        }

        public bool Initialize()
        {
            if (this.m_IsRun) return false;

            int timeoutSeconds = 5;

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

            this.m_IsRun = true;
            Task.Run(() =>
             {
                 bool alive = false;
                 while (this.m_IsRun)
                 {
                     alive = this.CCIE_Reader.readBit(CIMRead.READ_B.ALIVEBIT_1);

                     this.HandShakeSignal(WRITE_B.ALIVEBIT_1, !alive, CIMRead.READ_B.ALIVEBIT_1, !alive, timeoutSeconds);
                 }
             });

            return true;
        }

        public bool SetDateTime()
        {
            // TODO CHECK LHJ : PC의 Local 시간에 Date Time이 변경되는지 여부 확인

            CIMRead.WORD_DATA varDateTime = this.CCIE_Reader.wordData(CIMRead.READ_W.ASCII_7_D000_Datetime);
            if (this.TryParseDateTime(varDateTime.text, out DateTime setDateTime) == false)
                return false;

            if (this.SetSystemLocalTime(setDateTime) == false)
            {
                return false;
            }

            return true;
        }

        #endregion


    }
}
