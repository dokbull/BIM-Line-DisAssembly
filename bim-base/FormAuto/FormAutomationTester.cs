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

        private void btnHandShake_Click(object sender, EventArgs e)
        {
            Automation.Instance.HandShakeSignal(CIMWrite.WRITE_B.TERMINALDISPLAY_3, true, CIMRead.READ_B.TERMINALDISPLAY_3, true);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            Automation.Instance.InitializeCIM();
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
        }
    }
}
