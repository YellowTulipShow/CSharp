using System;
using System.Reflection;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 解析-模型属性值为表列
    /// </summary>
    public interface IPropertyColumn
    {
        ColumnModel[] AnalysisPropertyColumns();
    }

    /// <summary>
    /// 列信息模型
    /// </summary>
    public class ColumnModel : AbsBasicsDataModel
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = String.Empty;

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get { return _property; } set { _property = value; } }
        private PropertyInfo _property = null;

        /// <summary>
        /// 数据列特性
        /// </summary>
        public ColumnAttribute ColAttr { get { return _colAttr; } set { _colAttr = value; } }
        private ColumnAttribute _colAttr = null;

        /// <summary>
        /// 排序比较 实现委托: Comparison<T> 值含义: 小于0:(x 小于 y)。0:(x 等于 y)。大于0:(x 大于 y)。
        /// </summary>
        public static int Sort(ColumnModel x, ColumnModel y) {
            // 主键
            if (x.ColAttr.IsPrimaryKey != y.ColAttr.IsPrimaryKey)
                return y.ColAttr.IsPrimaryKey ? 1 : -1;
            // 标识列
            if (x.ColAttr.IsIDentity != y.ColAttr.IsIDentity)
                return y.ColAttr.IsIDentity ? 1 : -1;
            // 自动生成的值
            if (x.ColAttr.IsDbGenerated != y.ColAttr.IsDbGenerated)
                return y.ColAttr.IsDbGenerated ? 1 : -1;
            // 允许为空的值
            if (x.ColAttr.IsCanBeNull != y.ColAttr.IsCanBeNull)
                return y.ColAttr.IsCanBeNull ? 1 : -1;
            return 0;
        }
    }
}
