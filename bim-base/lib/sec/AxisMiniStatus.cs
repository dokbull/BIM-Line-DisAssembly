using System;
using System.Drawing;
using System.Windows.Forms;

public partial class AxisMiniStatus : UserControl
{
    RSAMMCEAxis m_axis = null;

    public bool m_select = false;
    public string m_name = "";

    public AxisMiniStatus()
    {
        InitializeComponent();
    }

    public void setAxis(RSAMMCEAxis axis)
    {
        m_axis = axis;

        if (axis == null)
            return;

        m_name = axis.name();
    }

    /// <summary>
    /// overwrite name
    /// </summary>
    /// <param name="name"></param>
    public void setName(string name)
    {
        m_name = name;
    }

    public RSAMMCEAxis axis()
    {
        return m_axis;
    }

    public bool _SELECT
    {
        get { return m_select; }
        set { 
            m_select = value;
        }
    }

    public void update()
    {
        if (m_axis == null)
            return;

        RSAMMCEAxis axis = m_axis;

        nameLabel.Text = m_name + "\r\n"
            + "(" + axis.pos().ToString("0.00") + ")";

        if (m_select == true)
        {
            nameLabel.BackColor = Color.Yellow;
            nameLabel.ForeColor = Color.Black;
        }
        else if (axis.isAlarm() || axis.isServoOn() == false)
        {
            nameLabel.BackColor = Color.DarkRed;
            nameLabel.ForeColor = Color.White;
        }
        else if (axis.isHomed() == true)
        {
            nameLabel.BackColor = Color.Lime;
            nameLabel.ForeColor = Color.Black;
        }
        else
        {
            nameLabel.BackColor = Color.Transparent;
            nameLabel.ForeColor = Color.Black;
        }

        setColor(inpos, axis.inpos());
        setColor(minusLimit, axis.minusLimit());
        setColor(plusLimit, axis.plusLimit());

        if (axis.isAlarm())
        {
            servoOn.BackColor = Color.DarkRed;
            servoOn.ForeColor = Color.White;
        }
        else
        {
            setColor(servoOn, axis.isServoOn());
            servoOn.ForeColor = Color.Black;
        }
    }

    void setColor(Label label, bool value)
    {
        label.BackColor = value ? Color.Lime : Color.White;
    }

    private void AxisMiniStatus_Click(object sender, EventArgs e)
    {
        
    }

    private void nameLabel_Click(object sender, EventArgs e)
    {
        this._SELECT = !this._SELECT;
    }
}
