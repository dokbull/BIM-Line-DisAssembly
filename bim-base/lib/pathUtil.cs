using System;
using System.Windows.Forms;

class pathUtil
{
    static bool m_useCustomPath = false;
    static string m_customPath = "";

    /// <summary>
    ///
    /// </summary>
    /// <param name="path"></param>
    public static void setCustomPath(string path)
    {
        m_useCustomPath = true;
        m_customPath = path;
    }

    /// <summary>
    /// get program export file folder path
    /// </summary>
    /// <returns>export file folder path</returns>
    public static string savePath()
    {
        if (m_useCustomPath)
        {
            return m_customPath;
        }

        return myDocumnent() + "\\" + productName();
    }

    /// <summary>
    /// get myDocuments folder path
    /// </summary>
    /// <returns>myDocuments folder path</returns>
    public static string myDocumnent()
    {
        return System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    /// <summary>
    /// get product name
    /// </summary>
    /// <returns>product name</returns>
    public static string productName()
    {
        return Application.ProductName;
    }

    public static string startupPath()
    {
        return Application.StartupPath;
    }
}
