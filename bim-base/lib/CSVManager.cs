using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;

public class CSVManager
{
    string m_section = "";
    string m_header = "";
    string m_model = "";
    string m_path = "";
    string m_subPath = "";

    bool m_useDayDirectory = false;
    bool m_useDataCount = false;

    bool m_autoLogDelete = true;

    bool m_useForceFileName = false;
    string m_forceFileName = "";

    Object m_lockObject = new Object();

    Queue<string> m_queue = null;

    public CSVManager(string path, bool useDayDirectory = false, bool autoDelete = true)
    {
        m_path = path;
        m_useDayDirectory = useDayDirectory;
        m_autoLogDelete = autoDelete;

        m_queue = new Queue<string>();
    }

    public void setSubPath(string subPath)
    {
        m_subPath = subPath;
    }

    public void setUseDataCount(bool value)
    {
        m_useDataCount = value;
    }

    public void setHeader(string header)
    {
        m_header = header;
    }

    public void setModel(string model)
    {
        m_model = model;
    }

    public string model()
    {
        return m_model;
    }

    public void setSection(string section)
    {
        m_section = section;
    }

    public string section()
    {
        return m_section;
    }

    public void setAutoLogDelete(bool value)
    {
        m_autoLogDelete = value;
    }

    public void setUseForceFileName(string fileName)
    {
        m_forceFileName = fileName;
        m_useForceFileName = true;
    }

