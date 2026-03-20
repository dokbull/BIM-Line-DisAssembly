using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bim_base.lib.Interfaces;

namespace bim_base
{
    public partial class FormServoOrigin : Form, IViewCloseable
    {

        bool m_checkChange = false;

        ProcessMain main = null;
        public event Action OnCloseRequested;

        public FormServoOrigin(ProcessMain procMain)
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
