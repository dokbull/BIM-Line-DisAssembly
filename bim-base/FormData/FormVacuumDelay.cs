using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormVacuumDelay : Form
    {
        ProcessMain main = null;

        public FormVacuumDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
    }
}
