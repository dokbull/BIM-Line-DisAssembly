using System;
using System.Collections.Generic;

public class RSAMMCEAxis
{
    RSAMMCEBoard m_board = null;
    int m_addrId = -1;
    int m_no = -1;
    string m_axisName = "";
    bool m_useSoftLimit = false;

    bool m_useJerkFactor = false;

    public enum AXIS_INFO
    {
        errorStop = 0,
        disabled,
        stopping,
        standStill,
        discreteMotion,
        contiMotion,
        syncMotion,
        homing,
        swLimitPositive,
        swLimitNegative,
        constantVelocity,
        accelerating,
        deceleration,
        dirPositive,
        dirNegative,
        limitNegative,
        limitPositive, //16
        homeSensor,
        HWLimitSwitchPosEvent,
        HWLimitSwitchNegEvent,
        driveFault,
        sensorStop,
        readyForPowerOn,
        powerOn,
        isHomed,
        axisWarning,
        motionComplete,
        gearing,
        groupMotion,
        bufferFull,
        reserved30,
        reserved31,
        MAX,
    }

    Dictionary<AXIS_INFO, bool> m_infoMap = new Dictionary<AXIS_INFO, bool>();

    double m_lastSWLimitPos = 1000000.0d;
    double m_lastSWLimitNeg = -1000000.0d;

    double m_pos = 0.0d;

    double m_lastMovePos = -1.0d;
    double m_factor = 0.05;
    public bool m_isPause = false;

    bool m_useSecAccMode = false; // use setting sec to accelation

    CElaspedTimer m_timeoutTimer = new CElaspedTimer(100);

    public RSAMMCEAxis(RSAMMCEBoard board, int no, int addrId, string axisName)
    {
        m_board = board;
        m_no = no;
        m_addrId = addrId;
        m_axisName = axisName;

        for (int i=0; i<(int)AXIS_INFO.MAX; i++)
        {
            AXIS_INFO axisInfo = (AXIS_INFO)i;
            m_infoMap[axisInfo] = false;
        }
    }

    public virtual void run()
    {
        uint axisStatus = 0;

        bool ret = m_board.readAxisStatus((ushort)m_addrId, ref axisStatus);

        uint status = 0;
        for (int i = 0; i < 32; i++)
        {
            status = axisStatus & (uint)(0x01 << i);
            bool value = (status != 0) ? true : false;

            AXIS_INFO axisInfo = (AXIS_INFO)i;
            m_infoMap[axisInfo] = (status != 0) ? true : false;
        }

        m_pos = m_board.readActualPosition((ushort)m_addrId);
    }

    public void setTimeout(int ms)
    {
        m_timeoutTimer.setTime(ms);
    }

    public bool setHomeParam(AXIS_DIR dir, double vel, double accDec, double creepVel, 
        double offset = 0.0f, int homeSensorType = 1)
    {
        double inPosWindows = 0.01d;
        double inVelWindow = 99999999d;

        double acc = accDec;
        double dec = accDec;

        if (m_useSecAccMode)
        {
            acc = calcSecToAcc(vel, accDec);
            dec = calcSecToAcc(vel, accDec);
        }

        return  m_board.setHomeParam((ushort)m_addrId, dir, vel,
            acc, dec, creepVel, inPosWindows, inVelWindow, 
            offset, (uint)homeSensorType);
    }

    public bool setUnitScaleFactor(double factor)
    {
        return m_board.setUnitScaleFactor((ushort)m_addrId, factor);
    }

    public bool isAlarm()
    {
        bool errorStop = m_infoMap[AXIS_INFO.errorStop];
        bool axisWarning = m_infoMap[AXIS_INFO.axisWarning];
        bool driveFault = m_infoMap[AXIS_INFO.driveFault];

        //bool ret = errorStop | axisWarning | driveFault;
        bool ret = errorStop | driveFault;

        return ret;
    }

    public bool isStopped()
    {
        bool motionComplete = m_infoMap[AXIS_INFO.motionComplete];
        bool standStill = m_infoMap[AXIS_INFO.standStill];

        bool ret = motionComplete & standStill;
        return ret;
    }

    public bool isDriveFault()
    {
        return m_infoMap[AXIS_INFO.driveFault];
    }

    public bool isReadyForPowerOn()
    {
        return m_infoMap[AXIS_INFO.readyForPowerOn];
    }

    public double pos()
    {
        return m_pos;
    }

    public double lastMovePos()
    {
        return m_lastMovePos;
    }

    public string getAlarmCode()
    {
        return m_board.alarmCode((ushort)m_addrId);
    }

