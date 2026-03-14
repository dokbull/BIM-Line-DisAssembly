using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public abstract class BaseMotion
{
    public abstract void init();
    public abstract bool isConnected();

    public abstract void run();
}

public class MotionAxis
{
    protected string m_name;
    protected int m_no;

    protected double m_cmd;
    protected double m_pos;

    protected double m_curSpeed;
    protected double m_cmdSpeed;

    protected bool m_ready;
    protected bool m_busy;
    protected bool m_inpos;

    protected bool m_alarm;
    protected int m_alarmCode;

    protected bool m_negLimitSensor;
    protected bool m_posLimitSensor;
    protected bool m_orgSensor;
    protected bool m_zPhase;

    protected bool m_servoOn;
    protected bool m_emergency;

    protected bool m_orgComplete;

    protected double m_loadRatio;

    protected bool m_swLimit = false;
    protected double m_swLimitNeg = 0.0d;
    protected double m_swLimitPos = 0.0d;

    protected double m_lastAcc = 0.0d;
    protected double m_lastDec = 0.0d;
    protected double m_lastMaxSpeed = 0.0d;

    protected double m_lastCmd = 0.0d;

    protected double m_cmdFactor = 0.01d;

    protected string m_homeStatus = "";

    bool m_simulation = false;

    public MotionAxis()
    {
        m_name = "";
        m_no = -1;
        
        m_cmd = 0.0f;
        m_pos = 0.0f;

        m_ready = false;
        m_inpos = false;
        m_alarm = false;
        m_alarmCode = 0;

        m_negLimitSensor = false;
        m_posLimitSensor = false;
        m_orgSensor = false;

        m_servoOn = false;

        m_emergency = false;
        m_orgComplete = false;

        m_loadRatio = 0.0d;

        string path = Common.PATH + "\\simulation";
        if (File.Exists(path))
            m_simulation = true;
    }

    public string name() { return m_name; }
    public int no() { return m_no; }

    public double cmd() { return m_cmd; }
    public double pos() { return m_pos; }

    public string homeStatus() { return m_homeStatus; }

    public double curSpeed() { return m_curSpeed; }
    public double cmdSpeed() { return m_cmdSpeed; }

    public bool ready() { return m_ready; }
    public bool busy() { return m_busy; }
    
    public bool inposition() 
    {
        if (m_busy)
            return false;

        return m_inpos; 
    }

    public void setCmdFactor(double value)
    {
        m_cmdFactor = value;
    }

    public bool checkPos(double targetPos)
    {
        if (m_pos - m_cmdFactor > targetPos || m_pos + m_cmdFactor < targetPos)
            return false;

        return true;
    }

    public bool checkPosFactor(double targetPos, double factor)
    {
        if (m_pos - factor > targetPos || m_pos + factor < targetPos)
            return false;

        return true;
    }

    public double lastCmd()
    {
        return m_lastCmd;
    }

    public bool inpos(double targetPos)
    {
        if (m_simulation)
            return true;

        if (checkPos(m_lastCmd) == false)
            return false;

        return inposition();
    }

    public bool inpos(bool lastPosCheck = true)
    {
        if (m_simulation)
            return true;

        if (lastPosCheck)
        {
            if (checkPos(m_lastCmd) == false)
                return false;
        }

        return inposition();
    }

    public bool inposFactor(double targetPos, double factor)
    {
        if (checkPosFactor(m_lastCmd, factor) == false)
            return false;

        return inposition();
    }

    public void setRatio(double value)
    {
        m_loadRatio = value;
    }

    public bool alarm() { return m_alarm; }
    public int alarmCode() { return m_alarmCode; }

    public bool negLimit() { return m_negLimitSensor; }
    public bool posLimit() { return m_posLimitSensor; }
    public bool orgSensor() { return m_orgSensor; }
    public bool zPhase() { return m_zPhase; }

    public bool servoOn() { return m_servoOn; }
    public bool emergency() { return m_emergency; }

    public bool orgComplete() { return m_orgComplete; }

    public double loadRatio() { return m_loadRatio; }

    public double velocity() { return m_lastMaxSpeed; }
}