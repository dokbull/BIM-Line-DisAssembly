using System.Collections.Generic;

namespace bim_base
{
    public enum ALARM
    {
        NONE = -1,

        // COMMUNICATION : CO 
        CO_SYSTEM_CCIE_COMM_DISCONNECT = 0,
        CO_MAIN_SYSTEM_PC_ECAT_DISCONNECT,

        // EMO : EM
        EM_MAIN_SYSTEM_PLC_EMERGENCY_OFF,
        EM_MAIN_SYSTEM_PLC_MC_OFF,

        // DOOR :DO
        DO_MAIN_DOOR_FRONT_LEFT_OPEN,
        DO_MAIN_DOOR_FRONT_RIGHT_OPEN,
        DO_MAIN_DOOR_REAR_LEFT_OPEN,
        DO_MAIN_DOOR_REAR_RIGHT_OPEN,

        // MOTOR : MO
        MO_UNLOADER_UBPP_AXIS_Z_SERVO_OFF,
        MO_UNLOADER_UBPP_AXIS_Y_SERVO_OFF,
        MO_UNLOADER_MOLDPP_AXIS_X_SERVO_OFF,
        MO_UNLOADER_MOLDPP_AXIS_Z1_SERVO_OFF,
        MO_UNLOADER_MOLDPP_AXIS_X2_SERVO_OFF,
        MO_BASE_MOLDBASE_AXIS_X_SERVO_OFF,
        MO_LOADER_INPP_AXIS_Z_SERVO_OFF,
        MO_LOADER_INPP_AXIS_Y_SERVO_OFF,

        MO_UNLOADER_UBPP_AXIS_Z_SERVO_ALARM,
        MO_UNLOADER_UBPP_AXIS_Y_SERVO_ALARM,
        MO_UNLOADER_MOLDPP_AXIS_X_SERVO_ALARM,
        MO_UNLOADER_MOLDPP_AXIS_Z1_SERVO_ALARM,
        MO_UNLOADER_MOLDPP_AXIS_X2_SERVO_ALARM,
        MO_BASE_MOLDBASE_AXIS_X_SERVO_ALARM,
        MO_LOADER_INPP_AXIS_Z_SERVO_ALARM,
        MO_LOADER_INPP_AXIS_Y_SERVO_ALARM,

        MO_UNLOADER_UBPP_AXIS_Z_HOME_FAIL,
        MO_UNLOADER_UBPP_AXIS_Y_HOME_FAIL,
        MO_UNLOADER_MOLDPP_AXIS_X_HOME_FAIL,
        MO_UNLOADER_MOLDPP_AXIS_Z1_HOME_FAIL,
        MO_UNLOADER_MOLDPP_AXIS_X2_HOME_FAIL,
        MO_BASE_MOLDBASE_AXIS_X_HOME_FAIL,
        MO_LOADER_INPP_AXIS_Z_HOME_FAIL,
        MO_LOADER_INPP_AXIS_Y_HOME_FAIL,

        MO_UNLOADER_UBPP_AXIS_Z_MOVE_TIMEOUT,
        MO_UNLOADER_UBPP_AXIS_Y_MOVE_TIMEOUT,
        MO_UNLOADER_MOLDPP_AXIS_X_MOVE_TIMEOUT,
        MO_UNLOADER_MOLDPP_AXIS_Z1_MOVE_TIMEOUT,
        MO_UNLOADER_MOLDPP_AXIS_X2_MOVE_TIMEOUT,
        MO_BASE_MOLDBASE_AXIS_X_MOVE_TIMEOUT,
        MO_LOADER_INPP_AXIS_Z_MOVE_TIMEOUT,
        MO_LOADER_INPP_AXIS_Y_MOVE_TIMEOUT,

        // VACUUM : VC
        VC_MAIN_SYSTEM_MAIN_AIR1_LOW,
        VC_MAIN_SYSTEM_MAIN_AIR2_LOW,
        VC_MAIN_SYSTEM_SUB_AIR1_LOW,
        VC_MAIN_SYSTEM_SUB_AIR2_LOW,
        VC_MAIN_SYSTEM_SUB_AIR3_LOW,
        VC_MAIN_SYSTEM_SUB_AIR4_LOW,

        VC_UNLOADER_UBPP_PICKER_ON,
        VC_UNLOADER_UBPP_PICKER_OFF,

        VC_REVERSE_UBREVERSE_HOLDER1_ON,
        VC_REVERSE_UBREVERSE_HOLDER1_OFF,

        VC_REVERSE_UBREVERSE_HOLDER2_ON,
        VC_REVERSE_UBREVERSE_HOLDER2_OFF,

        // CYLINDER : CY
        CY_LOADER_INPP_GRIPPER_CYL_GRIP,
        CY_LOADER_INPP_GRIPPER_CYL_UNGRIP,

        CY_UNLOADER_MOLDPP_GRIPPER1_CYL_GRIP,
        CY_UNLOADER_MOLDPP_GRIPPER1_CYL_UNGRIP,
        CY_UNLOADER_MOLDPP_GRIPPER2_CYL_GRIP,
        CY_UNLOADER_MOLDPP_GRIPPER2_CYL_UNGRIP,

