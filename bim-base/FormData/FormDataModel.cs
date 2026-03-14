using System;
using System.Collections.Generic;
using System.Drawing;
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

            string currentModel = Common.MODEL_INFO.currentModelName(); 

            for (int i = 0; i < modelList.Items.Count; i++)
            {
                if (modelList.Items[i].ToString() == currentModel)
                {
                    modelList.SelectedIndex = i;
                    currentModelLabel.Text = currentModel;
                    break;
                }
            }
        }

        private void fromButton_Click(object sender, EventArgs e)
        {
            if (modelList.SelectedIndex < 0)
                return;
            fromLabel.Text = modelList.SelectedItem.ToString();
        }

        private void toButton_Click(object sender, EventArgs e)
        {
            if (modelList.SelectedIndex < 0)
                return;
            toLabel.Text = modelList.SelectedItem.ToString();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if (fromLabel.Text.ToString() == toLabel.Text.ToString())
            {
                CMessageBox errMsg = new CMessageBox(Common.TITLE, "복사할 파일이 동일한 파일입니다.", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                errMsg.ShowDialog();
                return;
            }

            string message = String.Format("%1의 데이터를 %2에 복사하시겠습니까?" , fromLabel.Text, toLabel.Text);
            CMessageBox msgBox = new CMessageBox(Common.TITLE, message, MessageBoxButtons.OKCancel, ContentAlignment.MiddleCenter);
            if (msgBox.ShowDialog() != DialogResult.OK)
                return;

            string srcName = Common.MODEL_PATH + fromLabel.Text;
            string destName = Common.MODEL_PATH + toLabel.Text;

            bool ret = Common.MODEL_INFO.copy(srcName, destName);
            main.writeSetupLog("FormSubDataModel::createButton_Click name:" + srcName + " base:" + destName);
            if (ret == false)
            {
                CMessageBox resMsg = new CMessageBox(Common.TITLE, "모델 복사에 실패하였습니다.", MessageBoxButtons.OKCancel, ContentAlignment.MiddleCenter);
                if (resMsg.ShowDialog() != DialogResult.OK)
                    Debug.debug("copy ressult fail");
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string dest = currentModelLabel.Text;
            string name = newModelName.Text;
            bool ret = Common.MODEL_INFO.addModel(name);
            if (ret == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "모델 생성에 실패하였습니다.", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }

            string srcName = Common.MODEL_PATH + name;
            string destName = Common.MODEL_PATH + dest;
            ret = Common.MODEL_INFO.copy(destName, srcName);
            if (ret == false)
            {
                Common.MODEL_INFO.delete(name);
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "모델 생성에 실패하였습니다.", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
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
            bool ret = Common.MODEL_INFO.delete(name);
            if (ret == false)
            {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "FAIL", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                msgBox.ShowDialog();
                return;
            }
            main.writeSetupLog("FormSubDataModel::deleteButton_Click name:" + name);
            modelList.Items.RemoveAt(modelList.SelectedIndex);
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            string name = modelList.SelectedItem.ToString();
            string curName = Common.MODEL_INFO.currentModelName();

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

            CMessageBox resMsg = new CMessageBox(Common.TITLE, "SUCCESS " + name, MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
            resMsg.ShowDialog();

            main.writeSetupLog("FormSubDataModel::changeButton_Click name:" + name + " bfModel:" + curName);
            currentModelLabel.Text = curName;
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
            //int index = modelList.SelectedIndex;

            //if (index < 0 || index >= modelList.Items.Count)
            //    return;

            //newModelName.Text = modelList.Items[index].ToString();
        }
    }
}
