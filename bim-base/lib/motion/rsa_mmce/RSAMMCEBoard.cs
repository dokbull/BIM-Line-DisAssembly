using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NMCMotionSDK;
using static NMCMotionSDK.NMCSDKLib;

using System.IO;
using System.Security.Permissions;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;

public class RSAMMCEBoard
{
    ushort m_id = 255;

    bool m_simulation = false;

    bool m_contiPause = false;

    ushort m_lastContiGroup = 0;
    ushort m_lastContiCount = 0;
    double m_lastContiVel = 0.0d;
    double m_lastContiAcc = 0.0d;
    double m_lastContiDec = 0.0d;
    double[] m_lastContiPos = null;

    public enum CUR_MODE
    {
        IDLE = 0,
        SCAN = 1,
        RUN = 2,
        INTRANSITION = 3,
        ERROR = 4,
        LINKBROKEN = 5,
    }

    public enum SLAVE_COMBI_MODE
    {
        INIT = 1,
        PRE_OP = 2,
        SAFE_OP = 4,
        OP = 8,
#if false
        STATUS_COMBINATION_1 = 1,   //Init.
        STATUS_COMBINATION_2 = 2,   //PreOP
        STATUS_COMBINATION_3 = 3,   //ProOP + Init or Bootstrap
        STATUS_COMBINATION_4 = 4,   //SafeOP
        STATUS_COMBINATION_5 = 5,   //SafeOP + Init
        STATUS_COMBINATION_6 = 6,   //SafeOP + PreOP
        STATUS_COMBINATION_7 = 7,   //SafeOP + PreOP + Init or SafeOP + Bootstrap
        STATUS_COMBINATION_8 = 8,   //OP
        STATUS_COMBINATION_9 = 9,   //OP + Init
        STATUS_COMBINATION_10 = 10, //OP + PreOP
        STATUS_COMBINATION_11 = 11, //OP + PreOP + Init or OP + Bootstrap
        STATUS_COMBINATION_12 = 12, //OP + SafeOP
        STATUS_COMBINATION_13 = 13, //OP + SafeOP + Init
        STATUS_COMBINATION_14 = 14, //OP + SafeOP + PreOP
        STATUS_COMBINATION_15 = 15,	//OP + SafeOP + PreOP + Init or OP + SafeOP + Bootstrap
#endif
    }

    public enum AXIS_ERROR_CODE
    {
        NONE = -1,
        MC_AXIS_DEVICE_ERROR = 1,
        MC_AXIS_INVALID_AXIS_STATE = 2,
        MC_AXIS_PARAMETER_INVALID = 3,
        MC_AXIS_RESOURCE_ERROR = 6,
        MC_AXIS_CONFIG_INVALID = 7,
        MC_AXIS_FOLLOWING_ERROR = 8,
        MC_AXIS_MAX_SYS_VEL_OVER_ERROR = 10,
        MC_AXIS_MAX_SYS_ACC_OVER_ERROR = 11,
        MC_AXIS_MAX_SYS_DEC_OVER_ERROR = 12,
        MC_AXIS_MAX_SYS_JERK_OVER_ERROR = 13,
        MC_AXIS_MALFUNCTION_ERROR = 14,
        MC_AXIS_GEARING_RULE_VIOLATION = 15,
        MC_AXIS_LIMIT_POSITION_ERROR = 16,
    }

    public enum AXIS_ERROR_INFO
    {
        NONE = -1,

        // API CODE
        MC_Power = 1,
        MC_MoveAbsolute,
        MC_MoveRelative = 3,

        MC_MoveVelocity = 6,
        MC_Home,
        MC_Stop,
        MC_ReadStatus,
        MC_ReadAxisError,
        MC_Reset,
        MC_ReadParameter,
        MC_WriteParameter,
        MC_ReadActualPosition = 14,

        MC_GearIn = 21,
        MC_GearOut = 22,

        MC_TouchProbe = 24,
        MC_AbortTrigger,
        MC_ReadDigitalInput,
        MC_ReadDigitalOutput,
        MC_WriteDigitalOutput,
        MC_SetPosition = 29,

