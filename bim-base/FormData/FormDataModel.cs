using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static SourceGrid.Cells.Models.ModelContainer;

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
            refreshModelName();

            uiTimer.Enabled = true;
        }

        void refreshModelName()
        {
            modelList.Items.Clear();

            for (int i = 0; i < Common.MODEL.Count; i++)
            {
                ModelInfo INFO = Common.MODEL[i];

                if (INFO.isUse() == true)
                {
                    modelList.Items.Add(INFO.modelName());
                }
            }

            int count = modelList.Items.Count;

            if (count > 0)
            {
                ModelInfo CURR_MODEL = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

                int index = 0;

                for (int i = 0; i < count; i++)
                {
                    string name = modelList.Items[i].ToString();

                    if (name == CURR_MODEL.modelName())
                    {
                        index = i;
                        break;
                    }
                }

                modelList.SelectedIndex = index;
                currentModelLabel.Text = CURR_MODEL.modelName();
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            string name = newModelName.Text;

            ModelInfo selModel = Common.MODEL_INFO(name);

            int modelIdx = selModel.index();

            if (Conf.CURR_MODEL_IDX == modelIdx)
            {
                CMessageBox errMsg = new CMessageBox(Common.TITLE, "SAME MODEL INDEX." , MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
                errMsg.ShowDialog();
                return;
            }

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "CHANGE?", MessageBoxButtons.OKCancel, ContentAlignment.MiddleCenter);
            if (msgBox.ShowDialog() != DialogResult.OK)
                return;

            Conf.CURR_MODEL_IDX = modelIdx;
            currentModelLabel.Text = Common.MODEL_INFO(modelIdx).modelName();

            CMessageBox resMsg = new CMessageBox(Common.TITLE, "SUCCESS", MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
            resMsg.ShowDialog();

            main.writeSetupLog("FormSubDataModel::changeButton_Click name:" + selModel.modelName());
        }

        private void newModelName_Click(object sender, EventArgs e)
        {
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

        private void modelNameChange_Click(object sender, EventArgs e)
        {
            int idx = modelList.SelectedIndex;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "NAME CHANGE?", MessageBoxButtons.OKCancel, ContentAlignment.MiddleCenter);
            
            if (msgBox.ShowDialog() != DialogResult.OK)
                return;

            ModelInfo model = Common.MODEL_INFO(idx);

            model.saveModelName(newModelName.Text);
            currentModelLabel.Text = Common.MODEL_INFO(idx).modelName();

            CMessageBox resMsg = new CMessageBox(Common.TITLE, "SUCCESS " + idx, MessageBoxButtons.OK, ContentAlignment.MiddleCenter);
            resMsg.ShowDialog();
        }

        private void currentModelLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
