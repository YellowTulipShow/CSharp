using System;
using YTS.Model.Table;

namespace YTS.Model.DataType
{
    /// <summary>
    /// 抽象-C# 数据类型映射
    /// </summary>
    public abstract class AbsCSType : IDataType
    {
        public AbsCSType() { }

        public abstract object OutputConvert(object sourceValue, ColumnItemModel colmodel);

        public abstract object InputConvert(object sourceValue, ColumnItemModel colmodel);
    }
}
