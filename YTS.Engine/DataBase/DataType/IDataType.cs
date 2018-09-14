using System;
using YTS.Model.Table;

namespace YTS.Model.DataType
{
    /// <summary>
    /// 接口-数据类型
    /// </summary>
    public interface IDataType
    {
        /// <summary>
        /// 输入转换
        /// </summary>
        object OutputConvert(object sourceValue, ColumnItemModel colmodel);
        /// <summary>
        /// 输出转换
        /// </summary>
        object InputConvert(object sourceValue, ColumnItemModel colmodel);
    }
}
