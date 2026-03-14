using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading;

public class AjinMotionAxis : BaseMotion
{
    protected int m_no = -1;
    protected string m_name = "EMPTY";

    bool[] m_output = new bool[5]; // Motion 출력 5개

    double m_lastDAcc = 0.0d;
    double m_lastAbsCmd = 0.0d;

    double m_virtualAct = 0.0d;

    public double cmd;

    double m_pos;
    double m_velocity = 0.0d;

    bool m_busy;
    bool m_inpos;

    bool m_alarm;
    int m_alarmCode;

    AXM_ACCEL_UNIT m_accel = AXM_ACCEL_UNIT.UNIT_SEC;

    AXM_SOFT_LIMIT m_swLimit = new AXM_SOFT_LIMIT();
    AXM_SOFT_LIMIT m_lastSwLimit = new AXM_SOFT_LIMIT();
    bool m_negLimit;
    bool m_posLimit;

    public bool org;
    public bool zPhase;

    bool m_servoOn;
    public bool emergency;

    bool m_orgComplete;

    double m_loadRatio;

    bool m_connect = false;
    bool m_simulation = false;

    Thread m_orgWaitThread = null;
    Thread m_simulationThread = null;
    bool m_stop = false;

    AXM_ELECTRIC_GEAR_RATIO m_electricGearRatio = new AXM_ELECTRIC_GEAR_RATIO();

    public class SETTING_SPEED
    {
        public double velocity;
        public double accel;
        public double decel;
    }

    SETTING_SPEED m_absSpeed = new SETTING_SPEED();

    public AjinMotionAxis(int axisNo, string axisName)
    {
        m_no = axisNo;
        m_name = axisName;

        string path = Common.PATH + "\\simulation";

        if (File.Exists(path))
            m_simulation = true;

        if (m_simulation)
        {
            m_simulationThread = new Thread(simulationThread);
            m_simulationThread.Start();
        }

        cmd = 0.0f;
        m_pos = 0.0f;

        m_inpos = false;

        m_alarm = false;
        m_alarmCode = 0;

        m_negLimit = false;
        m_posLimit = false;
        org = false;

        m_servoOn = false;

        emergency = false;
        m_orgComplete = false;

        m_loadRatio = 0.0d;
    }

    public string name() { return m_name; }
    public int no() { return m_no; }

    public override void init()
    {
    }

    public void close()
    {
        orgWaitStop();
    }

    public override bool isConnected()
    {
        return m_connect;
    }

    double m_moveFactor = 0.0d;

    public void simulationThread()
    {
        Debug.debug("AjinMotionAxis::simulationThread START");
        while (true)
        {
            if (m_stop)
            {
                Debug.debug("AjinMotionAxis::simulationThread STOP");
                break;
            }

            if (m_moveFactor != 0.0d)
            {
                m_pos += m_moveFactor;

                if (m_moveFactor > 0)
                {
                    if (m_pos > cmd)
                    {
                        m_pos = cmd;
                        m_moveFactor = 0.0d;
                    }
                }
                else
                {
                    if (m_pos < cmd)
                    {
                        m_pos = cmd;
                        m_moveFactor = 0.0d;
                    }
                }
            }

            Thread.Sleep(1000);
        }

        Debug.debug("AjinMotionAxis::simulationThread END");
    }

    public override void run()
    {
        cmd = getCmdPos();
        m_pos = getActPos();

        statusReadMotion(m_readMotion);
        m_busy = m_readMotion.busy;
        if (m_busy == true)
            m_inpos = false;
        else
            m_inpos = true;

        // AJIN 자체 inpos 이상함
        //m_inpos = getInpos();
        m_servoOn = readServoOn();
        m_loadRatio = statusReadServoLoadRatio();

        bool[] inputArr = readInput();

        if (inputArr == null)
            return;

        org = inputArr[(int)AXT_MOTION_UNIV_INPUT.UIO_INP0]; // 범용 원점
        zPhase = inputArr[(int)AXT_MOTION_UNIV_INPUT.UIO_INP1]; // 범용 원점
                                                                // 2 ~ 5 사용자 정의

        // 리미트 시그날
        signalReadLimit(ref m_negLimit, ref m_posLimit);
         
        // 서보 알람
        m_alarm = signalReadServoAlarm();
    }

    public void orgWaitStop()
    {
        m_stop = true;
    }

    void waitOrgThread()
    {
        Debug.debug("AjinMotionAxis::waitOrgThread START no:" + m_no);

        AXT_MOTION_HOME_RESULT result = AXT_MOTION_HOME_RESULT.HOME_RESERVED;

        while (true)
        {
            if (m_stop)
            {
                Debug.debug("AjinMotionAxis::waitOrgThread STOP no:" + m_no);
                break;
            }

            result = homeGetResult();

            if (result != AXT_MOTION_HOME_RESULT.HOME_RESERVED &&
                result != AXT_MOTION_HOME_RESULT.HOME_SEARCHING)
            {
                if (result == AXT_MOTION_HOME_RESULT.HOME_SUCCESS)
                {
                    m_orgComplete = true;
                    if (m_lastSwLimit.use == true)
                        restoreSoftLimit(); // SW 리미트 설정 재적용
                    break;
                }

                Debug.debug("AjinMotionAxis::waitOrgThread result:" + result);
            }

            Thread.Sleep(100);
        }

        Debug.debug("AjinMotionAxis::waitOrgThread END no:" + m_no);
    }

