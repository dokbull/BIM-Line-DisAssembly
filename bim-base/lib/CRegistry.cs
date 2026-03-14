using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

public class CRegistry
{
    RegistryKey m_regKey;

    Dictionary<string, string> m_valueMap = new Dictionary<string, string>();
    List<string> m_valueList = new List<string>();
    
    public CRegistry(RegistryKey key)
    {
        m_regKey = key;
    }

    public void setSubKey(string name)
    {
        m_regKey = m_regKey.OpenSubKey(name);
    }

    public void aa()
    {
        m_regKey.CreateSubKey("CMD_WORK", RegistryKeyPermissionCheck.Default);
    }

    public bool setValueAddr(string key)
    {
        if (m_valueMap.ContainsKey(key) == true)
            return false;

        m_valueMap[key] = "";
        return true;
    }

    public string valueAddr(int index)
    {
        return "";
    }

    public string getValue(string key, string defaultValue)
    {
        object obj = m_regKey.GetValue(key);

        if (obj == null)
            return defaultValue;

        string text = obj.ToString();

        m_valueMap[key] = text;

        return text;
    }

    public double getValue(string key, double dafaultValue)
    {
        string text = getValue(key, "0");
        return Util.toDouble(text, dafaultValue);
    }

#if false
    RegistryKey reg = Registry.LocalMachine;
    reg = reg.OpenSubKey("Software\\GeoService\\GeoService-Xr", true);

    if (reg != null)
    {
        Object val = reg.GetValue("INSTALL_PATH");
        if (null != val)
        {
            MessageBox.Show(Convert.ToString(val));
        }
    }
#endif
}