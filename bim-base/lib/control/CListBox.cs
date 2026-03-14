using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

public class CListBox : System.Windows.Forms.ListBox
{
    string m_logName = "";
    string m_path = "";

    int m_maxCount = 100;
    bool m_autoSave = true;

    public string _LogName
    {
        get { return m_logName; }
        set { m_logName = value; }
    }

    public int _MaxCount
    {
        get { return m_maxCount; }
        set { m_maxCount = value; }
    }

    public bool _AutoSave
    {
        get { return m_autoSave; }
        set { m_autoSave = value; }
    }

    CFileManager m_fileManager = null;

    protected override void OnCreateControl()
    {
        this.DoubleBuffered = true;
        base.OnCreateControl();

        if (m_logName == "")
            return;

        m_path = Common.PATH + "\\AutoSave\\CListBox\\" + m_logName + ".log";
        m_fileManager = new CFileManager(m_path);

        List<string> list = m_fileManager.readAll();

        if (list == null)
            return;

        this.SuspendLayout();

        int count = 0;
        int checkCount = (list.Count - m_maxCount);

        foreach (string text in list)
        {
            if (count < checkCount)
            {
                count++;
                continue;
            }

            this.Items.Add(text);
            count++;
        }

        if (this.Items.Count > 0)
        {
            this.SelectedIndex = this.Items.Count - 1;
        }

        this.ResumeLayout();
    }

    protected override void Dispose(bool disposing)
    {
        if (m_autoSave)
        {
            if (m_fileManager != null)
            {
                List<string> list = new List<string>();
                foreach (object obj in this.Items)
                {
                    list.Add(obj.ToString());
                }

                m_fileManager.writeAll(list, false);
            }
        }
        base.Dispose(disposing);
    }

    public void addItem(string text)
    {
        this.Items.Add(text);
    }

    public void writeLog(string text)
    {
        if (m_fileManager != null)
        {
            m_fileManager.write(text, true);
        }
    }
}
