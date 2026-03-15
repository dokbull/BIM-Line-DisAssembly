using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Lib.Generic.ClassFunc
{
    public class TypeConverterEnumDescription : EnumConverter
    {
        private Type m_EnumType;


        public TypeConverterEnumDescription(Type type) : base(type)
        {
            this.m_EnumType = type;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            return destType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
                                         object value, Type destType)
        {
            FieldInfo fi = this.m_EnumType.GetField(Enum.GetName(this.m_EnumType, value));
            DescriptionAttribute dna = (DescriptionAttribute)System.Attribute.GetCustomAttribute(fi,
                                        typeof(DescriptionAttribute));
            if (dna != null)
                return dna.Description;
            else
                return value.ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
        {
            return srcType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture,
                                           object value)
        {
            foreach (FieldInfo fi in this.m_EnumType.GetFields())
            {
                DescriptionAttribute dna = (DescriptionAttribute)System.Attribute.GetCustomAttribute(fi,
                                            typeof(DescriptionAttribute));
                if ((dna != null) && ((string)value == dna.Description))
                    return Enum.Parse(this.m_EnumType, fi.Name);
            }
            return Enum.Parse(this.m_EnumType, (string)value);
        }
    }
}
