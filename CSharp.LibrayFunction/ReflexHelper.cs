using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 反射帮助操作类
    /// </summary>
    public static class ReflexHelper
    {
        /// <summary>
        /// 获得 "Object" 对象公共属性 及其值
        /// </summary>
        public static Dictionary<string, string> GetObjectAttribute(Object obj)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            Type type = obj.GetType();
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                dicArray.Add(pi.Name, pi.GetValue(obj, null).ToString());
            }
            return dicArray;
        }

        /// <summary>
        /// 获得 "Object" 对象公共字段 及其值
        /// </summary>
        public static Dictionary<string, string> GetObjectField(Object obj)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            Type type = obj.GetType();
            FieldInfo[] fis = type.GetFields();
            foreach (FieldInfo fi in fis)
            {
                dicArray.Add(fi.Name, fi.GetValue(obj).ToString());
            }
            return dicArray;
        }

        /// <summary>
        /// 获得 "Object" 对象公共属性 名称列表
        /// </summary>
        public static string[] GetObjectAttributeNames(Object obj) {
            List<string> names = new List<string>();
            foreach (KeyValuePair<string,string> item in GetObjectAttribute(obj)) {
                names.Add(item.Key);
            }
            return names.ToArray();
        }

        /// <summary>
        /// 克隆 对象 公共属性属性值
        /// </summary>
        public static T CloneObjectAttribute<T>(T obj) {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            T model = (T)type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, obj, null);
            foreach (PropertyInfo pi in properties) {
                if (pi.CanWrite) {
                    object value = pi.GetValue(obj, null);
                    pi.SetValue(model, value, null);
                }
            }
            return model;
        }
    }
}
