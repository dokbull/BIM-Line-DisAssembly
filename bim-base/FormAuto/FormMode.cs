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
    public partial class FormMode : Form
    {
        ProcessMain main = null;
        public FormMode(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void exitButton_onMouseClick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void autoButton_Click(object sender, EventArgs e)
        {
            autoButton._CHECKED = true;
            dryButton._CHECKED = false;
            byPassButton._CHECKED = false;
        }

        private void byPassButton_Click(object sender, EventArgs e)
        {
            autoButton._CHECKED = false;
            dryButton._CHECKED = false;
            byPassButton._CHECKED = true;
        }

        private void dryButton_Click(object sender, EventArgs e)
        {
            autoButton._CHECKED = false;
            dryButton._CHECKED = true;
            byPassButton._CHECKED = false;
        }

        private void passButton_Click(object sender, EventArgs e)
        {
            autoButton._CHECKED = false;
            dryButton._CHECKED = false;
            byPassButton._CHECKED = false;
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            Debug.debug("FormSubAuto::changeButton_Click");

            if (autoButton._CHECKED == true)
                main.setState(MACHINE_STATE.AUTO);
            else if (dryButton._CHECKED == true)
                main.setState(MACHINE_STATE.DRY_RUN);
            else if (byPassButton._CHECKED == true)
                main.setState(MACHINE_STATE.BYPASS);
        }

        private void FormSubAuto_Load(object sender, EventArgs e)
        {
            autoButton._CHECKED = false;
            dryButton._CHECKED = false;
            byPassButton._CHECKED = false;

            if (main.state() == MACHINE_STATE.AUTO)
                autoButton._CHECKED = true;
            else if (main.state() == MACHINE_STATE.DRY_RUN)
                dryButton._CHECKED = true;
            else if (main.state() == MACHINE_STATE.BYPASS)
                byPassButton._CHECKED = true;
        }
    }
}
