using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class DeviceButton : Button
{
    public int BorderThickness { get; set; } = 2;
    public Color BorderColor { get; set; } = Color.Black;

    public DeviceButton() : base()
    {

    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        // 버튼의 클라이언트 영역을 가져옴
        Rectangle rect = this.ClientRectangle;

        // 테두리의 크기를 줄임
        rect.Width -= 1;
        rect.Height -= 1;

        // 테두리 색과 두께를 설정
        using (Pen pen = new Pen(BorderColor, BorderThickness))
        {
            pevent.Graphics.DrawRectangle(pen, rect);
        }
    }
}
