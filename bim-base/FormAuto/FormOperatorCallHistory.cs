using bim_base.data.CIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormOperatorCallHistory : Lib.UI.Generic.DarkMode.Forms.DarkForm
    {
        public FormOperatorCallHistory()
        {
            InitializeComponent();
        }

        private void InitialzieGrid()
        {
            gridHistory.setRowCol(1, 3);
            string[] header = new string[] { "DateTime", "No.", "Message" };
            gridHistory.setHeader(header);

            gridHistory.setHeaderColor(Color.White, Color.Black);
            gridHistory.FixedRows = 1;

            int w = gridHistory.Width - 2;
            gridHistory.Columns[0].Width = 80; w -= 80;
            gridHistory.Columns[1].Width = 60; w -= 60;
            gridHistory.Columns[2].Width = w;

            gridHistory.setTextAlignment(0, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            gridHistory.setTextAlignment(0, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);
            gridHistory.setTextAlignment(0, 2, DevAge.Drawing.ContentAlignment.MiddleCenter);

        }


        private void AddGridRowData(string[] texts)
        {
            int row = 1;
            int colCount = gridHistory.ColumnsCount;
            gridHistory.Rows.Insert(row);

            for (int i = 0; i < colCount; i++)
            {
                CCell cell = new CCell();
                CViewCell viewCell = new CViewCell();

                gridHistory[row, i] = cell;
                gridHistory[row, i].View = viewCell;
            }

            gridHistory[row, 0].Value = texts[0];
            gridHistory[row, 1].Value = texts[1];
            gridHistory[row, 2].Value = texts[2];

            gridHistory.setTextAlignment(row, 0, DevAge.Drawing.ContentAlignment.MiddleCenter);
            gridHistory.setTextAlignment(row, 1, DevAge.Drawing.ContentAlignment.MiddleCenter);

        }

        private void FormOperatorCallHistory_InputLanguageChanging(object sender, InputLanguageChangingEventArgs e)
        {
            this.InitialzieGrid();

            List<OperatorCallData> history = new List<OperatorCallData>();
            history.AddRange(Automation.Instance.OperatorCallHistory.ToArray());

            string strDateTime = string.Empty;
            string strNo = string.Empty;
            string strMessage = string.Empty;

            foreach (var item in history)
            {
                strDateTime = item.ReceivedTime.ToString("yyyy-MM-dd HH:mm:ss");
                strNo = item.ID.ToString();
                strMessage = item.Message;

                this.AddGridRowData(new string[] { strDateTime, strNo, strMessage });
            }
        }
    }
}
