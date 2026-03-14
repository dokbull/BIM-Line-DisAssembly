using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

public partial class DirectoryTreeView : UserControl
{
    public event EventHandler changedFolder;

    TreeNode m_rootNode = null;
    bool m_setFolder = false;

    public DirectoryTreeView()
    {
        InitializeComponent();
        
        if (m_rootNode == null)
            m_rootNode = dirTreeView.Nodes.Add("My Computer");
    }

    public void setRootPath(string path)
    {
        if (path != dirTreeView.Nodes[0].Text)
        {
            m_setFolder = false;
            dirTreeView.Nodes[0].Nodes.Clear();
        }
        if (m_setFolder == true)
            return;

        dirTreeView.Nodes[0].Text = path;
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirTreeView.Nodes[0].Text);
            DirectoryInfo[] dirInfoList = dirInfo.GetDirectories();

            foreach (DirectoryInfo info in dirInfoList)
            {
                TreeNode dirNode = dirTreeView.Nodes[0].Nodes.Add(info.Name);
                dirNode.Tag = info.FullName;
            }
        }
        catch (Exception)
        {
            MessageBox.Show(path + " 경로를 찾을수 없습니다.", "directoryTreeView",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dirTreeView.Nodes[0].Text = "My Computer";
            return;
        }

        m_setFolder = true;
    }

    private void dirTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        TreeNode selectNode = e.Node;

        if (selectNode == m_rootNode)
        {
            if (m_setFolder == true) return;

            try
            {
                DriveInfo[] driveList = DriveInfo.GetDrives();

                m_rootNode.Nodes.Clear();

                foreach (DriveInfo info in driveList)
                {
                    TreeNode node = m_rootNode.Nodes.Add(info.Name);
                    node.Name = node.Text;
                    node.Tag = node.Text;
                }

                m_rootNode.Expand();
            }
            catch (Exception)
            {
                MessageBox.Show("Drive 목록을 불러오지 못하였습니다.", "directoryTreeView",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        else
        {
            for (int i = 0; i < selectNode.Parent.Nodes.Count; i++)
            {
                TreeNode node = selectNode.Parent.Nodes[i];

                if (node == selectNode) continue;

                node.Remove();
                i--;
            }

            selectNode.Nodes.Clear();

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(selectNode.Tag.ToString());
                DirectoryInfo[] dirInfoList = dirInfo.GetDirectories();

                foreach (DirectoryInfo info in dirInfoList)
                {
                    FileAttributes attr = info.Attributes;

                    if ((attr & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                    if ((attr & FileAttributes.System) == FileAttributes.System) continue;
                    if ((attr & FileAttributes.Temporary) == FileAttributes.Temporary) continue;

                    TreeNode dirTree = selectNode.Nodes.Add(info.Name);
                    dirTree.Name = info.Name;
                    dirTree.Tag = info.FullName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("디렉토리 접근에 실패하였습니다.", "directoryTreeView",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                dirTreeView.SelectedNode = selectNode.Parent;

                return;
            }
            
            if (changedFolder != null)
                changedFolder(selectNode.Tag.ToString(), null);

            selectNode.Expand();
        }
    }
}
