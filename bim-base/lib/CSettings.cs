using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

using System.Drawing;

public class CSettings
{
    private string m_path = "";
    bool m_backup = false;

    public CSettings(string path)
    {
        load(path);
    }

    public void load(string path)
    {
        m_path = path;

        FileInfo fileInfo = new FileInfo(m_path);

        if (fileInfo.Directory.Exists == false)
            fileInfo.Directory.Create();

        if (!fileInfo.Exists)
            Debug.warning("CSettings::CSettings file not found. path:" + m_path);
    }

    public void setMakeBackupFile(bool value)
    {
        m_backup = value;
    }

    void makeBackupFile()
    {
        if (m_backup == false)
            return;

        if (File.Exists(m_path))
        {
            FileInfo fi = new FileInfo(m_path);

            string backupPath = fi.DirectoryName;
            backupPath += "\\backup\\";

            if (Directory.Exists(backupPath) == false)
                Directory.CreateDirectory(backupPath);

            string nowString = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string backupFileName = backupPath + "BAK_" + nowString + "_" + fi.Name;

            if (File.Exists(backupFileName) == false)
                File.Copy(m_path, backupFileName);
        }
    }

    public string path()
    {
        return m_path;
    }

    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(string section, string key, string def, byte[] lpszReturnBuffer, int size, string filePath);

    [DllImport("kernel32.dll")]
    private static extern int WritePrivateProfileString(string section, string key, byte[] lpszBuffer, string filePath);

    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
    static extern int GetSectionNamesListA(byte[] lpszReturnBuffer, int nSize, string lpFileName);

    public string getValue(string section, string key, string defaultValue)
    {
        byte[] buffer = new byte[255];

        int ret = GetPrivateProfileString(section, key, defaultValue, buffer, 255, m_path);

        return Encoding.UTF8.GetString(buffer).Trim('\0');
    }

    public string getValueEUCKR(string section, string key, string defaultValue)
    {
        byte[] buffer = new byte[255];

        int ret = GetPrivateProfileString(section, key, defaultValue, buffer, 255, m_path);

        return Encoding.GetEncoding("euc-kr").GetString(buffer).Trim('\0');
    }

    public int getValue(string section, string key, int defaultValue)
    {
        string text = getValue(section, key, "");

        if (text == "" || text == "0.001")
            return defaultValue;

        return Convert.ToInt32(text);
    }

    public bool getValue(string section, string key, bool defaultValue)
    {
        string text = getValue(section, key, "");

        if (text == "")
            return defaultValue;

        return Convert.ToBoolean(text);
    }

    public double getValue(string section, string key, double defaultValue)
    {
        string text = getValue(section, key, "");

        if (text == "")
            return defaultValue;

        return Convert.ToDouble(text);
    }

    public float getValue(string section, string key, float defaultValue)
    {
        string text = getValue(section, key, "");

        if (text == "")
            return defaultValue;

        return (float)Convert.ToDouble(text);
    }

    public Color getValue(string section, string key, Color defaultValue)
    {
        string[] keyName = { "_alpha", "_red", "_green", "_blue" };

        string[] text = new string[4];
        byte[] value = new byte[4];

        for (int i=0; i<4; i++)
        {
            text[i] = getValue(section, key + keyName, "");

            if (text[i] == "")
                return defaultValue;

            try
            {
                value[i] = Convert.ToByte(text[i]);
            }
            catch (Exception ex)
            {
                Debug.debug("CSettings::getValue error reason:" + ex.Message +
                    " stackTrace:" + ex.StackTrace);
                return defaultValue;
            }
        }

        return Color.FromArgb(value[0], value[1], value[2], value[3]);
    }

    public DateTime getValue(string section, string key, DateTime defaultValue)
    {
        string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        string text = getValue(section, key, defaultValue.ToString(dateTimeFormat));

        return strToDateTime(text);
    }

    //FIXME@tmdwn..추후에 StringBulider 로 교체할 것 (최적화)
    public DateTime strToDateTime(string text)
    {
        string yearText = text.Substring(0, 4);
        string monthText = text.Substring(5, 2);
        string dayText = text.Substring(8, 2);

        string hourText = text.Substring(11, 2);
        string minuteText = text.Substring(14, 2);
        string secondText = text.Substring(17, 2);

        int year = Util.toInt32(yearText);
        int month = Util.toInt32(monthText);
        int day = Util.toInt32(dayText);

        int hour = Util.toInt32(hourText);
        int minutes = Util.toInt32(minuteText);
        int second = Util.toInt32(secondText);

        return new DateTime(year, month, day, hour, minutes, second);
    }

    public void setValue(string section, string key, string value)
    {
        makeBackupFile();

        byte[] buffer = Encoding.UTF8.GetBytes(value);
        WritePrivateProfileString(section, key, buffer, m_path);
    }

    public void setValueEUCKR(string section, string key, string value)
    {
        makeBackupFile();

        byte[] buffer = Encoding.GetEncoding("euc-kr").GetBytes(value);
        WritePrivateProfileString(section, key, buffer, m_path);
    }

    public void setValue(string section, string key, int value)
    {
        makeBackupFile();

        setValue(section, key, value.ToString());
    }

    public void setValue(string section, string key, double value)
    {
        makeBackupFile();

        setValue(section, key, value.ToString());
    }

    public void setValue(string section, string key, float value)
    {
        makeBackupFile();

        setValue(section, key, value.ToString());
    }

    public void setValue(string section, string key, bool value)
    {
        makeBackupFile();

        setValue(section, key, value.ToString());
    }

    public void setValue(string section, string key, Color value)
    {
        makeBackupFile();

        setValue(section, key + "_alpha", value.A);
        setValue(section, key + "_red", value.R);
        setValue(section, key + "_green", value.G);
        setValue(section, key + "_blue", value.B);
    }

    public void setValue(string section, string key, DateTime dateTime)
    {
        makeBackupFile();

        string dateTimeFormat = "yyy-MM-dd HH:mm:ss";
        setValue(section, key, dateTime.ToString(dateTimeFormat));
    }

    public void deleteKey(string section, string key)
    {
        WritePrivateProfileString(section, key, null, m_path);
    }

    public void deleteAllKeys(string section)
    {
        List<string> keys = getKeys(section);

        if (keys == null)
            return;

        foreach (string key in keys)
        {
            deleteKey(section, key);
        }
    }

    public void deleteSection(string section)
    {
        WritePrivateProfileString(section, null, null, m_path);
    }

    public void deleteAllSection()
    {
        List<string> sectionList = getAllSection();

        for (int i = 0; i < sectionList.Count; i++)
            WritePrivateProfileString(sectionList[i], null, null, m_path);
    }

    public List<string> getAllSection()
    {
        byte[] buff = new byte[1024];
        GetSectionNamesListA(buff, buff.Length, m_path);
        String s = Encoding.UTF8.GetString(buff);
        String[] names = s.Trim('\0').Split('\0');

        List<string> result = new List<String>();

        foreach (String name in names)
        {
            if (name != "")
                result.Add(name);
        }

        return result;
    }

    public List<string> getKeys(string section)
    {
        byte[] buffer = new byte[1024 * 1024];

        GetPrivateProfileSection(section, buffer, 1024 * 1024, m_path);
        String[] tmp = Encoding.UTF8.GetString(buffer).Trim('\0').Split('\0');

        List<string> result = new List<string>();

        if (tmp[0] == "")
            return null;

        foreach (String entry in tmp)
        {
            if (entry.Substring(0, 1) == "#")
                continue;

            result.Add(entry.Substring(0, entry.IndexOf("=")));
        }

        return result;
    }
}
