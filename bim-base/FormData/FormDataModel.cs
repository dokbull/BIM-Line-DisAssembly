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
            List<string> items = Common.MODEL_INFO.loadModelList();

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
            string dest = currentModelLabel.Text;
            string name = newModelName.Text;
            bool ret = Common.MODEL_INFO.addModel(name);
            if (ret == false || name == "VSFRONT")
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "Can't Create This Model with Name.", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }

            string srcName = Common.MODEL_PATH + name;
            string destName = Common.MODEL_PATH + dest;
            ret = Common.MODEL_INFO.copy(destName, srcName);
            if (ret == false)
            {
                Common.MODEL_INFO.delete(name);
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "Can't Create This Model with Name.", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }

            main.writeSetupLog("FormSubDataModel::createButton_Click name:" + name + " base:" + dest);
            modelList.Items.Add(name);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (modelList.SelectedItem == null || modelList.SelectedIndex < 0)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "선택된 모델이 없습니다.", MessageBoxButtons.OK);
                msgBox.ShowDialog();
                return;
            }
            if (modelList.Items.Count <= 1)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "하나 이상의 모델이 존재해야합니다.", MessageBoxButtons.OK);
                msgBox.ShowDialog();
                return;
            }

            bool msgRet = CMessageBox.showMessage("Do you want to delete ?");

            if (msgRet == false)
                return;

            string name = Common.MODEL_PATH + modelList.SelectedItem.ToString();
            string CognexFile = Common.MODEL_PATH + @"Vision\Cognex\" + $"{modelList.SelectedItem.ToString()}.vpp";
            string VSDataFile = Common.MODEL_PATH + @"Vision\" + $"{modelList.SelectedItem.ToString()}.json";
            bool ret = Common.MODEL_INFO.delete(name);
            if (ret == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "FAIL", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }

            /*******************************************************************************************************************/
            ret = Common.MODEL_INFO.delete(CognexFile);
            if (ret == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "FAIL", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }
            /*******************************************************************************************************************/
            ret = Common.MODEL_INFO.delete(VSDataFile);
            if (ret == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "FAIL", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }
            /*******************************************************************************************************************/
            main.writeSetupLog("FormSubDataModel::deleteButton_Click name:" + name);
            modelList.Items.RemoveAt(modelList.SelectedIndex);
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

            Common.MODEL_INFO.modelChange(name);

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Do you want to save?", MessageBoxButtons.OKCancel, ContentAlignment.MiddleCenter);

            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            Common.MODEL_INFO.load();
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
