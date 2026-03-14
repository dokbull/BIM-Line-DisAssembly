using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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


        #endregion

        #region Public Method

        public bool HandShakeSignal(CIMWrite.WRITE_B _addrWrite, bool _writeValue, CIMRead.READ_B _addrRead, bool _readValue, int _timeoutSeconds = 0, bool _isOnError = true)
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
            });

            Task<bool> asyncHS = Task.Run(() => function());
            asyncHS.Wait(_timeoutSeconds * 1000);


            return asyncHS.Result;
        }

        //public bool Initialize()
        //{
        //    this.m_IsRun = true;

        //    Task<bool> reset = this.HandShakeSignal(WRITE_B.ALIVEBIT_1, false, CIMRead.READ_B.ALIVEBIT_1, false, 5, false));
            




        //    while (this.m_IsRun)
        //    {
        //    }
        //}


        #endregion


    }
}
