using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

class CStatusLabel : Label
{
    // status -   0: none, 1: on, 2: off
    public int _Status
    {
        get;
        set;
    }

    public CStatusLabel()
    {
        BorderStyle = BorderStyle.FixedSingle;
    }

    public void setStatus(int idx)
    {
        _Status = idx;

        Color color = Color.DarkGray;

        if (idx == 0)
        {
            color = Color.DarkRed;
        }
        else if (idx == 1)
        {
            color = Color.LawnGreen;
        }
        else if (idx == 2)
        {
            color = Color.DarkGray;
        }

        BackColor = color;
    }
}
