using bim_base;
using System;
using static bim_base.CSTATION;

class Conf
{
    static CSettings m_setting = null;

    static string m_db_ip = "127.0.0.1";
    static string m_db_scheme = "knk-se-base";

    static int m_currModelIdx = 0;

    static int MAX_AXIS = 255;

    static double[] m_vel = new double[MAX_AXIS];
    static double[] m_acc = new double[MAX_AXIS];
    static double[] m_dec = new double[MAX_AXIS];

    static double[] m_jogLow = new double[MAX_AXIS];
    static double[] m_jogMid = new double[MAX_AXIS];
    static double[] m_jogHigh = new double[MAX_AXIS];

    static double[] m_negLimit = new double[MAX_AXIS];
    static double[] m_posLimit = new double[MAX_AXIS];

    static int[] m_delayTime = new int[512];

    static bool m_productInfoVisible = true;

    // ADMIN
    static string m_password = "1234";


    // STATION
    static CSTATION.TYPE[] m_lastStationType = new CSTATION.TYPE[(int)STATION.MAX];
    static string[] m_lastStationBcr = new string[(int)STATION.MAX];
    static bool[] m_lastStationPass = new bool[(int)STATION.MAX];


    public static void load()
    {
        string path = Common.PATH + "\\info\\options.dat";
        m_setting = new CSettings(path);

        m_db_ip = m_setting.getValue("DB", "IP", "127.0.0.1");
        m_db_scheme = m_setting.getValue("DB", "SCHEME", "knk-se-base");

        m_currModelIdx = m_setting.getValue("MODELS", "CURR_MODEL_IDX", 0);

        m_productInfoVisible = m_setting.getValue("SETTING", "PRODUCT_INFO_VISIBLE", true);
        
        // ADMIN
        m_password = m_setting.getValue("SYSTEM", "PASSWORD", "1234");

        // STATION
        for (int i = 0; i < (int)STATION.MAX; i++)
        {
            m_lastStationBcr[i] = m_setting.getValue("STATION", "LAST_STATION_BCR" + (i + 1), "");
            m_lastStationPass[i] = m_setting.getValue("STATION", "LAST_STATION_PASS" + (i + 1), true);

            string type = m_setting.getValue("STATION", "LAST_STATION_TYPE" + (i + 1), "EMPTY");
            m_lastStationType[i] = (CSTATION.TYPE)Enum.Parse(typeof(CSTATION.TYPE), type);
        }

        loadMotorVariable();
        loadDelayVariable();
    }

    private static void loadMotorVariable()
    {
        for (int i = 0; i < MAX_AXIS; i++)
        {
            string text = i.ToString();

            m_vel[i] = m_setting.getValue("MOTOR_" + text, "VEL", 50.0d);
            m_acc[i] = m_setting.getValue("MOTOR_" + text, "ACC", 0.25d);
            m_dec[i] = m_setting.getValue("MOTOR_" + text, "DEC", 0.25d);

            m_jogLow[i] = m_setting.getValue("MOTOR_" + text, "JOG_LOW" + i, 5.0d);
            m_jogMid[i] = m_setting.getValue("MOTOR_" + text, "JOG_MID" + i, 10.0d);
            m_jogHigh[i] = m_setting.getValue("MOTOR_" + text, "JOG_HIGH" + i, 20.0d);

            m_negLimit[i] = m_setting.getValue("MOTOR_" + text, "LIMIT_NEG", -9999.99d);
            m_posLimit[i] = m_setting.getValue("MOTOR_" + text, "LIMIT_POS", +9999.99d);
        }
    }

    private static void loadDelayVariable()
    {
        // DELAY
        foreach (DELAY delayValue in Enum.GetValues(typeof(DELAY)))
        {
            m_delayTime[(int)delayValue] = m_setting.getValue("DELAY", delayValue.ToString(), 0);
        }

        // MOTOR DELAY
        foreach (MOTOR_DELAY delayValue in Enum.GetValues(typeof(MOTOR_DELAY)))
        {
            m_delayTime[(int)delayValue] = m_setting.getValue("MOTOR DELAY", delayValue.ToString(), 0);
        }

        // CYLINDER DELAY
        foreach (CYLINDER_DELAY delayValue in Enum.GetValues(typeof(CYLINDER_DELAY)))
        {
            m_delayTime[(int)delayValue] = m_setting.getValue("CYLINDER DELAY", delayValue.ToString(), 0);
        }

        // VACUUM DELAY
        foreach (VACUUM_DELAY delayValue in Enum.GetValues(typeof(VACUUM_DELAY)))
        {
            m_delayTime[(int)delayValue] = m_setting.getValue("VACUUM DELAY", delayValue.ToString(), 0);
        }
    }

    #region DB
    public static string DB_IP
    {
        get { return m_db_ip; }
    }

    public static string DB_SCHEME
    {
        get { return m_db_scheme; }
    }
    #endregion

    static public int CURR_MODEL_IDX
    {
        get { return m_currModelIdx; }
        set { m_setting.setValue("MODELS", "CURR_MODEL_IDX", value); m_currModelIdx = value; }
    }

