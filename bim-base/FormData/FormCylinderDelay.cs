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

        // Save Button Click
        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("Cylinder Delay parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        // Save
        private void save()
        {

        }

        // Exit Button Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
        // Form Data Load
        // Load
    }
}
