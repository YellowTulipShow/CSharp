using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    public class Int : AbsFieldType
    {
        public override string FieldTypeName() {
            return @"int";
        }

        public override string PrintSaveValue(object programValue) {
            if (CheckData.IsObjectNull(programValue) || !CheckData.IsNumber(programValue))
                return string.Empty;
            return programValue.ToString();
        }
    }
}
