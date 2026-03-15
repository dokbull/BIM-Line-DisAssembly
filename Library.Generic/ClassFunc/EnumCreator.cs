using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;

namespace Lib.Generic.ClassFunc
{
    public class EnumCreator
    {
        private Dictionary<string, int> m_EnumKeyValus = new Dictionary<string, int>();

        public object EnumObject { get; set; } = null;

        public bool IsCreated { get; private set; } = false;
        public string EnumName { get; private set; } = string.Empty;
        public int EnumCount { get; private set; } = 0;

        


        public void CreateEnum(string enumName, Dictionary<string, int> enumKeyValus)
        {
            var enumBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("DynamicEnumAssembly"), AssemblyBuilderAccess.Run)
                .DefineDynamicModule("DynamicEnumModule")
                .DefineEnum(enumName, TypeAttributes.Public, typeof(int));

            foreach (var value in enumKeyValus)
            {
                enumBuilder.DefineLiteral(value.Key, value.Value);
            }

            var dynamicEnumType = enumBuilder.CreateType();
            this.EnumObject = Activator.CreateInstance(dynamicEnumType);


            this.m_EnumKeyValus = enumKeyValus;
            this.EnumName = enumName;
            this.EnumCount = enumKeyValus.Count;
            this.IsCreated = true;
        }


        public List<string> GetNames()
        {
            return this.m_EnumKeyValus.Keys.ToList();
        }

        public List<int> GetValues()
        {
            return this.m_EnumKeyValus.Values.ToList();
        }

        public bool TryParse(string enumKey, out object parsedEnum)
        {
            parsedEnum = null;
            if (this.m_EnumKeyValus.ContainsKey(enumKey) == false) return false;

            parsedEnum = Enum.Parse(this.EnumObject.GetType(), enumKey);
            return true;
        }

        public bool TryParse(int enumValue, out object parsedEnum)
        {
            parsedEnum = null;
            if (this.m_EnumKeyValus.ContainsValue(enumValue) == false) return false;

            parsedEnum = Enum.Parse(this.EnumObject.GetType(), enumValue.ToString());
            return true;
        }
    }
}
