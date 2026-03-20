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
    public partial class FormServoVelocity : Form
    {
        bool m_checkChange = false;

        ProcessMain main = null;
        public event Action OnCloseRequested;
        public FormServoVelocity(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void btnServoVelocityExit_Click(object sender, EventArgs e)
        {
            OnCloseRequested?.Invoke();
        }

    }
}
