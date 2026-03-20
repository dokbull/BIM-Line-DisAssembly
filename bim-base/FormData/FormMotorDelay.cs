using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormMotorDelay : Form
    {
        ProcessMain main = null;

        public FormMotorDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
    }
}
