using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bim_base.data.CIM;

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
            await Automation.Instance.HandShakeSignal(CIMWrite.WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true, 5);
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            if (Automation.Instance.IsInitialized == false) return;

            lblInitialized.BackColor = Color.LightGreen;

            // CIM 요청 중 병목현상이 생기는지 여부 확인용도
            this.lblRunScan.BackColor = (Automation.Instance.IsRun ? Color.LightGreen : SystemColors.Control);

            this.lblRunProcessing.BackColor = (Automation.Instance.RequestProcStateList.Count > 0 ? Color.LightGreen : SystemColors.Control);
            if(Automation.Instance.RequestProcStateList.Count > 0)
            {
                this.lblRunProcessingList.Text = string.Join(Environment.NewLine, Automation.Instance.RequestProcStateList.Select(x => $"{x.ToString()}\r\n"));
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
            DateTimeTextBox.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.tmrRedraw.Start();
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
    }
}
