using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormAxisOrigin : Form
    {
        ProcessMain main = null;

        Button[] axisButton = new Button[(int)AXIS.MAX];
        bool[] m_axisSelected = new bool[(int)AXIS.MAX];

        bool m_waitOrigin = false;
        List<AjinMiniStatus> m_axisStatusList = new List<AjinMiniStatus>();

        public FormAxisOrigin(ProcessMain _main)
        {
            InitializeComponent();

            main = _main;

            inPP_Y.setAxis(main.axis(AXIS.IN_PP_Y));
            inPP_Z.setAxis(main.axis(AXIS.IN_PP_Z));
            outPP_Y.setAxis(main.axis(AXIS.UB_PP_Y));
            outPP_Z.setAxis(main.axis(AXIS.UB_PP_Z));
            moldPP_X.setAxis(main.axis(AXIS.MOLD_PP_X));
            moldPP_Z1.setAxis(main.axis(AXIS.MOLD_PP_ZL));
            moldPP_Z2.setAxis(main.axis(AXIS.MOLD_PP_ZR));
            shuttle_X.setAxis(main.axis(AXIS.BASE_X));

            Util.findControlsByType<AjinMiniStatus, List<AjinMiniStatus>>(m_axisStatusList, this);
        }

        List<AjinMiniStatus> selectedAxis()
        {
            List<AjinMiniStatus> list = new List<AjinMiniStatus>();

            foreach (AjinMiniStatus con in m_axisStatusList)
            {
                if (con._SELECT == false)
                    continue;

                list.Add(con);
            }

            return list;
        }

        private void allSelectButton_Click(object sender, EventArgs e)
        {
            foreach (AjinMiniStatus con in m_axisStatusList)
            {
                con._SELECT = true;
            }
        }

        private void cancleAllButton_Click(object sender, EventArgs e)
        {
            foreach (AjinMiniStatus con in m_axisStatusList)
            {
                con._SELECT = false;
            }
        }

        private void servoOnButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Servo On?", MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            List<AjinMiniStatus> list = selectedAxis();

            foreach (AjinMiniStatus con in list)
            {
                AjinMotionAxis axis = con.axis();

                if (axis.isServoOn() == true)
                    continue;

                axis.setServoOn(true);
            }
        }

        private void servoOffButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Servo Off?", MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            List<AjinMiniStatus> list = selectedAxis();

            foreach (AjinMiniStatus con in list)
            {
                AjinMotionAxis axis = con.axis();

                if (axis.isServoOn() == false)
                    continue;

                axis.setServoOn(false);
            }
        }

        private void execOriginButton_Click(object sender, EventArgs e)
        {
            if (main.isSimulation() == true)
                return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, PopupMessage.message(POPUP.ORIGIN_START), MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            List<ExtAxis> selectList = new List<ExtAxis>();

            foreach (AjinMiniStatus con in m_axisStatusList)
                selectList.Add((ExtAxis)con.axis());

            main.procOrg().start(selectList);

            List<string> axisTexts = new List<string>();
            if (m_axisSelected[0] == true || m_axisSelected[1] == true) axisTexts.Add("IN PP");
            if (m_axisSelected[2] == true || m_axisSelected[3] == true) axisTexts.Add("OUT PP");
            if (m_axisSelected[4] == true || m_axisSelected[5] == true || m_axisSelected[6] == true) axisTexts.Add("MOLD PP");
            if (m_axisSelected[7] == true) axisTexts.Add("SHUTTLE");

            string text = string.Join(",", axisTexts);
            main.writeBottomHistory(text + " Origin Start");

            m_waitOrigin = true;
        }

        private void resetAlarmButton_Click(object sender, EventArgs e)
        {
            if (main.isSimulation() == true)
                return;

            for (int i = 0; i < axisButton.Length; i++)
            {
                ExtAxis axis = main.axis(i);

                //FIXME@tmdwn..
                //if (axis.isAlarm() == true)
                    axis.alarmClear();
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            foreach (AjinMiniStatus con in m_axisStatusList)
            {
                con.update();
            }

            if (m_waitOrigin == true)
            {
                allSelectButton.Enabled = false;
                cancelAllButton.Enabled = false;
                servoOnButton.Enabled = false;
                servoOffButton.Enabled = false;
                exitButton.Enabled = false;

                execOriginButton.BackColor = Color.Yellow;

                if (main.procOrg().isComplete() == true)
                {
                    m_waitOrigin = false;

                    enableSoftLimit();

                    CMessageBox msgBox = new CMessageBox(Common.TITLE, PopupMessage.message(POPUP.ORIGIN_COMPLETE), MessageBoxButtons.OK);
                    msgBox.showDialog();
                }
                else if (main.procOrg().isRun() == false)
                {
                    m_waitOrigin = false;

                    enableSoftLimit();

                    CMessageBox msgBox = new CMessageBox(Common.TITLE, PopupMessage.message(POPUP.ORIGIN_FAIL), MessageBoxButtons.OK);
                    msgBox.showDialog();
                }
            }
            else
            {
                allSelectButton.Enabled = true;
                cancelAllButton.Enabled = true;
                servoOnButton.Enabled = true;
                servoOffButton.Enabled = true;
                exitButton.Enabled = true;

                execOriginButton.BackColor = Color.White;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSubAxisOrigin_Load(object sender, EventArgs e)
        {
            uiTimer.Enabled = true;
        }

        private void disableSoftLimit()
        {
            main.axis(AXIS.IN_PP_Y).disableSoftLimit();
            main.axis(AXIS.IN_PP_Z).disableSoftLimit();
            main.axis(AXIS.MOLD_PP_X).disableSoftLimit();
        }

        private void enableSoftLimit()
        {
            int cnt = (int)AXIS.MAX;
            AXM_SOFT_LIMIT[] limit = new AXM_SOFT_LIMIT[cnt];

            for (int i = 0; i < cnt; i++)
            {
                limit[i] = new AXM_SOFT_LIMIT();

                //TODO@damkina set sw limit param

                main.axis((AXIS)i).setSoftLimit(limit[i]);
            }
        }
    }
}
