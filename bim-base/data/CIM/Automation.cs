using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CIMWrite;

namespace bim_base.data.CIM
{
    internal class Automation
    {
        private bool m_IsRun = false;

        private void SleepWithDoEvent(int _seconds)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan ts = DateTime.Now  - startTime;

            while (ts.TotalSeconds < _seconds)
            {
                Thread.Sleep(100);
                ts = DateTime.Now - startTime;  
            }
        }

        //private void WaitBitSignal(CIMRead.READ_B _addr, bool _waitValue, int timeoutSeconds = 0)
        //{
        //    bool val = CIMRead.Instance.readBit(_addr);


        //}

        //public void Initialize()
        //{
        //    this.m_IsRun = true;

        //    while (this.m_IsRun)
        //    {
        //        try
        //        {
        //            CIMWrite.Instance.setBit(WRITE_B.ALIVEBIT_1, false);
        //            this.SleepWithDoEvent(5);
        //            CIMWrite.Instance.setBit(WRITE_B.ALIVEBIT_1, true);
        //            this.SleepWithDoEvent(5);
        //        }
        //        catch
        //        {
        //            this.m_IsRun = false;
        //            break;
        //        }
        //    }
        //}

        //public void TerminalDisplay()
        //{

        //}


    }
}
