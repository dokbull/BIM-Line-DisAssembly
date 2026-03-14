using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

class ExtTextBox : TextBox
{
    public override bool AutoSize
    {
        get
        {
            return base.AutoSize;
        }
        set
        {
            base.AutoSize = value;
        }
    }

    public ExtTextBox()
    {
        this.AutoSize = false;
    }
}
