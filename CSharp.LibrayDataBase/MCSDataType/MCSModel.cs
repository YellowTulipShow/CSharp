using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase.MCSDataType
{
    public class MCSModel : AbsCSType
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
