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

        private void timer1_Tick(object sender, EventArgs e)
        {
            modelNameLabel.Text = Common.MODEL_INFO.currentModelName();
            timeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

            if (main.isAdmin() == false)
            {
                projectNameLabel.Text = Common.TITLE;
                projectNameLabel.ForeColor = Color.White;
            }
            else
            {
                projectNameLabel.Text = "ADMIN MODE";
                projectNameLabel.ForeColor = Color.Red;
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
        }

        private void FormTop_Load(object sender, EventArgs e)
        {
            projectNameLabel.Text = Common.TITLE;
            versionLabel.Text = Common.VERSION;
        }

        private void projectNameLabel_DoubleClick(object sender, EventArgs e)
        {
            if (main.isAdmin() == true)
            {
                main.setAdmin(false);
                return;
            }

            FormPassword passDlg = new FormPassword(main);
            if (passDlg.ShowDialog() != DialogResult.OK)
                return;

            main.setAdmin(true);
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

        private void mxLabel_Click(object sender, EventArgs e)
        {
            FormStep dlg = new FormStep(main);
            dlg.ShowDialog();
        }
    }
}
