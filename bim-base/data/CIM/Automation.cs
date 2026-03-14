using lib.plc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bim_base.CSTATION;
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

        //private RMS m_RMS = new RMS();

        #endregion

        #region public Properties

        private int commandHoldTimeMs = 1000;
        public static Automation Instance = new Automation();

        MelsecCCLink m_ccLink = null;
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

        public bool Initialize()
        {
            m_ccLink = new MelsecCCLink();

            m_ccLink.NetworkNo = 1;
            m_ccLink.StationNo = 255;
            m_ccLink.ChannelNo = 151;

            if (!m_ccLink.open())
                return false;

            m_Reader = new CIMRead();
            m_Writer = new CIMWrite();

            return true;
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
        public void commCIM()
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

            if (ret == false)
            {
                // Debug.warning("ProcessMain::run cclink write failed");
            }
        }

        public void setCimBit(CIMWrite.WRITE_B addr, bool value)
        {
            m_Writer.setBit(addr, value);
        }

        public bool readCimBit(CIMWrite.WRITE_B addr)
        {
            return m_Writer.bit(addr);
        }

        public bool readCimBit(CIMRead.READ_B addr)
        {
            return m_Reader.readBit(addr);
        }

        public string readCimWord(CIMRead.READ_W addr)
        {
            CIMRead.WORD_DATA data = m_Reader.wordData(addr);

            string text = "";

            if (data.type == CIMRead.READ_TYPE.DEC)
                text = data.value.ToString();

            if (data.type == CIMRead.READ_TYPE.ASCII)
                text = data.text;

            return text;
        }

        public string readCimWord(CIMWrite.WRITE_W addr)
        {
            CIMWrite.WORD_DATA data = m_Writer.wordData(addr);

            string text = "";

            if (data.type == CIMWrite.WRITE_TYPE.DEC)
                text = data.value.ToString();

            if (data.type == CIMWrite.WRITE_TYPE.ASCII)
                text = data.text;

            return text;
        }

        public void writeCimWord(CIMWrite.WRITE_W addr, string text)
        {
            CIMWrite.WORD_DATA data = m_Writer.wordData(addr);

            if (data.type == CIMWrite.WRITE_TYPE.DEC)
                data.value = Util.toInt32(text);

            if (data.type == CIMWrite.WRITE_TYPE.ASCII)
                data.text = text;
        }

        //public bool Initialize()
        //{
        //    this.m_IsRun = true;

        //    int timeoutSeconds = 5;

        //    Task<bool> reset = Task.Run(() => this.HandShakeSignal(WRITE_B.ALIVEBIT_1, false, CIMRead.READ_B.ALIVEBIT_1, false, timeoutSeconds, false));
        //    reset.Wait(timeoutSeconds);




        //    while (this.m_IsRun)
        //    {
        //    }
        //}


        #endregion


        #region RMS 관련 펑션
        //상시 대기
        public void PpidListRequest()
        {
            //ModelInfo
            if (m_Reader.readBit(CIMRead.READ_B.CURRENTEQUIPPPIDLISTREQUEST_56) == true)
            {
                List<string> items = Common.MODEL_INFO.loadModelList();

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
