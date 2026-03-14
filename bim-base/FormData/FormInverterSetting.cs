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

    public partial class FormInverterSetting : Form
    {
        ProcessMain main;

        public List<double> m_inverterSpeeds = new List<double>();
        public List<string> m_inverterDirections = new List<string>();
        public List<Button> m_inverterApplyButtons = new List<Button>();
        public List<Label> m_inverterSpeedLabels = new List<Label>();
        public List<Label> m_inverterSpeedTargetLabels = new List<Label>();
        public List<Label> m_inverterDirectionLabels = new List<Label>();
        public List<Label> m_inverterOnOffLabels = new List<Label>();
        public FormInverterSetting(ProcessMain procMain)
        {
            InitializeComponent();

            for (int i = 0; i < 11; i++)
            { 
                m_inverterSpeeds.Add(0.0d);
            }

            m_inverterApplyButtons.Add(btnApplyInput);
            m_inverterApplyButtons.Add(btnApplyLoader);
            m_inverterApplyButtons.Add(btnApplyF51);
            m_inverterApplyButtons.Add(btnApplyF52);
            m_inverterApplyButtons.Add(btnApplyF41);
            m_inverterApplyButtons.Add(btnApplyF42);
            m_inverterApplyButtons.Add(btnApplyF31);
            m_inverterApplyButtons.Add(btnApplyF32);
            m_inverterApplyButtons.Add(btnApplyF21);
            m_inverterApplyButtons.Add(btnApplyF22);
            m_inverterApplyButtons.Add(btnApplyF11);
            m_inverterApplyButtons.Add(btnApplyF12);
            m_inverterApplyButtons.Add(btnApplyUnloader);
            m_inverterApplyButtons.Add(btnApplyOutput);

            m_inverterDirectionLabels.Add(lblDirectionInput);
            m_inverterDirectionLabels.Add(lblDirectionLoader);
            m_inverterDirectionLabels.Add(lblDirectionF51);
            m_inverterDirectionLabels.Add(lblDirectionF52);
            m_inverterDirectionLabels.Add(lblDirectionF41);
            m_inverterDirectionLabels.Add(lblDirectionF42);
            m_inverterDirectionLabels.Add(lblDirectionF31);
            m_inverterDirectionLabels.Add(lblDirectionF32);
            m_inverterDirectionLabels.Add(lblDirectionF21);
            m_inverterDirectionLabels.Add(lblDirectionF22);
            m_inverterDirectionLabels.Add(lblDirectionF11);
            m_inverterDirectionLabels.Add(lblDirectionF12);
            m_inverterDirectionLabels.Add(lblDirectionUnloader);
            m_inverterDirectionLabels.Add(lblDirectionOutput);

            m_inverterSpeedLabels.Add(lblSpeedLoader);
            m_inverterSpeedLabels.Add(lblSpeedTransfer);
            m_inverterSpeedLabels.Add(lblSpeedF51);
            m_inverterSpeedLabels.Add(lblSpeedF52);
            m_inverterSpeedLabels.Add(lblSpeedF41);
            m_inverterSpeedLabels.Add(lblSpeedF42);
            m_inverterSpeedLabels.Add(lblSpeedF31);
            m_inverterSpeedLabels.Add(lblSpeedF32);
            m_inverterSpeedLabels.Add(lblSpeedF21);
            m_inverterSpeedLabels.Add(lblSpeedF22);
            m_inverterSpeedLabels.Add(lblSpeedF11);
            m_inverterSpeedLabels.Add(lblSpeedF12);
            m_inverterSpeedLabels.Add(lblSpeedUnloaderTransfer);
            m_inverterSpeedLabels.Add(lblSpeedUnloader);

            m_inverterSpeedTargetLabels.Add(lblSpeedLoaderTarget);
            m_inverterSpeedTargetLabels.Add(lblSpeedTransferLDTarget);
            m_inverterSpeedTargetLabels.Add(lblSpeedF51Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF52Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF41Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF42Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF31Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF32Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF21Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF22Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF11Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedF12Tar);
            m_inverterSpeedTargetLabels.Add(lblSpeedUnloaderTransferTar);
            m_inverterSpeedTargetLabels.Add(lblSpeedUnloaderTar);

            m_inverterOnOffLabels.Add(lblONOFFInput);
            m_inverterOnOffLabels.Add(lblONOFFLoader);
            m_inverterOnOffLabels.Add(lblONOFFF51);
            m_inverterOnOffLabels.Add(lblONOFFF52);
            m_inverterOnOffLabels.Add(lblONOFFF41);
            m_inverterOnOffLabels.Add(lblONOFFF42);
            m_inverterOnOffLabels.Add(lblONOFFF31);
            m_inverterOnOffLabels.Add(lblONOFFF32);
            m_inverterOnOffLabels.Add(lblONOFFF21);
            m_inverterOnOffLabels.Add(lblONOFFF22);
            m_inverterOnOffLabels.Add(lblONOFFF11);
            m_inverterOnOffLabels.Add(lblONOFFF12);
            m_inverterOnOffLabels.Add(lblONOFFUnloader);
            m_inverterOnOffLabels.Add(lblONOFFOutput);

            main = procMain;
            int idxBTN = -1;
            foreach (Button button in m_inverterApplyButtons) {
                idxBTN += 1;
                button.Click += btnApplyAlign_Click;
                //main.pFrenic.setFrequency(idxBTN + 1, m_inverterSpeeds[idxBTN]);
            }
            foreach (Label lbel in m_inverterDirectionLabels) {
                lbel.Click += btnChangeDirection_Click;
            }
            foreach (Label lbel in m_inverterSpeedLabels) {
                lbel.Click += btnChangeSpeed_Click;
            }
            foreach (Label lbel in m_inverterOnOffLabels) {
                lbel.Click += btnONOFF_Click;
            }
            ui_timer.Start();
        }

        public FormInverterSetting()
        {
            InitializeComponent();
        }

        public void InverterApplyParam()
        {

        }
        public void InverterChangeSpeed(int slaveID)
        {
            if (slaveID >= 14 || slaveID < 0) return;
            string str1 = "0";
            if (slaveID == 0) { str1 = lblSpeedLoader.Text; }
            else if (slaveID == 1) { str1 = lblSpeedTransfer.Text; }
            else if (slaveID == 2) { str1 = lblSpeedF51.Text; }
            else if (slaveID == 3) { str1 = lblSpeedF52.Text; }
            else if (slaveID == 4) { str1 = lblSpeedF51.Text; }
            else if (slaveID == 5) { str1 = lblSpeedF52.Text; }
            else if (slaveID == 6) { str1 = lblSpeedF51.Text; }
            else if (slaveID == 7) { str1 = lblSpeedF52.Text; }
            else if (slaveID == 8) { str1 = lblSpeedF51.Text; }
            else if (slaveID == 9) { str1 = lblSpeedF52.Text; }
            else if (slaveID == 10) { str1 = lblSpeedF51.Text; }
            else if (slaveID == 11) { str1 = lblSpeedF52.Text; }
            else if (slaveID == 12) { str1 = lblSpeedUnloaderTransfer.Text; }
            else if (slaveID == 13) { str1 = lblSpeedUnloader.Text; }
            FormNumpad dlg = new FormNumpad(str1, true);

            if (dlg.ShowDialog() == DialogResult.OK) {
                double value = Util.toDouble(dlg.getNewValue());
                m_inverterSpeeds[slaveID] = value;
                main.frenic().setFrequency(slaveID + 1, value);
            }
        }
        public void InverterChangeDirection(int SlaveID)
        {
            int value = (main.frenic().status(SlaveID) == 1) ? 2 :1;
        }
        public void InverterOnOff(int SVIdx)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void BT_EXIT_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int m_idx = button.TabIndex;

        }
        private void btnChangeSpeed_Click(object sender, EventArgs e)
        {
            Label button = (Label)sender;
            int m_idx = m_inverterSpeedLabels.IndexOf(button);
            InverterChangeSpeed(m_idx);

        }
        private void btnChangeDirection_Click(object sender, EventArgs e)
        {
            Label button = (Label)sender;
            int m_idx = m_inverterDirectionLabels.IndexOf(button);
            InverterChangeDirection(m_idx);
            //InverterOnOff(m_idx);
        }

        private void btnONOFF_Click(object sender, EventArgs e)
        {
            Label button = (Label)sender;
            int m_idx = button.TabIndex;
            InverterOnOff(m_idx);

        }
        private void btnApplyUnloader_Click(object sender, EventArgs e)
        {

        }
        public void saveInverterParamters(int idx)
        {

        }

        private void btnApplyAlign_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int m_idx = m_inverterApplyButtons.IndexOf(btn);
            saveInverterParamters(m_idx);
        }

        private void btnApplyTransfer_Click(object sender, EventArgs e)
        {

        }

        private void btnApplyLoader_Click(object sender, EventArgs e)
        {
            m_inverterSpeeds[0] = Util.toDouble(lblSpeedLoader.Text);
            m_inverterSpeeds[1] = Util.toDouble(lblSpeedTransfer.Text);
            m_inverterSpeeds[2] = Util.toDouble(lblSpeedF51.Text);
            m_inverterSpeeds[3] = Util.toDouble(lblSpeedF52.Text);

            m_inverterDirections[0] = lblDirectionInput.Text;
            m_inverterDirections[1] = lblDirectionLoader.Text;
            m_inverterDirections[2] = lblDirectionF51.Text;
            m_inverterDirections[3] = lblDirectionF52.Text;
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            // ******************** Inverter Speed     **************************** //
            lblSpeedLoaderTarget.Text = main.frenic().setFreq(0).ToString();
            lblSpeedTransferLDTarget.Text = main.frenic().setFreq(1).ToString();
            lblSpeedF51Tar.Text = main.frenic().setFreq(2).ToString();
            lblSpeedF52Tar.Text = main.frenic().setFreq(3).ToString();
            lblSpeedF41Tar.Text = main.frenic().setFreq(4).ToString();
            lblSpeedF42Tar.Text = main.frenic().setFreq(5).ToString();
            lblSpeedF31Tar.Text = main.frenic().setFreq(6).ToString();
            lblSpeedF32Tar.Text = main.frenic().setFreq(7).ToString();
            lblSpeedF21Tar.Text = main.frenic().setFreq(8).ToString();
            lblSpeedF22Tar.Text = main.frenic().setFreq(9).ToString();
            lblSpeedF11Tar.Text = main.frenic().setFreq(10).ToString();

            lblSpeedLoader.Text = m_inverterSpeeds[0].ToString();
            lblSpeedTransfer.Text = m_inverterSpeeds[1].ToString();
            lblSpeedF51.Text = m_inverterSpeeds[2].ToString();
            lblSpeedF52.Text = m_inverterSpeeds[3].ToString();
            lblSpeedF41.Text = m_inverterSpeeds[4].ToString();
            lblSpeedF42.Text = m_inverterSpeeds[5].ToString();
            lblSpeedF31.Text = m_inverterSpeeds[6].ToString();
            lblSpeedF32.Text = m_inverterSpeeds[7].ToString();
            lblSpeedF21.Text = m_inverterSpeeds[8].ToString();
            lblSpeedF22.Text = m_inverterSpeeds[9].ToString();
            lblSpeedF11.Text = m_inverterSpeeds[10].ToString();

            // ******************** Inverter Direction **************************** //
            int dirLoad = main.frenic().status(0);
            int dirTransfer = main.frenic().status(1);
            int dirF51 = main.frenic().status(2);
            int dirF52 = main.frenic().status(3);
            int dirF41 = main.frenic().status(4);
            int dirF42 = main.frenic().status(5);
            int dirF31 = main.frenic().status(6);
            int dirF32 = main.frenic().status(7);
            int dirF21 = main.frenic().status(8);
            int dirF22 = main.frenic().status(9);
            int dirF11 = main.frenic().status(10);

            lblDirectionInput.Text = (dirLoad == 1) ? "CCW" : "CW";
            lblDirectionLoader.Text = (dirTransfer == 1) ? "CCW" : "CW";
            lblDirectionF51.Text = (dirF51 == 1) ? "CCW" : "CW";
            lblDirectionF52.Text = (dirF52 == 1) ? "CCW" : "CW";
            lblDirectionF41.Text = (dirF41 == 1) ? "CCW" : "CW";
            lblDirectionF42.Text = (dirF42 == 1) ? "CCW" : "CW";
            lblDirectionF31.Text = (dirF31 == 1) ? "CCW" : "CW";
            lblDirectionF32.Text = (dirF32 == 1) ? "CCW" : "CW";
            lblDirectionF21.Text = (dirF21 == 1) ? "CCW" : "CW";
            lblDirectionF22.Text = (dirF22 == 1) ? "CCW" : "CW";
            lblDirectionF11.Text = (dirF11 == 1) ? "CCW" : "CW";
            
        }

        private void colorButton3_Click(object sender, EventArgs e)
        {

        }

        private void lblCheckingValue_Click(object sender, EventArgs e)
        {

        }

        private void FormInverterSetting_Load(object sender, EventArgs e)
        {

        }

        private void FormInverterSetting_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
