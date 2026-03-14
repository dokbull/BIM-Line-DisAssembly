using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

class FormUtil
{
    public static void warning(string caption, string text)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void setMdiChild(Form form, Form parent)
    {
        form.Margin = new Padding(0);
        form.MdiParent = parent;

        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;
    }
}
