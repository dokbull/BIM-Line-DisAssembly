using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormData : Form, IForm
    {
        ProcessMain main = null;

        bool swLimitClicked = false;
        CElaspedTimer swLimitTimer = new CElaspedTimer(1000 * 3);

        public FormData(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        public void onShow(bool enable)
        {

        }

        private void modelButton_Click(object sender, EventArgs e)
        {
            FormDataModel dlg = new FormDataModel(main);
            dlg.ShowDialog();
        }

        private void systemManagerButton_Click(object sender, EventArgs e)
        {

        }

        private void motorVelButton_Click(object sender, EventArgs e)
        {
            FormMotorVelocity dlg = new FormMotorVelocity(main);
            dlg.ShowDialog();

            if (main.isSimulation() == false)
            {
                main.setAbsSpeedConf(AXIS.IN_PP_Z);
                main.setAbsSpeedConf(AXIS.MOLD_PP_ZL);
                main.setAbsSpeedConf(AXIS.MOLD_PP_ZR);                
            }
        }

        private void jogVelButton_Click(object sender, EventArgs e)
        {
            FormJogVelocity dlg = new FormJogVelocity(main);
            dlg.ShowDialog();
        }

        private void pressMCButton_Click(object sender, EventArgs e)
        {
        }

        private void portSettingButton_Click(object sender, EventArgs e)
        {
            FormPortSetting dlg = new FormPortSetting(main);
            dlg.ShowDialog();
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            if (swLimitClicked)
            {
                if (swLimitTimer.isElasped() == true)
                {
                    swLimitClicked = false;
                    //clickCount = 0;
                    swLimitTimer.stop();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void softLimitLabel_DoubleClick(object sender, EventArgs e)
        {
            FormAxisLimit dlg = new FormAxisLimit(main);
            dlg.ShowDialog();
        }

        private void timerSettingButton_Click(object sender, EventArgs e)
        {
            FormTimer dlg = new FormTimer(main);
            dlg.ShowDialog();
        }       
        private void label1_Click_1(object sender, EventArgs e)
        {
            FormSetLimit formSetLimit = new FormSetLimit(main);
            formSetLimit.ShowDialog();
        }
    }
}
