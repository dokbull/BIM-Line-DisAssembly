using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Etier.IconHelper;

namespace libTest.lib.control
{
    public partial class FileListView : UserControl
    {
        public event EventHandler setPath;

        public FileListView()
        {
            InitializeComponent();

            listView.Columns.Add("Name", listView.Width - 100, HorizontalAlignment.Center);
        }

        public void changedFolder(object sender, EventArgs e)
        {
            string path = (string)sender;
            setFolder(path);
        }

        public void setFolder(string path)
        {
            listView.Items.Clear();

            DirectoryInfo dir = new DirectoryInfo(path);

            try
            {
                imageList.Images.Clear();

                FileInfo[] files = dir.GetFiles();

                int cnt = 0;

                foreach (FileInfo file in files)
                {
                    Icon icon = IconReader.GetFileIcon(file.Name, IconReader.IconSize.Small, false);
                    imageList.Images.Add(file.Name, icon);
                    listView.Items.Add(file.Name, cnt);
                    listView.Items[cnt].Tag = file.FullName;
                    cnt++;
                }
            }
            catch (Exception /*ex*/)
            {
                MessageBox.Show("File 목록을 불러오지 못하였습니다.", "directoryTreeView",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;

            if (setPath != null)
                setPath(listView.SelectedItems[0].Tag, null);
        }
    }
}
