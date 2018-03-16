﻿using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 检查数据 Is: True为符合条件 False不匹配条件   (待详细处理,测试)
    /// </summary>
    public static class CheckData
    {
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

        #region  === Is Size Empty ===
        /// <summary>
        /// 判断是否: ICollection 数据集合 大小 为 '空'
        /// </summary>
        public static bool IsSizeEmpty(this ICollection icoll) {
            return IsObjectNull(icoll) || icoll.Count <= 0;
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
        #endregion

        #region === Is Data Type ===
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


        /// <summary>
        /// 模型不能使用的判断
        /// </summary>
        /// <typeparam name="M">数据模型类型</typeparam>
        /// <param name="item">数据来源</param>
        /// <returns>True: 不能使用, 反之亦然</returns>
        public delegate bool ModelNotUseIF<M>(M item) where M : AbsBasicDataModel;
        /// <summary>
        /// 检查模型可以使用 单例
        /// </summary>
        /// <typeparam name="M">数据模型类型</typeparam>
        /// <param name="item">数据来源</param>
        /// <param name="errorMethod">不能使用的判断条件</param>
        /// <returns>True: 可以使用, 反之亦然</returns>
        public static bool CheckModelCanUseItem<M>(M item, ModelNotUseIF<M> errorMethod) where M : AbsBasicDataModel {
            return !(CheckData.IsObjectNull(item) || errorMethod(item));
        }
        /// <summary>
        /// 检查模型可以使用 多例
        /// </summary>
        /// <typeparam name="M">数据模型类型</typeparam>
        /// <param name="array">数据来源</param>
        /// <param name="errorMethod">不能使用的判断条件</param>
        /// <returns>True: 可以使用, 反之亦然</returns>
        public static bool CheckModelCanUseArray<M>(M[] array, ModelNotUseIF<M> errorMethod) where M : AbsBasicDataModel {
            if (!CheckData.IsSizeEmpty(array)) {
                foreach (M item in array) {
                    if (CheckModelCanUseItem(item, errorMethod)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
