using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 抽象-CSharp数据类型
    /// </summary>
    public abstract class AbsCsType
    {
        public AbsCsType() { }

        public abstract object ToModelValue(ColumnItemModel colinfo, object value);
    }
}