    private void deleteOldFiles(string path, int days)
    {
        if (m_autoLogDelete == false)
            return;

        // 기존 파일 삭제
        DirectoryInfo rootInfo = new DirectoryInfo(path);

        DirectoryInfo[] childDir = null;

        try
        {
            childDir = rootInfo.GetDirectories();
        }
        catch (Exception e) // 폴더내에 디렉토리가 없을 경우 발생
        {
            Debug.debug("CSVManager::deleteOldFiles error reason:" + e.Message +
                " stackTrace: " + e.StackTrace);

            Directory.CreateDirectory(path + "\\empty");

            return;
        }

        foreach (DirectoryInfo di in childDir)
        {
            if (di.Name == "empty")
                continue;

            DateTime date = DateTime.ParseExact(di.Name, "yyyyMM", null);
            TimeSpan ts = date - DateTime.Now;

            if (ts.Days < (days * -1))
            {
                try
                {
                    Directory.Delete(di.FullName, true);
                }
                catch (Exception ex)
                {
                    Debug.warning("CSVManager::deleteOldFiles error:" + ex.Message +
                        " stackTrace:" + ex.StackTrace);
                    continue;
                }
            }
        }
    }
    object lockObject = new object();
    public void logWrite(string text)
    {
        try
        {
            bool m_writeHeader = false;

            lock (lockObject)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(m_path + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM"));

                if (directoryInfo.Exists == false)
                    directoryInfo.Create();

                string fileName = DateTime.Now.ToString("yyyyMMdd") + ".csv";

                string fullName = m_path + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + fileName;

                if (m_header != "")
                {
                    if (File.Exists(fullName) == false)
                        m_writeHeader = true;
                }

                using (StreamWriter file = new StreamWriter(fullName, true, Encoding.Default))
                {
                    if (m_writeHeader)
                        file.WriteLine(m_header);

                    file.WriteLine(text);
                }
            }
        }
        catch (IOException ioex)
        {
            Debug.warning("CsvLogManager logWrite error::" + ioex.Message);
        }
    }
    public void logWrite(string text, string categoty, string mcName)
    {
        try
        {
            bool m_writeHeader = false;

            lock (lockObject)
            {
                string path = "D:\\CSV\\backup\\" ;

                DirectoryInfo directoryInfo = new DirectoryInfo(path + "\\" +  DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM"));

                if (directoryInfo.Exists == false)
                    directoryInfo.Create();

                string fileName = "";
                string name = categoty;

                if (categoty == "MC")
                {
                    if (mcName!="")
                        fileName = mcName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                    else
                        fileName = name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                }
                else if (categoty == "MASTER")
                {
                    if (mcName != "")
                        fileName = mcName + "_MASTER_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                    else
                        fileName = name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                }
                else if (categoty == "MASTER_NG")
                {
                    if (mcName != "")
                        fileName = mcName + "_MASTER_NG_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                    else
                        fileName = name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                }
                else
                    fileName = name + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";

                string fullName = path + "\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + fileName;

                if (m_header != "")
                {
                    if (File.Exists(fullName) == false)
                        m_writeHeader = true;
                }

                using (StreamWriter file = new StreamWriter(fullName, true, Encoding.Default))
                {
                    if (m_writeHeader)
                        file.WriteLine(m_header);

                    file.WriteLine(text);
                }
            }
        }
        catch (IOException ioex)
        {
            Debug.warning("CsvLogManager logWrite error::" + ioex.Message);
        }
    }
    public void write(string lotID, string text, bool writeTime = false)
    {
        string path = m_path + "\\";
        DateTime now = DateTime.Now;

        if (m_model != "") path += (m_model + "\\");
        if (m_section != "") path += (m_section + "\\");

        if (m_useDayDirectory)
        {
            path +=
                now.ToString("yyyy") + "\\" +
                now.ToString("MM") + "\\" +
                now.ToString("dd") + "\\";
        }

        if (m_subPath != "") // 추가 하위 디렉토리 사용
            path += m_subPath;

        else
        {
            try
            {
                deleteOldFiles(path, 365);
            }
            catch (Exception /*e*/)
            {
                //Debug.warning("CSVManager::write error:" + e.Message);
                //Debug.warning("CSVManager::write error stackTrace:" + e.StackTrace);
            }
        }

        DirectoryInfo dirInfo = new DirectoryInfo(path);

        string fileName = lotID + "_" + now.ToString("yyyyMMdd") + ".csv";

        if (lotID == "")
            fileName = now.ToString("yyyyMMdd") + ".csv";

        if (m_useForceFileName)
            fileName = m_forceFileName + ".csv";

        string fullFileName = path + fileName;

        bool m_writeHeader = false;

        if (m_header != "")
        {
            if (File.Exists(fullFileName) == false)
                m_writeHeader = true;
        }

        if (dirInfo.Exists == false)
            dirInfo.Create();

        string timeText = DateTime.Now.ToString("HH:mm:ss");

        lock (m_lockObject)
        {
            if (m_useDataCount)
            {
                int lineCount = 0;

                if (m_writeHeader == false)
                {
                    string[] readAll = File.ReadAllLines(fullFileName);

                    if (readAll[readAll.Length - 1].IndexOf("갯수") > 0 - 1)
                        readAll[readAll.Length - 1] = text;

                    File.WriteAllLines(fullFileName, readAll, System.Text.Encoding.UTF8);

                    lineCount = readAll.Length;
                }

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fullFileName, true, System.Text.Encoding.UTF8))
                {
                    if (m_writeHeader)
                        file.WriteLine(m_header);

                    if (m_writeHeader)
                        file.WriteLine(text);

                    if (lineCount == 0)
                        lineCount = 2; // 밑에서 -1을 하게 되니 "1" 로 보이기 위해서 2로 만듬

                    file.WriteLine("갯수," + (lineCount - 1));
                }
            }
            else
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fullFileName, true, System.Text.Encoding.Default))
                {
                    if (m_writeHeader)
                        file.WriteLine(m_header);

                    if (writeTime) // 시간을 표기
                        text = timeText + "," + text;

                    file.WriteLine(text);
                }
            }
        } // lock (m_lockObject)
    }


    public void write(string lotID, string text, string dateFormat)
    {
        string path = m_path + "\\";
        DateTime now = DateTime.Now;

        if (m_model != "") path += (m_model + "\\");
        if (m_section != "") path += (m_section + "\\");

        if (m_useDayDirectory)
        {
            path +=
                now.ToString("yyyy") + "\\" +
                now.ToString("MM") + "\\" +
                now.ToString("dd") + "\\";
        }

        if (m_subPath != "") // 추가 하위 디렉토리 사용
            path += m_subPath;

        else
        {
            try
            {
                deleteOldFiles(path, 365);
            }
            catch (Exception /*e*/)
            {
                //Debug.warning("CSVManager::write error:" + e.Message);
                //Debug.warning("CSVManager::write error stackTrace:" + e.StackTrace);
            }
        }

        DirectoryInfo dirInfo = new DirectoryInfo(path);


        string fileName = lotID + "_" + now.ToString(dateFormat) + ".csv";

        if (lotID == "")
            fileName = now.ToString(dateFormat) + ".csv";

        string fullFileName = path + fileName;

        bool m_writeHeader = false;

        if (m_header != "")
        {
            if (File.Exists(fullFileName) == false)
                m_writeHeader = true;
        }

        if (dirInfo.Exists == false)
            dirInfo.Create();

        string timeText = DateTime.Now.ToString("HH:mm:ss");

        lock (m_lockObject)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fullFileName, true, System.Text.Encoding.Default))
            {
                if (m_writeHeader)
                    file.WriteLine(m_header);

                file.WriteLine(text);
            }
        } // lock (m_lockObject)
    }


    public void writeWithFileName(string fileName, string text, bool writeTime = false)
    {
        string path = m_path;

        DateTime now = DateTime.Now;

        DirectoryInfo dirInfo = new DirectoryInfo(path);

        string fullFileName = path + fileName;

        bool m_writeHeader = false;

        if (m_header != "")
        {
            if (File.Exists(fullFileName) == false)
                m_writeHeader = true;
        }

        if (dirInfo.Exists == false)
            dirInfo.Create();

        string timeText = DateTime.Now.ToString("HH:mm:ss");

        lock (m_lockObject)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fullFileName, true, System.Text.Encoding.Default))
            {
                if (m_writeHeader)
                    file.WriteLine(m_header);

                if (writeTime) // 시간을 표기
                    text = timeText + "," + text;

                file.WriteLine(text);
            }

        } // lock (m_lockObject)
    }





    public void addQueue(string text)
    {
        lock (m_queue)
        {
            m_queue.Enqueue(text);
        }
    }

    public void writeQueue(string lotID, bool writeTime = false)
    {
        lock (m_queue)
        {
            if (m_queue.Count == 0)
                return;

            string path = m_path + "\\";
            DateTime now = DateTime.Now;

            if (m_model != "")
                path += (m_model + "\\");

            if (m_section != "")
                path += (m_section + "\\");

            if (m_useDayDirectory)
            {
                path +=
                    now.ToString("yyyy") + "\\" +
                    now.ToString("MM") + "\\" +
                    now.ToString("dd") + "\\";
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            string fileName = lotID + "_" + now.ToString("yyyyMMdd") + ".csv";

            if (m_useForceFileName)
                fileName = m_forceFileName + ".csv";

            string fullFileName = path + fileName;

            if (dirInfo.Exists == false)
                dirInfo.Create();

            string timeText = DateTime.Now.ToString("HH:mm:ss");

            lock (m_lockObject)
            {
                string[] data = new string[m_queue.Count];

                int cnt = 0;

                foreach (string item in m_queue)
                {
                    data[cnt] = "";

                    if (writeTime) // 시간을 표기
                        data[cnt] = timeText + ",";

                    data[cnt] += item;

                    cnt++;
                }

                m_queue.Clear();

                List<string> writeList = new List<string>();

                if (File.Exists(fullFileName) == false)
                {
                    if (m_header != "")
                    {
                        writeList.Add(m_header);
                    }
                }
                else
                {
                    string[] readAll = File.ReadAllLines(fullFileName);

                    writeList.AddRange(readAll);
                }

                writeList.AddRange(data);

                string[] writeData = writeList.ToArray();

                File.WriteAllLines(fullFileName, writeData, System.Text.Encoding.UTF8);
            } // lock (m_lockObject)
        } // lock (m_queue)
    }

    public void setPath(string path)
    {
        m_path = path;
    }
}
