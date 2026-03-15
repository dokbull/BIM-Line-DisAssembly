using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib.UI.Generic.ControlFunc
{
    public static class PropertyGridFunc
    {
        /// <summary>
        /// ProertyGrid의 Name Column Width를 설정합니다.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        /// <param name="isInheritance">PropertyGrid를 상속받은 UserControl인지 여부</param>
        public static void SetNameColumnWidth(PropertyGrid grid, int width, bool isInheritancedUserControl = false)
        {
            FieldInfo oFieldInfo = null;
            if (isInheritancedUserControl)
            {
                // Using reflection to access non-public members of the base PropertyGrid class
                oFieldInfo = typeof(PropertyGrid).GetField("gridView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            }
            else
            {
                oFieldInfo = grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            if(oFieldInfo == null) return;

            if (!(oFieldInfo.GetValue(grid) is Control oView)) return;

            var oMethodInfo = oView.GetType().GetMethod("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic);
            if (oMethodInfo == null) return;

            oMethodInfo.Invoke(oView, new object[] { width });
        }
        
    }
}
