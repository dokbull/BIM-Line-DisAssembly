using System;
using System.IO;
using System.Collections.Generic;

public class CLogManager
{
    private object lockObject = new object();

    string m_name = "";
    string m_path = "";
    string m_prefix = "";
    string m_filePrefix = "";

    public CLogManager(string name, string path, string prefix = "", string filePrefix = "")
    {
        m_name = name;
        m_path = path;

        m_prefix = prefix; // 폴더 접두어
        m_filePrefix = filePrefix; // 파일 접두어
    }

    public void writeFast(Queue<string> queue, bool divHour = false)
    {
        try // 파일 쓰기 도중 외부에서 해당 파일 엑세스하는 경우 프로그램 종료되는 문제로 try-catch 적용
        {
            lock (lockObject)
            {
                if (queue.Count == 0)
                    return;

                string filename = "";

                if (m_filePrefix != "")
                    filename = m_filePrefix + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";

                else if (divHour)
                    filename = DateTime.Now.ToString("yyyyMMddHH") + ".log";

                else
                    filename = DateTime.Now.ToString("yyyyMMdd") + ".log";

                string fullName = "";

                if (m_prefix != "")
                    fullName = m_path + "\\" + m_prefix + "_" + m_name + "\\" + filename;
                else
                    fullName = m_path + "\\" + m_name + "\\" + filename;

                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(fullName, true))
                {
                    foreach (string text in queue)
                    {
                        file.WriteLine(text);
                        Console.WriteLine(text);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.warning("CLogManager::writeFast exception message:" + ex.Message + " trace:" + ex.StackTrace);
        }
    }

    public void write(string text, bool divHour = false)
    {
        try // 파일 쓰기 도중 외부에서 해당 파일 엑세스하는 경우 프로그램 종료되는 문제로 try-catch 적용
        {
            lock (lockObject)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(m_path + "\\" + m_name);

                DateTime now = DateTime.Now;
                DateTime lastYear = now.AddYears(-1);

                string filename = "";

                if (m_filePrefix != "")
                    filename = m_filePrefix + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";

                else if (divHour)
                    filename = DateTime.Now.ToString("yyyyMMddHH") + ".log";

                else
                    filename = DateTime.Now.ToString("yyyyMMdd") + ".log";

                if (directoryInfo.Exists == false)
                    directoryInfo.Create();

                string fullName = "";

                if (m_prefix != "")
                    fullName = m_path + "\\" + m_prefix + "_" + m_name + "\\" + filename;
                else
                    fullName = m_path + "\\" + m_name + "\\" + filename;

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fullName, true))
                {
                    file.WriteLine(text);
                }

#if true
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (file.Extension != ".log")
                    {
                        continue;
                    }

                    if (file.CreationTime < lastYear)
                    {
                        file.Delete();
                    }

                }
#endif
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine("CLogManager::write exception message:" + ex.Message + " trace:" + ex.StackTrace);
        }
    }
} // class