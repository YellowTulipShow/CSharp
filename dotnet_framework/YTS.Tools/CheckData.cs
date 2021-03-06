﻿using System;
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
        /// 比较两个类型是否是一样的
        /// </summary>
        /// <param name="t1">类型: 1</param>
        /// <param name="t2">类型: 2</param>
        /// <returns>是否相同</returns>
        public static bool IsTypeEqual(Type t1, Type t2) {
            if (CheckData.IsObjectNull(t1) && CheckData.IsObjectNull(t2)) {
                // 都为空
                return true;
            }
            if (CheckData.IsObjectNull(t1) && !CheckData.IsObjectNull(t2)) {
                // t1为空 t2不为空
                return false;
            }
            if (!CheckData.IsObjectNull(t1) && CheckData.IsObjectNull(t2)) {
                // t1不为空 t2为空
                return false;
            }
            // 都不为空
            return t1.Equals(t2) && t1.FullName == t2.FullName;
        }

        /// <summary>
        /// 比较两个类型是否是一样的 深入查询类型的继承链 (递归)
        /// </summary>
        /// <param name="depth_find_type">需要递归查询的类型</param>
        /// <param name="type">用于比较的类型</param>
        /// <param name="is_depth">是否深入查询</param>
        /// <returns>是否相同</returns>
        public static bool IsTypeEqualDepth(Type depth_find_type, Type type, bool is_depth) {
            if (IsTypeEqual(typeof(object), type)) {
                return true;
            }
            if (!is_depth) {
                return IsTypeEqual(depth_find_type, type);
            }
            if (IsTypeEqual(depth_find_type, type)) {
                return true;
            }
            if (CheckData.IsObjectNull(depth_find_type) ||
                CheckData.IsObjectNull(type)) {
                return false;
            }
            return IsTypeEqualDepth(depth_find_type.BaseType, type, true);
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
