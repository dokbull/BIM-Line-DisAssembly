using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormPassword : Form
    {
        ProcessMain main = null;

        public FormPassword(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
            this.AcceptButton = okButton;
        }

        private void pwTextBox_Click(object sender, EventArgs e)
        {
            FormKeyboard dlg = new FormKeyboard();
            dlg._TYPE = KEYBOARD_TYPE.Password;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
                pwTextBox.Text = dlg.getKeyword();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (pwTextBox.Text != Conf.PASSWORD)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "Password is incorrect.", MessageBoxButtons.OK);
                msgBox.ShowDialog();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
