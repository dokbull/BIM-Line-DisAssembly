using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

public static class CVirtualKeyboard
{
    static Process m_proc = new Process();

    public static void open()
    {
        var proc = Process.GetProcessesByName("osk");

        if (proc.Length > 0) 
            return;

        m_proc.StartInfo.FileName = "osk.exe";
        m_proc.Start();
    }

    public static void close()
    {
        var proc = Process.GetProcessesByName("osk");

        if (proc.Length > 0)
        {
            foreach (var process in proc)
            {
                process.Kill();
            }
        }    
    }
}
