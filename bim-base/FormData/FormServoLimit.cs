using bim_base.lib.Interfaces;
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
    public partial class FormServoLimit : Form, IViewCloseable
    {

        bool m_checkChange = false;

        ProcessMain main = null;
        public event Action OnCloseRequested;

        public FormServoLimit(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            OnCloseRequested?.Invoke();
        }
    }
}