        MC_ReadActualVelocity = 31,

        Homing = 37,
        MC_TriggerMonitor,
        MC_ReadMotionState,
        MC_ReadAxisInfo,
        MC_ReadAxisStatus,
        MC_GearMonitor,
        MC_ReadProfileData,
        MC_AddAxisToGroup,
        MC_RemoveAxisFromGroup,
        MC_UngroupAllAxes,
        MC_GroupReadConfiguration,
        MC_GroupEnable,
        MC_GroupDisable,
        MC_MoveLinearAbsolute,
        MC_GroupHalt,
        MC_GroupStop,
        MC_MoveCircularAbsolute,
        MC_GroupReadStatus,
        MC_GroupReadError,
        MC_GroupReset,
        MC_GroupoResetProfileData,
        MC_GroupoReadInfo = 58,

        MC_GroupSetRawDataMode = 60,
        MC_GroupSetRawDataStatus,
        MC_GroupClearRawData,
        MC_GroupSetRawData,
        MC_ReadRemainBuffer,
        MC_GroupReadRemainBuffer = 65,

        // ERROR CODE 1
        MC_DEVICE_ERROR_FAULT = 101,
        MC_DEVICE_ERROR_ILLEGAL_OP = 102,

        // ERROR CDOE 6
        MC_RESOURCE_ERROR_CMD_BUFFER = 601,

        // ERROR CODE 7
        MC_AXIS_CFG_PARM_POS_LIM_SW_INPUT_NUM = 706,
        MC_AXIS_CFG_PARM_NEG_LIM_SW_INPUT_NUM = 709,
        MC_AXIS_CFG_PARM_HOME_SENSOR_INPUT_NUM = 711,
        MC_AXIS_CFG_PARM_Z_PHASE_INPUT_NUM = 713,
        MC_AXIS_CFG_PARM_HOME_TYPE = 750,
    
        // ERROR CODE 16
        Positive_HW_Limit_Pending = 1600,
        Negative_HW_Limit_Pending = 1601,
        Positive_SW_Limit_Pending = 1602,
        Negative_SW_Limit_Pending = 1603,
        MC_Home_ERROR = 1607,
    }

    public enum AXIS_ERROR_PARAMETER
    {       
        NONE = -1,

        Disable = 0,
        StandStill,
        DiscreteMotion,
        ContinuousMotion,
        SynchronizedMotion,
        Homing,
        Stopping,
        ErrorStop,
    }

    public RSAMMCEBoard(int id, RSAMMCELib lib)
    {
        m_id = (ushort)id;

        string path = Common.PATH + "\\simulation";
        m_simulation = File.Exists(path);

        if (isStopMode() == true)
            masterRun();
    }

    public bool masterInit()
    {
        Debug.debug("RSAMMCEBoard::masterInit id:" + m_id);

        MC_STATUS ret = NMCSDKLib.MC_MasterInit(m_id);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::masterRun failed. id:" + m_id + " ret:" + ret);
            return false;
        }