    public void setAbsSpeed(double vel, double acc, double dec)
    {
        m_absSpeed.velocity = vel;
        m_absSpeed.accel = acc;
        m_absSpeed.decel = dec;
    }

    public double pos() { return m_pos; }

    public double posRead()
    {
        m_pos = getActPos();
        return m_pos;
    }

    public bool busyRead()
    {
        statusReadMotion(m_readMotion);
        m_busy = m_readMotion.busy;

        return m_busy;
    }

    public bool inpos(bool lastPosCheck = true) 
    {
        if (lastPosCheck)
        {
            if (checkPos(m_lastAbsCmd) == false)
                return false;
        }

#if SPEED_MODE
        busyRead();
#endif
        if (m_servoOn)
        {
            if (m_busy)
                m_inpos = false;
            else
                m_inpos = true;
        }
        else
        {
            m_inpos = false;
        }

        return m_inpos; 
    }
    public bool busy() { return m_busy; }
    public double loadRatio() { return m_loadRatio; }
    
    public bool isServoOn() 
    {
        return m_servoOn; 
    }
    public double velocity() { return m_velocity; }

    public bool negLimit() { return m_negLimit; }
    public bool posLimit() { return m_posLimit; }

    public bool inpos(double targetPos)
    {
        bool ret = inpos();

        if (ret == false)
            return false;

        if (checkPos(targetPos) == false)
            return false;

        return true;
    }

    public bool checkPos(double targetPos)
    {
#if SPEED_MODE
        posRead();
#endif

        if (m_pos - 0.1d > targetPos || m_pos + 0.1d < targetPos)
            return false;

        return true;
    }

    //===
    public bool setPulseOut(AXT_MOTION_PULSE_OUTPUT type)
    {        uint ret = 0;
        ret = CAXM.AxmMotSetPulseOutMethod(m_no, (uint)type);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setPulse AxmMotSetPulseOutMethod failed. type:" + type);
            return false;
        }

