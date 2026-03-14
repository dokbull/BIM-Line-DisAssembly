using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bim_base.data.CIM
{
    internal class CIMEnumeric
    {
        public enum EnumAvailabilityState
        {
            Down = 1,
            Up = 2,
        }

        public enum EnumInterlockState
        {
            On = 1,
            Off = 2,
        }

        public enum EnumMoveState
        {
            Pause = 1,
            Runnning = 2,
        }

        public enum EnumRunState
        {
            Idle = 1,
            Run = 2,
        }
    }
}
