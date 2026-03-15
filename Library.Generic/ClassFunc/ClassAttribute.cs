using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic.ClassFunc
{

    public static class ClassAttribute
    {
        #region Class


        /// <summary>
        /// [Category("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <returns></returns>
        public static string GetClassCategory(Type classType)
        {
            CategoryAttribute categoryAttribute = classType.GetCustomAttributes(typeof(CategoryAttribute), false).FirstOrDefault() as CategoryAttribute;
            return categoryAttribute?.Category ?? string.Empty;
        }

        /// <summary>
        /// [DisplayName("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <returns></returns>
        public static string GetClassDisplayName(Type classType)
        {
            DisplayNameAttribute displayNameAttribute = classType.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
            return displayNameAttribute?.DisplayName ?? string.Empty;
        }

        /// <summary>
        /// [Description("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <returns></returns>
        public static string GetClassDescription(Type classType)
        {
            DescriptionAttribute descriptionAttribute = classType.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute?.Description ?? string.Empty;
        }

        #endregion

        #region Property


        /// <summary>
        /// [Category("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="propertyName">nameof(this.Proptery)</param>
        /// <returns></returns>
        public static string GetPropertyCategory(Type classType, string propertyName)
        {
            PropertyInfo property = classType.GetProperty(propertyName);
            CategoryAttribute categoryAttribute = property.GetCustomAttributes(typeof(CategoryAttribute), false).FirstOrDefault() as CategoryAttribute;
            return categoryAttribute?.Category ?? string.Empty;
        }

        /// <summary>
        /// [DisplayName("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="propertyName">nameof(this.Property)</param>
        /// <returns></returns>
        public static string GetPropertyDisplayName(Type classType, string propertyName)
        {
            PropertyInfo property = classType.GetProperty(propertyName);
            DisplayNameAttribute displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
            return displayNameAttribute?.DisplayName ?? string.Empty;
        }

        /// <summary>
        /// [Description("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="propertyName">nameof(this.Property)</param>
        /// <returns></returns>
        public static string GetPropertyDescription(Type classType, string propertyName)
        {
            PropertyInfo property = classType.GetProperty(propertyName);
            DescriptionAttribute descriptionAttribute = property.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute?.Description ?? string.Empty;
        }



        #endregion

        #region Method


        /// <summary>
        /// [Category("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="propertyName">nameof(this.Method)</param>
        /// <returns></returns>
        public static string GetMethodCategory(Type classType, string propertyName)
        {
            MethodInfo method = classType.GetMethod(propertyName);
            var categoryAttribute = method.GetCustomAttributes(typeof(CategoryAttribute), false).FirstOrDefault() as CategoryAttribute;
            return categoryAttribute?.Category ?? string.Empty;
        }

        /// <summary>
        /// [DisplayName("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="methodName">nameof(this.Method) </param>
        /// <returns></returns>
        public static string GetMethodDisplayName(Type classType, string methodName)
        {
            MethodInfo method = classType.GetMethod(methodName);
            var displayNameAttribute = method.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
            return displayNameAttribute?.DisplayName ?? string.Empty;
        }


        /// <summary>
        /// [Description("Get")] 를 가져온다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="methodName">nameof(this.Method)</param>
        /// <returns></returns>
        public static string GetMethodDescription(Type classType, string methodName)
        {
            MethodInfo method = classType.GetMethod(methodName);
            var descriptionAttribute = method.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute?.Description ?? string.Empty;
        }


        #endregion

    }
}
