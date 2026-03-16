using bim_base.data.CIM;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormTop : Form
    {
        ProcessMain main;

        bool infoLabelClicked = false;
        int clickCount = 0;
        CElaspedTimer clickTimer = new CElaspedTimer(1000 * 3);
        public FormTop(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void bottomButtonClick(object sender, EventArgs e)
        {
            switch ((PAGE)sender)
            {
                case PAGE.AUTO:
                {
                    lbScreenNo.Text = "1";
                    lbScreenName.Text = "Auto Screen";
                    break;
                }
                case PAGE.MANUAL:
                {
                    lbScreenNo.Text = "1000";
                    lbScreenName.Text = "Manual Screen";
                    break;
                }
                case PAGE.TEACH:
                {
                    lbScreenNo.Text = "2000";
                    lbScreenName.Text = "Teach Screen";
                    break;
                }
                case PAGE.DATA:
                {
                    lbScreenNo.Text = "3000";
                    lbScreenName.Text = "Data Screen";
                    break;
                }
                case PAGE.LOG:
                {
                    lbScreenNo.Text = "4000";
                    lbScreenName.Text = "Alarm Screen";
                    break;
                }
                case PAGE.MONITOR:
                {
                    lbScreenNo.Text = "6000";
                    lbScreenName.Text = "Monitor Screen";
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //modelNameLabel.Text = $"Model Name : {Conf.CURR_MODEL}";

            lbVersion.Text = Common.SHOT_VERSION;
            lbCurrentDate.Text = DateTime.Now.ToString("yyyy . MM . dd");
            lbCurrentTime.Text = DateTime.Now.ToString("HH : mm : ss");
            if (main.isAdmin() == false)
            {
                lbProjectName.Text = Common.TITLE;
                lbProjectName.ForeColor = Color.LimeGreen;
            }
            else
            {
                lbProjectName.Text = "ADMIN MODE";
                lbProjectName.ForeColor = Color.Red;
            }

            if (infoLabelClicked)
            {
                if (clickTimer.isElasped() == true)
                {
                    infoLabelClicked = false;
                    clickCount = 0;
                    clickTimer.stop();
                }
            }

            //scanTimeLabel.Text = main.scanTime().ToString() + "ms";
        }

        private void FormTop_Load(object sender, EventArgs e)
        {
            lbProjectName.Text = Common.TITLE;

            FormMain.inst().m_formBottom.bottomButtonClick += bottomButtonClick;

        }


        private void projectNameLabel_DoubleClick(object sender, EventArgs e)
        {

        }

        private void infoLabel_Click(object sender, EventArgs e)
        {
            if (clickCount == 0)
                clickTimer.start();

            infoLabelClicked = true;
            clickCount++;

            if (clickCount >= 2)
            {
                infoLabelClicked = false;
                clickCount = 0;

                FormMain.inst().changeFormBorderStyle();
            }
        }

        private void projectNameLabel_Click(object sender, EventArgs e)
        {
            //FormStep dlg = new FormStep(main);
            //dlg.TopMost = true;
            //dlg.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //FormPassword passDlg = new FormPassword(main);
            //if (passDlg.ShowDialog() != DialogResult.OK)
            //    return;

            //if (main.isAdmin() == false)
            //{
            //    main.setAdmin(true);
            //    return;
            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FormStep dlg = new FormStep(main);
            dlg.TopMost = true;
            dlg.ShowDialog();
        }

        private void lbPPID_Click(object sender, EventArgs e)
        {
            if (main.isAuto()) return;
            using (var dlg = new FormKeyboard())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    lbPPID.Text = dlg.getKeyword().ToUpper();
            }
        }

        private void lbEQNo_Click(object sender, EventArgs e)
        {
            if (main.isAuto()) return;
            using (var dlg = new FormNumpad(lbEQNo.Text, false))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    lbEQNo.Text = dlg.getNewValue();
            }
        }
    }
}
