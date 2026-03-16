using bim_base.data.CIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bim_base.data.CIM.CIMEnumeric;

namespace bim_base
{
    public partial class FormAutomationTester : Form
    {
        public FormAutomationTester()
        {
            InitializeComponent();
        }

        private async void btnHandShake_Click(object sender, EventArgs e)
        {
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            if (Automation.Instance.IsInitialized == false) return;

            lblInitialized.BackColor = Color.LightGreen;

            // CIM 요청 중 병목현상이 생기는지 여부 확인용도
            //this.lblRunScan.BackColor = (Automation.Instance.IsRun ? Color.LightGreen : SystemColors.Control);

            try
            {
                // 스냅샷을 만들어서 안전하게 열거
                //var snapshot = Automation.Instance.RequestProcStateList.ToList();
                //this.lblRunProcessing.BackColor = (snapshot.Count > 0 ? Color.LightGreen : SystemColors.Control);
                //if (snapshot.Count > 0)
                //{
                //    this.lblRunProcessingList.Text = string.Join(Environment.NewLine, snapshot.Select(x => $"{x.ToString()}\r\n"));
                //}
            }
            catch (InvalidOperationException)
            {
                // 컬렉션 수정 중 발생한 예외는 무시하고 다음 타이머에서 재시도
            }

            bool writeReady = Automation.Instance.ReadBit(CIMWrite.WRITE_B.TPMLOSSREADY_19);
            bool readReady = Automation.Instance.ReadBit(CIMRead.READ_B.TPMLOSSREADY_19);

            if (!writeReady)
            {
                lblTpmLossMonitor.Text = "TPM LOSS : 대기 상태";
                lblTpmLossMonitor.BackColor = SystemColors.Control;
            }
            else if (writeReady && !readReady)
            {
                lblTpmLossMonitor.Text = "TPM LOSS : 응답 대기 상태";
                lblTpmLossMonitor.BackColor = Color.Orange;
            }
            else
            {
                lblTpmLossMonitor.Text = "TPM LOSS : 응답 수신";
                lblTpmLossMonitor.BackColor = Color.LightGreen;
            }
        }

        private void FormAutomationTester_Load(object sender, EventArgs e)
        {
            lblTpmLossMonitor.Text = "TPM LOSS : 대기 상태";

            lblTerminalRecvMonitor.Text = "수신 대기";
            lblTerminalSendMonitor.Text = "송신 대기";


            DateTimeTextBox.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.tmrRedraw.Start();
        }

