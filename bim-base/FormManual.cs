using QuadTreeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bim_base
{
    public partial class FormManual : Form
    {
        FormTeach manualUnit1 = null;
        Form[] m_formManuals = null;

        ProcessMain main = null;
        

        int mSelForm = -1;
        public FormManual(ProcessMain procMain)
        {
            main = procMain;
            InitializeComponent();
            addAllForm();
            HideAllForm(0);
            //timerUpdate.Start();
            updateButtonAction();
        }

        public void updateButtonAction()
        {
            //pButtonControl1.setOutput1 += () =>
            //{
            //    //main.se)();
            //};
            //pButtonControl1.getInput += () =>
            //{
            //    return false;
            //};
            //pButtonControl1.getOutput += () =>
            //{
            //    return false;
            //};
            //pButtonControl1.getInput += isAlignStopper;
            //pButtonControl1.getAlarm += isAlarm;
            //pButtonControl1.isInterlock += isInterlock;
            //pButtonControl1.setWatchTime(2000);
        }
        private void FormTeach_Load(object sender, EventArgs e)
        {
            SelForm(0);
        }
        public void SelForm(int index)  
        {
            HideAllForm(index);
            mSelForm = index;
            if (mSelForm == 0)
            {
            }
            else if (index == 1)
            {
            }
        }
        public void HideAllForm(int _index)
        {
            //tableLayoutPanel1.SuspendLayout();
            //manualUnit1 = new FormManualUnit1(main);
            //manualUnit1.TopLevel = false;
            //manualUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
            //tableLayoutPanel1.Controls.Add(manualUnit1, 0, 1);
            //manualUnit1.Show();
            //tableLayoutPanel1.ResumeLayout();

        }
        void addAllForm() 
        {

            //TeachPanel.Dock = DockStyle.Fill;
            //TeachPanel.Padding = new Padding(0,0,0, 0);
            //TeachPanel.Margin = new Padding(0, 0, 0, 0);
        }

        private void traytab_Click(object sender, EventArgs e)
        {
            SelForm(0);
        }

        private void pptab_Click_1(object sender, EventArgs e)
        {
            SelForm(1);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            //pButtonControl1.updateState();
        }

        public bool isAlarm()
        {
            if (main.isAlarm() == true) {
                return true;
            }
            return true;
        }
        public bool isAlignStopper()
        {
            //return main.procCVAlignFR().isStopperUP();
            return false;
        }
        public bool isInterlock()
        {
            if (main.isAlarm() == true) {
                return true;
            }
            return true;
        }

        private void pButtonControl3_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl2_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void colorButton2_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void colorButton11_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl4_Click(object sender, EventArgs e)
        {

        }

        private void colorButton6_Click(object sender, EventArgs e)
        {

        }

        private void colorButton5_Click(object sender, EventArgs e)
        {

        }

        private void colorButton7_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl5_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl6_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl8_Click(object sender, EventArgs e)
        {

        }

        private void colorButton8_Click(object sender, EventArgs e)
        {

        }

        private void colorButton9_Click(object sender, EventArgs e)
        {

        }

        private void pButtonControl7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void colorButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
