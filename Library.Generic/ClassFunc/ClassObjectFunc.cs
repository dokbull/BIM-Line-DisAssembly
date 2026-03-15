using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic.ClassFunc
{
    public static class ClassObjectFunc
    {
        /// <summary>
        /// 클래스의 속성값을 복사하여 객체를 생성한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="obj">복사할 원본 객체</param>
        /// <returns></returns>
        public static T Clone<T>(Type classType, T obj)
        {
            // 깊은 복사
            T clone = (T)Activator.CreateInstance(classType);
            foreach (var property in classType.GetProperties())
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(obj);
                    property.SetValue(clone, value);
                }
            }
            return clone;
        }

        /// <summary>
        /// 클래스의 속성값을 복사한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="source">복사할 원본 객체</param>
        /// <param name="target">복사된 객체</param>
        public static void Copy<T>(Type classType, T source, T target)
        {
            // 얕은 복사
            foreach (var property in classType.GetProperties())
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(source);
                    property.SetValue(target, value);
                }
            }
        }

        /// <summary>
        /// Property의 이름을 가져온다. (ex) GetPropertyName(() => this.Property
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public static string GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }

        /// <summary>
        /// Class 객체에 정의되어 있는 Property를 수집하여 반환한다.
        /// </summary>
        /// <param name="classType">typeof(Class)</param>
        /// <param name="filter">ex) new List() { new BrowsableAttribute(true) }</param>
        /// <returns></returns>
        public static List<string> GetPropertyNameList(Type classType, List<System.Attribute> filter = null)
        {
            List<string> results = new List<string>();

            PropertyDescriptorCollection properties = null;

            if (filter == null || filter.Count == 0)
            {
                properties = TypeDescriptor.GetProperties(classType);
            }
            else
            {
                properties = TypeDescriptor.GetProperties(classType, filter.ToArray());
            }


            List<string> list2 = new List<string>();
            new Queue<string>();
            Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
            Dictionary<int, List<string>> dictionary2 = new Dictionary<int, List<string>>();
            while (classType != typeof(object))
            {
                dictionary[classType] = dictionary.Count;
                dictionary2[dictionary.Count - 1] = new List<string>();
                classType = classType.BaseType;
            }

            foreach (PropertyDescriptor item in properties)
            {
                if (item.ComponentType != null)
                {
                    int key = dictionary[item.ComponentType];
                    dictionary2[key].Add(item.Name);
                }
                else
                {
                    results.Add(item.Name);
                }
            }

            if (dictionary2.Count > 0)
            {
                List<int> list3 = dictionary2.Keys.ToList();
                list3.Reverse();
                foreach (int item2 in list3)
                {
                    if (dictionary2[item2].Count > 0)
                    {
                        list2.AddRange(dictionary2[item2]);
                    }
                }
            }

            if (list2.Count > 0)
            {
                results.InsertRange(0, list2);
            }

            return results;
        }

        /// <summary>
        /// Class에 propertyName과 동일한 이름의 Property가 존재하는지 확인한다.
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool IsExistProperty(Type classType, string propertyName)
        {
            return GetPropertyNameList(classType).Contains(propertyName);
        }

        /// <summary>
        /// Property의 값을 설정한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classType"></param>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(Type classType, T obj, string propertyName, object value)
        {
            foreach (var property in classType.GetProperties())
            {
                if (property.Name != propertyName) continue;

                if (property.CanWrite == false) return;

                property.SetValue(obj, value);
                return;
            }
        }

    }
}