    public bool alarmClear()
    {
        if (isDriveFault())
        {
            m_board.setServoOnOff(m_addrId, false);
            m_timeoutTimer.start();

            while (true)
            {
                run();

                if (isServoOn() == false)
                    break;

                if (m_timeoutTimer.isElasped() == true)
                    return false;

                Util.waitTick(30);
            }

            m_board.alarmClear(m_addrId);

            while (true)
            {
                run();

                if (isReadyForPowerOn() == true)
                    break;

                if (m_timeoutTimer.isElasped() == true)
                    return false;

                Util.waitTick(30);
            }

            m_board.setServoOnOff(m_addrId, true);

            while (true)
            {
                run();

                if (isServoOn() == true)
                    break;

                if (m_timeoutTimer.isElasped() == true)
                    return false;

                Util.waitTick(30);
            }


        }

        return m_board.alarmClear((ushort)m_addrId);
    }

    public void refreshPosition()
    {
        m_lastMovePos = m_pos;
    }
    public bool isMove()
    {
        return !m_infoMap[AXIS_INFO.standStill];
    }

    public bool inpos()
    {
        if (checkPos(m_lastMovePos) == false)
            return false;

        bool motionComplete = m_infoMap[AXIS_INFO.motionComplete];
        bool standStill = m_infoMap[AXIS_INFO.standStill];

        bool ret = motionComplete & standStill;

        return ret;
    }

    public bool inpos(double pos)
    {
        if (checkPos(pos))
            return inpos();

        return false;
    }

    public bool minusLimit()
    {
        return negLimit();
    }

    public bool plusLimit()
    {
        return posLimit();
    }

    public virtual bool stop()
    {
        double dec2 = m_dec;

        if (m_useSecAccMode)
        {
            dec2 = calcSecToAcc(m_vel, m_dec);
        }

        return m_board.stop((ushort)m_addrId, dec2);
    }

    public bool emoStop(bool isStop)
    {
        return m_board.emoStop((ushort)m_addrId, isStop);
    }

    public void setFactor(double factor)
    {
        m_factor = factor;
    }

    public bool checkPos(double pos)
    {
        double factor = m_factor;

        if (m_pos > pos - factor && m_pos < pos + factor)
            return true;

        return false;
    }
    
    public bool checkPos(double pos, double factor)
    {
        if (m_pos > pos - factor && m_pos < pos + factor)
            return true;

        return false;
    }

    public bool setSoftLimit(bool use, double posValue, double negValue)
    {
        bool ret = m_board.setSoftwareLimit((ushort)m_addrId, use, posValue, negValue);

        if (ret == true)
        {
            if (use == true)
                m_useSoftLimit = true;
            else
                m_useSoftLimit = false;
        }

        return ret;
    }

    public bool disableSoftLimit()
    {
        bool ret = m_board.getSoftwareLimit((ushort)m_addrId, ref m_lastSWLimitPos, ref m_lastSWLimitNeg);

        if (ret == true)
            m_useSoftLimit = false;

        return m_board.setSoftwareLimit((ushort)m_addrId, false, m_lastSWLimitPos, m_lastSWLimitNeg);
    }

    public bool restoreSoftLimit()
    {
        bool ret = m_board.setSoftwareLimit((ushort)m_addrId, true, m_lastSWLimitPos, m_lastSWLimitNeg);

        if (ret == true)
            m_useSoftLimit = true;

        return ret;
    }

    public bool isServoOn()
    {
        bool powerOn = m_infoMap[AXIS_INFO.powerOn];

        return powerOn;
    }

    public bool setServoOn(bool value)
    {
        return m_board.setServoOnOff((ushort)m_addrId, value);
    }

    public bool orgComplete()
    {
        bool isHomed = m_infoMap[AXIS_INFO.isHomed];
        bool isHoming = m_infoMap[AXIS_INFO.homing];

        if (isHomed == true && isHoming == false)
            return true;

        return false;
    }

    public bool isHomed()
    {
        return m_infoMap[AXIS_INFO.isHomed];
    }

    public bool posLimit()
    {
        bool ret = m_infoMap[AXIS_INFO.limitPositive];
        
        if (m_useSoftLimit && ret == false) // not detected hardware limit
            ret = m_infoMap[AXIS_INFO.swLimitPositive];

        return ret;
    }

    public bool negLimit()
    {
        bool ret = m_infoMap[AXIS_INFO.limitNegative];

        if (m_useSoftLimit && ret == false) // not detected hardware limit
            ret = m_infoMap[AXIS_INFO.swLimitNegative];

        return ret;
    }

