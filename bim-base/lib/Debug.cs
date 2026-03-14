using System;
using System.Collections.Generic;
using System.Windows.Forms;

static class Debug
{
    static CLogManager m_logManager = null;
    static CLogManager m_logLvdtManager = null;
    static CLogManager m_warningLogManager = null;
    static CLogManager m_criticalLogManager = null;

    static string m_path = "";

    static bool m_skipDebug = false;
    static bool m_isQueueMode = false;
    static Queue<string> m_queue = new Queue<string>();

    static bool m_divHour = false;

    static Debug()
    {
        m_path = pathUtil.savePath();

        m_logManager = new CLogManager("log", m_path);
        m_logLvdtManager = new CLogManager("logLvdt", m_path);
        m_warningLogManager = new CLogManager("warn-log", m_path);
        m_criticalLogManager = new CLogManager("crit-log", m_path);
    }

    public static void setPath(string path, string log="log", string warn = "warn-log", string crit = "crit-log", string lvdt = "logLvdt")
    {
        m_path = path;

        if (m_logManager != null) 
            m_logManager = null;
        m_logManager = new CLogManager(log, m_path);
        if (m_logLvdtManager != null)
            m_logLvdtManager = null;
        m_logLvdtManager = new CLogManager(lvdt, m_path);
        if (m_warningLogManager != null)
            m_warningLogManager = null;
        m_warningLogManager = new CLogManager(warn, m_path);
        if (m_criticalLogManager != null)
            m_criticalLogManager = null;
        m_criticalLogManager = new CLogManager(crit, m_path);
    }

    public static void skipDebug(bool value)
    {
        m_skipDebug = value;
    }

    //TODO@tmdwn..로그 ON/OFF 및 Console ON/OFF 기능 추가 해야 함
    public static void debug(String text)
    {
        if (m_skipDebug)
            return;

        if (m_path == "")
            throw new ArgumentException("Debug::debug path is not defined");

        text = text.Replace(Environment.NewLine, "");

#if true
        string timeString = "[" + DateTime.Now.ToString("HHmmss.ffff") + "]";
        timeString += " D ";

        string message = timeString + text;

        if (m_isQueueMode)
        {
            lock (m_queue)
            {
                m_queue.Enqueue(message);
            }

            return;
        }
        
        Console.WriteLine(text);
        m_logManager.write(message, m_divHour);
#endif
    }

    public static void debugLvdt(String text)
    {
        if (m_skipDebug)
            return;

        if (m_path == "")
            throw new ArgumentException("Debug::debug path is not defined");

        text = text.Replace(Environment.NewLine, "");

#if true
        string timeString = "[" + DateTime.Now.ToString("HHmmss.ffff") + "]";
        timeString += " Lvdt ";

        string message = timeString + text;

        if (m_isQueueMode)
        {
            lock (m_queue)
            {
                m_queue.Enqueue(message);
            }

            return;
        }

        Console.WriteLine(text);
        m_logLvdtManager.write(message);
#endif
    }

    //TODO@tmdwn..로그 ON/OFF 및 Console ON/OFF 기능 추가 해야 함
    public static void warning(String text)
    {
        if (m_path == "")
            throw new ArgumentException("Debug::warning path is not defined");

        text = text.Replace(Environment.NewLine, "");

#if true
        string timeString = "[" + DateTime.Now.ToString("HHmmss.ffff") + "]";
        timeString += " W ";

        string message = timeString + text;

        Console.WriteLine(text);

        m_logManager.write(message, m_divHour);
        m_warningLogManager.write(message);
#endif
    }

    public static void critical(Exception e, string section = "")
    {
        if (m_path == "")
            throw new ArgumentException("Debug::critical path is not defined");

#if true
        string timeString = "[" + DateTime.Now.ToString("HHmmss.ffff") + "]";
        timeString += " C ";

        string message = timeString + "section:" + section + " stackTrace : " + e.StackTrace + " Message : " + e.Message;

        Console.WriteLine(message);

        m_logManager.write(message, m_divHour);
        m_criticalLogManager.write(message);
#endif
    }

    static public bool queueMode()
    {
        return m_isQueueMode;
    }

    static public void setQueueMode(bool value)
    {
        if (value == false)
            flush();

        m_isQueueMode = value;
    }

    static public void flush()
    {
        lock (m_queue)
        {
            m_logManager.writeFast(m_queue, m_divHour);
            m_queue.Clear();
        }
    }

    static public void setDivHour(bool value)
    {
        m_divHour = value;
    }
} // class
