using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormData : Form, IForm
    {
        ProcessMain main = null;

        bool swLimitClicked = false;
        int clickCount = 0;
        CElaspedTimer swLimitTimer = new CElaspedTimer(1000 * 3);

        public event EventHandler modelButton_Clicked;
        public event EventHandler systemManagerButton_Clicked;
        public event EventHandler motorVelButton_Clicked;
        public event EventHandler jogVelButton_Clicked;
        public event EventHandler portSettingButton_Clicked;


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
            if (modelButton_Clicked != null)
                modelButton_Clicked(this, e);
        }

        private void systemManagerButton_Click(object sender, EventArgs e)
        {
            if (systemManagerButton_Clicked != null)
                systemManagerButton_Clicked(this, e);
        }

        private void motorVelButton_Click(object sender, EventArgs e)
        {
            if (main.isSimulation() == false)
            {
                main.setAbsSpeedConf(AXIS.IN_PP_Y);
                main.setAbsSpeedConf(AXIS.IN_PP_Z);
                main.setAbsSpeedConf(AXIS.MOLD_PP_X);
            }

            if (motorVelButton_Clicked != null)
                motorVelButton_Clicked(this, e);
        }

        private void jogVelButton_Click(object sender, EventArgs e)
        {
            if (jogVelButton_Clicked != null)
                jogVelButton_Clicked(this, e);
        }
        private void portSettingButton_Click(object sender, EventArgs e)
        {
            if (portSettingButton_Clicked != null)
                portSettingButton_Clicked(this, e);
        }

        private void swLimitLabel_Click(object sender, EventArgs e)
        {
            if (clickCount == 0)
                swLimitTimer.start();

            swLimitClicked = true;
            clickCount++;

            if (clickCount >= 3)
            {
                swLimitClicked = false;
                clickCount = 0;
                Debug.debug("FormData::setSWLimit_Click");

                main.axis(AXIS.IN_PP_Y).disableSoftLimit();
                main.axis(AXIS.IN_PP_Z).disableSoftLimit();
                main.axis(AXIS.MOLD_PP_X).disableSoftLimit();

                FormAxisLimit dlg = new FormAxisLimit(main);
                dlg.ShowDialog();

                //main.axis(AXIS.X).setSoftLimit(true, Conf.LIMIT_POS_X, Conf.LIMIT_NEG_X);
                //main.axis(AXIS.Y).setSoftLimit(true, Conf.LIMIT_POS_Y, Conf.LIMIT_NEG_Y);
                //main.axis(AXIS.Z).setSoftLimit(true, Conf.LIMIT_POS_Z, Conf.LIMIT_NEG_Z);
            }
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
                    clickCount = 0;
                    swLimitTimer.stop();
                }
            }
        }
    }
}