    public bool moveVel(double vel, double acc, double dec)
    {
        Debug.debug("RSAMMCEAxis::moveVel id:" + m_addrId);

        double acc2 = acc;
        double dec2 = dec;

        if (m_useSecAccMode)
        {
            acc2 = calcSecToAcc(vel, acc);
            dec2 = calcSecToAcc(vel, dec);
        }
        return m_board.velMove((ushort)m_addrId, vel, acc2, dec2);
    }


    public void resetLastPos(double pos)
    {
        m_lastMovePos = pos;
    }

    public bool relMove(double dist)
    {
        double acc = m_acc;
        double dec = m_dec;

        if (m_useSecAccMode)
        {
            acc = calcSecToAcc(m_vel, m_acc);
            dec = calcSecToAcc(m_vel, m_dec);
        }

        bool ret = true;

        if (m_useJerkFactor == true)
            ret = m_board.relMove((ushort)m_addrId, dist, m_vel, acc, dec, m_jerkFactor);
        else
            ret = m_board.relMove((ushort)m_addrId, dist, m_vel, acc, dec);

        return ret;
    }

    public virtual bool absMove(double pos)
    {
#if false
        Debug.debug("RSAMMCEAxis::absMove id:" + m_addrId + " name:" + m_axisName +
            " pos:" + pos.ToString("0.00"));
#endif
        double acc = m_acc;
        double dec = m_dec;

        if (m_useSecAccMode)
        {
            acc = calcSecToAcc(m_vel, m_acc);
            dec = calcSecToAcc(m_vel, m_dec);
        }

        bool ret = true;

        if (m_useJerkFactor == true)
            ret = m_board.absMove((ushort)m_addrId, pos, m_vel, acc, dec, m_jerkFactor);
        else
            ret = m_board.absMove((ushort)m_addrId, pos, m_vel, acc, dec);

        if (ret == true)
        {
            m_lastMovePos = pos;
        }
        m_isPause = false;

        return ret;
    }

    public virtual bool absMove(double pos, double vel, double acc)
    {
#if false
        Debug.debug("RSAMMCEAxis::absMove id:" + m_addrId + " name:" + m_axisName +
            " pos:" + pos.ToString("0.00"));
#endif
        double acc2 = acc;
        double dec2 = acc;

        if (m_useSecAccMode)
        {
            acc2 = calcSecToAcc(vel, acc);
            dec2 = calcSecToAcc(vel, acc);
        }

        bool ret = true;

        if (m_useJerkFactor == true)
            ret = m_board.absMove((ushort)m_addrId, pos, vel, acc2, dec2, m_jerkFactor);
        else
            ret = m_board.absMove((ushort)m_addrId, pos, vel, acc2, dec2);

        if (ret == true)
        {
            m_lastMovePos = pos;
        }
        m_isPause = false;

        return ret;
    }
    
    public virtual bool homeMove(double pos = 0)
    {
        Debug.debug("RSAMMCEAxis::homeMove id:" + m_addrId);
        resetLastPos(pos);
        return m_board.homeMove((ushort)m_addrId, pos);
    }

    public bool orgWaitStop()
    {
        return false;
    }

    double m_vel = 0.0d;
    double m_acc = 0.0d;
    double m_dec = 0.0d;
    double m_jerkFactor = 1.0d;

    public void setAbsSpeed(double vel, double acc, double dec)
    {
        m_vel = vel;
        m_acc = acc;
        m_dec = dec;
    }


    public bool setJerkFactor(double jerk)
    {
        double lowLimit = 1.0d;

        if (jerk < lowLimit)
            jerk = lowLimit;

        m_jerkFactor = jerk;
        m_useJerkFactor = true;

        return true;
    }


    public bool servoOnOff()
    {
        return false;
    }

    public bool setCmdPos(double pos)
    {
        return m_board.setCmdPosition((ushort)m_addrId, pos);
    }

    public bool pause()
    {
        if (m_isPause == true)
            return false;

        stop();
        m_isPause = true;

        return true;
    }

    public bool resume()
    {
        if (m_isPause == false)
            return false;

        m_isPause = false;

        if (checkPos(m_lastMovePos))
            return true;

        absMove(m_lastMovePos);
        return true;
    }


    public void setUseSecAccMode(bool value)
    {
        m_useSecAccMode = value;
    }

    public double calcSecToAcc(double vel, double sec)
    {
        double acc = (Math.Abs(vel) / sec);

        return acc;
    }

    public bool isCalcSecToAcc()
    {
        return m_useSecAccMode;
    }

    public string name() { return m_axisName; }
    public int no() { return m_no; }
}
