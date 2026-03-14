using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormStep : Form
    {
        ProcessMain main = null;

        public FormStep(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        private void FormStep_Load(object sender, EventArgs e)
        {
            stepGrid.setRowCol(3, 3, true, true);
            stepGrid.setHeader(new string[] { "UNIT", "AGO", "NOW" });
            stepGrid.setHeaderColor(Color.Black, Color.White);
            stepGrid.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);

            List<string> rowHeaderList = new List<string>();
            rowHeaderList.Add("ORG");
            rowHeaderList.Add("WORK");

            for (int i = 0; i < rowHeaderList.Count; i++)
            {
                int r = i + 1;

                stepGrid.setValue(r, 0, rowHeaderList[i]);
                stepGrid.setColors(r, 0, Color.Black, Color.White);
            }
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            stepGrid.setValue(1, 1, "" + main.procOrg().agoStep());
            stepGrid.setValue(1, 2, "" + main.procOrg().step());

            stepGrid.setValue(2, 1, "" + main.procInWork().agoStep());
            stepGrid.setValue(2, 2, "" + main.procInWork().step());

        }
    }
}
