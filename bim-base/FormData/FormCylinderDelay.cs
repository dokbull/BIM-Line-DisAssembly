using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormCylinderDelay : Form
    {
        ProcessMain main = null;

        public FormCylinderDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
    }
}
