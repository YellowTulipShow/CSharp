using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 转化工具
    /// </summary>
    public class ConvertTool
    {
        public ConvertTool() { }
        public override string ToString() {
            return @"这是一个转化工具类 常用于静态方法";
        }

        /// <summary>
        /// "垃圾"集合字符串堆 (组合所有参数ToString()方法组合)
        /// </summary>
        public static string ToString(params object[] objs) {
            StringBuilder returnString = new StringBuilder();
            foreach (object obj in objs) {
                returnString.Append(obj.ToString());
            }
            return returnString.ToString();
        }

        /// <summary>
        /// 字符串转数组列表
        /// </summary>
        /// <param name="strValue">要转化的字符串</param>
        /// <param name="Symbol">用于分隔的间隔符号</param>
        /// <returns></returns>
        public static string[] StringToArray(string strValue, char Symbol) {
            if (CheckData.IsStringNull(strValue)) {
                return new List<string>().ToArray();
            }
            string[] strarr = strValue.Split(Symbol);
            return strarr;
        }
        /// <summary>
        /// 字符串转泛型字符串列表
        /// </summary>
        /// <param name="strValue">要转化的字符串</param>
        /// <param name="Symbol">用于分隔的间隔符号</param>
        /// <returns></returns>
        public static List<string> StringToList(string strValue, char Symbol) {
            return new List<string>(StringToArray(strValue, Symbol));
        }

        /// <summary>
        /// 数组列表转字符串
        /// </summary>
        /// <param name="arrayList">需要合并的字符串数组</param>
        /// <param name="Symbol">用于间隔内容的间隔符号</param>
        /// <returns></returns>
        public static string ArrayToString(string[] arrayList, char Symbol) {
            StringBuilder strs = new StringBuilder();
            for (int i = 0; i < arrayList.Length; i++) {
                if (i != 0) {
                    strs.Append(Symbol);
                }
                strs.Append(arrayList[i]);
            }
            return strs.ToString();
        }
        /// <summary>
        /// 泛型字符串列表转字符串
        /// </summary>
        /// <param name="arrayList">需要合并的字符串泛型列表</param>
        /// <param name="Symbol">用于间隔内容的间隔符号</param>
        /// <returns></returns>
        public static string ListToString(List<string> arrayList, char Symbol) {
            return ArrayToString(arrayList.ToArray(), Symbol);
        }

        /// <summary>
        /// 将Int类型列表转为String字符串列表
        /// </summary>
        public static List<string> ListIntToListString(List<int> objvalues) {
            List<string> strLists = new List<string>();
            foreach (int item in objvalues) {
                strLists.Add(item.ToString());
            }
            return strLists;
        }

        /// <summary>
        /// 获得 time.Year time.Month time.Day 00:00:00 点时间
        /// </summary>
        public static DateTime GetTimeZero(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
        }
        /// <summary>
        /// 获得 time.Year time.Month time.Day 23:59:59 点时间
        /// </summary>
        public static DateTime GetTimeTwoFour(DateTime time) {
            return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59);
        }
    }
}
