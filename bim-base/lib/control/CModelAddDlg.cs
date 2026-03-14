using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class CModelAddDlg : Form
{
    List<string> m_modelList = new List<string>();

    public CModelAddDlg(List<string> modelList=null)
    {
        InitializeComponent();

        m_modelList = modelList;
    }

    public CModelAddDlg(ListBox.ObjectCollection collection)
    {
        InitializeComponent();

        foreach (object obj in collection)
        {
            m_modelList.Add(obj.ToString());
        }
    }

    public string resultText()
    {
        return textBox1.Text;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(resultText()))
        {
            CMessageBox msgBox = new CMessageBox(pathUtil.productName(), "모델명을 입력해주세요", MessageBoxButtons.OK);
            msgBox.ShowDialog();
            return;
        }

        if (m_modelList.Contains(resultText().ToUpper()))
        {
            CMessageBox msgBox = new CMessageBox(pathUtil.productName(), "중복된 모델명입니다. 다른 모델명을 입력해주세요", MessageBoxButtons.OK);
            msgBox.ShowDialog();
            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;

        Close();
    }
}
