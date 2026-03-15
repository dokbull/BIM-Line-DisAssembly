using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lib.Generic.ClassFunc
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="srcObj">변경하기 전 객체 Data</param>
    /// <param name="editObj">변경한 후 객체 Data</param>
    /// <returns></returns>
    public delegate bool EditorSelectedObjectEventHandler(object srcObj, out object editObj);

    /// <summary>
    /// example using attribute) [Editor(typeof(TypeConverterObjectEditEvent), typeof(System.Drawing.Design.UITypeEditor))]
    /// </summary>
    public class TypeConverterObjectEditEvent : UITypeEditor
    {
        /// <summary>
        /// 값을 변경하려고 할 때 발생한다.
        /// </summary>
        public static event EditorSelectedObjectEventHandler SelectedObjectEvent;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            if (context == null || context.Instance == null || provider == null)
            {
                return value;
            }

            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService editorService)
            {
                if (SelectedObjectEvent != null)
                {
                    if(SelectedObjectEvent(value, out object editValue))
                        value = editValue;

                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return UITypeEditorEditStyle.None;
        }
    }
}
