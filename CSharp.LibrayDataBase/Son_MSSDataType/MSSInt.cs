using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase.MSSDataType
{
    /// <summary>
    /// Microsoft SQL Server 字段 int 类型
    /// </summary>
    public class MSSInt : AbsFieldType
    {
        public override string FieldTypeName() {
            return @"int default(0)";
        }

        public override string PrintSaveValue(object programValue) {
            if (CheckData.IsObjectNull(programValue) || !CheckData.IsNumber(programValue))
                return string.Empty;
            return programValue.ToString();
        }
    }
}
