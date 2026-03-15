using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bim_base.data.CIM
{
    internal class CIMEnumeric
    {
        internal enum EnumRequestProcState
        {
            Alive,
            TerminalDisplay,
            OperatorCall,
            RequestInterlcokState,
            RequestPpidList,
        }

        public enum EnumAvailabilityState
        {
            /// <summary>
            /// 고장. 설비 운영이 불가능한 상태. Alarm ID를 함께 보고해야 한다.
            /// </summary>
            Down = 1,
            /// <summary>
            /// 설비가 가동 가능한 상태
            /// </summary>
            Up = 2,
        }

        public enum EnumInterlockState
        {
            /// <summary>
            /// 인터락 명령에 의해 인터락이 된 경우
            /// </summary>
            On = 1,
            /// <summary>
            /// 설비가 인터락 상태가 아닌 경우
            /// </summary>
            Off = 2,
        }

        public enum EnumMoveState
        {
            /// <summary>
            /// 설비가 외부 요인(인터락, 자체 에러, 작업자에 의한 정지 등)에 의해 동작하지 않고 정지중인 상태
            /// </summary>
            Pause = 1,
            /// <summary>
            /// 설비가 동작 가능한 상태
            /// </summary>
            Runnning = 2,
        }

        public enum EnumRunState
        {
            /// <summary>
            /// 설비에 샘플(CELL)이 없는 상태
            /// </summary>
            Idle = 1,
            /// <summary>
            /// 설비에 샘플(CELL)이 있는 상태
            /// </summary>
            Run = 2,
        }

        public enum EnumInterlockRCMD
        {
            TransferStop = 11,
            LoadingStop = 12,
            StepStop = 13,
            OWNStop = 14,
        }

        public enum EnumAlarmLevel
        {
            //None = 0, : 0사용 불가
            LightAlarm = 1,
            HeavyAlarm = 2,
        }

        public enum EnumAlarmState
        {
            None = 0, 
            LightAlarm = EnumAlarmLevel.LightAlarm,
            HeavyAlarm = EnumAlarmLevel.HeavyAlarm,
        }

        public enum EnumEqControlMode
        {
            /// <summary>
            /// 설비가 자동 운전 모드인 경우
            /// </summary>
            Auto = 1,
            /// <summary>
            /// 설비가 수동 운전 모드인 경우
            /// </summary>
            Manual = 2,
        }

        public enum EnumProcessingState
        {
            /// <summary>
            /// 설비가 샘플(CELL)을 처리하지 않고 있는 상태
            /// </summary>
            Idle = 1,
            /// <summary>
            /// 설비가 샘플(CELL)을 처리하고 있는 상태
            /// </summary>
            Run = 2,
        }

        public enum EnmumEqStopByOperatorType
        {
            Alarm = 1,
            Manual = 2,
            Teaching = 3,
        }
    }
}
