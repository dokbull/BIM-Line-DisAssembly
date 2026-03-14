using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormDataModel : Form
    {
        ProcessMain main = null;

        public FormDataModel(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
        }

        private void FormSubDataModel_Load(object sender, EventArgs e)
        {
            List<string> items = new List<string>();

            for (int i = 0; i < Common.MODEL.Count; i++)
            {
                ModelInfo INFO = Common.MODEL[i];

                items.Add(INFO.modelName());
            }

            for (int i = 0; i < items.Count; i++)
                modelList.Items.Add(items[i]);

            int count = modelList.Items.Count;

            if (count > 0) 
            {
                int index = 0;

                for (int i = 0; i < count; i++)
                {
                    string name = modelList.Items[i].ToString();

                    if (Conf.CURR_MODEL == name)
                    {
                        index = i;
                        break;
                    }
                }

                modelList.SelectedIndex = index;
                currentModelLabel.Text = Conf.CURR_MODEL;

                uiTimer.Enabled = true;
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            string name = modelList.SelectedItem.ToString();
            string curName = Conf.CURR_MODEL;
            if (name == curName)
            {
                CMessageBox errMsg = new CMessageBox(Common.TITLE, "SAME NAME." , MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                errMsg.ShowDialog();
                return;
            }

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "CHANGE?", MessageBoxButtons.OKCancel, ContentAlignment.MiddleCenter);
            if (msgBox.ShowDialog() != DialogResult.OK)
                return;

            Conf.CURR_MODEL = curName;
            currentModelLabel.Text = Conf.CURR_MODEL;

            CMessageBox resMsg = new CMessageBox(Common.TITLE, "SUCCESS " + name, MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
            resMsg.ShowDialog();

            main.writeSetupLog("FormSubDataModel::changeButton_Click name:" + name + " bfModel:" + curName);
            
        }

        private void newModelName_Click(object sender, EventArgs e)
        {
            FormKeyboard dlg = new FormKeyboard();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                newModelName.Text = dlg.getKeyword();
            }
        }

        private void modelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = modelList.SelectedIndex;

            if (index < 0 || index >= modelList.Items.Count)
                return;

            newModelName.Text = modelList.Items[index].ToString();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {

        }

        private void uiTimer_Tick(object sender, EventArgs e)
        {
        }

        private void modeLabel_Click(object sender, EventArgs e)
        {
        }

        private void btTrayParameter_Click(object sender, EventArgs e)
        {
            
        }
    }
}
