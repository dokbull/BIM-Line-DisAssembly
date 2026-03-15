using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic.File
{
    public static class FileVersion
    {
        public enum EnumAssemblyAttribute
        {
            /// <summary>
            /// 제품명
            /// </summary>
            Product,
            /// <summary>
            /// 버전
            /// </summary>
            FileVersion,
            /// <summary>
            /// 설명
            /// </summary>
            Description,
            /// <summary>
            /// 제목
            /// </summary>
            TitleName
        }

        public static Dictionary<EnumAssemblyAttribute, string> GetAssemblyVersionString(List<EnumAssemblyAttribute> readAssemblyAttributes)
        {
            string name = string.Empty;
            string product = string.Empty;
            //string lineType = OEK.Util.StringFunction.GetEnumDescription((object)SystemSetting.GetInstance().General.LineType);
            string version = string.Empty;
            string description = string.Empty;
            string bitMode = (IntPtr.Size == 4) ? " x32" : " x64";

            System.Reflection.Assembly _assm = System.Reflection.Assembly.GetExecutingAssembly();

            foreach (System.Reflection.CustomAttributeData _data in System.Reflection.Assembly.GetExecutingAssembly().CustomAttributes)
            {
                if (_data.AttributeType == typeof(System.Reflection.AssemblyProductAttribute))
                {
                    if (_data.ConstructorArguments.Count > 0)
                    {
                        product = _data.ConstructorArguments[0].Value.ToString();
                    }

                }
                else if (_data.AttributeType == typeof(System.Reflection.AssemblyFileVersionAttribute))
                {
                    if (_data.ConstructorArguments.Count > 0)
                    {
                        version = _data.ConstructorArguments[0].Value.ToString();
                    }

                }
                else if (_data.AttributeType == typeof(System.Reflection.AssemblyDescriptionAttribute))
                {
                    if (_data.ConstructorArguments.Count > 0)
                    {
                        description = _data.ConstructorArguments[0].Value.ToString();
                    }
                }
                else if (_data.AttributeType == typeof(System.Reflection.AssemblyTitleAttribute))
                {
                    if (_data.ConstructorArguments.Count > 0)
                    {
                        name = _data.ConstructorArguments[0].Value.ToString();
                    }
                }

                if (name != string.Empty && product != string.Empty && version != string.Empty && description != string.Empty)
                {
                    break;
                }
            }

            Dictionary<EnumAssemblyAttribute, string> result = new Dictionary<EnumAssemblyAttribute, string>();
            foreach (EnumAssemblyAttribute attribute in readAssemblyAttributes)
            {
                switch (attribute)
                {
                    case EnumAssemblyAttribute.Product: result.Add(attribute, product); break;
                    case EnumAssemblyAttribute.FileVersion: result.Add(attribute, version); break;
                    case EnumAssemblyAttribute.Description: result.Add(attribute, description); break;
                    case EnumAssemblyAttribute.TitleName: result.Add(attribute, name); break;
                    default: break;
                }
            }

            return result;
        }
    }
}
