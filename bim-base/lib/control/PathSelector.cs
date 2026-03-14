using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

public partial class PathSelector : UserControl
{
    public PathSelector()
    {
        InitializeComponent();
    }

    private void PathSelector_Load(object sender, EventArgs e)
    {
    }

    public string SelectedPath
    {
        get;
        set;
    }

    public bool checkValidDirectory()
    {
        if (SelectedPath == "")
            return false;

        string path = SelectedPath;

        if (Directory.Exists(path))
            return true;

        try
        {
            Directory.CreateDirectory(path);

            if (Directory.Exists(path))
                return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Lib::control PathSelector directory create failed. reason:" + e.Message);
            return false;
        }

        return false;
    }

    private void pathChangeButton_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog dlg = new FolderBrowserDialog();

        bool ret = checkValidDirectory();

        if (ret)
        {
            dlg.SelectedPath = label1.Text;
        }

        DialogResult result = dlg.ShowDialog();

        if (result == DialogResult.OK)
        {
            label1.Text = dlg.SelectedPath;
            SelectedPath = label1.Text;
        }
    }
}
