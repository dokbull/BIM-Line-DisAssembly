using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class AxisLimitControl : UserControl
    {
        ProcessMain main;
        AjinMotionAxis m_axis;

        bool m_leftPress = false;
        bool m_rightPress = false;

        public AxisLimitControl()
        {
            InitializeComponent();
        }

        public void setMain(ProcessMain procMain)
        {
            main = procMain;
        }

        public void setAxis(AjinMotionAxis axis)
        {
            m_axis = axis;
        }

        private void AxisLimitControl_Load(object sender, EventArgs e)
        {
            if (m_axis == null)
                return;

            AXIS no = (AXIS)m_axis.no();
            axisGroupBox.Text = "Axis-" + no.ToString();

            axisNegLimitLabel.Text = Conf.negLimit(no).ToString("0.##");
            axisPosLimitLabel.Text = Conf.posLimit(no).ToString("0.##");

            setIconImage();
        }

        private void setIconImage()
        {
            if (m_axis == null)
                return;

            Bitmap leftIcon = null;
            Bitmap rightIcon = null;

            //if ((AXIS)m_axis.no() == AXIS.PP_ATTACH_X)
            //{
            //    leftIcon = Properties.Resources.jog_left;
            //    rightIcon = Properties.Resources.jog_right;
            //}
            //else
            //{
            //    leftIcon = Properties.Resources.jog_up;
            //    rightIcon = Properties.Resources.jog_down;
            //}

            //if (m_leftPress)
            //{
            //    if ((AXIS)m_axis.no() == AXIS.PP_ATTACH_X)
            //        leftIcon = Properties.Resources.jog_left_press;
            //    else
            //        leftIcon = Properties.Resources.jog_up_press;
            //}
            //else if (m_rightPress)
            //{
            //    if ((AXIS)m_axis.no() == AXIS.PP_ATTACH_X)
            //        rightIcon = Properties.Resources.jog_right_press;
            //    else
            //        rightIcon = Properties.Resources.jog_down_press;
            //}

            axisMinusButton.BackgroundImage = leftIcon;
            axisPlusButton.BackgroundImage = rightIcon;
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;
            if (m_axis == null)
                return;

            setIconImage();

            if (main.isSimulation() == false)
                axisCurPosLabel.Text = m_axis.pos().ToString("0.##");

            string speedText = "LOW";
            JOG_SPEED jogSpeed = main.getJogSpeed();
            if (jogSpeed == JOG_SPEED.MIDDLE)
                speedText = "MID";
            else if (jogSpeed == JOG_SPEED.HIGH)
                speedText = "HIGH";
            axisSpeedButton.Text = speedText;
        }

        private void axisMinusButton_MouseDown(object sender, MouseEventArgs e)
        {
            m_leftPress = true;

            if (m_axis == null)
                return;

            jogMove(m_axis, DIRECTION.NEGATIVE);
        }

        private void axisPlusButton_MouseDown(object sender, MouseEventArgs e)
        {
            m_rightPress = true;

            if (m_axis == null)
                return;

            jogMove(m_axis, DIRECTION.POSITIVE);
        }

        private void axisButton_MouseUp(object sender, MouseEventArgs e)
        {
            m_leftPress = false;
            m_rightPress = false;

            if (m_axis == null)
                return;

            m_axis.stop();
        }

        private void jogMove(AjinMotionAxis axis, DIRECTION dir)
        {
            AXIS axisNo = (AXIS)axis.no();
            JOG_SPEED jogSpeed = main.getJogSpeed();

            double vel = Conf.jogLow(axisNo);
            if (jogSpeed == JOG_SPEED.MIDDLE)
                vel = Conf.jogMid(axisNo);
            else if (jogSpeed == JOG_SPEED.HIGH)
                vel = Conf.jogHigh(axisNo);

            if (dir == DIRECTION.NEGATIVE)
                vel = vel * -1;

            axis.moveVel(vel, 0.2, 0.2);
        }

        private void axisSpeedButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();
            int tagValue = Util.toInt32(tag);

            tagValue++;
            if (tagValue > 2)
                tagValue = 0;

            btn.Tag = tagValue;
            JOG_SPEED jogSpeed = (JOG_SPEED)Util.toInt32(tag);

            main.setJogSpeed(jogSpeed);
        }

        private void negLimitButton_Click(object sender, EventArgs e)
        {
            bool res = checkSaveMessage();
            if (res == false)
                return;

            AXIS no = (AXIS)m_axis.no();
            double value = Util.toDouble(axisCurPosLabel.Text.ToString());
            main.writeSetupLog("Axis Limit: axis " + no.ToString() +
                " Neg(-) Limit Before:" + Conf.negLimit(no) + " After:" + value);
            Conf.setNegLimit(no, value);
            axisNegLimitLabel.Text = axisCurPosLabel.Text;
        }

        private void posLimitButton_Click(object sender, EventArgs e)
        {
            bool res = checkSaveMessage();
            if (res == false)
                return;

            AXIS no = (AXIS)m_axis.no();
            double value = Util.toDouble(axisCurPosLabel.Text.ToString());
            main.writeSetupLog("Axis Limit: axis " + no.ToString() +
                " Pos(+) Limit Before:" + Conf.posLimit(no) + " After:" + value);
            Conf.setPosLimit(no, value);
            axisPosLimitLabel.Text = axisCurPosLabel.Text;
        }

        private bool checkSaveMessage()
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "LIMIT CHANGE?", MessageBoxButtons.OKCancel);
            DialogResult res = msgBox.ShowDialog();

            return res == DialogResult.OK;
        }
    }
}
