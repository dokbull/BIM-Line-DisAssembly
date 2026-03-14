using bim_base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

public class Common
{
    static Common m_instance = null;

    public static string TITLE = "BASE-S-V1"; 
    public static string MC_NAME = "TOPM00";

    public static string PATH = pathUtil.savePath();
    public static string MES_PATH = "C:\\FA\\MES\\mesConverter.exe";

    public static string VERSION = TITLE + "_1." + getBuildDate().ToString("yyyyMMdd") + ".01";
    public static string SHOT_VERSION = "(Ver 1." + getBuildDate().ToString("yyyyMMdd") + ".01)";

    public static string MODEL_PATH = PATH + "\\models\\";

    public static ModelInfo MC_INFO = null;
    public static ModelInfo MODEL_INFO = null;

    public static string LOG_PATH = PATH + "\\log";
    public static string PRODUCT_PATH = LOG_PATH + "\\product_log\\";
    public static CLogManager ALARM_MANAGER = new CLogManager("alarm", LOG_PATH, "", "alarm");

    public Common()
    {
        m_instance = this;
    }

    public static Common inst()
    {
        if (m_instance == null)
            m_instance = new Common();

        PATH = pathUtil.savePath();

        Conf.load();
        Alarm.load();

        MC_INFO = new ModelInfo("MACHINE");
        MODEL_INFO = new ModelInfo(Conf.CURR_MODEL);

        return m_instance;
    }

    public static void setCustomPath(string path)
    {
        PATH = path;
        MODEL_PATH = PATH + "\\models\\";
        LOG_PATH = PATH + "\\log";
        PRODUCT_PATH = LOG_PATH + "\\product_log\\";
        ALARM_MANAGER = new CLogManager("alarm", LOG_PATH, "", "alarm");
    }

    public static DateTime getBuildDate()
    {
        Version ver = Assembly.GetExecutingAssembly().GetName().Version;

        int day = ver.Build;
        DateTime dt = (new DateTime(2000, 1, 1)).AddDays(day);

        DaylightTime dlt = TimeZone.CurrentTimeZone.GetDaylightChanges(dt.Year);
        if (TimeZone.IsDaylightSavingTime(dt, dlt))
            dt = dt.Add(dlt.Delta);

        return dt;
    }

    public static string inputEnumString(INPUT input)
    {
        string retn = "";
        Type enumType = typeof(INPUT);
        MemberInfo[] memberInfos = enumType.GetMember(input.ToString());
        if (memberInfos.Length > 0)
        {
            retn = input.ToString();
        }
        return retn;
    }

    public static string outputEnumString(OUTPUT output)
    {
        string retn = "";
        Type enumType = typeof(OUTPUT);
        MemberInfo[] memberInfos = enumType.GetMember(output.ToString());
        if (memberInfos.Length > 0)
        {
            retn = output.ToString();
        }
        return retn;
    }
}



public static class MessageText
{
    public static string saveMessage = "Save complete";
}

public class MESData
{
    string m_bcr = "";
    bool m_result = false;

    public bool isRecv = false;

    public void setBarcode(string value) { m_bcr = value; }
    public string getBarcode() { return m_bcr; }
    public bool result
    {
        get { return m_result; }
        set { m_result = value; }
    }
}