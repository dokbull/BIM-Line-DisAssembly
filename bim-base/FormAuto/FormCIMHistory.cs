using bim_base.data.CIM;
using Lib.UI.Generic.DarkMode;
using Lib.UI.Generic.DarkMode.Forms;
using Lib.UI.Generic.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static bim_base.data.CIM.CIMEnumeric;

namespace bim_base
{
    public partial class FormCIMHistory : Lib.UI.Generic.DarkMode.Forms.DarkForm
    {
        public FormCIMHistory()//(string _Title, EnumCimHistoryType _HistoryType)
        {
            InitializeComponent();

            //this.m_HistoryType = _HistoryType;
            //this.TitleText = _Title;
        }

        private EnumCimHistoryType m_HistoryType = EnumCimHistoryType.OperatorCall;



        private void FormCIMHistory_Load(object sender, EventArgs e)
        {
            //this.InitialzieGrid();

            List<HistoryItem> history = new List<HistoryItem>();

            switch (this.m_HistoryType)
            {
                case EnumCimHistoryType.OperatorCall:
                    history.AddRange(Automation.Instance.OperatorCallHistory.ToArray());
                    break;
                case EnumCimHistoryType.InterlockOccured:
                    break;
                case EnumCimHistoryType.InterlockReleased:
                    break;
                default:
                    break;
            }

            if (history.Count <= 0) return;

            List<string> rowDate = new List<string>();
            foreach (var item in history)
            {
                rowDate.Add(item.ReceivedTime.ToString("yyyy-MM-dd HH:mm:ss"));
                rowDate.Add(item.ID.ToString());
                rowDate.Add(item.Message);

                this.gridHistory.Rows.Add(rowDate.ToArray());
            }

            Automation.Instance.ReceivedOperatorCallEvent += Automation_ReceivedOperatorCallEvent;    

        }


        private async void Automation_ReceivedOperatorCallEvent(int _OpCallNum, string _OpCallText)
        {
            List<string> rowDate = new List<string>();

            switch (this.m_HistoryType)
            {
                case EnumCimHistoryType.OperatorCall:
                    rowDate.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    rowDate.Add(_OpCallNum.ToString());
                    rowDate.Add(_OpCallText);
                    break;
                case EnumCimHistoryType.InterlockOccured:
                    break;
                case EnumCimHistoryType.InterlockReleased:
                    break;
                default:
                    break;
            }
            this.gridHistory.Rows.Add(rowDate.ToArray());
        }
    }
}
