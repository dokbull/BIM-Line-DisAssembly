using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class UtilProcess
{
    Process m_proc = null;
    string m_execFilePath = "";
    string m_exeName = "";

    public UtilProcess(string execPath, string exeName)
    {
        m_execFilePath = execPath;
        m_exeName = exeName;
    }

    public bool isNull()
    {
        if (m_proc == null)
            return true;

        return false;
    }

    public bool start()
    {
        if (m_execFilePath == "")
            throw new ArgumentException("execFilePath is empty!");

        if (m_proc != null)
            return false;

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = m_execFilePath;
            startInfo.FileName = m_exeName;

            m_proc = Process.Start(startInfo);
        }
        catch (Exception /*ex*/)
        {
        }

        return true;
    }

    public bool kill()
    {
        if (m_proc == null)
            return false;

        try
        {
            m_proc.Kill();
        }
        catch (Exception ex)
        {
            Debug.warning("UtilProcess::kill error reason:" + ex.Message +
                " stackTrace:" + ex.StackTrace);
        }

        return true;
    }

    public static bool killProcess(string name)
    {
        Process[] processArr = Process.GetProcessesByName(name);

        if (processArr.Length == 0)
            return false;

        foreach (Process proc in processArr)
        {
            Debug.warning("UtilProcesss::killProcess name:" + name);
            proc.Kill();
        }

        return true;
    }
}
