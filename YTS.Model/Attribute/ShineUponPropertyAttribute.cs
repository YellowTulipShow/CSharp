using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Model.Attribute
{
    /// <summary>
    /// 映射-属性-特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShineUponPropertyAttribute : Tools.AbsBasicAttribute
    {
        /// <summary>
        /// 是否映射数据
        /// </summary>
        public bool IsShineUpon { get { return _IsShineUpon; } set { _IsShineUpon = value; } }
        private bool _IsShineUpon = true;

        /// <summary>
        /// 排序序列, 数字越小越大
        /// </summary>
        public ushort SortIndex { get { return _SortIndex; } set { _SortIndex = value; } }
        private ushort _SortIndex = 999;
    }
}
