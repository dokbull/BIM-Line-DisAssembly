using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class CNotifyIcon
{
    NotifyIcon notifyIcon1 = null;
    ContextMenuStrip contextMenuStrip1 = null;
    ToolStripMenuItem eXITToolStripMenuItem = null;

    private System.ComponentModel.IContainer components = null;

    public event EventHandler onExit;
    public event EventHandler onView;

    public CNotifyIcon(string name, System.Drawing.Icon icon)
    {
        this.components = new System.ComponentModel.Container();

        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
        this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;


        // contextMenuStrip1
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eXITToolStripMenuItem});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);

        // eXITToolStripMenuItem
        this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
        this.eXITToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        this.eXITToolStripMenuItem.Text = "EXIT";
        this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click);

        // notifyIcon1
        this.notifyIcon1.Icon = icon;
        this.notifyIcon1.Text = name;
        this.notifyIcon1.Visible = true;
        this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);   
    }

    private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CMessageBox msgBox = new CMessageBox(Common.TITLE,
            "종료 하시겠습니까 ?", MessageBoxButtons.OKCancel);

        DialogResult result = msgBox.ShowDialog();

        if (result == DialogResult.Cancel)
        {
            return;
        }

        if (onExit != null)
            onExit(this, null);
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (onView != null)
            onView(this, null);
    }
}
