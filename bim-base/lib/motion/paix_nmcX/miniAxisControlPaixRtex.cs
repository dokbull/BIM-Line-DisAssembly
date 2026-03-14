using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

public partial class miniAxisControlPaixRtex : UserControl
{
    PaixRtexMotionAxis m_axis = null;
    public bool m_select = false;

    public miniAxisControlPaixRtex()
    {
        InitializeComponent();
    }

    private void AxisControl_Load(object sender, EventArgs e)
    {
    }

    public void setParameter(PaixRtexMotionAxis axis)
    {
        m_axis = axis;
        nameLabel.Text = axis.name() + "\r\n"
            + "(" + axis.pos().ToString("0.00") + ")";
    }
    public PaixRtexMotionAxis axis()
    {
        return m_axis;
    }

    public bool _SELECT
    {
        get { return m_select; }
        set
        {
            m_select = value;

            if (m_select)
                this.BackColor = Color.Lime;
            else
                this.BackColor = Color.Transparent;
        }
    }
    public void updateStatus()
    {
        if (m_axis == null)
            return;

        PaixRtexMotionAxis axis = m_axis;

        nameLabel.Text = axis.name() + "\r\n"
            + "(" + axis.pos().ToString("0.00") + ")";

        setColor(inpos, axis.inpos());
        setColor(minusLimit, axis.negLimit());
        setColor(plusLimit, axis.posLimit());

        if (axis.alarm())
        {
            servoOn.BackColor = Color.DarkRed;
            servoOn.ForeColor = Color.White;
        }
        else
        {
            setColor(servoOn, axis.servoOn());
            servoOn.ForeColor = Color.Black;
        }

        this.Refresh();
    }

    void setColor(Control con, bool value)
    {
        if (value == true)
        {
            if (con.BackColor == Color.Lime)
                return;

            con.BackColor = Color.Lime;
            return;
        }
        else
        {
            con.BackColor = Color.White;
        }
    }

    private void nameLabel_Click(object sender, EventArgs e)
    {
        this._SELECT = !this._SELECT;
    }
} // class
