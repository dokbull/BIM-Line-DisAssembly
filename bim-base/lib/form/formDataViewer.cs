using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class formDataViewer : Form
{
    CSettings m_settings = new CSettings(pathUtil.savePath() +
        "\\dataViewer.ini");

    formDataViewerSetting.Setting m_viewerSetting = null;
    MySqlManager m_sqlManager = null;

    public formDataViewer()
    {
        InitializeComponent();

        m_viewerSetting = new formDataViewerSetting.Setting(m_settings);

        m_sqlManager = new MySqlManager(m_viewerSetting.address,
            m_viewerSetting.account,
            m_viewerSetting.password,
            m_viewerSetting.scheme);

        if (m_sqlManager.connTest() == false)
        {
            MessageBox.Show("DB 접속에 실패하였습니다.");
            return;
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        formDataViewerSetting form = new formDataViewerSetting();
        form.ShowDialog();
    }

    private void formDataViewer_Load(object sender, EventArgs e)
    {
        extDataGridView1.DataSource = m_sqlManager.dataTable("SELECT * FROM test");
    }
}