        return true;
    }

    public bool setEncInput(AXT_MOTION_EXTERNAL_COUNTER_INPUT type)
    {
        uint ret = 0;
        ret = CAXM.AxmMotSetEncInputMethod(m_no, (uint)type);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setEncInput AxmMotSetEncInputMethod failed. type:" + type);
            return false;
        }

        return true;
    }

    public bool getUnitPerPulse(ref double unit, ref int pulse)
    {
        uint ret = CAXM.AxmMotGetMoveUnitPerPulse(m_no, ref unit, ref pulse);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setUnitPerPulse getUnitPerPulse failed. no:" + m_no);
            return false;
        }
        return true;
    }

    public bool setUnitPerPulse(double unit, int pulse)
    {
        if (m_simulation)
            return true;

        uint ret = 0;
        ret = CAXM.AxmMotSetMoveUnitPerPulse(m_no, unit, pulse);

        Debug.debug("############## m_no:" + m_no + " unit:" + unit + " pulse:" + pulse);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setUnitPerPulse AxmMotSetEncInputMethod failed. unit:" + unit + " pulse:" + pulse);
            return false;
        }

        return true;
    }

    public bool setMinVelocity(double velocity)
    {
        uint ret = 0;
        ret = CAXM.AxmMotSetMinVel(m_no, velocity);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setMinVelocity AxmMotSetMinVel failed. velocity:" + velocity);
            return false;
        }

        return true;
    }

    public bool setMaxVelocity(double velocity)
    {
        uint ret = 0;
        ret = CAXM.AxmMotSetMaxVel(m_no, velocity);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setMaxVelocity failed." +
                " no:" + m_no + " velocity:" + velocity);
            return false;
        }

        return true;
    }

    public bool setAbsRelMode(AXT_MOTION_ABSREL mode)
    {
        uint ret = 0;
        ret = CAXM.AxmMotSetAbsRelMode(m_no, (uint)mode);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setAbsRelMode AxmMotSetAbsRelMode failed. mode:" + mode);
            return false;
        }

        return true;
    }

    public bool setProfileMode(AXT_MOTION_PROFILE_MODE mode)
    {
        uint ret = 0;
        ret = CAXM.AxmMotSetProfileMode(m_no, (uint)mode);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setProfileMode AxmMotSetProfileMode failed. mode:" + mode);
            return false;
        }

        return true;
    }

    public bool setAccelUnit(AXM_ACCEL_UNIT unit)
    {
        if (m_simulation)
            return true;

        uint ret = CAXM.AxmMotSetAccelUnit(m_no, (uint)unit);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setAccelUnit AxmMotSetAccelUnit failed. unit:" + unit);
            return false;
        }

        m_accel = unit;

        return true;
    }

    public AXM_ACCEL_UNIT accelUnit()
    {
        return m_accel;
    }

    public bool setServoOnLevel(AXM_LEVEL level)
    {
        uint ret = 0;
        ret = CAXM.AxmSignalSetServoOnLevel(m_no, (uint)level);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setServoOnLevel failed. level:" + level);
            return false;
        }
        return true;
    }

    public bool getSoftLimit(AXM_SOFT_LIMIT value)
    {
        uint useVal = 0, stopVal = 0, selectionVal = 0;
        double positiveVal = 0, negativeVal = 0;

        uint ret = CAXM.AxmSignalGetSoftLimit(m_no, ref useVal, ref stopVal, ref selectionVal,
            ref positiveVal, ref negativeVal);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::getSoftLimit failed. m_no:" + m_no);
            return false;
        }

        value.use = (useVal == 1) ? true : false;
        value.stopMode = (AXT_MOTION_STOPMODE)stopVal;
        value.selection = (AXT_MOTION_SELECTION)selectionVal;
        value.positivePos = positiveVal;
        value.negativePos = negativeVal;

        return true;
    }

    public bool disableSoftLimit()
    {
        m_lastSwLimit.setValue(m_swLimit);

        m_swLimit.negativePos = -9999.0d;
        m_swLimit.positivePos = 9999.0d;

        return setSoftLimit(m_swLimit);
    }

    public bool restoreSoftLimit()
    {
        return setSoftLimit(m_lastSwLimit);
    }

    public bool releaseSoftLimit()
    {
        return setSoftLimit(new AXM_SOFT_LIMIT(false, 0.0d, 0.0d));
    }

    public bool setSoftLimit(AXM_SOFT_LIMIT setting)
    {
        uint useVal = 0;

        useVal = (uint)((setting.use == true) ? 1 : 0);

        uint ret = CAXM.AxmSignalSetSoftLimit(m_no,
            useVal, (uint)setting.stopMode, (uint)setting.selection,
            setting.positivePos, setting.negativePos);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setSoftLimit AxmSignalSetSoftLimit failed. m_no:" + m_no);
            return false;
        }

        m_swLimit.setValue(setting);

        return true;
    }

    public bool setSoftLimit(bool use, AXT_MOTION_STOPMODE stopMode, AXT_MOTION_SELECTION selection, double posValue, double negValue)
    {
        uint useVal = 0;

        useVal = (uint)((use == true) ? 1 : 0);

        uint ret = CAXM.AxmSignalSetSoftLimit(m_no, 
            useVal, (uint)stopMode, (uint)selection,
            posValue, negValue);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setSoftLimit AxmSignalSetSoftLimit failed. m_no:" + m_no);
            return false;
        }

        AXM_SOFT_LIMIT setLimit = new AXM_SOFT_LIMIT();
        setLimit.use = use;
        setLimit.stopMode = stopMode;
        setLimit.selection = selection;
        setLimit.positivePos = posValue;
        setLimit.negativePos = negValue;

        m_swLimit.setValue(setLimit);

        return true;
    }

    public bool getServoOnLevel()
    {
        uint value = 0;
        uint ret = 0;
        ret = CAXM.AxmSignalGetServoOnLevel(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::getServoOnLevel AxmSignalGetServoOnLevel failed. axisNo:" + m_no);
            return false;
        }

        if (value == 1)
            return true;

        return false;
    }

    public bool setInposLevel(AXM_LEVEL level)
    {
        uint ret = 0;
        ret = CAXM.AxmSignalSetInpos(m_no, (uint)level);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setInposLevel AxmSignalSetInpos failed. level:" + level);
            return false;
        }

        return true;
    }

    public bool setServoAlarmResetLevel(AXM_LEVEL level)
    {
        uint ret = CAXM.AxmSignalSetServoAlarmResetLevel(m_no, (uint)level);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setServoAlarmResetLevel AxmSignalSetServoAlarmResetLevel failed. level:" + level);
            return false;
        }

        return true;
    }

    public AXM_LEVEL getInposLevel()
    {
        uint ret = 0;
        uint value = 0;

        ret = CAXM.AxmSignalGetInpos(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::getInposLevel AxmSignalGetInpos failed. m_axisNo:" + m_no);
            return AXM_LEVEL.LOW;
        }

        return (AXM_LEVEL)value;
    }

    public bool getInpos()
    {
        uint value = 0;
        uint ret = 0;
        ret = CAXM.AxmSignalReadInpos(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::getInpos AxmSignalReadInpos failed. m_axisNo:" + m_no);
            return false;
        }

        bool boolValue = false;

        if (value == 1)
            boolValue = true;

        return boolValue;
    }

    public bool setServoAlarmLevel(AXM_LEVEL level)
    {
        uint ret = 0;
        ret = CAXM.AxmSignalSetServoAlarm(m_no, (uint)level);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setServoAlarmLevel AxmSignalSetServoAlarm failed. level:" + level);
            return false;
        }

        return true;
    }

    public bool signalReadServoAlarm()
    {
        uint value = 0;
        uint ret = 0;
        ret = CAXM.AxmSignalReadServoAlarm(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::signalReadServoAlarm AxmSignalReadServoAlarm failed. axisNo:" + m_no);
            return false;
        }

        if (value == 0x00)
            return false;

        return true;
    }

    public int signalReadServoAlarmCode()
    {
        ushort value = 0;
        uint ret = CAXM.AxmSignalReadServoAlarmCode(m_no, ref value);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::signalReadServoAlarmCode AxmSignalReadServoAlarmCode failed. axisNo:" + m_no);
            return 0xFFFF;
        }

        return value;
    }

    public bool setLimit(AXT_MOTION_STOPMODE stopMode, AXM_LEVEL positiveLevel, AXM_LEVEL negativeLevel)
    {
        uint ret = 0;
        ret = CAXM.AxmSignalSetLimit(m_no, (uint)stopMode, (uint)positiveLevel, (uint)negativeLevel);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setLimit AxmSignalSetLimit failed. stopMode:" + stopMode + 
                " positiveLevel:" + positiveLevel + " negativeLevel: " + negativeLevel);

            return false;
        }

        return true;
    }

    public bool setStop(AXT_MOTION_STOPMODE mode, AXM_LEVEL level)
    {
        uint ret = 0;
        ret = CAXM.AxmSignalSetStop(m_no, (uint)mode, (uint)level);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setStopSignal AxmSignalSetStop failed. mode:" + mode +
                " level:" + level);

            return false;
        }

        return true;
    }

    public bool setEncoderType(AXM_ENCODER_TYPE encType)
    {
        uint ret = CAXDev.AxmSignalSetEncoderType(m_no, (uint)encType);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setEncoderType AxmSignalSetEncoderType failed." +
                " no:" + m_no + " type:" + encType);
            return false;
        }
        return true;
    }

    public bool setZPhaseLevel(AXM_LEVEL level)
    {
        uint ret = CAXM.AxmSignalSetZphaseLevel(m_no, (uint)level);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setZPhaseLevel AxmSignalSetZphaseLevel failed." +
                " no:" + m_no + " level:" + level); 
            return false;
        }
        return true;
    }

    READ_MOTION m_readMotion = new READ_MOTION();

    double getCmdPos()
    {
        uint ret = 0;
        double value = 0.0d;
        ret = CAXM.AxmStatusGetCmdPos(m_no, ref value);

        if (checkFuncResult(ret, false) == false)
        {
            if ((AXT_FUNC_RESULT)ret != AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
                Debug.warning("AjinMotionAxis::getCmdPos AxmStatusGetCmdPos failed");

            return 0.0d;
        }

        return value;
    }

    double getActPos()
    {
        if (m_simulation)
        {
            return m_virtualAct;
        }

        uint ret = 0;
        double value = 0.0d;
        ret = CAXM.AxmStatusGetActPos(m_no, ref value);

        if (checkFuncResult(ret, false) == false)
        {
            if ((AXT_FUNC_RESULT)ret != AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
                Debug.warning("AjinMotionAxis::getActPos AxmStatusGetCmdPos failed");

            return 0.0d;
        }

        m_connect = true;

        return value;
    }

    public void relMove(double pos, double speed)
    {
        uint ret = 0;
        CAXM.AxmMotSetAbsRelMode(m_no, (uint)AXT_MOTION_ABSREL.POS_REL_MODE);

        m_lastDAcc = speed;

        ret = CAXM.AxmMovePos(m_no, pos, speed, speed, speed);
    }

    public bool stop()
    {
        if (m_simulation)
            return true;

        //Debug.debug("AjinMotionAxis::stop m_no:" + m_no);
        uint ret = 0;

        ret = CAXM.AxmMoveSStop(m_no);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::stop AxmMoveSStop failed");
            return true;
        }

        return false;
    }

    public double readVelocity()
    {
        uint ret = 0;
        double value = 0;

        ret = CAXM.AxmStatusReadVel(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::readVelocity AxmStatusReadVel failed");
            return 0.0d;
        }

        return value;
    }

    public bool homeSetMethod(AXT_MOTION_MOVE_DIR dir, AXT_MOTION_HOME_DETECT detectSensor, AXM_Z_PHASE phase, 
        int clearTime, double offset)
    {
        uint ret = 0;

        ret = CAXM.AxmHomeSetMethod(m_no, (int)dir, (uint)detectSensor, (uint)phase, clearTime, offset);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setHomeParameter AxmHomeSetMethod failed. axisNo:" + m_no +
                " dir:" + dir + " detectSensor:" + detectSensor + " phase:" + phase + 
                " waitTime:" + clearTime + " offset:" + offset);
            
            return false;
        }

        return true;
    }

    public class HOME_PARAM
    {
        public AXT_MOTION_MOVE_DIR dir;
        public AXT_MOTION_HOME_DETECT detectSensor;
        public AXM_Z_PHASE zPhase;
        public double homeClearTime;
        public double homeOffset;
    }

    public void homeGetMethod(HOME_PARAM param)
    {
        int dir = 0;
        uint sensor = 0;
        uint zPhase = 0;
        double clearTime = 0;
        double offset = 0;

        uint ret = CAXM.AxmHomeGetMethod(m_no, ref dir, ref sensor, ref zPhase, ref clearTime, ref offset);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeGetMethod AxmHomeGetMethod failed. axisNo:" + m_no);
            return;
        }

        param.dir = (AXT_MOTION_MOVE_DIR)dir;
        param.detectSensor = (AXT_MOTION_HOME_DETECT)sensor;
        param.zPhase = (AXM_Z_PHASE)zPhase;
        param.homeClearTime = clearTime;
        param.homeOffset = offset;
    }
    
    public void setServoOn(bool value)
    {
        servoOnOff(value);
    }

    public void servoOnOff(bool value)
    {
        stop();

        uint ret = 0;
        uint use = 0;

        if (value == true)
            use = (uint)AXM_USE.ENABLE;
        else
            use = (uint)AXM_USE.DISABLE;

        Debug.debug("AjinMotionAxis::servoOnOff axisNo:" + m_no + " value:" + value);

        ret = CAXM.AxmSignalServoOn(m_no, use);

        //m_orgComplete = false;

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::servoOnOff failed");
            return;
        }
    }

    bool output(int index)
    {
        return m_output[index];
    }

    void writeOutput(int index, bool value)
    {
        uint use = (value == true) ? (uint)1 : 0;

        uint ret = 0;
        ret = CAXM.AxmSignalWriteOutputBit(m_no, index, use);

        m_output[index] = value;

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setOutput AxmSignalWriteOutputBit failed");
            return;
        }

        m_connect = true;
    }

    public void writeOutput(AXM_OUT index, bool value)
    {
        writeOutput((int)index, value);
    }

    public void alarmClear()
    {
        writeOutput(AXM_OUT.ALARM_CLEAR, true);
        Util.waitTick(100);
        writeOutput(AXM_OUT.ALARM_CLEAR, false);
    }

    public bool readServoOn()
    {
        if (m_simulation)
            return true;

        return output(AXM_OUT.SERVO_ON);
    }

    public bool readAlarmClear()
    {
        return output(AXM_OUT.ALARM_CLEAR);
    }

    public bool output(AXM_OUT index)
    {
        return readOutput((int)index);
    }

    bool readOutput(int index)
    {
        uint value = 0;

        uint ret = 0;
        ret = CAXM.AxmSignalReadOutputBit(m_no, index, ref value);

        if (checkFuncResult(ret, false) == false)
        {
            if ((AXT_FUNC_RESULT)ret != AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
                Debug.warning("AjinMotionAxis::getOutput AxmSignalReadOutputBit failed. index:" + index);
            
            return false;
        }

        if (value == 0)
            return false;

        return true;
    }

    public bool[] readInput()
    {
        uint value = 0;
        uint ret = CAXM.AxmSignalReadInput(m_no, ref value);

        if (checkFuncResult(ret, false) == false)
        {
            if ((AXT_FUNC_RESULT)ret != AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
                Debug.warning("AjinMotionAxis::readInput AxmSignalReadInputBit failed. m_no:" + m_no);
            
            return null;
        }

        m_connect = true;

        bool[] bit = new bool[32];
        Util.wordToBit(value, ref bit);

        return bit;
    }

    public bool singalGetLimit(ref AXT_MOTION_STOPMODE mode, ref AXM_LEVEL negLevel, ref AXM_LEVEL posLevel)
    {
        uint modeVal = 0;
        uint negVal = 0;
        uint posVal = 0;

        uint ret = CAXM.AxmSignalGetLimit(m_no, ref modeVal, ref negVal, ref posVal);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::singalGetLimit AxmSignalGetLimit failed. m_no:" + m_no);
            return false;
        }

        mode = (AXT_MOTION_STOPMODE)modeVal;
        negLevel = (AXM_LEVEL)negVal;
        posLevel = (AXM_LEVEL)posVal;

        return true;
    }

    public bool signalReadLimit(ref bool neg, ref bool pos)
    {
        uint posVal = 0;
        uint negVal = 0;

        uint ret = 0;
        
        if (m_swLimit.use == true) // SW 리미트 사용시
            ret = CAXM.AxmSignalReadSoftLimit(m_no, ref posVal, ref negVal);
        else
            ret = CAXM.AxmSignalReadLimit(m_no, ref posVal, ref negVal);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::signalReadLimit AxmSignalReadLimit failed. m_no:" + m_no);
            return false;
        }

        neg = (negVal == 1) ? true : false;
        pos = (posVal == 1) ? true : false;

        return true;
    }

    /// <summary>
    /// 지정 축에 대한 모션 완료 확인
    /// </summary>
    /// <returns></returns>
    public bool statusReadInMotion()
    {
        uint value = 0;
        uint ret = 0;
        ret = CAXM.AxmStatusReadInMotion(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::statusReadInMotion AxmStatusReadInMotion failed. axisNo:" + m_no);
            return false;
        }

        if (value == 0)
            return false;

        return true;
    }
    /// <summary>
    /// 지정 한 축의 모션 구동 시작부터 종료시점까지의 이동량을 펄스 단위 값으로 반환한다.
    /// </summary>
    /// <returns></returns>
    public long statusReadDrivePulseCount()
    {
        int value = 0;
        uint ret = 0;

        ret = CAXM.AxmStatusReadDrivePulseCount(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::statusReadDrivePulseCount AxmStatusReadDrivePulseCount failed. axisNo:" + m_no);
            return 0;
        }

        return value;
    }

    public class READ_MOTION
    {
        public bool busy;
        public bool decel;
        public bool constRun;
        public bool accel;
    }

    /// <summary>
    /// 지정 축의 모션 구동 상태 값을 반환한다.
    /// </summary>
    /// <returns></returns>
    public bool statusReadMotion(READ_MOTION data)
    {
        if (m_simulation)
        {
            if (m_moveFactor == 0.0d)
                data.busy = false;
            else
                data.busy = true;

            return true;
        }

        uint value = 0;
        uint ret = 0;

        ret = CAXM.AxmStatusReadMotion(m_no, ref value);

        if (checkFuncResult(ret, false) == false)
        {
            if ((AXT_FUNC_RESULT)ret != AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
                Debug.warning("AjinMotionAxis::statusReadMotion AxmStatusReadMotion failed. axisNo:" + m_no);
            
            return false;
        }

        bool[] bit = new bool[32];
        Util.wordToBit(value, ref bit, 0);

        int cnt = 0;

        bool busy = bit[cnt++];
        bool decel = bit[cnt++];
        bool constRun = bit[cnt++];
        bool accel = bit[cnt++];

        data.busy = busy;
        data.decel = decel;
        data.constRun = constRun;
        data.accel = accel;

//FIXME@tmdwn.. 추후 모델에 따라 자동구분되는 코드를 삽입할 것
#if false
        // p.454 참조 (SMC-2V03)
        bool icl = bit[cnt++]; // 0x00000010
        bool icg = bit[cnt++];
        bool ecl = bit[cnt++];
        bool ecg = bit[cnt++];
        bool dir = bit[cnt++];
        bool cmd_busy = bit[cnt++];
        bool preset_driving = bit[cnt++];
        bool conti_driving = bit[cnt++];
        bool signal_search_driving = bit[cnt++];
        bool org_search_driving = bit[cnt++];
        bool mpg_driving = bit[cnt++];
        bool sensor_driving = bit[cnt++];
        bool l_c_interpolation = bit[cnt++];
        bool pattern_interpoloation = bit[cnt++];
        bool interrupt_bank1 = bit[cnt++];
        bool interrupt_bank2 = bit[cnt++];
#else
        // p.455 참조 (PCI-N804/404)
        bool conti_driving = bit[cnt++];
        bool cmd_driving = bit[cnt++];
        bool mpg_driving = bit[cnt++];
        bool org_search_driving = bit[cnt++];
        bool signal_search_driving = bit[cnt++];
        bool pattern_interpolation = bit[cnt++];
        bool slave_drive_driving = bit[cnt++];
        bool dir = bit[cnt++];
        bool wait_servo_inpos = bit[cnt++];
        bool line_interpolation_driving = bit[cnt++];
        bool circle_interpolation_driving = bit[cnt++];
        bool pulse_printing = bit[cnt++];
        // QIDRIVE_STATUS_16 부터 생략
#endif

        return true;
    }

#if false //TODO@tmdwn
    public void statusReadStop()
    {
        uint value = 0;
        uint ret = 0;
        ret = CAXM.AxmStatusReadStop(m_axisNo, ref value);
    }

    public void statusAxmStatusReadMechanical()
    {
        uint value = 0;
        uint ret = CAXM.AxmStatusReadMechanical(m_axisNo, ref value);
    }
#endif

    public double statusReadVel()
    {
        double value = 0;
        uint ret = CAXM.AxmStatusReadVel(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::statusReadVel AxmStatusReadVel failed. axisNo:" + m_no);
            return 0.0d;
        }

        return value;
    }

    public double statusReadPosError()
    {
        double value = 0;
        uint ret = CAXM.AxmStatusReadPosError(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::statusReadPosError AxmStatusReadPosError failed. axisNo:" + m_no);
            return 0.0d;
        }

        return value;
    }

    public double statusReadDriveDistance()
    {
        double value = 0;
        uint ret = CAXM.AxmStatusReadDriveDistance(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::statusReadDriveDistance AxmStatusReadDriveDistance failed. axisNo:" + m_no);
            return 0.0d;
        }

        return value;
    }

    public bool statusSetReadServoLoadRatio()
    {
        uint value = 2;
        // 지정 축의 부하율을 읽을 수 있도록 설정 합니다.(MLII : Sigma-5, SIIIH : MR_J4_xxB 전용)
        // (MLII, Sigma-5, dwSelMon : 0 ~ 2) ==
        //     [0] : Accumulated load ratio
        //     [1] : Regenerative load ratio
        //     [2] : Reference Torque load ratio
        // (SIIIH, MR_J4_xxB, dwSelMon : 0 ~ 4) ==
        //     [0] : Assumed load inertia ratio(0.1times)
        //     [1] : Regeneration load factor(%)
        //     [2] : Effective load factor(%)
        //     [3] : Peak load factor(%)
        //     [4] : Current feedback(0.1%)	

        uint ret = CAXM.AxmStatusSetReadServoLoadRatio(m_no, value);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::statusSetReadServoLoadRatio failed");
            return false;
        }
        return true;
    }

    public double statusReadServoLoadRatio()
    {
        double value = 0;
        uint ret = CAXM.AxmStatusReadServoLoadRatio(m_no, ref value);
        if (checkFuncResult(ret, false) == false)
        {
            if ((AXT_FUNC_RESULT)ret != AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
                Debug.warning("AjinMotionAxis::statusReadServoLoadRatio failed");

            return 0.0d;
        }
        return value;
        
    }

    public bool setActPos(double value)
    {
        uint ret = CAXM.AxmStatusSetActPos(m_no, value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setActPos AxmStatusSetActPos failed. axisNo:" + m_no +
                " value:" + value);
            return false;
        }
        return true;
    }

    public bool setCmdPos(double value)
    {
        uint ret = CAXM.AxmStatusSetCmdPos(m_no, value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setCmdPos AxmStatusSetCmdPos failed. axisNo:" + m_no +
                " value:" + value);
            return false;
        }
        return true;
    }

    //원점 관련 함수
    public void homeSetSignalLevel(AXM_LEVEL level)
    {
        uint ret = CAXM.AxmHomeSetSignalLevel(m_no, (uint)level);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeSetSignalLevel AxmHomeSetSignalLevel failed. axisNo:" + m_no);
        }
    }

    public AXM_LEVEL homeGetSignalLevel()
    {
        uint value = 0;
        uint ret = CAXM.AxmHomeGetSignalLevel(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeGetSignalLevel AxmHomeGetSignalLevel failed. axisNo:" + m_no);
            return AXM_LEVEL.LOW;
        }

        return (AXM_LEVEL)value;
    }

    public bool homeReadSignal()
    {
        uint value = 0;
        uint ret = CAXM.AxmHomeReadSignal(m_no, ref value);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeReadSignal AxmHomeReadSignal failed. axisNo:" + m_no);
            return false;
        }

        if (value == 0)
            return false;

        return true;
    }


    public class HOME_VEL_PARAM
    {
        public double vel1;
        public double vel2;
        public double vel3;
        public double vel4;
        public double accVel1;
        public double accVel2;
    }

    public bool homeSetVel(double vel1, double vel2, double vel3, double vel4, double accVel1, double accVel2)
    {
        if (m_simulation)
            return true;

        uint ret = CAXM.AxmHomeSetVel(m_no, vel1, vel2, vel3, vel4, accVel1, accVel2);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeSetVel AxmHomeSetVel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool homeGetVel(HOME_VEL_PARAM param)
    {
        uint ret = CAXM.AxmHomeGetVel(m_no, ref param.vel1, ref param.vel2, ref param.vel3, ref param.vel4,
            ref param.accVel1, ref param.accVel2);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeGetVel AxmHomeGetVel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 반드시 주의를 기울여서 값을 주어야 함 (강제 홈 완료)
    /// </summary>
    /// <returns></returns>
    public void forceHomeOn()
    {
        Debug.warning("AjinMotionAxis::forceHomeOn no:" + m_no);
        m_orgComplete = true;
    }

    public bool isHomed()
    {
        return m_orgComplete;
    }

    public bool homeMove(AXT_MOTION_MOVE_DIR direction, double offset = 0.0d)
    {
        Debug.debug("AjinMotionAxis::homeMove no:" + m_no);

        bool ret = true;

        AXT_MOTION_HOME_DETECT sensor = AXT_MOTION_HOME_DETECT.HomeSensor;
        AXM_Z_PHASE phase = AXM_Z_PHASE.DISABLE;
        int clearTime = 1000; // 원점 서치 완료 후 해당 ms 만큼 정지한 다음 원점 세팅

        ret &= homeSetMethod(direction, sensor, phase, clearTime, offset);

        if (ret == false)
        {
            Debug.debug("AjinMotionAxis::homeMove no:" + m_no + " fail homeSetMethod");
            return ret;
        }

        ret &= homeSetStart();

        return ret;
    }

    public bool homeSetStart()
    {
        Debug.debug("AjinMotionAxis::homeSetStart no:" + m_no);
        stop();

        m_orgComplete = false;

        Thread.Sleep(100);

        if (m_swLimit.use == true)
            disableSoftLimit(); // SW 리미트 임시 해제

        if (m_simulation)
        {
            m_orgComplete = true;
            return true;
        }

        uint ret = CAXM.AxmHomeSetStart(m_no);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeSetStart AxmHomeSetStart failed. axisNo:" + m_no);
            return false;
        }

        if (m_orgWaitThread != null)
        {
            m_orgWaitThread.Abort();
            m_orgWaitThread = null;
        }
        m_stop = false;
        m_orgWaitThread = new Thread(waitOrgThread);
        m_orgWaitThread.Start();

        //if (m_lastSwLimit.use == true)
        //    restoreSoftLimit(); // SW 리미트 설정 재적용

        return true;
    }

    //AxmHomeSetResult - 쓰지않음. 특수한 경우 쓰게 될때 구현 할 것

    public bool orgComplete()
    {
#if false
        // 원점 완료 상태인지 체크
        AXT_MOTION_HOME_RESULT result = homeGetResult();

        if (result == AXT_MOTION_HOME_RESULT.HOME_SUCCESS)
            return true;
#endif

        if (m_orgComplete == true)
            return true;

        return false;
    }

    public AXT_MOTION_HOME_RESULT homeGetResult()
    {
        uint value = 0;
        uint ret = CAXM.AxmHomeGetResult(m_no, ref value);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::homeGetResult AxmHomeGetResult failed. axisNo:" + m_no);
            return AXT_MOTION_HOME_RESULT.HOME_ERR_UNKNOWN;
        }

        return (AXT_MOTION_HOME_RESULT)value;
    }

    public virtual bool absMove(double pos)
    {
        Debug.debug("AjinMotionAxis::absMove no:" + m_no + " name:" + m_name + " pos:" + pos);
        return absMove(pos, m_absSpeed.velocity, m_absSpeed.accel, m_absSpeed.decel);
    }

    public bool absMove(double pos, double vel, double acc, double dece)
    {
        if (m_simulation)
        {
            cmd = pos;
            m_moveFactor = (pos - m_pos) / 5.0d;
            return true;
        }

        double setAcc = acc;
        double setDec = dece;

        setAbsRelMode(AXT_MOTION_ABSREL.POS_ABS_MODE);
        m_lastDAcc = acc;
        m_lastAbsCmd = pos;
        return moveStartPos(pos, vel, setAcc, setDec);
    }

    public bool resume()
    {
        if (checkPos(m_lastAbsCmd))
        {
            return true;
        }

        return absMove(m_lastAbsCmd, m_absSpeed.velocity, m_absSpeed.accel, m_absSpeed.decel);
    }

    bool moveStartPos(double pos, double vel, double acc, double dece)
    {
        uint ret = CAXM.AxmMoveStartPos(m_no, pos, vel, acc, dece);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::moveStartPos AxmMoveStartPos failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool moveVel(double vel, double acc, double dece)
    {
        if (m_simulation)
        {
            m_pos += vel;
            return false;
        }

        Debug.debug("AjinMotionAxis::moveVel axisNo:" + m_no + " name:" + m_name +
            " vel:" + vel + " acc:" + acc + " dece:" + dece);

        m_lastDAcc = dece;

        uint ret = CAXM.AxmMoveVel(m_no, vel, acc, dece);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::moveVel AxmMoveVel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    //AxmMoveStartMultiVel
    //AxmMoveSignalSearch
    //AxmMoveSignalCapture
    //AxmMoveGetCapturePos
    //AxmMoveStartMultiPos
    //AxmMoveMultiPos

    public bool emgStop()
    {
        uint ret = CAXM.AxmMoveEStop(m_no);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::emgStop AxmMoveEStop failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

#if false
    public bool sStop()
    {
        if (m_simulation)
            return true;

        uint ret = CAXM.AxmMoveSStop(m_no);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::sStop AxmMoveSStop failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }
#endif

    public bool overridePos(double pos)
    {
        uint ret = CAXM.AxmOverridePos(m_no, pos);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::overridePos AxmOverridePos failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool overrideSetMaxVel(double vel)
    {
        uint ret = CAXM.AxmOverrideSetMaxVel(m_no, vel);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::overrideSetMaxVel AxmOverrideSetMaxVel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool overrideVel(double vel)
    {
        uint ret = CAXM.AxmOverrideVel(m_no, vel);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::overrideVel AxmOverrideVel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool overrideAccVelDece(double vel, double acc, double dece)
    {
        uint ret = CAXM.AxmOverrideAccelVelDecel(m_no, vel, acc, dece);
        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::overrideAccVelDece AxmOverrideAccelVelDecel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    public bool overrideVelAtPos(double pos, double vel, double acc, double dece,
        double overridePos, double overrideVel, AXT_MOTION_SELECTION target)
    {
        int targetVal = (int)target;

        uint ret = CAXM.AxmOverrideVelAtPos(m_no, pos, vel, acc, dece,
            overridePos, overrideVel, targetVal);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::overrideAccVelDece AxmOverrideAccelVelDecel failed. axisNo:" + m_no);
            return false;
        }
        return true;
    }

    // MASTER, SLAVE 구동 함수

    public bool linkGetMode(AXM_GEAR_RATIO ratio)
    {
        uint ret = CAXM.AxmLinkGetMode(m_no, ref ratio.masterValue, ref ratio.slaveValue);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::linkGetMode AxmLinkGetMode failed. axisNo:" + m_no);
            return false;
        }

        return true;
    }
   

    // Trigger 함수 p.679

    // Gantry 구동함수는 AjinMotion 쪽으로 구현

    // 전자기어비 관련
    public bool setElectricGearRatio(int numerator, int denominator)
    {
        uint ret = CAXM.AxmMotSetElectricGearRatio(m_no, numerator, denominator);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::setElectricGearRatio AxmMotSetElectricGearRatio failed. axisNo:" + m_no +
                " numerator:" + numerator +
                " denominator:" + denominator);

            return false;
        }

        Debug.debug("AjinMotionAxis::setElectricGearRatio numerator:" + numerator +
            " denominator:" + denominator);

        m_electricGearRatio.numerator = numerator;
        m_electricGearRatio.denominator = denominator;

        return true;
    }

    public bool getElectricGearRatio(AXM_ELECTRIC_GEAR_RATIO gearRatio = null)
    {
        if (gearRatio == null)
            gearRatio = m_electricGearRatio;

        uint ret = CAXM.AxmMotGetElectricGearRatio(m_no,
            ref gearRatio.numerator, ref gearRatio.denominator);

        if (checkFuncResult(ret) == false)
        {
            Debug.warning("AjinMotionAxis::getElectricGearRatio AxmMotGetElectricGearRatio failed. " +
                " axisNo:" + m_no);

            return false;
        }

        Debug.debug("AjinMotionAxis::getElectricGearRatio axisNo:" + m_no +
            " numerator:" + gearRatio.numerator +
            " denominator:" + gearRatio.denominator);

        return true;
    }

    public AXM_ELECTRIC_GEAR_RATIO gearRatio()
    {
        return m_electricGearRatio;
    }

    public bool isAlarm()
    {
        return m_alarm;
    }

    public int alarmCode()
    {
        return m_alarmCode;
    }

    bool checkFuncResult(uint result, bool networkErrorLoging = true)
    {
        AXT_FUNC_RESULT ret = (AXT_FUNC_RESULT)result;

        if (ret == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            return true;

        if (networkErrorLoging)
        {
            Debug.warning("AjinMotionAxis::checkResult failed to func. reason:" + ret);
        }

        if (ret == AXT_FUNC_RESULT.AXT_RT_NETWORK_ERROR)
            m_connect = false;

        return false;
    }
}