using System;
using System.Windows.Forms;
using System.Drawing;

public class CPanelTransparent : CPanel
{
    public CPanelTransparent() : base()
    {
        this.SetStyle(
            ControlStyles.SupportsTransparentBackColor |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw, true);

        this.BackColor = Color.Transparent;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        base.OnPaintBackground(e);
    }
}

