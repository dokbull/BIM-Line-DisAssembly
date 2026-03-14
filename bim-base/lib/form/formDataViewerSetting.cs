using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class formDataViewerSetting : Form
{
    CSettings m_settings = new CSettings(pathUtil.savePath() +
        "\\dataViewer.ini");

    public class Setting
    {
        int m_deleteYear;
        int m_deleteMonth;

        string m_address;
        string m_account;
        string m_password;
        string m_scheme;

        public Setting(CSettings setting)
        {
            m_deleteYear = setting.getValue("PERIOD", "YEAR", 0);
            m_deleteMonth = setting.getValue("PERIOD", "MONTH", 0);

            m_address = setting.getValue("DATABASE", "ADDRESS", "127.0.0.1");
            m_account = setting.getValue("DATABASE", "ACCOUNT", "root");
            m_password = setting.getValue("DATABASE", "PASSWORD", "");
            m_scheme = setting.getValue("DATABASE", "SCHEME", "");
        }

        public string address
        {
            get { return m_address; }
        }

        public string account
        {
            get { return m_account; }
        }

        public string password
        {
            get { return m_password; }
        }

        public string scheme
        {
            get { return m_scheme; }
        }
    }

    public formDataViewerSetting()
    {
        InitializeComponent();

        yearUpDown.Value = m_settings.getValue("PERIOD", "YEAR", 0);
        monthUpDown.Value = m_settings.getValue("PERIOD", "MONTH", 0);

        // database setting
        dbAddressTextBox.Text = m_settings.getValue("DATABASE", "ADDRESS", "127.0.0.1");
        dbAccountTextBox.Text = m_settings.getValue("DATABASE", "ACCOUNT", "root");
        dbPasswordTextBox.Text = m_settings.getValue("DATABASE", "PASSWORD", "");
        dbNameTextBox.Text = m_settings.getValue("DATABASE", "SCHEME", "");
    }

    private void yearUpDown_ValueChanged(object sender, EventArgs e)
    {
        NumericUpDown upDown = (NumericUpDown)sender;

        int value = Decimal.ToInt32(upDown.Value);
        m_settings.setValue("PERIOD", "YEAR", value);

        updateDeleteDate();
    }

    private void monthUpDown_ValueChanged(object sender, EventArgs e)
    {
        NumericUpDown upDown = (NumericUpDown)sender;

        int value = Decimal.ToInt32(upDown.Value);
        m_settings.setValue("PERIOD", "MONTH", value);

        updateDeleteDate();
    }

    private void updateDeleteDate()
    {
        int year = Decimal.ToInt32(yearUpDown.Value);
        int month = Decimal.ToInt32(monthUpDown.Value);

        DateTime date = DateTime.Now;
        date = date.AddYears(year * -1);
        date = date.AddMonths(month * -1);

        deleteDateLabel.Text = date.ToString("yyyy-MM-dd");
    }

    private void dbAddressTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        m_settings.setValue("DATABASE", "ADDRESS", textBox.Text);
    }

    private void dbAccountTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        m_settings.setValue("DATABASE", "ACCOUNT", textBox.Text);
    }

    private void dbPasswordTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        m_settings.setValue("DATABASE", "PASSWORD", textBox.Text);
    }

    private void dbNameTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        m_settings.setValue("DATABASE", "SCHEME", textBox.Text);
    }

    private void connectTestButton_Click(object sender, EventArgs e)
    {
        MySqlManager sql = new MySqlManager(
            dbAddressTextBox.Text,
            dbAccountTextBox.Text,
            dbPasswordTextBox.Text,
            dbNameTextBox.Text);

        if (sql.connTest() == false)
            addLog("Connect Test Failed");
        else
            addLog("Connect Test Success");
    }

    private void addLog(string text)
    {
        string dateTimeText = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
        listBox1.Items.Insert(0, dateTimeText + text);
    }
}
