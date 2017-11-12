using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        public static bool IsObjectNull(object obj) {
            return (Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value));
        }
        /// <summary>
        /// String 对象 是否为空 无值
        /// </summary>
        public static bool IsStringNull(String str) {
            return String.Equals(str, String.Empty) || String.Equals(str, "") || IsObjectNull(str) || str.Length <= 0;
        }
        /// <summary>
        /// DataTable 对象 是否为空表 无值
        /// </summary>
        public static bool IsDataTableEmpty(DataTable dt) {
            return dt.Rows.Count <= 0 || IsObjectNull(dt) ? true : false;
        }
    }
}
