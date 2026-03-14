using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormMotorVelocity : Form
    {
        ProcessMain main = null;
        bool m_checkChange = false;

        public FormMotorVelocity(ProcessMain procMain)
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

            Conf.setVel(AXIS.IN_PP_Z, Util.toDouble(InTransferZVelLabel.Text));
            Conf.setVel(AXIS.MOLD_PP_ZL, Util.toDouble(OutTransferZVelLabel.Text));
            Conf.setVel(AXIS.MOLD_PP_ZR, Util.toDouble(UnloadYVelLabel.Text));

            Conf.setAcc(AXIS.IN_PP_Z, Util.toDouble(InTransferZAccLabel.Text));
            Conf.setAcc(AXIS.MOLD_PP_ZL, Util.toDouble(OutTransferZAccLabel.Text));
            Conf.setAcc(AXIS.MOLD_PP_ZR, Util.toDouble(UnloadYAccLabel.Text));

            Conf.setDec(AXIS.IN_PP_Z, Util.toDouble(InTransferZAccLabel.Text));
            Conf.setDec(AXIS.MOLD_PP_ZL, Util.toDouble(OutTransferZAccLabel.Text));
            Conf.setDec(AXIS.MOLD_PP_ZR, Util.toDouble(UnloadYAccLabel.Text));

            main.axis(AXIS.IN_PP_Z).setAbsSpeed(Conf.vel(AXIS.IN_PP_Z), Conf.acc(AXIS.IN_PP_Z) / 1000.0d, Conf.acc(AXIS.IN_PP_Z) / 1000.0d);
            main.axis(AXIS.MOLD_PP_ZL).setAbsSpeed(Conf.vel(AXIS.MOLD_PP_ZL), Conf.acc(AXIS.MOLD_PP_ZL) / 1000.0d, Conf.acc(AXIS.MOLD_PP_ZL) / 1000.0d);
            main.axis(AXIS.MOLD_PP_ZR).setAbsSpeed(Conf.vel(AXIS.MOLD_PP_ZR), Conf.acc(AXIS.MOLD_PP_ZR) / 1000.0d, Conf.acc(AXIS.MOLD_PP_ZR) / 1000.0d);

            m_checkChange = false;

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

                if (value.ToString() != curValue)
                    m_checkChange = true;

                if (value > 2500)
                    value = 2500;

                if (value < 100)
                    value = 100;

                lb.Text = dlg.getNewValue();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (m_checkChange == true)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE,
                    "Unsaved data exists.\r\n Do you want to quit?",
                    MessageBoxButtons.OKCancel);

                if (msgBox.showDialog() == false)
                    return;
            }
            this.Close();
            m_checkChange = false;
        }

        private bool checkChange()
        {
            double xVel = Util.toDouble(InTransferZVelLabel.Text);
            double yVel = Util.toDouble(OutTransferZVelLabel.Text);
            double zVel = Util.toDouble(UnloadYVelLabel.Text);

            double xAcc = Util.toDouble(InTransferZAccLabel.Text);
            double yAcc = Util.toDouble(OutTransferZAccLabel.Text);
            double zAcc = Util.toDouble(UnloadYAccLabel.Text);

            if (xVel != Conf.vel(AXIS.IN_PP_Z)) return true;
            if (yVel != Conf.vel(AXIS.MOLD_PP_ZL)) return true;
            if (zVel != Conf.vel(AXIS.MOLD_PP_ZR)) return true;


            if (xAcc != Conf.acc(AXIS.IN_PP_Z)) return true;
            if (yAcc != Conf.acc(AXIS.MOLD_PP_ZL)) return true;
            if (zAcc != Conf.acc(AXIS.MOLD_PP_ZR)) return true;

            return false;
        }

        private void loadData()
        {
            InTransferZVelLabel.Text = Conf.vel(AXIS.IN_PP_Z).ToString();
            OutTransferZVelLabel.Text = Conf.vel(AXIS.MOLD_PP_ZL).ToString();
            UnloadYVelLabel.Text = Conf.vel(AXIS.MOLD_PP_ZR).ToString();

            InTransferZAccLabel.Text = Conf.acc(AXIS.IN_PP_Z).ToString();
            OutTransferZAccLabel.Text = Conf.acc(AXIS.MOLD_PP_ZL).ToString();
            UnloadYAccLabel.Text = Conf.acc(AXIS.MOLD_PP_ZR).ToString();

        }

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            if (m_checkChange == true)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE,
                    "Unsaved data exists.\r\n Do you want to quit?",
                    MessageBoxButtons.OKCancel);

                if (msgBox.showDialog() == false)
                    return;
            }
            this.Close();
            m_checkChange = false;
        }
    }
}
