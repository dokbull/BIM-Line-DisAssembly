using QuadTreeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormTeach : Form, IForm
    {
        ProcessMain main = null;
 
        public event EventHandler button0_Click; // IN PP
        public event EventHandler button1_Click; // TRAY PP
        public event EventHandler button2_Click; // OUT PP

        public FormTeach(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        public void onShow(bool enable)
        {
        }

        private void inPPButton_Click(object sender, EventArgs e)
        {
            if (button0_Click != null)
                button0_Click(sender, e);
        }

        private void trayPPButton_Click(object sender, EventArgs e)
        {
            if (button1_Click != null)
                button1_Click(sender, e);
        }

        private void outPPButton_Click(object sender, EventArgs e)
        {
            if (button2_Click != null)
                button2_Click(sender, e);
        }
    }
}
