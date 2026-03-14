using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormDataMotorVel : Form
    {
        ProcessMain main = null;

        public FormDataMotorVel(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void FormSubMotorVelocity_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("SAVE?");

            if (ret == false)
                return;

            Conf.setVel(AXIS.IN_PP_Y, Util.toDouble(xVelLabel.Text));
            Conf.setVel(AXIS.IN_PP_Z, Util.toDouble(yVelLabel.Text));
            Conf.setVel(AXIS.MOLD_PP_X, Util.toDouble(zVelLabel.Text));

            Conf.setAcc(AXIS.IN_PP_Y, Util.toDouble(xVelLabel.Text));
            Conf.setAcc(AXIS.IN_PP_Z, Util.toDouble(yVelLabel.Text));
            Conf.setAcc(AXIS.MOLD_PP_X, Util.toDouble(zVelLabel.Text));

            CMessageBox.showInfo(MessageText.saveMessage);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Reload?");

            if (ret == false)
                return;

            loadData();
        }

        private void loaderLabel_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;

            string curValue = lb.Text;
            FormNumpad dlg = new FormNumpad(curValue, false);
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                int value = Util.toInt32(dlg.getNewValue());

                if (value > 4000)
                    value = 4000;

                if (value < 100)
                    value = 100;

                lb.Text = dlg.getNewValue();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (checkChange() == true)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE,
                    "Unsaved data exists.\r\n Do you want to quit?",
                    MessageBoxButtons.OKCancel);

                if (msgBox.showDialog() == false)
                    return;
            }
            this.Close();
        }

        private bool checkChange()
        {
            double xVel = Util.toDouble(xVelLabel.Text);
            double yVel = Util.toDouble(yVelLabel.Text);
            double zVel = Util.toDouble(zVelLabel.Text);

            double xAcc = Util.toDouble(xAccLabel.Text);
            double yAcc = Util.toDouble(yAccLabel.Text);
            double zAcc = Util.toDouble(zAccLabel.Text);

            if (xVel != Conf.vel(AXIS.IN_PP_Y)) return true;
            if (yVel != Conf.vel(AXIS.IN_PP_Z)) return true;
            if (zVel != Conf.vel(AXIS.MOLD_PP_X)) return true;

            if (xAcc != Conf.acc(AXIS.IN_PP_Y)) return true;
            if (yAcc != Conf.acc(AXIS.IN_PP_Z)) return true;
            if (zAcc != Conf.acc(AXIS.MOLD_PP_X)) return true;

            return false;
        }

        private void loadData()
        {
            xVelLabel.Text = Conf.vel(AXIS.IN_PP_Y).ToString();
            yVelLabel.Text = Conf.vel(AXIS.IN_PP_Z).ToString();
            zVelLabel.Text = Conf.vel(AXIS.MOLD_PP_X).ToString();

            xAccLabel.Text = Conf.acc(AXIS.IN_PP_Y).ToString();
            yAccLabel.Text = Conf.acc(AXIS.IN_PP_Z).ToString();
            zAccLabel.Text = Conf.acc(AXIS.MOLD_PP_X).ToString();
        }
    }
}
