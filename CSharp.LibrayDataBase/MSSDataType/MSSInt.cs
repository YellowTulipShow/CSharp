using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 int 类型
    /// </summary>
    public class MSSInt : AbsDBType
    {
        public override string TypeName() {
            return @"int";
        }

        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            if (CheckData.IsObjectNull(sourceValue) || !CheckData.IsNumber(sourceValue)) {
                return GetDefaultValueString();
            }
            return sourceValue.ToString();
        }
    }
}
