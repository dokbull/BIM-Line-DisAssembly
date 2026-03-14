using bim_base.data.CIM;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormSimMonitor : Form
    {
        ProcessMain main = null;

        public FormSimMonitor(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
            init();
        }

        private void init()
        {
            for (int i = 0; i < (int)CIMRead.READ_B.MAX; i++)
            {
                string[] text = new string[4];

                text[0] = "B" + (i + 0x1000).ToString("X4");
                text[2] = "B" + i.ToString("X4");
                text[1] = ((CIMRead.READ_B)i).ToString();
                text[3] = ((CIMWrite.WRITE_B)i).ToString();

                gridView.Rows.Add(text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < (int)CIMRead.READ_B.MAX; i++)
            {
                DataGridViewCell cell = gridView.Rows[i].Cells[0];

                CIMRead.READ_B addr = (CIMRead.READ_B)i;

                if(Automation.Instance.readCimBit(addr))
                    cell.Style.BackColor = Color.Lime;
                else
                    cell.Style.BackColor = Color.White;
            }

            for (int i = 0; i < (int)CIMWrite.WRITE_B.MAX; i++)
            {
                DataGridViewCell cell = gridView.Rows[i].Cells[2];

                CIMWrite.WRITE_B addr = (CIMWrite.WRITE_B)i;

                if (Automation.Instance.readCimBit(addr) == true)
                    cell.Style.BackColor = Color.Lime;
                else
                    cell.Style.BackColor = Color.White;
            }

            // WORD

            if (readDropDown.SelectedIndex > -1)
            {
                CIMRead.READ_W addr = (CIMRead.READ_W)readDropDown.SelectedIndex;
                readLabel.Text = Automation.Instance.readCimWord(addr);
            }

            if (writeDropDown.SelectedIndex > -1)
            {
                CIMWrite.WRITE_W addr = (CIMWrite.WRITE_W)writeDropDown.SelectedIndex;
                readLabel.Text = Automation.Instance.readCimWord(addr);
            }

            Refresh();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DataGridViewCell outputValueCell = gridView.Rows[e.RowIndex].Cells[2];

                CMessageBox msgBox = new CMessageBox(Common.TITLE, outputValueCell.Value.ToString() + " change value?", MessageBoxButtons.OKCancel);
                bool ret = msgBox.showDialog();

                if (ret == false)
                    return;

                bool value = true;

                if (outputValueCell.Style.BackColor == Color.Lime)
                    value = false;

                int idx = e.RowIndex;

                CIMWrite.WRITE_B addr = (CIMWrite.WRITE_B)idx;

                Automation.Instance.setCimBit(addr, value);
            }
        }

        private void FormSimMonitor_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < (int)CIMRead.READ_W.MAX; i++)
            {
                string text = ((CIMRead.READ_W)i).ToString();

                readDropDown.Items.Add(text);
            }

            for (int i = 0; i < (int)CIMWrite.WRITE_W.MAX; i++)
            {
                string text = ((CIMWrite.WRITE_W)i).ToString();

                writeDropDown.Items.Add(text);
            }
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            if (writeDropDown.SelectedIndex > -1)
            {
                CIMWrite.WRITE_W addr = (CIMWrite.WRITE_W)writeDropDown.SelectedIndex;

                string text = setTextBox.Text;
                Automation.Instance.writeCimWord(addr, text);
            }
        }
    }
}
