using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormDataJogVelocity : Form
    {
        ProcessMain main = null;

        public FormDataJogVelocity(ProcessMain procMain)
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
            bool ret = CMessageBox.showMessage("저장하시겠습니까?");
            if (ret == false)
                return;

            Conf.setJogLow(AXIS.IN_PP_Y, Util.toDouble(xLowLabel.Text));
            Conf.setJogLow(AXIS.IN_PP_Z, Util.toDouble(yLowLabel.Text));
            Conf.setJogLow(AXIS.MOLD_PP_X, Util.toDouble(zLowLabel.Text));
            Conf.setJogLow(AXIS.IN_PP_Y, Util.toDouble(xMidLabel.Text));
            Conf.setJogLow(AXIS.IN_PP_Z, Util.toDouble(yMidLabel.Text));
            Conf.setJogLow(AXIS.MOLD_PP_X, Util.toDouble(zMidLabel.Text));
            Conf.setJogLow(AXIS.IN_PP_Y, Util.toDouble(xHighLabel.Text));
            Conf.setJogLow(AXIS.IN_PP_Z, Util.toDouble(yHighLabel.Text));
            Conf.setJogLow(AXIS.MOLD_PP_X, Util.toDouble(zHighLabel.Text));

            CMessageBox.showInfo(MessageText.saveMessage);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("데이터를 다시 불러오시겠습니까?");

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

                if (value > 1000)
                    value = 1000;

                if (value < 0)
                    value = 100;

                lb.Text = dlg.getNewValue();
            }
        }

        private bool checkChange()
        {
            double xLow = Util.toDouble(xLowLabel.Text);
            double yLow = Util.toDouble(yLowLabel.Text);
            double zLow = Util.toDouble(zLowLabel.Text);

            double xMid = Util.toDouble(xMidLabel.Text);
            double yMid = Util.toDouble(yMidLabel.Text);
            double zMid = Util.toDouble(zMidLabel.Text);

            double xHigh = Util.toDouble(xHighLabel.Text);
            double yHigh = Util.toDouble(yHighLabel.Text);
            double zHigh = Util.toDouble(zHighLabel.Text);

            if (xLow != Conf.jogLow(AXIS.IN_PP_Y)) return true;
            if (yLow != Conf.jogLow(AXIS.IN_PP_Z)) return true;
            if (zLow != Conf.jogLow(AXIS.MOLD_PP_X)) return true;
            if (xMid != Conf.jogMid(AXIS.IN_PP_Y)) return true;
            if (yMid != Conf.jogMid(AXIS.IN_PP_Z)) return true;
            if (zMid != Conf.jogMid(AXIS.MOLD_PP_X)) return true;
            if (xHigh != Conf.jogHigh(AXIS.IN_PP_Y)) return true;
            if (yHigh != Conf.jogHigh(AXIS.IN_PP_Z)) return true;
            if (zHigh != Conf.jogHigh(AXIS.MOLD_PP_X)) return true;

            return false;
        }

        private void loadData()
        {
            xLowLabel.Text = Conf.jogLow(AXIS.IN_PP_Y).ToString("0.##");
            yLowLabel.Text = Conf.jogLow(AXIS.IN_PP_Z).ToString("0.##");
            zLowLabel.Text = Conf.jogLow(AXIS.MOLD_PP_X).ToString("0.##");
            xMidLabel.Text = Conf.jogMid(AXIS.IN_PP_Y).ToString("0.##");
            yMidLabel.Text = Conf.jogMid(AXIS.IN_PP_Z).ToString("0.##");
            zMidLabel.Text = Conf.jogMid(AXIS.MOLD_PP_X).ToString("0.##");
            xHighLabel.Text = Conf.jogHigh(AXIS.IN_PP_Y).ToString("0.##");
            yHighLabel.Text = Conf.jogHigh(AXIS.IN_PP_Z).ToString("0.##");
            zHighLabel.Text = Conf.jogHigh(AXIS.MOLD_PP_X).ToString("0.##");
        }
    }
}
