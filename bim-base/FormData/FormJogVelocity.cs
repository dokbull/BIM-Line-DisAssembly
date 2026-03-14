using System;
using System.Windows.Forms;


namespace bim_base
{
    public partial class FormJogVelocity : Form
    {
        bool m_checkChange = false;

        ProcessMain main = null;

        public FormJogVelocity(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("DO YOY WANT TO SAVE?");
            if (ret == false)
                return;

            //Low
            Conf.setJogLow(AXIS.IN_PP_Z, Util.toDouble(dispInTransferZLowLabel.Text));
            Conf.setJogLow(AXIS.MOLD_PP_ZL, Util.toDouble(dispOutTransferZLowLabel.Text));
            Conf.setJogLow(AXIS.MOLD_PP_ZR, Util.toDouble(dispUnloadYLowLabel.Text));

            //Mid
            Conf.setJogMid(AXIS.IN_PP_Z, Util.toDouble(dispInTransferZMidLabel.Text));
            Conf.setJogMid(AXIS.MOLD_PP_ZL, Util.toDouble(dispOutTransferZMidLabel.Text));
            Conf.setJogMid(AXIS.MOLD_PP_ZR, Util.toDouble(dispUnloadYMidLabel.Text));

            //High
            Conf.setJogHigh(AXIS.IN_PP_Z, Util.toDouble(dispInTransferZHighLabel.Text));
            Conf.setJogHigh(AXIS.MOLD_PP_ZL, Util.toDouble(dispOutTransferZHighLabel.Text));
            Conf.setJogHigh(AXIS.MOLD_PP_ZR, Util.toDouble(dispUnloadYHighLabel.Text));

            m_checkChange = false;
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("DO YOU WANT TO RELOAD DATA?");

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

                if (curValue != value.ToString())
                    m_checkChange = true;

                if (value > 1000)
                    value = 1000;

                if (value < 0)
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
        }


        private void loadData()
        {
            //    if (main.isSimulation() == true)
            //        return;

            dispInTransferZLowLabel.Text = Conf.jogLow(AXIS.IN_PP_Z).ToString("0.##");
            dispOutTransferZLowLabel.Text = Conf.jogLow(AXIS.MOLD_PP_ZL).ToString("0.##");
            dispUnloadYLowLabel.Text = Conf.jogLow(AXIS.MOLD_PP_ZR).ToString("0.##");

            dispInTransferZMidLabel.Text = Conf.jogMid(AXIS.IN_PP_Z).ToString("0.##");
            dispOutTransferZMidLabel.Text = Conf.jogMid(AXIS.MOLD_PP_ZL).ToString("0.##");
            dispUnloadYMidLabel.Text = Conf.jogMid(AXIS.MOLD_PP_ZR).ToString("0.##");

            dispInTransferZHighLabel.Text = Conf.jogHigh(AXIS.IN_PP_Z).ToString("0.##");
            dispOutTransferZHighLabel.Text = Conf.jogHigh(AXIS.MOLD_PP_ZL).ToString("0.##");
            dispUnloadYHighLabel.Text = Conf.jogHigh(AXIS.MOLD_PP_ZR).ToString("0.##");

        }

        private void FormJogVelocity_Load(object sender, EventArgs e)
        {
            loadData();
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
        }
    }
}
