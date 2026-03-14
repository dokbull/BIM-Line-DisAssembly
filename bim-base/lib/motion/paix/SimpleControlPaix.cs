using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class SimpleControl : UserControl
{
    string m_name = "";

    Button[] m_speedButton = new Button[3];
    public int[] m_jogSpeed = new int[] { 1, 10, 50 };

    PaixMotionAxis m_axis = null;

    List<Control> m_allControl = new List<Control>();

    public SimpleControl()
    {
        InitializeComponent();
    }

    private void SimpleControl_Load(object sender, EventArgs e)
    {
        m_speedButton[0] = speedLowButton;
        m_speedButton[1] = speedMidButton;
        m_speedButton[2] = speedHighButton;

        m_allControl.Add(speedLowButton);
        m_allControl.Add(speedMidButton);
        m_allControl.Add(speedHighButton);

        m_allControl.Add(jogNegButton);
        m_allControl.Add(jogPosButton);
        m_allControl.Add(moveButton);
        m_allControl.Add(stopButton);

        m_allControl.Add(speedTextBox);
        m_allControl.Add(movePosTextBox);

        selectSpeedButton(SPEED.LOW);
    }

    public void setParameter(PaixMotionAxis axis)
    {
        if (axis == null)
            return;

        m_name = axis.name();
        m_axis = axis;

        nameLabel.Text = m_name;
    }

    bool moveVel(double vel, double acc, double dece, bool empty = false)
    {
        m_axis.setSpeed(1, acc, dece, Math.Abs(vel));

        if (vel < 0)
            m_axis.jogMove(PAIX_DIR.CCW);
        else
            m_axis.jogMove(PAIX_DIR.CW);

        return true;
    }

    public void setButtonBackColor(Color color)
    {
        foreach (Control con in m_allControl)
            con.BackColor = color;
    }

    public void setButtonForeColor(Color color)
    {
        foreach (Control con in m_allControl)
            con.ForeColor = color;
    }

    public int axisNo()
    {
        return m_axis.no();
    }

    public int arrNo()
    {
        return m_axis.arrNo();
    }

    public void updateStatus()
    {
        if (m_axis == null)
            return;

        setBackColor(limitNegLabel, m_axis.negLimit());
        setBackColor(limitPosLabel, m_axis.posLimit());
        setBackColor(orgLabel, m_axis.orgSensor());

        if (m_axis.alarm() == true)
        {
            alarmLabel.BackColor = Color.DarkRed;
            //FIXME@tmdwn
            //alarmLabel.Text = m_axis.alarmCode + "";
        }
        else
            alarmLabel.BackColor = Color.Transparent;

        loadPerLabel.Text = "";

        nowPosLabel.Text = m_axis.pos().ToString("0.000");

        if (m_axis.ready())
        {
            nameLabel.BackColor = Color.Lime;
            nameLabel.ForeColor = Color.Black;
        }
        else
        {
            nameLabel.BackColor = Color.DarkRed;
            nameLabel.ForeColor = Color.White;
        }

        loadPerLabel.Text = m_axis.loadRatio().ToString("0") + "%";
    }

    void setBackColor(Label label, bool value)
    {
        if (value == false)
        {
            if (label.BackColor == Color.Transparent)
                return;

            label.BackColor = Color.Transparent;
            return;
        }

        if (label.BackColor == Color.Lime)
            return;

        label.BackColor = Color.Lime;
    }

    void selectSpeedButton(SPEED speed)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((int)speed == i)
                m_speedButton[i].BackColor = Color.Lime;
            else
                m_speedButton[i].BackColor = Color.White;
        }

        speedTextBox.Text = m_jogSpeed[(int)speed] + "";
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void button4_Click(object sender, EventArgs e)
    {
        m_axis.stop();
    }

    private void speedLowButton_Click(object sender, EventArgs e)
    {
        selectSpeedButton(SPEED.LOW);
    }

    private void speedMidButton_Click(object sender, EventArgs e)
    {
        selectSpeedButton(SPEED.MID);
    }

    private void speedHighButton_Click(object sender, EventArgs e)
    {
        selectSpeedButton(SPEED.HIGH);
    }

    public virtual void jogNegButton_MouseDown(object sender, MouseEventArgs e)
    {
        if (m_axis.alarm()) return;
        if (m_axis.emergency()) return;
        if (m_axis.servoOn() == false) return;
        //if (data.lowLimit == true) return;

        double speed = Util.toDouble(speedTextBox.Text, 0.0d);
        moveVel(speed * -1, 0.1d, 0.1d);
    }

    private void jogNegButton_MouseUp(object sender, MouseEventArgs e)
    {
        m_axis.stop();
    }

    public virtual void jogPosButton_MouseDown(object sender, MouseEventArgs e)
    {
        if (m_axis.alarm()) return;
        if (m_axis.emergency()) return;
        if (m_axis.servoOn() == false) return;
        //lif (data.highLimit == true) return;

        double speed = Util.toDouble(speedTextBox.Text, 0.0d);
        moveVel(speed, 0.1d, 0.1d);
    }

    private void jogPosButton_MouseUp(object sender, MouseEventArgs e)
    {
        m_axis.stop();
    }

    private void label8_Click(object sender, EventArgs e)
    {

    }

    private void orgLabel_Click(object sender, EventArgs e)
    {

    }


    public virtual void moveButton_Click(object sender, EventArgs e)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE,
            "이동 하시겠습니까 ?" + "\r\nDo you want to perform the move ?", MessageBoxButtons.OKCancel);

        if (msgBox.showDialog() == false)
            return;

        double value = Util.toDouble(movePosTextBox.Text, 0.0d);
        m_axis.absMove(value);
    }

    MOTION_ARROW m_minusArrow = MOTION_ARROW.LEFT;
    MOTION_ARROW m_plusArrow = MOTION_ARROW.RIGHT;

    public void setJogArrow(MOTION_ARROW neg, MOTION_ARROW pos)
    {
        setMinusArrow(neg);
        setPlusArrow(pos);
    }

    public void setMinusArrow(MOTION_ARROW arrow)
    {
        m_minusArrow = arrow;
        jogNegButton.Text = "JOG (-)" + "\r\n" + arrowToString(arrow);
    }

    public void setPlusArrow(MOTION_ARROW arrow)
    {
        m_plusArrow = arrow;
        jogPosButton.Text = "JOG (+)" + "\r\n" + arrowToString(arrow);
    }

    public string arrowToString(MOTION_ARROW arrow)
    {
        if (arrow == MOTION_ARROW.LEFT)
            return "◀";
        else if (arrow == MOTION_ARROW.RIGHT)
            return "▶";
        else if (arrow == MOTION_ARROW.UP)
            return "▲";
        else if (arrow == MOTION_ARROW.DOWN)
            return "▼";
        else if (arrow == MOTION_ARROW.Z_UP)
            return "△";
        else if (arrow == MOTION_ARROW.Z_DOWN)
            return "▽";

        return "";
    }

    private void speedTextBox_TextChanged(object sender, EventArgs e)
    {
        CTextBox textBox = (CTextBox)sender;

        double value = Convert.ToDouble(textBox.Text);

        if (value > 100.0d)
            value = 100.0d;

        textBox.Text = value.ToString("0");
    }

    private void stopButton_Click(object sender, EventArgs e)
    {
        m_axis.stop();
    }
}