        CY_UNLOADER_UBPP_PICKER_CYL_FWD,
        CY_UNLOADER_UBPP_PICKER_CYL_BWD,

        CY_LOADER_ALIGNCV_ALIGN_CYL_FWD,
        CY_LOADER_ALIGNCV_ALIGN_CYL_BWD,

        CY_LOADER_ALIGNCV_UPDOWN_CYL_UP,
        CY_LOADER_ALIGNCV_UPDOWN_CYL_DOWN,

        CY_REVERSE_MOLDREVERSE_GRIPPER_CYL_GRIP,
        CY_REVERSE_MOLDREVERSE_GRIPPER_CYL_UNGRIP,

        CY_REVERSE_MOLDREVERSE_TURN_CYL_TURN,
        CY_REVERSE_MOLDREVERSE_TURN_CYL_RETURN,

        CY_REVERSE_MOLDREVERSE_UBDOWN_CYL_UP,
        CY_REVERSE_MOLDREVERSE_UBDOWN_CYL_DOWN,

        CY_BASE_MOLDBASE_UPDOWN_CYL_UP,
        CY_BASE_MOLDBASE_UPDOWN_CYL_DOWN,

        CY_BASE_MOLDBASE_HOLD_CYL_FWD,
        CY_BASE_MOLDBASE_HOLD_CYL_BWD,

        CY_BASE_MOLDBASE_UNLOCK_CYL_FWD,
        CY_BASE_MOLDBASE_UNLOCK_CYL_BWD,

        CY_BASE_MOLDBASE_UPPER_GUIDE_FWD,
        CY_BASE_MOLDBASE_UPPER_GUIDE_BWD,

        CY_REVERSE_UBREVERSE_TURN1_CYL_TURN,
        CY_REVERSE_UBREVERSE_TURN1_CYL_RETURN,
        CY_REVERSE_UBREVERSE_TURN2_CYL_TURN,
        CY_REVERSE_UBREVERSE_TURN2_CYL_RETURN,

        CY_REVERSE_UBREVERSE_UPDOWN1_CYL_UP,
        CY_REVERSE_UBREVERSE_UPDOWN1_CYL_DOWN,
        CY_REVERSE_UBREVERSE_UPDOWN2_CYL_UP,
        CY_REVERSE_UBREVERSE_UPDOWN2_CYL_DOWN,

        CY_UNLOADER_RETURNCV_UPDOWN_CYL_UP,
        CY_UNLOADER_RETURNCV_UPDOWN_CYL_DOWN,

        // SENSOR : SE
        SE_REVERSE_MOLDREVERSE_DETECT_PRODUCT,
        SE_REVERSE_MOLDREVERSE_NOT_PRODUCT,

        SE_BASE_MOLDBASE_ST1_DETECT_PRODUCT,
        SE_BASE_MOLDBASE_ST2_DETECT_PRODUCT,
        SE_BASE_MOLDBASE_ST3_DETECT_PRODUCT,
        SE_BASE_MOLDBASE_ST1_NOT_PRODUCT,
        SE_BASE_MOLDBASE_ST2_NOT_PRODUCT,
        SE_BASE_MOLDBASE_ST3_NOT_PRODUCT,

        // TEMPERATURE : TE

        // UTILITY : UT

        // OPERATION : OT
    }

    public enum ALARM_TYPE
    {
        NONE = 0,
        LIGHT,
        WARNING,
        CRITICAL
    }

    public class AlarmData
    {
        public int code;
        public string datetime;
        public int type;
        public string desc;
    }

    public class Alarm
    {
        static CSettings m_setting = null;

        static Dictionary<string, string> m_msgEngMap = new Dictionary<string, string>();
        static Dictionary<string, string> m_actEngMap = new Dictionary<string, string>();

        public Alarm()
        {
        }

        public static void load()
        {
            string path = Common.PATH + "\\data\\alarmMessage.dat";
            m_setting = new CSettings(path);

            List<string> msgList = m_setting.getKeys("MESSAGE_ENG");
            List<string> actList = m_setting.getKeys("ACTION_ENG");

            if (msgList != null)
            {
                foreach (string text in msgList)
                {
                    m_msgEngMap[text] = m_setting.getValue("MESSAGE_ENG", text, text);
                    m_actEngMap[text] = m_setting.getValue("ACTION_ENG", text, text);
                }
            }
        }

        public static string messageEng(int alarm, string desc = "")
        {
            return messageEng((ALARM)alarm, desc);
        }

        public static string messageEng(ALARM alarm, string desc = "")
        {
            string text = "";

            string codeText = alarm.ToString();

            if (m_msgEngMap.ContainsKey(codeText) == true)
                text = m_msgEngMap[codeText];
            else
                text = alarm.ToString();

            if (desc != "")
                text += " / " + desc;

            return text;
        }

        public static string actionEng(int alarm, string desc = "")
        {
            return actionEng((ALARM)alarm, desc);
        }

        public static string actionEng(ALARM alarm, string desc = "")
        {
            string text = "";

            string codeText = alarm.ToString();

            if (m_actEngMap.ContainsKey(codeText) == true)
                text = m_actEngMap[codeText];

            if (desc != "")
                text += " / " + desc;

            return text;
        }
    }//class
}//namespace
