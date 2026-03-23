using bim_base.data.CIM;
using Lib.UI.Generic.DarkMode;
using Lib.UI.Generic.DarkMode.Forms;
using Lib.UI.Generic.Icons;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bim_base.data.CIM.CIMEnumeric;
using static LSFenet.FNET;
using static LSFenet_MOBIS.FNET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace bim_base
{
    public partial class FormAuto : Form, IForm
    {
        private static System.Threading.Timer dailyTimer;
        ProcessMain main = null;
        bool m_bBufferFull = false;
        TimerDelay m_iCheckBufferNG = new TimerDelay();
        bool m_clearProductCounter = false;
        public int m_floorCoolingSelect = 0;
        public FormAuto(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;

            SetDailyTimer();
        }

        private void FormAuto_Load(object sender, EventArgs e)
        {
            uiTimer.Enabled = true;

            Automation.Instance.OnResetSignalTowerBuzzorEvent += Automation_OnResetSignalTowerBuzzorEvent;
            Automation.Instance.ReceivedOperatorCallEvent += Automation_ReceivedOperatorCallEvent;
            Automation.Instance.ReceivedInterlockEvent += Automation_ReceivedInterlockEvent;
            Automation.Instance.ReleaseInterlockEvent += Automation_ReleaseInterlockEvent;
            Automation.Instance.GetSampleExistEvent += Automation_GetSampleExistEvent;
        }

        private void Automation_OnResetSignalTowerBuzzorEvent()
        {
            this.m_IsSignalTowerBlink = false;
            //this.tBlink.Wait();

            main.setOutput(OUTPUT.BUZZER_1, false);
            main.setOutput(OUTPUT.TOWER_R, false);
            main.setOutput(OUTPUT.TOWER_Y, false);


        }

        private bool Automation_GetSampleExistEvent()
        {
            CSTATION[] station = main.station();

            bool isExist = false;
            foreach (var item in station)
            {
                isExist |= (item.type() != CSTATION.TYPE.EMPTY);
            }

            return isExist;
        }


        Task tBlink = null;
        bool m_IsSignalTowerBlink = true;

        private async void Automation_ReceivedOperatorCallEvent(int _OpCallNum, string _OpCallText)
        {
            main.setOutput(OUTPUT.BUZZER_1, true);


            tBlink = Task.Run(async () =>
            {
                while (m_IsSignalTowerBlink)
                {
                    main.setOutput(OUTPUT.TOWER_Y, true);
                    Automation.Instance.SleepWithDoEvent(1000);
                    main.setOutput(OUTPUT.TOWER_Y, false);
                    Automation.Instance.SleepWithDoEvent(1000);
                }

            });

        }

        private async void Automation_ReceivedInterlockEvent(string _ID, string _Message, EnumInterlockRCMD _RCMD)
        {
            // TODO CHECK LHJ to HJP : RCMD별로 메인 SW에서 인터락 처리 필요
            switch (_RCMD)
            {
                case EnumInterlockRCMD.CycleStop:
                    break;
                case EnumInterlockRCMD.TransferStop:
                    break;
                case EnumInterlockRCMD.LoadingStop:
                    break;
                case EnumInterlockRCMD.StepStop:
                    break;
                case EnumInterlockRCMD.OWNStop:
                    break;
                default:
                    return;
            }


            main.setOutput(OUTPUT.BUZZER_1, true);

            this.m_IsSignalTowerBlink = true;
            tBlink = Task.Run(async () =>
            {

                while (m_IsSignalTowerBlink)
                {
                    main.setOutput(OUTPUT.TOWER_R, true);
                    main.setOutput(OUTPUT.TOWER_Y, true);
                    Automation.Instance.SleepWithDoEvent(1000);
                    main.setOutput(OUTPUT.TOWER_R, false);
                    main.setOutput(OUTPUT.TOWER_Y, false);
                    Automation.Instance.SleepWithDoEvent(1000);
                }

            });

        }

        private async void Automation_ReleaseInterlockEvent(int _ID, EnumInterlockRCMD _RCMD, string _LogMessage)
        {
            // TODO CHECK LHJ to HJP : RCMD별로 메인 SW에서 인터락 해제 처리 필요
            switch (_RCMD)
            {
                case EnumInterlockRCMD.TransferStop:
                    break;
                case EnumInterlockRCMD.LoadingStop:
                    break;
                case EnumInterlockRCMD.StepStop:
                    break;
                case EnumInterlockRCMD.OWNStop:
                    break;
                default:
                    return;
            }
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
            if (main.procOrg().isComplete() == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "AXIS NOT READY", MessageBoxButtons.OK);
                msgBox.ShowDialog(this);

                main.setAuto(false);
                return;
            }

            if (main.isOutOfPPlan() == true)
                main.setOutOfPPlan(false);
            /****************************************************************************************/
            string _tmpError = "";
            ALARM _tmpAlm = ALARM.NONE;

            //if (!main.CheckSafetyMove(ref _tmpError, ref _tmpAlm, ref _tmpIO))
            //{
            //    CMessageBox msgBox = new CMessageBox(Common.TITLE, _tmpError, MessageBoxButtons.OK);
            //    msgBox.ShowDialog();
            //    return;
            //}
            /****************************************************************************************/
            if (main.isAuto() == false)
            {
                main.resume();
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
                main.lampControl(LAMP_STATE.RED);
            }
        }

        private void productInfoButton_VisibleChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            TacTimeLayout.Visible = btn.Visible;
            setCountLayout.Visible = btn.Visible;
        }
        private void clearProductCounter()
        {

            string dateNow = DateTime.Now.ToString("HHmm");
            if (dateNow == "0800" || dateNow == "2000")
            {
                if (m_clearProductCounter == false)
                {
                    main.outputCountReset();
                }
                m_clearProductCounter = true;
            }
            else if (m_clearProductCounter == true)
            {
                m_clearProductCounter = false;
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            clearProductCounter();

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

            TacTimeLayout.Visible = Conf.PRODUCT_INFO_VISIBLE;
            setCountLayout.Visible = Conf.PRODUCT_INFO_VISIBLE;

            ngCountLabel.Text = main.outputCount().ToString();
            totalCountLabel.Text = main.outputCount().ToString();
            lastTactLabel.Text = (main.getLastCycleTime() / 1000.0d).ToString("0.0") + " s";
            avgTactLabel.Text = (main.getAvgCycleTime() / 1000.0d).ToString("0.0") + " s";

            // TOWER LAMP
            towerR.BackColor = main.output(OUTPUT.TOWER_R) ? Color.DarkRed : Color.White;
            towerR.ForeColor = main.output(OUTPUT.TOWER_R) ? Color.White : Color.Black;

            towerY.BackColor = main.output(OUTPUT.TOWER_Y) ? Color.Yellow : Color.White;
            towerY.ForeColor = main.output(OUTPUT.TOWER_Y) ? Color.Black : Color.Black;

            towerG.BackColor = main.output(OUTPUT.TOWER_G) ? Color.DarkGreen : Color.White;
            towerG.ForeColor = main.output(OUTPUT.TOWER_G) ? Color.White : Color.Black;

            //DOOR & MC
            mcLabel.BackColor = main.isDetectEMC() ? Color.DarkRed : Color.Lime;
            safetyLabel.BackColor = main.isDetectSafetyMC() ? Color.DarkRed : Color.Lime;
            lbFrontLeftDoor.BackColor = main.isDoorDetect(ProcessMain.DOOR_ID.FR_LEFT) ? Color.DarkRed : Color.Lime;
            lbFrontRightDoor.BackColor = main.isDoorDetect(ProcessMain.DOOR_ID.FR_RIGHT) ? Color.DarkRed : Color.Lime;
            lbRearLeftDoor.BackColor = main.isDoorDetect(ProcessMain.DOOR_ID.RR_LEFT) ? Color.DarkRed : Color.Lime;
            lbRearRightDoor.BackColor = main.isDoorDetect(ProcessMain.DOOR_ID.RR_RIGHT) ? Color.DarkRed : Color.Lime;
            lbCBOX1LeftDoor.BackColor = main.isDoorDetect(ProcessMain.DOOR_ID.C_BOX_1_LEFT) ? Color.DarkRed : Color.Lime;
            lbCBOX1RightDoor.BackColor = main.isDoorDetect(ProcessMain.DOOR_ID.C_BOX_2_LEFT) ? Color.DarkRed : Color.Lime;

            lastWorkButton.BackColor = main.isLastWork() ? Color.LightBlue : Color.White;

            readFRENIC();

            updateLabelState();
        }


        void readFRENIC()
        {
            Label[] freq = new Label[11] { freq1, freq2, freq3, freq4, freq5, f6, f7, f8, f9, f10, f11 };
            Label[] setFreq = new Label[11] { state1, state2, state3, state4, state5, s6, s7, s8, s9, s10, s11 };
            Label[] status = new Label[11] { alarm1, alarm2, alarm3, alarm4, alarm5, a6, a7, a8, a9, a10, a11 };

            CSerialFRENIC FRENIC = main.frenic();

            if (FRENIC.isConnect() == false)
                return;

            for (int i = 0; i < 11; i++)
            {
                freq[i].Text = FRENIC.freq(i).ToString("0.0");
                setFreq[i].Text = FRENIC.setFreq(i).ToString("0.0");
                status[i].Text = FRENIC.status(i).ToString();
            }
        }

        public void updateLabelState()
        {
           
        }

        private void tackTimeButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "T/T reset?", MessageBoxButtons.YesNo);
            DialogResult res = msgBox.ShowDialog(this);
            if (res == DialogResult.Yes)
            {
                main.lastCycleTimeClear();
                avgTactLabel.Text = "0.0";
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
            if (main.isAuto() == true)
                return;

            FormUnitInit form = new FormUnitInit(main);
            form.ShowDialog();
        }

        private void tower_DoubleClick(object sender, EventArgs e)
        {
            FormStep dlg = new FormStep(main);
            dlg.ShowDialog();
        }

        private void bufferClearButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Do you want clear of buffer?", MessageBoxButtons.OKCancel);

            bool ret = msgBox.showDialog();

            if (ret == false)
                return;
        }
        private void currentSetNGCountButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "NG count reset?", MessageBoxButtons.YesNo);
            DialogResult res = msgBox.ShowDialog(this);
            if (res == DialogResult.Yes)
            {
                main.outputCountReset();
                totalCountLabel.Text = "0";
            }

        }
        #region DELETE IMAGE
        private void SetDailyTimer()
        {
            DateTime now = DateTime.Now;
            DateTime nextRunTime = now.Date.AddDays(1);
            double timeToNextRun = (nextRunTime - now).TotalMilliseconds;
            dailyTimer = new System.Threading.Timer(_ => OnTimedEvent(), null, (long)timeToNextRun, Timeout.Infinite);
        }

        private void OnTimedEvent()
        {
            SetDailyTimer();
        }
        private void DeleteOldDateFolders(string folderPath, DateTime selectedDate)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var directories = Directory.GetDirectories(folderPath);

                    foreach (var directory in directories)
                    {
                        var dirName = Path.GetFileName(directory);

                        if (DateTime.TryParseExact(dirName, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime folderDate))
                        {
                            if (folderDate < selectedDate)
                            {
                                try
                                {
                                    Directory.Delete(directory, true);
                                    main.writeBottomHistory($"Đã xóa thư mục: {directory}");
                                }
                                catch (Exception ex)
                                {
                                    main.writeBottomHistory($"Không thể xóa thư mục: {directory}. Lỗi: {ex.Message}");
                                }
                            }
                        }
                        if (DateTime.TryParseExact(dirName, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime folderDate1))
                        {
                            if (folderDate1 < selectedDate)
                            {
                                try
                                {
                                    Directory.Delete(directory, true);
                                    main.writeBottomHistory($"Đã xóa thư mục: {directory}");
                                }
                                catch (Exception ex)
                                {
                                    main.writeBottomHistory($"Không thể xóa thư mục: {directory}. Lỗi: {ex.Message}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    main.writeBottomHistory("Delete Image Fail");
                    Debug.debug(ex.ToString());
                }
            });

        }
        #endregion

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
            FormCimMonitor form = new FormCimMonitor(main);

            form.ShowDialog();
        }


        private void btnCimTester_Click(object sender, EventArgs e)
        {

            new FormCIMSimulationMonitor(main).Show();
            new FormAutomationTester().Show();
        }

        private void cvSpeedButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                main.frenic().setFrequency(i, 60.0d);
            }
        }

        private void alarmTestButton_Click(object sender, EventArgs e)
        {
            main.addAlarm(ALARM.TE_MAIN_COOLING_FAN_ALARM);
        }
    }
}
