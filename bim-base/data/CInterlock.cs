using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bim_base
{
    public class CInterlock
    {
        ProcessMain main = null;
        public CInterlock(ProcessMain procMain)
        {
            main = procMain;
        }
    }
}
