using QuadTreeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bim_base
{
    public partial class FormMonitor : Form
    {
        ProcessMain main = null;
        FormTeachPP _teachppAttach = null;

        int mSelForm = -1;
        public FormMonitor(ProcessMain procMain)
        {
            main = procMain;
            _teachppAttach = new FormTeachPP(main);
            InitializeComponent();                     
        }
        private void FormTeach_Load(object sender, EventArgs e)
        {
            
        }
        
       

        private void traytab_Click(object sender, EventArgs e)
        {
            
        }      
        private void colorButton2_Click(object sender, EventArgs e)
        {
            FormIOMonitor dlg = new FormIOMonitor(main);
            dlg.ShowDialog(this);
        }

        private void colorButton3_Click(object sender, EventArgs e)
        {
            FormInterface form = new FormInterface(main);
            form.ShowDialog();
        }

        private void colorButton4_Click(object sender, EventArgs e)
        {
            FormStep dlg = new FormStep(main);
            dlg.TopMost = true;
            dlg.ShowDialog();
        }

        private void colorButton8_Click(object sender, EventArgs e)
        {
            FormOutputTactime form = new FormOutputTactime(main);
            form.ShowDialog();
        }

        private void btnInterverSetting_Click(object sender, EventArgs e)
        {

            FormInverterSetting form = new FormInverterSetting(main);
            form.ShowDialog();
        }
    }
}