        return true;
    }

    public bool masterRun()
    {
        if (m_simulation == true)
            return true;

        Debug.debug("RSAMMCEBoard::masterRun id:" + m_id);

        MC_STATUS ret = NMCSDKLib.MC_MasterRUN(m_id);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::masterRun failed. id:" + m_id + " ret:" + ret);
            return false;
        }

        CElaspedTimer timeOutTimer = new CElaspedTimer(10 * 1000);
        timeOutTimer.start();

        while (true)
        {
            bool result = isRunMode();

            if (result == true)
                break;

            if (timeOutTimer.isElasped())
            {
                Debug.debug("RSAMMCEBoard::masterRun isRunMode timeout. id:" + m_id);
                return false;
            }

            Util.waitTick(100);
        }

        CElaspedTimer slaveTimeOutTimer = new CElaspedTimer(10 * 1000);
        slaveTimeOutTimer.start();

        while (true)
        {
            bool isSlaveOp = slaveGetCurStateAll();

            if (isSlaveOp == true)
                break;

            if (slaveTimeOutTimer.isElasped())
            {
                Debug.debug("RSAMMCEBoard::masterRun slave OP timeout. id:" + m_id);
                return false;
            }

            Util.waitTick(100);
        }

        Util.waitTick(100);

        return true;
    }

    public bool masterStop()
    {
        Debug.debug("RSAMMCEBoard::masterStop id:" + m_id);

        MC_STATUS ret = NMCSDKLib.MC_MasterSTOP(m_id);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::masterStop failed. id:" + m_id + " ret:" + ret);
            return false;
        }

        CElaspedTimer timeoutTimer = new CElaspedTimer(10 * 1000);
        timeoutTimer.start();

        while (true)
        {
            bool result = isStopMode();

            if (result == true)
                break;

            if (timeoutTimer.isElasped())
            {
                Debug.debug("RSAMMCEBoard::masterStop timeout. id:" + m_id);
                return false;
            }

            Util.waitTick(100);
        }

        return true;
    }
    public bool isRunMode()
    {
        byte boardState = 0;

        MC_STATUS ret = NMCSDKLib.MasterGetCurMode(m_id, ref boardState);

        CUR_MODE mode = (CUR_MODE)boardState;

        //Debug.debug("RSAMMCEBoard::isRunMode state:" + mode);

        if (boardState == (int)CUR_MODE.RUN) // RUN
            return true;

        return false;
    }

    public bool isStopMode()
    {
        if (m_simulation == true)
            return true;

        byte boardState = 0;

        MC_STATUS ret = NMCSDKLib.MasterGetCurMode(m_id, ref boardState);

        if (boardState == (int)CUR_MODE.IDLE)
            return true;

        return false;
    }

    public bool slaveGetCurStateAll()
    {
        ushort deviceCount = 0;
        ushort workingCount = 0;
        byte statusCombination = 0;

        MC_STATUS ret = NMCSDKLib.SlaveGetCurStateAll(m_id, ref deviceCount, ref workingCount, ref statusCombination);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::slaveGetCurStateAll failed. id:" + m_id + " ret:" + ret);
            return false;
        }

        SLAVE_COMBI_MODE mode = (SLAVE_COMBI_MODE)statusCombination;

        if (mode == SLAVE_COMBI_MODE.OP)
            return true;

        return false;
    }


    public bool setServoOnOff(int axisId, bool value)
    {
        Debug.debug("RSAMMCEBoard::setServoOnOff boardId:" + m_id +
            " axisId:" + axisId + " value:" + value);

        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_Power(m_id, (ushort)axisId, value);

        if (ret == MC_STATUS.MC_DD_OK)
            return true;

        Debug.debug("RSAMMCEBoard::setServoOnOff failed. ret:" + ret + " id:" + m_id + " axisId:" + axisId + " value:" + value);
        return false;
    }

    public bool setCmdPosition(int axisId, double pos)
    {
        Debug.debug("RSAMMCEBoard::setCmdPosition boardId:" + m_id +
            " axisId:" + axisId + " pos:" + pos.ToString("0.00"));

        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_SetPosition(m_id, (ushort)axisId, pos, true, MC_EXECUTION_MODE.mcImmediately);

        if (ret == MC_STATUS.MC_DD_OK)
            return true;

        Debug.debug("RSAMMCEBoard::setCmdPosition failed. ret:" + ret + " id:" + m_id + 
            " axisId:" + axisId + " pos:" + pos.ToString("0.00"));
        return false;
    }

    public bool alarmClear(int axisId)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_Reset(m_id, (ushort)axisId);
        
        if (ret == MC_STATUS.MC_DD_OK)
            return true;

        Debug.debug("RSAMMCEBoard::alarmClear failed. id:" + m_id + " axisId:" + axisId);
        return false;
    }

    public string alarmCode(ushort axisId)
    {
        if (m_simulation == true)
            return "simulation";

        ushort _id = 0;            //리턴받을 ErrorID 
        ushort _info0 = 0;        //리턴받을 ErrorInfo0
        ushort _info1 = 0;        //리턴받을 ErrorInfo1

        MC_STATUS ret = NMCSDKLib.MC_ReadAxisError(m_id, axisId,
            ref _id,
            ref _info0,
            ref _info1);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::alarmCode failed. id:" + m_id + " axisId:" + axisId
                + " ret:" + ret);
            return "" + ret;
        }

        AXIS_ERROR_CODE code = AXIS_ERROR_CODE.NONE;
        AXIS_ERROR_INFO info = AXIS_ERROR_INFO.NONE;

        string codeStr = "";
        string infoStr = "";
        string paramStr = "";

        try
        {
            code = (AXIS_ERROR_CODE)_id;
            codeStr = code.ToString();

            if (_id == 15) // not use info
            {
                info = AXIS_ERROR_INFO.NONE;
            }
            else if (_id == 1 || _id == 6 || _id == 7 || _id == 16) // not use API CODE
            {
                int index = (m_id * 100) + _info0;
                info = (AXIS_ERROR_INFO)index;
            }
            else // use API CODE
            {
                info = (AXIS_ERROR_INFO)_info0;
            }

            infoStr = info.ToString();

            if (code == AXIS_ERROR_CODE.MC_AXIS_INVALID_AXIS_STATE)
            {
                paramStr = ((AXIS_ERROR_PARAMETER)_info1).ToString();
            }
            else if (code == AXIS_ERROR_CODE.MC_AXIS_PARAMETER_INVALID)
            {
                paramStr = "PARAM" + _info1.ToString();
            }
        }
        catch (Exception e)
        {
            Debug.warning("RSAMMCEBoard::alarmCode exception enum find. message:" + e.Message);
        }

        string text = codeStr + "/" + infoStr;
        
        if (paramStr != "")
            text += "-" + paramStr;

        return text;
    }

    public bool readAxisStatus(ushort axisId, ref uint axisStatus)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_ReadAxisStatus(m_id, axisId, ref axisStatus);

        if (ret == MC_STATUS.MC_DD_OK)
            return true;

        Debug.debug("RSAMMCEBoard::readAxisStatus failed. id:" + m_id + " axisId:" + axisId + 
            " ret:" + ret);
        return false;
    }

    public double readActualPosition(ushort axisId)
    {
        if (m_simulation == true)
            return 0.0d;

        double val = 0.0d;
        MC_STATUS ret = NMCSDKLib.MC_ReadActualPosition(m_id, axisId, ref val);

        if (ret == MC_STATUS.MC_DD_OK)
            return val;

        Debug.debug("RSAMMCEBoard::readActualPosition failed. id:" + m_id + " axisId:" + axisId);
        return 0.0d;
    }

    public double readParameter(int axisId, MC_ParamID param)
    {
        if (m_simulation == true)
            return 0.0d;

        double value = 0.0d;
        MC_STATUS ret = NMCSDKLib.MC_ReadParameter(m_id, (ushort)axisId, (uint)param, ref value);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::readParameter id:" + m_id +
                " axisId:" + axisId + " param:" + param + " ret:" + ret);
            return 0.0d;
        }

        return value;
    }

    public bool writeParameter(int axisId, MC_ParamID param, double value)
    {
        MC_STATUS ret = NMCSDKLib.MC_WriteParameter(m_id, (ushort)axisId, (uint)param, value);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::writeParameter failed id:" + m_id +
                " axisId:" + axisId + " param:" + param + " value:" + value +
                " ret:" + ret);
            return false;
        }

        return true;
    }

    public void testRead(ushort axisId)
    {
        double value = readParameter(5, MC_ParamID.mcpCmdScaleFactor);
    }

    public bool setHomeParam(ushort axisId, AXIS_DIR dir,
        double vel, double acc, double dec, double creepVel,
        double inPosWindow, double inVelWindow, 
        double offset, uint homeSensorType = 1)
    {
        if (m_simulation == true)
            return true;

        uint rsaDir = (uint)(dir == AXIS_DIR.NEG ? 0 : 1);

        MC_STATUS ret = NMCSDKLib.MC_WriteIntParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingDir, rsaDir);

        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingVelocity, vel);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingAcceleration, acc);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingDeceleration, dec);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingJerk, dec * 10);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingCreepVelocity, creepVel);

        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpInPositionWindowSize, inPosWindow);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpInVelocityWindowSize, inVelWindow);

        ret &= NMCSDKLib.MC_WriteParameter(m_id, axisId, (uint)MC_ParamID.mcpHomePositionOffset, offset);
		ret &= NMCSDKLib.MC_WriteIntParameter(m_id, axisId, (uint)MC_ParamID.mcpHomingType, homeSensorType);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::setHomeParam failed. id:" + m_id + 
                " axisId:" + axisId + " ret:" + ret);
            return false;
        }

        return true;
    }

    public void getMasterLastError()
    {
        uint seqNo = 0;
        uint errorCode = 0;
        byte[] extErrorInfo = new byte[8];

        NMCSDKLib.MasterGetLastError(m_id, ref seqNo, ref errorCode, extErrorInfo);

        string extInfoText = "";

        for (int i=0; i<8; i++)
        {
            extInfoText += extErrorInfo[i];

            if (i < 7)
                extInfoText += "-";
        }

        Debug.debug("RSAMMCEBoard::getMasterLastError id:" + m_id + " seqNo:" + seqNo + 
            " errorCode:" + errorCode + " extErrorInfo:" + extInfoText);

    }

    public bool setUnitScaleFactor(ushort axisId, double value)
    {
        if (m_simulation == true)
            return true;

        bool ret = writeParameter(axisId, MC_ParamID.mcpCmdScaleFactor, value);
        ret &= writeParameter(axisId, MC_ParamID.mcpFeedbackScaleFactor, value);

        if (ret == false)
        {
            Debug.debug("RSAMMCEBoard::setUnitScaleFactor failed. id:" + m_id + 
                " axisId:" + axisId + " ret:" + ret);

            getMasterLastError();

            return false;
        }

        return true;
    }

    public bool homeMove(ushort axisId, double pos = 0)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_Home(m_id, axisId, pos, MC_BUFFER_MODE.mcAborting);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::homeMove failed. id:" + m_id + 
                " axisId:" + axisId + " ret:" + ret);
            return false;
        }

        return true;
    }

    public bool relMove(ushort axisId, double distance, double vel, double acc, double dec)
    {
        if (m_simulation == true)
            return true;

        double jerk = vel * 10.0d;

        MC_STATUS ret = NMCSDKLib.MC_MoveRelative(m_id, axisId,
            distance, vel, acc, dec, jerk, MC_BUFFER_MODE.mcBuffered);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::relMove failed. id:" + m_id + " axisId:" + axisId +
                " distance:" + distance);
            return false;
        }

        return true;
    }

    public bool relMove(ushort axisId, double distance, double vel, double acc, double dec, double jerkFactor)
    {
        if (m_simulation == true)
            return true;

        double jerk = acc * jerkFactor; // Jerk = Acceleration * Jerk Factor

        MC_STATUS ret = NMCSDKLib.MC_MoveRelative(m_id, axisId,
            distance, vel, acc, dec, jerk, MC_BUFFER_MODE.mcBuffered);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::relMove failed. id:" + m_id + " axisId:" + axisId +
                " distance:" + distance);
            return false;
        }

        return true;
    }

    public bool absMove(ushort axisId, double pos, double vel, double acc, double dec)
    {
        if (m_simulation == true)
            return true;

        double jerk = vel * 10.0d;

        MC_STATUS ret = NMCSDKLib.MC_MoveAbsolute(m_id, axisId,
            pos, vel, acc, dec, jerk, MC_DIRECTION.mcPositiveDirection,
            MC_BUFFER_MODE.mcBuffered);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::absMove failed. id:" + m_id + " axisId:" + axisId + " pos:" + pos);
            return false;
        }

        return true;
    }

    public bool absMove(ushort axisId, double pos, double vel, double acc, double dec, double jerkFactor)
    {
        if (m_simulation == true)
            return true;

        double jerk = acc * jerkFactor; // Jerk = Acceleration * Jerk Factor

        MC_STATUS ret = NMCSDKLib.MC_MoveAbsolute(m_id, axisId,
            pos, vel, acc, dec, jerk, MC_DIRECTION.mcPositiveDirection, 
            MC_BUFFER_MODE.mcBuffered);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::absMove failed. id:" + m_id + " axisId:" + axisId + " pos:" + pos);
            return false;
        }

        return true;
    }

    public bool velMove(ushort axisId, double vel, double acc, double dec)
    {
        if (m_simulation == true)
            return true;

        MC_DIRECTION dir = (vel > 0) ? MC_DIRECTION.mcPositiveDirection : MC_DIRECTION.mcNegativeDirection;
        vel = Math.Abs(vel); 

        MC_STATUS ret = NMCSDKLib.MC_MoveVelocity(m_id, axisId,
            Math.Abs(vel), acc, dec, 0, dir, MC_BUFFER_MODE.mcAborting);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::velMove failed. id:" + m_id + " axisId:" + axisId + " vel:" + vel);
            return false;
        }

        return true;
    }

    public bool stop(ushort axisId, double deacc)
    {
        if (m_simulation == true)
            return true;

        if (deacc < 500)
            deacc = 500;

        MC_STATUS ret = NMCSDKLib.MC_Halt(m_id, axisId,
            deacc, 0, MC_BUFFER_MODE.mcAborting);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::stop failed. id:" + m_id + " axisId:" + axisId);
            return false;
        }

        return true;
    }

    /// <summary>
    /// exec true를 사용해 멈추고, false를 한번 더 호출하여 Stopping 상태를 풀어줘야함.
    /// </summary>
    /// <param name="axisId"></param>
    /// <param name="exec">
    ///    true : 모션 정지 및 Stopping 상태
    ///    false : Stopping 해제
    /// </param>
    /// <returns></returns>
    public bool emoStop(ushort axisId, bool exec)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_Stop(m_id, axisId,
            exec, 18446744073709551615, 18446744073709551615);

        if (ret != MC_STATUS.MC_OK)
        {
            //Debug.debug("RSAMMCEBoard::emoStop failed. id:" + m_id +  " axisId:" + axisId + " ret:" + ret);

            return false;
        }

        return true;
    }

    public bool ioReadByte(ushort eCatAddr, RSA_IO rasIo, int offset, ref byte value)
    {
        if (m_simulation == true)
            return true;

        IOBufMode bufMode = IOBufMode.BUF_IN;

        if (rasIo == RSA_IO.OUT)
            bufMode = IOBufMode.BUF_OUT;

        if (m_simulation)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_IO_READ_BYTE(m_id, eCatAddr, 
            (ushort)bufMode, (uint)offset, ref value);
        
        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::ioReadByte failed. id:" + m_id + " eCatAddr:" + eCatAddr + 
                " rasIO:" + rasIo + " offset:" + offset);

            return false;
        }

        return true;
    }

    public bool ioWriteBit(ushort eCatAddr, int cardIndex, int offset, int size, bool value)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_IO_WRITE_BIT(
            m_id, (ushort)eCatAddr, (uint)cardIndex, 
            (byte)offset, value);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::ioWriteBit failed. id:" + m_id + " eCatAddr:" + eCatAddr +
                " cardIndex:" + cardIndex + " offset:" + offset + " value:" + value + 
                " ret:" + ret);

            return false;
        }

        return true;
    }

    public bool setSoftwareLimit(ushort eCatAddr, bool use, double posLimit, double negLimit)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_WriteBoolParameter(m_id, (ushort)eCatAddr, (uint)NMCSDKLib.MC_ParamID.mcpEnableLimitPos, use);
        ret &= NMCSDKLib.MC_WriteBoolParameter(m_id, (ushort)eCatAddr, (uint)NMCSDKLib.MC_ParamID.mcpEnableLimitNeg, use);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, (ushort)eCatAddr, (uint)NMCSDKLib.MC_ParamID.mcpSWLimitPos, posLimit);
        ret &= NMCSDKLib.MC_WriteParameter(m_id, (ushort)eCatAddr, (uint)NMCSDKLib.MC_ParamID.mcpSWLimitNeg, negLimit);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::setSoftwareLimit failed. id:" + m_id + " eCatAddr:" + eCatAddr + " isUse:" + use);

            return false;
        }

        return true;
    }

    public bool getSoftwareLimit(ushort eCatAddr, ref double posLimit, ref double negLimit)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_ReadParameter(m_id, (ushort)eCatAddr, (uint)NMCSDKLib.MC_ParamID.mcpSWLimitPos, ref posLimit);
        ret &= NMCSDKLib.MC_ReadParameter(m_id, (ushort)eCatAddr, (uint)NMCSDKLib.MC_ParamID.mcpSWLimitNeg, ref negLimit);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::getSoftwareLimit failed. id:" + m_id + " eCatAddr:" + eCatAddr);

            return false;
        }

        return true;
    }

    // Coordinated Motion
    public virtual bool addContiGroup(ushort axis, ushort group, ushort id)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_AddAxisToGroup(m_id, axis, group, id);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::addContiGroup failed. id:" + m_id + " group:" + group + " axis:" + axis + " id:" + id);
            return false;
        }

        return true;
    }

    public virtual bool deleteContiGroup(ushort axis, ushort group, ushort id)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_RemoveAxisFromGroup(m_id, group, id);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::deleteContiGroup failed. id:" + m_id + " group:" + group + " id:" + id);

            return false;
        }

        return true;
    }

    public virtual bool clearContiGroup(ushort group)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_UngroupAllAxes(m_id, group);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::clearContiGroup failed. id:" + m_id + " group:" + group);

            return false;
        }

        return true;
    }

    public virtual bool enableContiGroup(ushort group)
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_GroupEnable(m_id, group);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::enableContiGroup failed. id:" + m_id + " group:" + group);
            return false;
        }

        return true;
    }

    public virtual bool contiLineMove(ushort group, ushort posCount, double[] pos, double vel, double acc, double dec)
    {
        if (m_simulation == true)
            return true;

        if (posCount != pos.Length)
        {
            Debug.debug("RSAMMCEBoard::contiMove failed. can not match position count. posCount:" + posCount + " posLength:" + pos.Length);
            return false;
        }

        ushort transitionParameterCount = 1;
        double[] transitionParameter = new double[1];

        MC_STATUS ret = NMCSDKLib.MC_MoveLinearAbsolute(m_id, group, posCount, pos, vel, acc, dec, vel * 10,
            MC_CoordSystem.mcACS, MC_BUFFER_MODE.mcBuffered, MC_TRANSITION_MODE.mcTMNone, transitionParameterCount, transitionParameter);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::contiMove failed. id:" + m_id + " group:" + group + " posCount:" + posCount);
            return false;
        }

        m_lastContiGroup = group;
        m_lastContiCount = posCount;
        m_lastContiPos = pos;
        m_lastContiVel = vel;
        m_lastContiAcc = acc;
        m_lastContiDec = dec;

        return true;
    }

    public virtual bool contiStop()
    {
        if (m_simulation == true)
            return true;

        MC_STATUS ret = NMCSDKLib.MC_GroupHalt(m_id, m_lastContiGroup, m_lastContiDec, m_lastContiVel * 10, MC_BUFFER_MODE.mcAborting);

        if (ret != MC_STATUS.MC_OK)
        {
            Debug.debug("RSAMMCEBoard::contiStop failed. id:" + m_id);
            return false;
        }

        m_contiPause = true;

        return true;
    }

    public virtual bool contiResume()
    {
        if (m_contiPause == false)
            return false;

        m_contiPause = false;

        bool ret = contiLineMove(m_lastContiGroup, m_lastContiCount, m_lastContiPos, m_lastContiVel, m_lastContiAcc, m_lastContiDec);

        return ret;
    }
}
