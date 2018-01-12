using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    public class Money : AbsFieldType
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
