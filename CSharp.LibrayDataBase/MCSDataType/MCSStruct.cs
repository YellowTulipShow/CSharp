using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using CSharp.LibrayFunction;

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

            if (t.IsEnum) { // 特别区别: 枚举类型
                sourceValue = Convert.ToInt32(sourceValue);
                return sourceValue;
            }

            return sourceValue;
        }

        /// <summary>
        /// 输出转换
        /// </summary>
        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            //string name = colmodel.Property.Name;
            //if (name == @"TimeAdd") {
            //    name = name;
            //}
            Type propertytype = colmodel.Property.PropertyType;
            if (propertytype.BaseType.FullName == typeof(Enum).FullName) {
                sourceValue = ConvertTool.ObjToInt(sourceValue, 0);
            }

            if (propertytype.FullName == typeof(Int32).FullName) {
                sourceValue = ConvertTool.ObjToInt(sourceValue, 0);
            }
            if (propertytype.FullName == typeof(Single).FullName) {
                sourceValue = ConvertTool.ObjToFloat(sourceValue, 0);
            }
            if (propertytype.FullName == typeof(Double).FullName) {
                sourceValue = ConvertTool.ObjToFloat(sourceValue, 0);
            }
            if (propertytype.FullName == typeof(Boolean).FullName) {
                sourceValue = ConvertTool.ObjToBool(sourceValue, false);
            }
            if (propertytype.FullName == typeof(DateTime).FullName) {
                sourceValue = ConvertTool.ObjToDateTime(sourceValue, SqlDateTime.MinValue.Value);
            }

            return sourceValue;
        }
    }
}
