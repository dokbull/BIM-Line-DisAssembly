using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.AxHost;

public partial class CImageButton : Button
{
    //public enum CBUTTON_POS
    //{
    //    None = -1,
    //    Left = 0,
    //    Right = 1,
    //}
    private Padding m_margin = new Padding(7, 0, 7, 0);

    private CBUTTON_POS m_imgPos = CBUTTON_POS.Left;
    private Size m_imgSize = new Size(20, 20);
    private string m_text = "Button";
    private ContentAlignment m_textAlign = ContentAlignment.MiddleCenter;
    private StringAlignment m_hAlign = StringAlignment.Center;
    private StringAlignment m_vAlign = StringAlignment.Center;


    private bool m_checked = false;
    private Color m_backColor = Color.FromKnownColor(KnownColor.Control);
    private Color m_checkedBackColor = Color.Transparent;

    private Image m_image = null;

    public CImageButton()
    {
        InitializeComponent();

        BackColor = m_checked ? m_checkedBackColor : m_backColor;
    }

    public CBUTTON_POS _IMG_POS
    {
        get { return m_imgPos; }
        set
        {
            m_imgPos = value;
            Invalidate(); // 컨트롤을 다시 그리도록 함
        }
    }

    public string _TEXT
    {
        get { return m_text; }
        set
        {
            m_text = value;
            Invalidate(); // 텍스트 변경 시 컨트롤을 다시 그리도록 함
        }
    }

    public bool _CHECKED
    {
        get { return m_checked; }
        set
        {
            m_checked = value;

            BackColor = m_checked ? m_checkedBackColor : m_backColor;
            Invalidate();
        }
    }

    public Color _BACK_COLOR
    {
        get { return m_backColor; }
        set { m_backColor= value;
            BackColor = m_checked ? m_checkedBackColor : m_backColor;
            Invalidate();
        }
    }

    public Color _CHECKED_BACK_COLOR
    {
        get { return m_checkedBackColor; }
        set
        {
            m_checkedBackColor = value;

            BackColor = m_checked ? m_checkedBackColor : m_backColor;
            Invalidate();
        }
    }

    public Image _IMAGE
    {
        get { return m_image; }
        set
        {
            m_image = value;
            Invalidate();
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

    public Size _IMAGE_SIZE
    {
        get { return m_imgSize; }
        set
        {
            m_imgSize = value;
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
        Pen borderPen = new Pen(Color.Black, 1);

        if (m_imgPos == CBUTTON_POS.Left && m_image != null)
        {
            graphics.DrawImage(m_image, m_margin.Left, ((Height - m_imgSize.Height) / 2) + m_margin.Top, m_imgSize.Width, m_imgSize.Height);
            textRect.X += m_imgSize.Width + m_margin.Left + m_margin.Right;
        }
        else if (m_imgPos == CBUTTON_POS.Right && m_image != null)
        {
            graphics.DrawImage(m_image, Width - m_imgSize.Width - m_margin.Right, ((Height - m_imgSize.Height) / 2) + m_margin.Top, m_imgSize.Width, m_imgSize.Height);
            textRect.Width -= m_imgSize.Width + m_margin.Left + m_margin.Right;
        }

        StringFormat stringFormat = new StringFormat
        {
            Alignment = m_hAlign,
            LineAlignment = m_vAlign
        };
        graphics.DrawString(m_text, Font, Brushes.Black, textRect, stringFormat);

        borderPen.Dispose();
    }
}
