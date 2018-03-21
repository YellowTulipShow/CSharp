using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 money 类型
    /// </summary>
    public class MSSMoney : AbsDataType
    {
        public override string TypeName() {
            return @"money";
        }

        public override object TypeConvert(object sourceValue) {
            if (CheckData.IsObjectNull(sourceValue) || !CheckData.IsDouble(sourceValue)) {
                return GetDefaultValueString();
            }
            return sourceValue.ToString();
        }
    }
}
