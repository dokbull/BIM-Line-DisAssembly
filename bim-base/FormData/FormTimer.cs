using System;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormTimer : Form
    {
        ProcessMain main = null;

        public FormTimer(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("System parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        private void save()
        {

        }

        private bool GetLabelValue(Label label, ref double value)
        {
            if (string.IsNullOrEmpty(label.Text)) return false;

            var str = label.Text.Replace(" Sec", "");
            return double.TryParse(str, out value);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSubSystemManager_Load(object sender, EventArgs e)
        {
            load();
        }

        private void load()
        {

        }

        private void lbTime_Click(object sender, EventArgs e)
        {
        }
    }
}
