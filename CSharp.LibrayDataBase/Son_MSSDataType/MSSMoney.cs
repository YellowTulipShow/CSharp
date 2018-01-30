using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 money 类型
    /// </summary>
    public class MSSMoney : AbsFieldType
    {
        public override string FieldTypeName() {
            return @"money";
        }

        public override string PrintSaveValue(object programValue) {
            if (CheckData.IsObjectNull(programValue) || !CheckData.IsDouble(programValue))
                return string.Empty;
            return programValue.ToString();
        }
    }
}
