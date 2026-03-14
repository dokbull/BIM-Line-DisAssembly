using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.CompilerServices;

public class CRect
{
    public float x;
    public float y;
    public float w;
    public float h;

    public CRect()
    {
    }

    public CRect(double _x, double _y, double _w, double _h)
    {
        x = (float)_x; y = (float)_y; w = (float)_w; h = (float)_h;
    }

    public CRect(float _x, float _y, float _w, float _h)
    {
        x = _x; y = _y; w = _w; h = _h;
    }

    public CRect(Rectangle rect)
    {
        x = rect.X; y = rect.Y; w = rect.Width; h = rect.Height;
    }

    public CRect(CRect rect)
    {
        x = rect.x; y = rect.y; w = rect.w; h = rect.h;
    }

    // * 연산자 오버로드
    public static CRect operator *(CRect rect, float value)
    {
        return new CRect
        {
            x = rect.x * value,
            y = rect.y * value,
            w = rect.w * value,
            h = rect.h * value
        };
    }

    // * 연산자 오버로드 (역순도 지원)
    public static CRect operator *(float value, CRect rect)
    {
        return rect * value;
    }
}


public class GraphicsManager
{
    RectangleF m_rect;

    float m_x, m_y, m_width, m_height;

    //float wRatio = 1.0f;
    //float hRatio = 1.0f;

    Graphics m_g = null;

    Font m_font = null;
    Brush m_brush;
    Pen m_pen;

    Matrix m_matrix = new Matrix();
    bool m_reverse = false;

    public GraphicsManager(Graphics g, float x, float y, float width, float height, bool reverse = false)
    {
        m_g = g;

        m_reverse = reverse;

        if (m_reverse)
        {
            m_matrix.Scale(-1, 1); // x축으로 -1을 곱하여 좌우 반전을 수행합니다.
            m_matrix.Translate(-width, 0); // 이미지의 폭만큼 이동시켜 원래 위치로 돌려놓습니다.
            g.Transform = m_matrix;
        }

        m_x = m_rect.X = x;
        m_y = m_rect.Y = y;
        m_width = m_rect.Width = width;
        m_height = m_rect.Height = height;

        m_font = new Font("NanumGothic", 11.0F, FontStyle.Regular);

        m_brush = Brushes.Black;
        m_pen = new Pen(m_brush);
    }

    public void setColor(Color color)
    {
        m_brush = new SolidBrush(color);
        m_pen = new Pen(m_brush);
    }

    public void setFontSize(float value)
    {
        m_font = new Font("NanumGothic", value, FontStyle.Regular);
    }

    public void drawString(float x, float y, string text, ContentAlignment align = ContentAlignment.MiddleLeft)
    {
        SizeF sf = measureString(text, m_font);

        PointF pt = new PointF(x, y);

        //TODO@tmdwn..나머지 정렬에 관한 것도 작성 해야 됨.
        if (align == ContentAlignment.MiddleCenter)
        {
            pt.X -= sf.Width / 2;

            if (m_reverse)
                pt.X = (m_width - sf.Width - pt.X);

            pt.Y -= sf.Height / 2;
        }
        else
        {
            if (m_reverse)
                pt.X = (m_width - sf.Width - pt.X);
        }

        m_g.ResetTransform();
        m_g.DrawString(text, m_font, m_brush, pt);

        if (m_reverse)
            m_g.Transform = m_matrix;
    }

    public void drawRect(double x, double y, double w, double h)
    {
        drawRect((float)x, (float)y, (float)w, (float)h);
    }

    public void drawRect(float x, float y, float width, float height)
    {
        m_g.DrawRectangle(m_pen, x, y, width, height);
    }

    public void drawRect(CRect rect)
    {
        m_g.DrawRectangle(m_pen, rect.x, rect.y, rect.w, rect.h);
    }

    public void drawRect(Rectangle rect)
    {
        m_g.DrawRectangle(m_pen, rect);
    }

    public void drawLine(Pen pen, float x1, float y1, float x2, float y2)
    {
        m_g.DrawLine(pen, x1, y1, x2, y2);
    }

    public void fillRect(float x, float y, float width, float height)
    {
        m_g.FillRectangle(m_brush, x, y, width, height);
    }

    public SizeF measureString(string text, Font font)
    {
        return m_g.MeasureString(text, font);
    }

    public void fillTriangle(float x, float y, float size)
    {
        PointF[] pt = new PointF[3];
        pt[0].X = x;
        pt[0].Y = y;

        pt[1].X = x - (float)(size / 1.5);
        pt[1].Y = y - size;

        pt[2].X = x + (float)(size / 1.5);
        pt[2].Y = y - size;

        m_g.FillPolygon(m_brush, pt);
    }

    public void drawTriangle(float x, float y, float size)
    {
        PointF[] pt = new PointF[3];
        pt[0].X = x;
        pt[0].Y = y;

        pt[1].X = x - (float)(size / 1.5);
        pt[1].Y = y - size;

        pt[2].X = x + (float)(size / 1.5);
        pt[2].Y = y - size;

        m_g.DrawPolygon(m_pen, pt);
    }
}
