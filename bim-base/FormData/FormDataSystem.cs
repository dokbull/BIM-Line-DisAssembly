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
    public partial class FormDataSystem : Form
    {
        ProcessMain main = null;

        double m_gripOnDelay = 0.0d;
        double m_gripOffDelay = 0.0d;

        public FormDataSystem(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            main.writeBottomHistory("System parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSubSystemManager_Load(object sender, EventArgs e)
        {
            gripOnLabel.Text = m_gripOnDelay.ToString("0.0") + " Sec";
            gripOffLabel.Text = m_gripOffDelay.ToString("0.0") + " Sec";
        }

        private void gripOnDelay_Click(object sender, EventArgs e)
        {
            FormNumpad dlg = new FormNumpad(m_gripOnDelay.ToString("0.0"));
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                m_gripOnDelay = Util.toDouble(dlg.getNewValue());
                gripOnLabel.Text = m_gripOnDelay.ToString("0.0") + " Sec";
            }
        }

        private void gripOffDelay_Click(object sender, EventArgs e)
        {
            FormNumpad dlg = new FormNumpad(m_gripOffDelay.ToString("0.0"));
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                m_gripOffDelay = Util.toDouble(dlg.getNewValue());
                gripOffLabel.Text = m_gripOffDelay.ToString("0.0") + " Sec";
            }
        }

        private void mesUseLabel_Click(object sender, EventArgs e)
        {
        }

        private void mesModeLabel_Click(object sender, EventArgs e)
        {
        }

        private void mesSuffixTypeLabel_Click(object sender, EventArgs e)
        {
        }
    }
}
