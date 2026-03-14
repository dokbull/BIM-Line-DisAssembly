using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class AxisControlPaixRtex : UserControl
{
    PaixRtexMotionAxis m_axis = null;
        
    Button[] m_speedButton = new Button[3];
    static int[] m_jogSpeed = new int[] {1, 5, 10};
    int m_speed = 0;

    public AxisControlPaixRtex()
    {
        InitializeComponent();
    }

    private void AxisControl_Load(object sender, EventArgs e)
    {
        m_speedButton[0] = jogSpeed1Button;
        m_speedButton[1] = jogSpeed2Button;
        m_speedButton[2] = jogSpeed3Button;

        selectSpeedButton(SPEED.LOW);
    }

    public void setParameter(PaixRtexMotionAxis axis)
    {
        m_axis = axis;
        nameLabel.Text = axis.name() + "(" + axis.no() + ")";
    }

    public void updateStatus()
    {
        if (m_axis == null)
            return;

        PaixRtexMotionAxis axis = m_axis;

        nowPositionLabel.Text = axis.pos().ToString("0.00");

        setColor(readyLabel, axis.ready());
        setColor(busyLabel, axis.busy());
        setColor(inposLabel, axis.inpos());
        setColor(minusLimitLabel, axis.negLimit());
        setColor(plusLimitLabel, axis.posLimit());
        setColor(orgLabel, axis.orgSensor());

        if (axis.alarm())
            alarmLabel.BackColor = Color.DarkRed;
        else
            alarmLabel.BackColor = Color.Transparent;

        bool servoOn = m_axis.servoOn();

        setColor(servoOnButton, servoOn);
        setColor(servoOffButton, !servoOn);

        double loadRatio = axis.loadRatio();
        loadRatioLabel.Text = loadRatio.ToString("0") + "%";

        setColor(orgButton, axis.orgComplete());

        if(m_jogSpeed != null)
        {
            jogSpeed1Button.Text = $@"저속 ({m_jogSpeed[0]})";
            jogSpeed2Button.Text = $@"중속 ({m_jogSpeed[1]})";
            jogSpeed3Button.Text = $@"고속 ({m_jogSpeed[2]})";
        }

        this.Refresh();
    }

    public void setOriginEnnable(bool value)
    {
        orgButton.Enabled = value;
    }

    void setColor(Control con, bool value)
    {
        if (value == true)
        {
            if (con.BackColor == Color.Lime)
                return;

            con.BackColor = Color.Lime;
            return;
        }

        if (con.BackColor == Color.Transparent)
            return;

        con.BackColor = Color.Transparent;
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
        m_speed = m_jogSpeed[(int)speed];
    }

    private void absMoveButton_Click(object sender, EventArgs e)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE,
            "Would you like to move ?", MessageBoxButtons.OKCancel);

        DialogResult result = msgBox.ShowDialog();

        if (result == DialogResult.Cancel)
            return;

        double pos = 0;

        try
        {
            pos = Util.toDouble(absMovePosTextBox.Text);
        }
        catch (Exception ex)
        {
            Debug.warning("AxisControl::absMoveButton_Click reason:" + ex.Message);
            return;
        }

        double speed = Util.toDouble(speedTextBox.Text, 0.0d);
        m_axis.setAbsSpeed(speed, 0.1, 0.1);
        m_axis.absMove(pos);
    }

    private void resetAlarmButton_Click(object sender, EventArgs e)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE,
            "알람 리셋을 수행하시겠습니까 ?" + "\r\nDo you want to perform an alarm reset ?", MessageBoxButtons.OKCancel);

        DialogResult result = msgBox.ShowDialog();

        if (result == DialogResult.Cancel)
            return;

        if (m_axis == null)
            return;

        m_axis.setAlarmResetReq();
    }

    private void orgButton_Click(object sender, EventArgs e)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE,
            "원점 수행 하시겠습니까 ?" + "\r\nDo you want to perform the origin ?", MessageBoxButtons.OKCancel);

        DialogResult result = msgBox.ShowDialog();

        if (result == DialogResult.Cancel)
            return;

        if (m_axis == null)
            return;

        CMessageBox dirBox = new CMessageBox(Common.TITLE,
            "원점 진행 방향이 NEG 입니까?" + "\r\n(POS 방향일 경우 NO)", MessageBoxButtons.YesNo);

        DialogResult check = dirBox.ShowDialog();

        PAIX_DIR dir = PAIX_DIR.CCW;

        if (check == DialogResult.Cancel)
            dir = PAIX_DIR.CW;

        m_axis.setHomeSpeed(10, 5, 1, 10);
        m_axis.homeMoveHome(dir, 0.0d);
    }

    private void servoOnButton_Click(object sender, EventArgs e)
    {
        m_axis.setServoOn(true);
    }

    private void servoOffButton_Click(object sender, EventArgs e)
    {
        m_axis.setServoOn(false);
    }

    private void emgStopButton_Click(object sender, EventArgs e)
    {
        m_axis.suddenStop();
    }

    private void jogSpeed1Button_Click(object sender, EventArgs e)
    {
        selectSpeedButton(SPEED.LOW);
    }

    private void jogSpeed2Button_Click(object sender, EventArgs e)
    {
        selectSpeedButton(SPEED.MID);
    }

    private void jogSpeed3Button_Click(object sender, EventArgs e)
    {
        selectSpeedButton(SPEED.HIGH);
    }

    private void minusButton_MouseDown(object sender, MouseEventArgs e)
    {
        if (m_axis.alarm()) return;
        if (m_axis.emergency()) return;
        if (m_axis.servoOn() == false) return;
        //if (data.lowLimit == true) return;

        double speed = Util.toDouble(speedTextBox.Text, 0.0d);
        m_axis.setSpeed(1, 0.1d, 0.1d, speed);
        m_axis.velMove(PAIX_DIR.CCW);
    }

    private void minusButton_MouseUp(object sender, MouseEventArgs e)
    {
        m_axis.stop();
    }

    private void plusButton_MouseDown(object sender, MouseEventArgs e)
    {
        if (m_axis.alarm()) return;
        if (m_axis.emergency()) return;
        if (m_axis.servoOn() == false) return;
        //lif (data.highLimit == true) return;

        double speed = Util.toDouble(speedTextBox.Text, 0.0d);
        m_axis.setSpeed(1, 0.1d, 0.1d, speed);
        m_axis.velMove(PAIX_DIR.CW);
    }

    private void plusButton_MouseUp(object sender, MouseEventArgs e)
    {
        m_axis.stop();
    }

    private void minusButton_Click(object sender, EventArgs e)
    {

    }

    MOTION_ARROW m_minusArrow = MOTION_ARROW.LEFT;
    MOTION_ARROW m_plusArrow = MOTION_ARROW.RIGHT;

    public void setMinusArrow(MOTION_ARROW arrow)
    {
        m_minusArrow = arrow;
        minusButton.Text = "JOG (-)" + "\r\n" + arrowToString(arrow);
    }

    public void setPlusArrow(MOTION_ARROW arrow)
    {
        m_plusArrow = arrow;
        plusButton.Text = "JOG (+)" + "\r\n" + arrowToString(arrow);
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

    private void plusButton_Click(object sender, EventArgs e)
    {

    }

    private void speedTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    public void setJogSpeed(int index, int speed)
    {
        if (index < 0 || index > m_jogSpeed.Length)
        {
            Debug.warning("AxisControlPaix::setJogSpeed failed. index:" + index);
            return;
        }

        m_jogSpeed[index] = speed;
    }
} // class
