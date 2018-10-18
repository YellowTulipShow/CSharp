using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;

namespace YTS.Tools
{
    /// <summary>
    /// 检查数据 Is: True为符合条件 False不匹配条件   (待详细处理,测试)
    /// </summary>
    public static class CheckData
    {
        #region ====== Basic Code Method Check: ======
        /// <summary>
        /// Object 对象 是否为空 无值
        /// </summary>
        public static bool IsObjectNull(this object obj) {
            return (Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value));
        }
        /// <summary>
        /// String 对象 是否为空 无值 如有需要请将参数.Trim()
        /// </summary>
        public static bool IsStringNull(this String str) {
            return IsObjectNull(str) || String.Equals(str, String.Empty) || String.Equals(str, "") || str.Length <= 0;
        }
        #endregion

        #region  ====== Is Size Empty ======
        /// <summary>
        /// 判断是否: IList.T 泛型集合 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty<T>(this IList<T> list) {
            return IsObjectNull(list) || list.Count <= 0;
        }
        /// <summary>
        /// 判断是否: DataSet 数据集 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty(this DataSet ds) {
            return IsObjectNull(ds) || ds.Tables.Count <= 0;
        }
        /// <summary>
        /// 判断是否: DataTable 数据表 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty(this DataTable dt) {
            return IsObjectNull(dt) || dt.Rows.Count <= 0;
        }
        /// <summary>
        /// 判断是否: DataRow 数据行 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty(this DataRow row) {
            return IsObjectNull(row) || row.Table.Rows.Count <= 0;
        }
        /// <summary>
        /// 判断是否: XmlNodeList 数据行 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty(this XmlNodeList list) {
            return IsObjectNull(list) || list.Count <= 0;
        }
        /// <summary>
        /// 判断是否: Dictionary 数据行 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty<K, V>(this Dictionary<K, V> dic) {
            return IsObjectNull(dic) || dic.Count <= 0;
        }
        #endregion

        #region ====== Is Data Type ======

        #region === IsType ===
        /// <summary>
        /// 检查两个类型是否相同
        /// </summary>
        /// <param name="tV1">值类型</param>
        /// <param name="tV2">值类型</param>
        /// <returns></returns>
        public static bool IsTypeEqual(Type tV1, Type tV2) {
            return tV1.Equals(tV2);
        }
        /// <summary>
        /// 检查两个类型是否相同
        /// </summary>
        /// <typeparam name="T1">泛型类型</typeparam>
        /// <typeparam name="T2">泛型类型</typeparam>
        /// <returns></returns>
        public static bool IsTypeEqual<T1, T2>() {
            return IsTypeEqual(typeof(T1), typeof(T2));
        }
        /// <summary>
        /// 检查两个类型是否相同
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="tV">值类型</param>
        /// <returns></returns>
        public static bool IsTypeEqual<T>(Type tV) {
            return IsTypeEqual(typeof(T), tV);
        }
        /// <summary>
        /// 检查两个类型是否相同(是否深入递归检查每层父级) 只检查了类的继承(不包括结果)
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="tV">值类型</param>
        /// <param name="isDepth">是否深入递归检查每层父级</param>
        /// <returns></returns>
        public static bool IsTypeEqual<T>(Type tV, bool isDepth) {
            if (!isDepth) {
                return IsTypeEqual<T>(tV);
            }
            if (IsTypeEqual<T>(tV) || IsTypeEqual<T, object>()) {
                return true;
            }
            if (IsTypeEqual<object>(tV)) {
                return false;
            }
            return IsTypeEqual<T>(tV.BaseType, true);
        }

        /// <summary>
        /// 检查数据值的类型
        /// </summary>
        /// <typeparam name="T">要检查的类型</typeparam>
        /// <param name="v_object">要检查的数据</param>
        public static bool IsTypeEqual<T>(object v_object) {
            return IsTypeEqual(typeof(T), v_object.GetType());
        }
        /// <summary>
        /// 检查数据值的类型(递归检查每层父级)
        /// </summary>
        /// <typeparam name="T">要检查的类型</typeparam>
        /// <param name="v_object">要检查的数据</param>
        public static bool IsTypeEqual<T>(object v_object, bool isDepth) {
            if (!isDepth) {
                return IsTypeEqual<T>(v_object);
            }
            return IsTypeEqual<T>(v_object.GetType(), true);
        }
        #endregion

        /// <summary>
        /// 判断对象是否可以转成int型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNumber(object o) {
            int tmpInt;
            if (o == null) {
                return false;
            }
            if (o.ToString().Trim().Length == 0) {
                return false;
            }
            if (!int.TryParse(o.ToString(), out tmpInt)) {
                return false;
            } else {
                return true;
            }
        }
        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression">表达内容</param>
        /// <returns></returns>
        public static bool IsDouble(object expression) {
            if (expression != null)
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");

            return false;
        }
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(String strEmail) {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidDoEmail(String strEmail) {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(String strUrl) {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        #endregion
    }
}
