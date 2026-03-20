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

        // Save Button Click
        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("Motor Delay parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        // Exit Button Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Save
        private void save()
        {

        }

        // Form Data Load
        // Load
    }
}
