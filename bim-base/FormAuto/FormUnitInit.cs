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
    public partial class FormUnitInit : Form
    {
        ProcessMain main = null;

        bool m_pp = false;

        public FormUnitInit(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        private void initPPButton_Click(object sender, EventArgs e)
        {
            m_pp = !m_pp;
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            m_pp = true;
        }

        private void DeselectAllButton_Click(object sender, EventArgs e)
        {
            m_pp = false;
        }

        private void initialButton_Click(object sender, EventArgs e)
        {
            if (m_pp == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "선택된 항목이 없습니다.", MessageBoxButtons.OKCancel);
                msgBox.ShowDialog();
                return;
            }

            //TODO : Init start
        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
            ppButton.BackColor = m_pp ? Color.Lime : Color.White;
        }
    }//class
}//namespace
