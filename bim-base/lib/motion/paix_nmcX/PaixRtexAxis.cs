using System.Collections.Generic;

public class PaixRtexAxis
{
    PaixRtexMotionAxis m_motion = null;

    int m_slaveNo = -1;
    int m_arrId = -1;

    string m_axisName = "";

    double m_lastSWLimitPos = 1000000.0d;
    double m_lastSWLimitNeg = -1000000.0d;

    double m_lastMovePos = -1.0d;
    bool m_isPause = false;

    int m_speedProfile = 0; // 0: 사다리, 1: S-Curve

    PAIX_DIR m_homeDir = PAIX_DIR.CCW;

    public PaixRtexAxis(PaixRtexLib lib, int no, int arrId, string axisName, int speedProfile)
    {
        m_motion = new PaixRtexMotionAxis(lib, no, axisName, arrId);

        m_slaveNo = no;
        m_arrId = arrId;
        m_axisName = axisName;
        m_speedProfile = speedProfile;
    }

    public PaixRtexMotionAxis motion()
    {
        return m_motion;
    }

    public virtual void run()
    {
        if (m_motion != null)   
           m_motion.update();
    }

    public bool isAlarm()
    {
        bool errorStop = m_motion.emergency();
        bool alarm = m_motion.alarm();

        bool ret = errorStop | alarm;

        return ret;
    }

    public bool isStopped()
    {
        bool motionComplete = !m_motion.busy();
        bool ready = m_motion.ready();

        bool ret = motionComplete & ready;
        return ret;
    }

    public bool isReadyForPowerOn()
    {
        bool servo = m_motion.servoOn();
        bool ready = m_motion.ready();

        bool ret = servo & ready;
        return ret;
    }

    public double pos()
    {
        return m_motion.pos();
    }

    public int getAlarmCode()
    {
        return m_motion.alarmCode();
    }

    public bool alarmClear()
    {
        return m_motion.setAlarmResetReq();
    }

    public void refreshPosition()
    {
        m_lastMovePos = m_motion.pos();
    }

    public bool isMove()
    {
        return m_motion.busy();
    }

    public bool inpos()
    {
        if (checkPos(m_lastMovePos) == false)
            return false;

        bool ret = isStopped();

        return ret;
    }

    public bool inpos(double pos)
    {
        if (checkPos(pos) == false)
            return false;

        bool ret = isStopped();

        return ret;
    }

    public bool stop()
    {
        return m_motion.decStop();
    }

    public bool emoStop(bool isStop)
    {
        return m_motion.suddenStop();
    }

    public bool checkPos(double pos, double factor = 0.05d)
    {
        double curPos = m_motion.pos();

        if (curPos > pos - factor && curPos < pos + factor)
            return true;

        return false;
    }

    public bool setSoftLimit(bool use, double posValue, double negValue)
    {
        return m_motion.setSoftLimit(use, negValue, posValue);
    }

    public bool resetOriginComplete()
    {
        return m_motion.resetOriginComplete();
    }

    public void disableSoftLimit()
    {
        m_motion.releaseSoftLimit();
    }

    public void restoreSoftLimit()
    {
        m_motion.restoreSoftLimit();
    }

    public bool isServoOn()
    {
        return m_motion.servoOn();
    }

    public bool setServoOn(bool value)
    {
        return m_motion.setServoOn(value);
    }

    public void setUnitPerPulse(double pulse)
    {
        m_motion.setUnitPerPulse(pulse);
    }

    public void setAccType(PAIX_ACCEL_UNIT unit)
    {
        m_motion.setAccelUnit(unit);
    }

    public bool orgComplete()
    {
        return m_motion.orgComplete();
    }

    public bool posLimit()
    {
        return m_motion.posLimit();
    }

    public bool negLimit()
    {
        return m_motion.negLimit();
    }

    public bool moveVel(PAIX_DIR dir, double vel, double acc, double dec)
    {
        Debug.debug("PaixRtexAxis::moveVel id:" + m_slaveNo + " vel:" + vel + " acc:" + acc);
        m_motion.setSpeed(0, acc, dec, vel);

        return m_motion.velMove(dir);
    }

    public void resetLastPos(double pos)
    {
        m_lastMovePos = pos;
    }

    public bool setCmdEncPos(double value)
    {
        resetLastPos(value);

        return m_motion.setCmdEncPos(value);
    }

    public virtual bool absMove(double pos)
    {
        bool ret = m_motion.absMove(pos, m_speedProfile);

        if (ret == true)
        {
            m_lastMovePos = pos;
        }

        return ret;
    }

    public virtual bool overrideAbsMove(double pos)
    {
        bool ret = m_motion.overrideAbsMove(pos);

        if (ret == true)
        {
            m_lastMovePos = pos;
        }

        return ret;
    }

    public virtual bool velMove(PAIX_DIR dir)
    {
        bool ret = m_motion.velMove(dir);

        return ret;
    }

    public bool setSpeed(double startVel, double acc, double dec, double driveVel)
    {
        bool ret = m_motion.setSpeed(startVel, acc, dec, driveVel);

        return ret;
    }

    public void setHomeDir(PAIX_DIR dir)
    {
        m_homeDir = dir;
    }

    public bool setHomeSpeed(double vel1, double vel2, double vel3, short accRatio)
    {
        return m_motion.setHomeSpeed(vel1, vel2, vel3, accRatio);
    }

    public bool homeMove(double offset = 0)
    {
        Debug.debug("PaixRtexAxis::homeMove id:" + m_slaveNo);
        resetLastPos(offset);

        return m_motion.homeMoveHome(m_homeDir, offset);
    }

    public void setAbsSpeed(double vel, double acc, double dec)
    {
        m_motion.setAbsSpeed(vel, acc, dec);
    }

    /// <summary>
    /// CV 제어 모드 전용 구동 함수
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="vel"></param>
    /// <param name="acc"></param>
    /// <param name="dec"></param>
    /// <returns></returns>
    public bool cvVelMove(PAIX_DIR dir, double vel, double acc, double dec)
    {
        return m_motion.cvVelMove(dir, vel, acc, dec);
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

    public string name() { return m_axisName; }
    public int slaveNo() { return m_slaveNo; }
    public int arrId() { return m_arrId; }
}
