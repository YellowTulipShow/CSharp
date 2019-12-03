using System;
using YTS.Model.Table;

namespace YTS.Model.DataType
{
    public class MCSList : AbsCSType
    {
        /// <summary>
        /// 输入转换
        /// </summary>
        public override object OutputConvert(object sourceValue, ColumnItemModel colmodel) {
            Type t = sourceValue.GetType();
            return sourceValue;
        }

        /// <summary>
        /// 输出转换
        /// </summary>
        public override object InputConvert(object sourceValue, ColumnItemModel colmodel) {
            Type t = sourceValue.GetType();
            return sourceValue;
        }
    }
}
