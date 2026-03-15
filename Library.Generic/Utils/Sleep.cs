using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic
{
    public static class Utils
    {
        public static void SleepWithDoEvents(int sleepTime)
        {
            if (sleepTime <= 0) return;

            const int sleepStop = 10;

            int sTimes = sleepTime / sleepStop;

            if (sleepStop < sleepTime)
            {
                for (int i = 0; i < sTimes; i++)
                {
                    System.Threading.Thread.Sleep(sleepStop);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else
            {
                System.Threading.Thread.Sleep(sleepTime);
                System.Windows.Forms.Application.DoEvents();
            }
        }


    }
}
