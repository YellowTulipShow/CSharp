using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase.MCSDataType
{
    /// <summary>
    /// Microsoft C# 基础数据类型(bool, byte, char, DateTime, decimal, double, short, int, long, sbyte, float, string, ushort, uint, ulong)
    /// </summary>
    public class MCSStruct : AbsCSType
    {
        /// <summary>
        /// 输入转换
        /// </summary>
        public override object OutputConvert(object sourceValue, ColumnItemModel colmodel) {
            Type t = sourceValue.GetType();
            if (t.IsEnum) {
                sourceValue = Convert.ToInt32(sourceValue);
                return sourceValue;
            }
            return sourceValue;
        }

        /// <summary>
        /// 输出转换
        /// </summary>
        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            Type t = sourceValue.GetType();
            return sourceValue;
        }
    }
}
