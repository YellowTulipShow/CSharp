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
        ColumnInfo[] AnalysisPropertyColumns();
    }

    /// <summary>
    /// 列信息模型
    /// </summary>
    public class ColumnInfo : AbsBasicsDataModel
    {
        /// <summary>
        /// 解释翻译信息
        /// </summary>
        public ExplainAttribute Explain { get { return _explain; } set { _explain = value; } }
        private ExplainAttribute _explain = null;

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get { return _property; } set { _property = value; } }
        private PropertyInfo _property = null;

        /// <summary>
        /// 数据列特性
        /// </summary>
        public ColumnAttribute Attribute { get { return _attribute; } set { _attribute = value; } }
        private ColumnAttribute _attribute = null;

        /// <summary>
        /// 排序比较 实现委托: Comparison泛型 值含义: 小于0:(x 小于 y)。0:(x 等于 y)。大于0:(x 大于 y)。
        /// </summary>
        public static int Sort(ColumnInfo x, ColumnInfo y) {
            // 主键
            if (x.Attribute.IsPrimaryKey != y.Attribute.IsPrimaryKey)
                return y.Attribute.IsPrimaryKey ? 1 : -1;
            // 标识列
            if (x.Attribute.IsIDentity != y.Attribute.IsIDentity)
                return y.Attribute.IsIDentity ? 1 : -1;
            // 自动生成的值
            if (x.Attribute.IsDbGenerated != y.Attribute.IsDbGenerated)
                return y.Attribute.IsDbGenerated ? 1 : -1;
            // 允许为空的值
            if (x.Attribute.IsCanBeNull != y.Attribute.IsCanBeNull)
                return y.Attribute.IsCanBeNull ? 1 : -1;
            return 0;
        }
    }
}
