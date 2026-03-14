using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

public partial class CIOButtonBoth : Button
{
    private Padding m_margin = new Padding(10, 0, 10, 0);

    private CBUTTON_POS m_ioPos = CBUTTON_POS.Left;
    private Size m_ioSize = new Size(20, 20);
    private string m_text = "Button";
    private ContentAlignment m_textAlign = ContentAlignment.MiddleCenter;
    private StringAlignment m_hAlign = StringAlignment.Center;
    private StringAlignment m_vAlign = StringAlignment.Center;

    private bool m_ioState = false;
    private bool m_ioState2 = false;
    private bool m_setOutputState = false;

    private Color m_trueColor = Color.Green; 
    private Color m_setOutColor = Color.Orange; // 출력 신호 발생중일때 사용
    private Color m_falseColor = Color.Red;

    public CIOButtonBoth()
    {
        InitializeComponent();
        this.Text = "";
    }

    public CBUTTON_POS _IO_POS
    {
        get { return m_ioPos; }
        set
        {
            m_ioPos = value;
        }
    }

    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [SettingsBindable(true)]
    public string _TEXT
    {
        get { return m_text; }
        set
        {
            m_text = value;
            Refresh();
        }
    }

    public bool _IO_STATE
    {
        get { return m_ioState; }
        set
        {
            m_ioState = value;
            Refresh();
        }
    }

    public bool _IO_STATE_EXTRA
    {
        get { return m_ioState2; }
        set
        {
            m_ioState2 = value;
            Refresh();
        }
    }

    public bool _OUT_STATE
    {
        get { return m_setOutputState; }
        set
        {
            m_setOutputState = value;
            Refresh();
        }
    }

    public Color _IO_TRUE_COLOR
    {
        get { return m_trueColor; }
        set { m_trueColor = value; 
            Refresh();
        }
    }

    public Color _IO_FALSE_COLOR
    {
        get { return m_falseColor; }
        set
        {
            m_falseColor = value;
            Refresh();
        }
    }

    public Size _IO_SIZE
    {
        get { return m_ioSize; }
        set
        {
            m_ioSize = value;
            Refresh();
        }
    }

    public Padding _MARGIN
    {
        get { return m_margin; }
        set
        {
            m_margin = value;
            Invalidate();
        }
    }

    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Localizable(true)]
    //[SRDescription("ButtonTextAlignDescr")]
    //[SRCategory("CatAppearance")]
    public ContentAlignment _TEXT_ALIGN
    {
        get { return m_textAlign; }
        set
        {
            m_textAlign = value;

            if ((m_textAlign & (ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight)) != 0)
            {
                m_vAlign = StringAlignment.Near;
            }
            else if ((m_textAlign & (ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight)) != 0)
            {
                m_vAlign = StringAlignment.Center;
            }
            else if ((m_textAlign & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight)) != 0)
            {
                m_vAlign = StringAlignment.Far;
            }


            if ((m_textAlign & (ContentAlignment.TopLeft | ContentAlignment.MiddleLeft | ContentAlignment.BottomLeft)) != 0)
            {
                m_hAlign = StringAlignment.Near;
            }
            else if ((m_textAlign & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != 0)
            {
                m_hAlign = StringAlignment.Center;
            }
            else if ((m_textAlign & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != 0)
            {
                m_hAlign = StringAlignment.Far;
            }

            Refresh();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        Rectangle textRect = ClientRectangle;
        Pen borderPen = new Pen(Color.Black, 2);

        Color color1 = m_falseColor;
        Color color2 = m_falseColor;

        if (m_setOutputState == true)
            color1 = m_setOutColor;
        else
        {
            if (m_ioState == true)
                color1 = m_trueColor;

            if (m_ioState2 == true)
                color2 = m_trueColor;
        }

        Brush brush1 = new SolidBrush(color1);
        Brush brush2 = new SolidBrush(color2);

        if (m_ioPos == CBUTTON_POS.Left)
        {
            graphics.FillEllipse(brush1, m_margin.Left, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);
            graphics.DrawEllipse(borderPen, m_margin.Left, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);

        }
        else if (m_ioPos == CBUTTON_POS.Right)
        {
            graphics.FillEllipse(brush1, Width - m_ioSize.Width - m_margin.Right, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);
            graphics.DrawEllipse(borderPen, Width - m_ioSize.Width - m_margin.Right, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);
        }
        else if (m_ioPos == CBUTTON_POS.Both)
        {
            graphics.FillEllipse(brush1, m_margin.Left, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);
            graphics.DrawEllipse(borderPen, m_margin.Left, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);

            graphics.FillEllipse(brush2, Width - m_ioSize.Width - m_margin.Right, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);
            graphics.DrawEllipse(borderPen, Width - m_ioSize.Width - m_margin.Right, ((Height - m_ioSize.Height) / 2) + m_margin.Top, m_ioSize.Width, m_ioSize.Height);
        }

        StringFormat stringFormat = new StringFormat
        {
            Alignment = m_hAlign,
            LineAlignment = m_vAlign
        };

        if (m_hAlign == StringAlignment.Near)
        {
            textRect.X += m_ioSize.Width + m_margin.Left + m_margin.Right;
            textRect.Width -= m_ioSize.Width + m_margin.Left + m_margin.Right;
        }
        if (m_hAlign == StringAlignment.Far)
        {
            textRect.X += m_ioSize.Width - m_margin.Left - m_margin.Right;
            textRect.Width -= m_ioSize.Width + m_margin.Left + m_margin.Right;
        }

        graphics.DrawString(m_text, Font, Brushes.Black, textRect, stringFormat);
        borderPen.Dispose();
    }

    public void setState(bool value)
    {
        _IO_STATE = value;
    }

    public void setState(bool input, bool output)
    {
        _IO_STATE = input;
        _OUT_STATE = output;
    }
}
