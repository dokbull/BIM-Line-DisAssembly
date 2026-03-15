using bim_base.data.CIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bim_base.data.CIM.CIMEnumeric;

namespace bim_base
{
    public partial class FormAlarm : Form
    {
        ProcessMain main = null;

        List<int> m_alarmList = new List<int>();
                public FormAlarm(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        public void addAlarmInfo(int code, string desc)
        {
            errMsgLabel.Text = String.Format("[{0:X4}] {1} {2}", code, Alarm.messageEng(code), desc);
            errActLabel.Text = String.Format("{0}", Alarm.actionEng(code));

            updateIOMark();

        }

        private void alarmResetButton_Click(object sender, EventArgs e)
        {
            main.clearAlarm();

            errMsgLabel.Text = "";
            errActLabel.Text = "";

            updateIOMark();
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (main.isAlarm() == false)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public void setAlarmColor(Label label, bool value)
        {
            label.BackColor = value ? Color.Red : Color.White;
            label.ForeColor = value ? Color.White : Color.Black;
        }

        private void updateIOMark()
        {
            setAlarmColor(statusEMO, true);
            setAlarmColor(statusDoor, true);
        }

        private void FormSubErrorAlarm_Load(object sender, EventArgs e)
        {
            uiTimer.Enabled = true;
        }
    }
}
