using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormPortSetting : Form
    {
        ProcessMain main = null;
        public FormPortSetting(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSubPortSetting_Load(object sender, EventArgs e)
        {

        }

        private void lbPort_Click(object sender, EventArgs e)
        {
            if (sender is Label lb)
            {
            }
        }
    }
}
