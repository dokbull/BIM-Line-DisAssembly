using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class CChartSetting : Form
{
    public class SettingAxisX
    {
        public int autoScrollCount = 0;
    }

    public class SettingAxisY
    {
        public float min = 0.0f;
        public float max = 0.0f;
        public float lower = 0.0f;
        public float upper = 0.0f;
        public float majorTick = 10.0f;
        public float minorTick = 1.0f;
    }
    
    public class SettingValue
    {
        string m_name = "";
        string m_path = "";
        
        public SettingAxisX axisX = new SettingAxisX();
        public SettingAxisY axisY = new SettingAxisY();
        
        public SettingValue(string name, string path)
        {
            m_name = name;
            m_path = path;
        }

        public void loadSetting()
        {
            if (m_path == "")
                throw new ArgumentException("SettingValue::loadSetting m_path is undefined");

            CSettings setting = new CSettings(m_path + "\\" + m_name + ".ini");

            // X
            axisX.autoScrollCount = setting.getValue("X", "autoScrollCount", 100);

            // Y
            axisY.min = setting.getValue("Y", "min", 0.0f);
            axisY.max = setting.getValue("Y", "max", 100.0f);
            axisY.lower = setting.getValue("Y", "lower", 40.0f);
            axisY.upper = setting.getValue("Y", "upper", 60.0f);
            axisY.majorTick = setting.getValue("Y", "majorTick", 10.0f);
            axisY.minorTick = setting.getValue("Y", "minorTick", 1.0f);
        }

        public void saveSetting()
        {
            if (m_path == "")
                throw new ArgumentException("SettingValue::loadSetting m_path is undefined");

            CSettings setting = new CSettings(m_path + "\\" + m_name + ".ini");

            // X
            setting.setValue("X", "autoScrollCount", axisX.autoScrollCount);

            // Y
            setting.setValue("Y", "min", axisY.min);
            setting.setValue("Y", "max", axisY.max);
            setting.setValue("Y", "lower", axisY.lower);
            setting.setValue("Y", "upper", axisY.upper);
            setting.setValue("Y", "majorTick", axisY.majorTick);
            setting.setValue("Y", "minorTick", axisY.minorTick);
        }
    }

    SettingValue m_value = null;

    public CChartSetting(string name, string path)
    {
        if (path == "")
            throw new ArgumentException("CChartSetting::CChartSetting m_path is undefined");

        InitializeComponent();

        m_value = new SettingValue(name, path);
        titleLabel.Text = name + " Chart Setting";

        loadSetting();
    }

    public void loadSetting()
    {
        m_value.loadSetting();

        // TextBox Update
        autoScrollCountTextBox.Text = m_value.axisX.autoScrollCount.ToString();

        minTextBox.Text = m_value.axisY.min.ToString();
        maxTextBox.Text = m_value.axisY.max.ToString();
        lowerTextBox.Text = m_value.axisY.lower.ToString();
        upperTextBox.Text = m_value.axisY.upper.ToString();

        majorTickTextBox.Text = m_value.axisY.majorTick.ToString();
        minorTickTextBox.Text = m_value.axisY.minorTick.ToString();
    }

    public bool saveSetting()
    {
        // TextBox -> Value
        try
        {
            m_value.axisX.autoScrollCount = Convert.ToInt32(autoScrollCountTextBox.Text);

            m_value.axisY.min = (float)Util.toDouble(minTextBox.Text);
            m_value.axisY.max = (float)Util.toDouble(maxTextBox.Text);
            m_value.axisY.lower = (float)Util.toDouble(lowerTextBox.Text);
            m_value.axisY.upper = (float)Util.toDouble(upperTextBox.Text);

            m_value.axisY.majorTick = (float)Util.toDouble(majorTickTextBox.Text);
            m_value.axisY.minorTick = (float)Util.toDouble(minorTickTextBox.Text);
        }
        catch (Exception)
        {
            MessageBox.Show("잘못된 데이터 값이 입력되었습니다. 확인하여 주십시오.");
            return false;
        }

        if (m_value.axisY.min == m_value.axisY.max)
        {
            MessageBox.Show("최소값과 최대값이 똑같습니다. 확인하여 주십시오.");
            return false;
        }

        if (m_value.axisY.max < m_value.axisY.min)
        {
            MessageBox.Show("최소값과 최대값이 잘못되었습니다. 확인하여 주십시오.");
            return false;
        }

        if (m_value.axisY.lower < m_value.axisY.min)
        {
            MessageBox.Show("하한값이 최소값보다 작습니다. 확인하여 주십시오.");
            return false;
        }

        if (m_value.axisY.upper > m_value.axisY.max)
        {
            MessageBox.Show("상한값이 최대값보다 큽니다. 확인하여 주십시오.");
            return false;
        }

        m_value.saveSetting();
        return true;
    }

    public SettingValue value()
    {
        return m_value;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (!saveSetting())
            return;

        Close();
        DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (!saveSetting())
            return;

        Close();
        DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        loadSetting();
    }
}
