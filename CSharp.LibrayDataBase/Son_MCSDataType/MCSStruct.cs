using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase.MCSDataType
{
    public class MCSStruct : AbsCsType
    {
        public override object ToModelValue(ColumnItemModel colinfo, object value) {
            return value;
        }
    }
}
