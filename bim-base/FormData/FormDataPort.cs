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
    public partial class FormDataPort : Form
    {
        ProcessMain main = null;

        public FormDataPort(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        private void FormSubPortSetting_Load(object sender, EventArgs e)
        {
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
        }

        private void bcrLabel_Click(object sender, EventArgs e)
        {
            FormNumpad dlg = new FormNumpad(mesLabel.Text, false);
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                string port = String.Format("COM{0:D}", Util.toInt32(dlg.getNewValue()));
                bcrLabel.Text = port;
            }
        }
        private void mesLabel_Click(object sender, EventArgs e)
        {
            FormNumpad dlg = new FormNumpad(mesLabel.Text, false);
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                string port = String.Format("COM{0:D}", Util.toInt32(dlg.getNewValue()));
                mesLabel.Text = port;
            }
        }
    }
}