    #region MOTOR_VARIABLE
    static public double vel(AXIS axis) { return m_vel[(int)axis]; }
    static public double acc(AXIS axis) { return m_acc[(int)axis]; }
    static public double dec(AXIS axis) { return m_dec[(int)axis]; }
    static public double jogLow(AXIS axis) { return m_jogLow[(int)axis]; }
    static public double jogMid(AXIS axis) { return m_jogMid[(int)axis]; }
    static public double jogHigh(AXIS axis) { return m_jogHigh[(int)axis]; }

    static public double negLimit(AXIS axis) { return m_negLimit[(int)axis]; }
    static public double posLimit(AXIS axis) { return m_posLimit[(int)axis]; }

    static void setMotorValue(AXIS axis, string key, double value)
    {
        string text = ((int)axis).ToString();
        m_setting.setValue("MOTOR" + text, key, value);
    }

    static public void setVel(AXIS axis, double value) { m_vel[(int)axis] = value; setMotorValue(axis, "VEL", value); }
    static public void setAcc(AXIS axis, double value) { m_acc[(int)axis] = value; setMotorValue(axis, "ACC", value); }
    static public void setDec(AXIS axis, double value) { m_dec[(int)axis] = value; setMotorValue(axis, "DEC", value); }
    static public void setJogLow(AXIS axis, double value) { m_jogLow[(int)axis] = value; setMotorValue(axis, "JOG_LOW", value); }
    static public void setJogMid(AXIS axis, double value) { m_jogMid[(int)axis] = value; setMotorValue(axis, "JOG_MID", value); }
    static public void setJogHigh(AXIS axis, double value) { m_jogHigh[(int)axis] = value; setMotorValue(axis, "JOG_HIGH", value); }
    static public void setNegLimit(AXIS axis, double value) { m_negLimit[(int)axis] = value; setMotorValue(axis, "LIMIT_NEG", value); }
    static public void setPosLimit(AXIS axis, double value) { m_posLimit[(int)axis] = value; setMotorValue(axis, "LIMIT_POS", value); }
    #endregion


    #region Product Information
    static public bool PRODUCT_INFO_VISIBLE
    {
        get { return m_productInfoVisible; }
        set { m_setting.setValue("SETTING", "PRODUCT_INFO_VISIBLE", value); m_productInfoVisible = value; }
    }
    #endregion

    // Get Delay Time
    static public int delayTime(DELAY delayEnum)
    {
        return m_delayTime[(int)delayEnum];
    }

    // Get Motor Delay Time
    static public int delayTime(MOTOR_DELAY delayEnum)
    {
        return m_delayTime[(int)delayEnum];
    }

    // Get Cylinder Delay Time
    static public int delayTime(CYLINDER_DELAY delayEnum)
    {
        return m_delayTime[(int)delayEnum];
    }

    // Get Vacuum Delay Time
    static public int delayTime(VACUUM_DELAY delayEnum)
    {
        return m_delayTime[(int)delayEnum];
    }

    // Set Delay Time
    static public void setDelayTime(DELAY delayEnum, int time)
    {
        m_delayTime[(int)delayEnum] = time;
        m_setting.setValue("DELAY", delayEnum.ToString(), time);
    }

    // Set Motor Delay Time
    static public void setDelayTime(MOTOR_DELAY delayEnum, int time)
    {
        m_delayTime[(int)delayEnum] = time;
        m_setting.setValue("MOTOR DELAY", delayEnum.ToString(), time);
    }

    // Set Cylinder Delay Time
    static public void setDelayTime(CYLINDER_DELAY delayEnum, int time)
    {
        m_delayTime[(int)delayEnum] = time;
        m_setting.setValue("CYLINDER DELAY", delayEnum.ToString(), time);
    }

    // Set Vacuum Delay Time
    static public void setDelayTime(VACUUM_DELAY delayEnum, int time)
    {
        m_delayTime[(int)delayEnum] = time;
        m_setting.setValue("VACUUM DELAY", delayEnum.ToString(), time);
    }

    static public string PASSWORD
    {
        get { return m_password; }
        set { m_password = value; }
    }


    public static string LAST_STATION_BCR(int index)
    {
        if (index > m_lastStationBcr.Length - 1)
            return "";

        return m_lastStationBcr[index];
    }

    public static void setLastStationBcr(int index, string bcr)
    {
        if (index > m_lastStationBcr.Length - 1)
            return;

        m_lastStationBcr[index] = bcr;
        m_setting.setValue("STATION", "LAST_STATION_BCR" + (index + 1), bcr);
    }

    public static bool LAST_STATION_PASS(int index)
    {
        if (index > m_lastStationPass.Length - 1)
            return false;

        return m_lastStationPass[index];
    }

    public static void setLastStationPass(int index, bool pass)
    {
        if (index > m_lastStationPass.Length - 1)
            return;

        m_lastStationPass[index] = pass;
        m_setting.setValue("STATION", "LAST_STATION_PASS" + (index + 1), pass);
    }

    public static CSTATION.TYPE LAST_STATION_TYPE(int index)
    {
        if (index > m_lastStationType.Length - 1)
            return CSTATION.TYPE.EMPTY;

        return m_lastStationType[index];
    }

    public static void setLastStationType(int index, CSTATION.TYPE value)
    {
        if (index > m_lastStationType.Length - 1)
            return;

        m_lastStationType[index] = value;
        m_setting.setValue("STATION", "LAST_STATION_TYPE" + (index + 1), value.ToString());
    }
}