        private void ReceiveTerminalDisplayEvent(int messageNum, string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => lblTerminalRecvMonitor.Text = $"수신 완료 MessageNum = {messageNum}, Message = {message} "));
                return;
            }

            lblTerminalRecvMonitor.Text = $"수신 완료 MessageNum = {messageNum}, Message = {message} ";
        }

        // TODO KGW : 입력에 대한 테스트로 추가, 테스트후 삭제
        private void btnSetDateTime_Click(object sender, EventArgs e)
        {
            string inputDataTime = DateTimeTextBox.Text.Trim();

            DateTime parseDateTime;
            
            if (inputDataTime.Length == 14 && DateTime.TryParseExact(inputDataTime, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out parseDateTime))
            {
                string testDateTime = parseDateTime.ToString("yyyyMMddHHmmss");
                Automation.Instance.WriteWord(CIMRead.READ_W.ASCII_7_D000_Datetime, testDateTime);
                Automation.Instance.SetDateTime();
            }
        }

        private void DataTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTpmLossStart_Click(object sender, EventArgs e)
        {
            // 테스트 시작 전 비트 OFF
            Automation.Instance.WriteBit(CIMRead.READ_B.TPMLOSSREADY_19, false);

            // 알람 사용
            Automation.Instance.EqStopByOperator(CIMEnumeric.EnmumEqStopByOperatorType.Alarm);
        }

        private void btnTpmLossReplyOn_Click(object sender, EventArgs e)
        {
            Automation.Instance.WriteBit(CIMRead.READ_B.TPMLOSSREADY_19, true);
        }

        private void btnTpmLossReplyOff_Click(object sender, EventArgs e)
        {
            Automation.Instance.WriteBit(CIMRead.READ_B.TPMLOSSREADY_19, false);
        }

        // 터미널 수신 테스트
        private async void btnTerminalRecv_Click(object sender, EventArgs e)
        {
            if (Automation.Instance?.IsInitialized != true)
            {
                lblTerminalRecvMonitor.Text = "오류: Automation이 초기화되지 않음";
                return;
            }

            try
            {
                btnTerminalRecv.Enabled = false;

                string recvNum = "1";
                string recvText = "CIM -> EQ Terminal Test";

                lblTerminalRecvMonitor.Text = $"수신 시작 Num = {recvNum}, Text = {recvText}";

                Automation.Instance.WriteWord(CIMRead.READ_W.ASCII_1_D04D_TerminalNumber, recvNum);
                Automation.Instance.WriteWord(CIMRead.READ_W.ASCII_60_D011_TerminalDisplayText, recvText);
                Automation.Instance.WriteBit(CIMRead.READ_B.TERMINALDISPLAY_3, true);

                await Task.Delay(100);


                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                lblTerminalRecvMonitor.Text = $"오류 발생: {ex.Message}";
            }
            finally
            {
                try
                {
                    Automation.Instance?.WriteBit(CIMRead.READ_B.TERMINALDISPLAY_3, false);
                }
                catch { }
                
                btnTerminalRecv.Enabled = true;
            }
        }

        // 터미널 송신 테스트
        private async void btnTerminalSend_Click(object sender, EventArgs e)
        {
            if (Automation.Instance?.IsInitialized != true)
            {
                lblTerminalSendMonitor.Text = "오류: Automation이 초기화되지 않음";
                return;
            }

            try
            {
                btnTerminalSend.Enabled = false;

                string sendText = "EQ -> CIM Terminal Test";
                lblTerminalSendMonitor.Text = $"송신 시작 Text = {sendText}";

                // 송신 작업을 제대로 await
                await Task.Run(() => Automation.Instance.SendTerminalDisplayReply(sendText));

                await Task.Delay(200);

                string writeMessage = Automation.Instance.ReadWord(CIMWrite.WRITE_W.ASCII_60_1086_TerminalDisplaySnd);
                bool writeBit = Automation.Instance.ReadBit(CIMWrite.WRITE_B.TERMINALDISPLAY_3);

                lblTerminalSendMonitor.Text = $"송신 중 WriteText = {writeMessage}, WriteBit = {writeBit}";

                await Task.Delay(300);
                Automation.Instance.WriteBit(CIMRead.READ_B.TERMINALDISPLAY_3, true);

                lblTerminalSendMonitor.Text = $"응답 설정 완료 (READ bit = true)";

                await Task.Delay(500);

                bool finalWriteBit = Automation.Instance.ReadBit(CIMWrite.WRITE_B.TERMINALDISPLAY_3);
                lblTerminalSendMonitor.Text = $"송신 완료 WriteBit = {finalWriteBit}";
            }
            catch (Exception ex)
            {
                lblTerminalSendMonitor.Text = $"오류 발생: {ex.Message}";
            }
            finally
            {
                try
                {
                    Automation.Instance?.WriteBit(CIMRead.READ_B.TERMINALDISPLAY_3, false);
                }
                catch { }
                
                btnTerminalSend.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //            public bool PpidChange()
            //public bool PpidCreate(/*string ppid*/)
            //public bool PpidDelete(/*string ppid*/)
            //public bool ParameterChange()

            Automation.Instance.PpidCreate(24);

        }

        private void btnSendTerminalDisplay_Click(object sender, EventArgs e)
        {
            Automation.Instance.SendTerminalDisplayReply("Disassembly Terminal Message");
        }

        private void btnAlarmOccured_Click(object sender, EventArgs e)
        {
            Automation.Instance.AlarmOccured(CIMEnumeric.EnumAlarmLevel.HeavyAlarm, 1, "Test");
        }

        private void btnAlarmReleased_Click(object sender, EventArgs e)
        {
            Automation.Instance.AlarmReleased(1, "Test");
        }

        private bool stateAvailiability = false;
        private void rdobtnAvailiability_Click(object sender, EventArgs e)
        {
            stateAvailiability = !stateAvailiability;
                this.rdobtnAvailiability.Checked = stateAvailiability;

            Automation.Instance.SetEqState(this.rdobtnAvailiability.Checked ? CIMEnumeric.EnumAvailabilityState.Down : CIMEnumeric.EnumAvailabilityState.Up);
        }

        private bool stateinterlock = false;
        private void rdobtnInterlock_Click(object sender, EventArgs e)
        {
            this.stateinterlock = !stateinterlock;
            this.rdobtnInterlock.Checked = this.stateinterlock;

            Automation.Instance.SetEqState(this.rdobtnInterlock.Checked ? CIMEnumeric.EnumInterlockState.On : CIMEnumeric.EnumInterlockState.Off);
        }

        private bool stateMove = false;
        private void rdobtnMove_Click(object sender, EventArgs e)
        {
            this.stateMove = !stateMove;
                this.rdobtnMove.Checked = stateMove;

            Automation.Instance.SetEqState(this.rdobtnMove.Checked ? CIMEnumeric.EnumMoveState.Pause: CIMEnumeric.EnumMoveState.Runnning);
        }

        private bool stateRun = false;
        private void rdobtnRun_Click(object sender, EventArgs e)
        {
            stateRun = !stateRun;
                this.rdobtnRun.Checked = stateRun;

            Automation.Instance.SetEqState(this.rdobtnRun.Checked ? CIMEnumeric.EnumRunState.Idle: CIMEnumeric.EnumRunState.Run);
        }

        private EnumJobProcessType resultTrackIn = EnumJobProcessType.Fail;
        private CellDataInfo trackInCellData = new CellDataInfo();

        private void btnTrackInLoading_Click(object sender, EventArgs e)
        {
            (EnumJobProcessType PassType, CellDataInfo CellData) result = Automation.Instance.TrackInLoadingCell("barcode");

            this.resultTrackIn = result.PassType;
            this.trackInCellData = result.CellData;
        }

        private void btnTrackOutUnloading_Click(object sender, EventArgs e)
        {
            Automation.Instance.TrackOutUnloadingCell("barcode", resultTrackIn, this.trackInCellData);
        }

        private void btnEqStateDefault_Click(object sender, EventArgs e)
        {
            Automation.Instance.SetEqState(EnumAvailabilityState.Up);
            Automation.Instance.SetEqState(EnumMoveState.Runnning);
            Automation.Instance.SetEqState(EnumRunState.Run);
            Automation.Instance.SetEqState(EnumInterlockState.Off);
        }

        private void btnAlarmOccured_Light_Click(object sender, EventArgs e)
        {

            Automation.Instance.AlarmOccured(CIMEnumeric.EnumAlarmLevel.LightAlarm, 2, "Test");
        }
    }
}
