using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class CTablControl : TabControl
{
    protected override void WndProc(ref Message m)
    {
        // TCM_ADJUSTRECT 메시지를 가로채서 탭 영역을 숨김
        if (m.Msg == 0x1328 && !DesignMode)
        {
            m.Result = (IntPtr)1;
            return;
        }
        base.WndProc(ref m);
    }
}
