using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

public static class ControlExtensions
{
    public static T Clone<T>(this T controlToClone)
        where T : Control
    {
        PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        T instance = Activator.CreateInstance<T>();

        foreach (PropertyInfo propInfo in controlProperties)
        {
            if (propInfo.CanWrite == false)
                continue;
            
            if (propInfo.Name == "WindowTarget")
                continue;

            propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
        }
        return instance;
    }

    public static int toInt(this bool value)
    {
        return (value == true) ? 1 : 0;
    }
}
