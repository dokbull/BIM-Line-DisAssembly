using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public enum DIRECTION
    {
        POSITIVE,
        NEGATIVE,
    }
    public partial class JogControl : UserControl
    {
        ProcessMain main = null;
        List<ExtAxis> m_axis = new List<ExtAxis>();
        JOG_SPEED m_jogSpeed = 0;
        DIRECTION m_direction = DIRECTION.POSITIVE;

        ExtAxis X1, Y1, Z1, X2;

        public JogControl()
        {
            InitializeComponent();
        }

        public void setMain(ProcessMain procMain) { main = procMain; }
        public void setDirection(DIRECTION direction) { m_direction = direction; }
        public void setAxis(List<ExtAxis> axisList)
        {
            m_axis = axisList;

            jogYUpLabel.Visible = false;
            jogYDownLabel.Visible = false;
            jogXLeftLabel.Visible = false;
            jogXRightLabel.Visible = false;
            jogX2LeftLabel.Visible = false;
            jogX2RightLabel.Visible = false;
            jogZDownLabel.Visible = false;
            jogZUpLabel.Visible = false;

            jogYUpButton.Visible = false;
            jogYDownButton.Visible = false;
            jogXLeftButton.Visible = false;
            jogXRightButton.Visible = false;
            jogX2LeftButton.Visible = false;
            jogX2RightButton.Visible = false;
            jogZDownButton.Visible = false;
            jogZUpButton.Visible = false;

            foreach (ExtAxis axis in axisList)
            {
                AXIS no = (AXIS)axis.no();
                switch (no)
                {
                    case AXIS.MOLD_PP_X:
                        {
                            jogXLeftLabel.Visible = true;
                            jogXRightLabel.Visible = true;

                            jogXLeftButton.Visible = true;
                            jogXRightButton.Visible = true;

                            X1 = axis;
                        }
                        break;

                    case AXIS.BASE_X:
                        {
                            jogX2LeftLabel.Visible = true;
                            jogX2RightLabel.Visible = true;

                            jogX2LeftButton.Visible = true;
                            jogX2RightButton.Visible = true;

                            X2 = axis;
                        }
                        break;

                    case AXIS.IN_PP_Y:
                    case AXIS.UB_PP_Y:
                        {

                            jogYUpLabel.Visible = true;
                            jogYDownLabel.Visible = true;

                            jogYUpButton.Visible = true;
                            jogYDownButton.Visible = true;

                            Y1 = axis;
                        }
                        break;

                    case AXIS.IN_PP_Z:
                    case AXIS.UB_PP_Z:
                    case AXIS.MOLD_PP_ZR:
                        {
                            jogZDownLabel.Visible = true;
                            jogZUpLabel.Visible = true;

                            jogZDownButton.Visible = true;
                            jogZUpButton.Visible = true;

                            Z1 = axis;
                        }
                        break;

                    case AXIS.MOLD_PP_ZL:
                        {
                            jogYUpLabel.Visible = true;
                            jogYDownLabel.Visible = true;

                            jogYUpLabel.Text = "Z UP";
                            jogYDownLabel.Text = "Z DN";

                            jogYUpButton.Visible = true;
                            jogYDownButton.Visible = true;

                            Y1 = axis;
                        }
                        break;
                }
            }
        }

        private int checkAxis(ExtAxis axis)
        {
            int axisNo = -1;

            foreach (ExtAxis item in m_axis)
            {
                if (item.no() == axis.no())
                {
                    axisNo = item.no();
                    break;
                }

                if (axisNo >= 0)
                    break;
            }

            return axisNo;
        }

        private void jogMove(ExtAxis axis, int dir)
        {
            double vel = 0.0d;

            AXIS axisNo = (AXIS)axis.no();

            if (m_jogSpeed == JOG_SPEED.LOW)
                vel = Conf.jogLow(axisNo);
            else if (m_jogSpeed == JOG_SPEED.MIDDLE)
                vel = Conf.jogMid(axisNo);
            else if (m_jogSpeed == JOG_SPEED.HIGH)
                vel = Conf.jogHigh(axisNo);

            if (m_direction == DIRECTION.NEGATIVE)
                dir = dir * -1;

            axis.moveVel(vel * dir, 0.2, 0.2);
        }

        private void jogYButton_MouseDown(object sender, MouseEventArgs e)
        {
            int axisNo = checkAxis(Y1);

            if (main.axis(axisNo) == null)
                return;

            Button btn = (Button)sender;
            int dir = Util.toInt32(btn.Tag.ToString());

            jogMove(main.axis(axisNo), dir);
        }

        private void jogXButton_MouseDown(object sender, MouseEventArgs e)
        {
            int axisNo = checkAxis(X1);

            if (main.axis(axisNo) == null)
                return;

            Button btn = (Button)sender;
            int dir = Util.toInt32(btn.Tag.ToString());

            jogMove(main.axis(axisNo), dir);
        }

        private void jogZButton_MouseDown(object sender, MouseEventArgs e)
        {
            int axisNo = checkAxis(Z1);

            if (main.axis(axisNo) == null)
                return;

            Button btn = (Button)sender;
            int dir = Util.toInt32(btn.Tag.ToString());

            jogMove(main.axis(axisNo), dir);
        }

        private void jogButton_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < (int)AXIS.MAX; i++)
            {
                ExtAxis axis = main.axis(i);
                if (axis == null)
                    continue;

                axis.stop();
            }
        }

        private void jogButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();

            m_jogSpeed = (JOG_SPEED)Util.toInt32(tag);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            jogLowButton.BackColor = (m_jogSpeed == JOG_SPEED.LOW) ? Color.Lime : Color.White;
            jogMidButton.BackColor = (m_jogSpeed == JOG_SPEED.MIDDLE) ? Color.Lime : Color.White;
            jogHighButton.BackColor = (m_jogSpeed == JOG_SPEED.HIGH) ? Color.Lime : Color.White;
        }

        private void jogX2Button_MouseDown(object sender, MouseEventArgs e)
        {
            int axisNo = checkAxis(X2);

            if (main.axis(axisNo) == null)
                return;

            Button btn = (Button)sender;
            int dir = Util.toInt32(btn.Tag.ToString());

            jogMove(main.axis(axisNo), dir);
        }
    }
}
