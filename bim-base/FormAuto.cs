using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{    public partial class FormAuto : Form, IForm
    {
        ProcessMain main = null;

        public FormAuto(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void FormAuto_Load(object sender, EventArgs e)
        {
            uiTimer.Enabled = true;
        }

        public void onShow(bool enable)
        {
        }

        private void selectModeButton_Click(object sender, EventArgs e)
        {
            if (main.isAuto())
                return;

            FormMode dlg = new FormMode(main);
            dlg.ShowDialog(this);
        }

        private void ioMonitoringButton_Click(object sender, EventArgs e)
        {
            FormIOMonitor dlg = new FormIOMonitor(main);

            main.setManualTestIO(true);

            dlg.ShowDialog(this);
        }

        private void axisOriginButton_Click(object sender, EventArgs e)
        {
            if (main.isAuto())
                return;

            FormAxisOrigin dlg = new FormAxisOrigin(main);
            dlg.ShowDialog(this);
        }

        private void productInfoButton_Click(object sender, EventArgs e)
        {
            FormProductInfo dlg = new FormProductInfo(main);
            dlg.ShowDialog(this);
        }

        private void outOfPPlanCheckBox_Click(object sender, EventArgs e)
        {
            outOfPPlanCheckBox.Checked = true;
            main.setOutOfPPlan(true);
        }

        private void InputStopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            main.setInputStop(cb.Checked);
        }

        private void outputStopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            main.setOutputStop(cb.Checked);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (main.axisOriComplete() == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, PopupMessage.message(POPUP.AXIS_NOT_READY), MessageBoxButtons.OK);
                msgBox.ShowDialog(this);

                main.setAuto(false);
                return;
            }

            if (main.isOutOfPPlan() == true)
                main.setOutOfPPlan(false);

            if (main.isAuto() == false)
            {
                main.setAuto(true);
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            main.setAuto(false);
            main.pause();

            if (main.isOutOfPPlan() == true)
            {
                main.setOutOfPPlan(false);
            }
        }

        private void productInfoButton_VisibleChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            cycleTimeLayout.Visible = btn.Visible;
            setCountLayout.Visible = btn.Visible;
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            StartButton.BackColor = (main.isAuto() == true) ? Color.Lime : Color.White;
            StopButton.BackColor = (main.isAuto() == false) ? Color.Red : Color.White;

            outOfPPlanCheckBox.Checked = main.isOutOfPPlan();

            string modeText = main.state().ToString().Replace("_", " ");
            selectModeButton.Text = modeText;

            InputStopCheckBox.FlatAppearance.MouseOverBackColor = InputStopCheckBox.Checked ? Color.Orange : Color.White;
            InputStopCheckBox.FlatAppearance.MouseDownBackColor = InputStopCheckBox.Checked ? Color.Orange : Color.White;
            outputStopCheckBox.FlatAppearance.MouseOverBackColor = outputStopCheckBox.Checked ? Color.Orange : Color.White;
            outputStopCheckBox.FlatAppearance.MouseDownBackColor = outputStopCheckBox.Checked ? Color.Orange : Color.White;
            outOfPPlanCheckBox.FlatAppearance.MouseOverBackColor = outOfPPlanCheckBox.Checked ? Color.Orange : Color.White;
            outOfPPlanCheckBox.FlatAppearance.MouseDownBackColor = outOfPPlanCheckBox.Checked ? Color.Orange : Color.White;

            cycleTimeLayout.Visible = Conf.PRODUCT_INFO_VISIBLE;
            setCountLayout.Visible = Conf.PRODUCT_INFO_VISIBLE;

            totalCountLabel.Text = main.outputCount().ToString();

            if (main.isCycleTimeUpdate() == true)
                cycleTimeLabel.Text = (main.getLastCycleTime() / 1000.0d).ToString("0.0") + " s";

            // TOWER LAMP
            towerR.BackColor = main.output(OUTPUT.TOWER_R) ? Color.DarkRed : Color.White;
            towerR.ForeColor = main.output(OUTPUT.TOWER_R) ? Color.White : Color.Black;

            towerY.BackColor = main.output(OUTPUT.TOWER_Y) ? Color.Yellow: Color.White;
            towerY.ForeColor = main.output(OUTPUT.TOWER_Y) ? Color.Black : Color.Black;

            towerG.BackColor = main.output(OUTPUT.TOWER_G) ? Color.DarkGreen : Color.White;
            towerG.ForeColor = main.output(OUTPUT.TOWER_G) ? Color.White : Color.Black;

            towerBuzz.BackColor = main.output(OUTPUT.BUZZER_1) ? Color.Pink : Color.White;
            towerBuzz.ForeColor = main.output(OUTPUT.BUZZER_1) ? Color.Black : Color.Black;

            lastWorkButton.BackColor = main.isLastWork() ? Color.LightBlue : Color.White;

            // DOOR & MC


            readFRENIC();
        }

        void readFRENIC()
        {
            Label[] freq = new Label[11] { freq1, freq2, freq3, freq4, freq5, f6, f7, f8, f9, f10, f11 };
            Label[] setFreq = new Label[11] { state1, state2, state3, state4, state5, s6, s7, s8, s9, s10, s11 };
            Label[] status = new Label[11] { alarm1, alarm2, alarm3, alarm4, alarm5, a6, a7, a8, a9, a10, a11 };

            CSerialFRENIC FRENIC = main.frenic();

            if (FRENIC.isConnect() == false)
                return;

            for (int i = 0; i < 4; i++)
            {
                freq[i].Text = FRENIC.freq(i).ToString("0.0");
                setFreq[i].Text = FRENIC.setFreq(i).ToString("0.0");
                status[i].Text = FRENIC.status(i).ToString();
            }
        }

        private void tackTimeButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "T/T reset?", MessageBoxButtons.YesNo);
            DialogResult res = msgBox.ShowDialog(this);
            if (res == DialogResult.Yes)
            {
                main.lastCycleTimeClear();
                cycleTimeLabel.Text = "0.0";
            }
        }

        private void currentSetButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Total count reset?", MessageBoxButtons.YesNo);
            DialogResult res = msgBox.ShowDialog(this);
            if (res == DialogResult.Yes)
            {
                main.outputCountReset();
                totalCountLabel.Text = "0";
            }
        }

        private void unitInitButton_Click(object sender, EventArgs e)
        {
            FormUnitInit form = new FormUnitInit(main);
            form.ShowDialog();
        }

        private void tower_DoubleClick(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                main.frenic().setFrequency(i + 1, 30.0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                main.frenic().setFrequency(i + 1, 60);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lastWorkButton_Click(object sender, EventArgs e)
        {
            if (main.procMoldReverseWork().isWaitMold() == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "Can not change last work.", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setLastWork(true);
        }

        private void simCommButton_Click(object sender, EventArgs e)
        {
            FormSimMonitor form = new FormSimMonitor(main);
            FormAutomationTester form2 = new FormAutomationTester();

            form2.Show();
            form.Show();

            // TODO : 테스트   용도로 만든 폼이므로, 나중에 적용할 것
            //form.ShowDialog();

        }
    }//class
}//namespace
