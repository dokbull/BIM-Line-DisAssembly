using Lib.UI.Generic.DarkMode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lib.UI.Generic.ControlFunc
{
    public static class DarkModeFunc
    {
        private const int LIGHT_VALUE = 160;

        public static (Color BackColor, Color ForeColor) GetDarkModeColor(int darkValue)
        {
            darkValue = Math.Max(0, Math.Min(255, darkValue)); // 0 ~ 255 사이의 값으로 제한

            Color foreColor = Color.White;
            Color backColor = System.Drawing.Color.FromArgb(darkValue, darkValue, darkValue);

            if (darkValue <= LIGHT_VALUE)
            {
                // 어두운 배경은 흰색 글자로
                foreColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }
            else
            {
                // 밝은 배경은 검은색 글자로
                foreColor = System.Drawing.Color.FromArgb(0, 0, 0);
            }

            return (backColor, foreColor);
        }

        public static int SetDarkMode(int darkValue, System.Windows.Forms.Control obj)
        {
            if(darkValue < 0) darkValue = 0;

            (Color BackColor, Color ForeColor) darkColor = GetDarkModeColor(darkValue);

            obj.BackColor = darkColor.BackColor;
            obj.ForeColor = darkColor.ForeColor;

            return darkValue;
        }

        //public static int SetDarkMode(int darkValue, Type objType, object obj)
        //{
        //    (Color BackColor, Color ForeColor) darkColor = GetDarkModeColor(darkValue);

        //    //objType.GetProperty("BackColor").SetValue(obj, darkColor.BackColor);
        //    //objType.GetProperty("ForeColor").SetValue(obj, darkColor.BackColor);

        //    Lib.Generic.Structure.ClassObject.SetPropertyValue(objType, obj, "BackColor", darkColor.BackColor);
        //    Lib.Generic.Structure.ClassObject.SetPropertyValue(objType, obj, "ForeColor", darkColor.BackColor);

        //    return darkValue;
        //}
    }
}
