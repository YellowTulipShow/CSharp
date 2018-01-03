using System;
using System.Collections.Generic;
using System.Reflection;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-访问类
    /// </summary>
    public abstract class AbsTableDAL<M> : IPropertyColumn where M : AbsModel_Null
    {
        #region === Property Field ===
        /// <summary>
        /// 所有 类型 列
        /// </summary>
        public Dictionary<PropertyInfo, ColumnAttribute> ALLTypeColumns {
            get {
                _alltypeColums = CheckData.IsSizeEmpty(_alltypeColums) ? AnalysisPropertyColumns() : _alltypeColums;
                return _alltypeColums;
            }
        }
        private Dictionary<PropertyInfo, ColumnAttribute> _alltypeColums = null;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsTableDAL() {
        }

        /// <summary>
        /// 默认模型实例
        /// </summary>
        /// <returns></returns>
        public M DefaultModel() {
            return System.Activator.CreateInstance<M>();
        }

        #region === Analytic Property Column Info ===

        /// <summary>
        /// 接口: IPropertyColumn 解析-属性-列
        /// </summary>
        public Dictionary<PropertyInfo, ColumnAttribute> AnalysisPropertyColumns() {
            Type modelT = typeof(M);
            if (!modelT.IsDefined(typeof(TableAttribute), false)) {
                return new Dictionary<PropertyInfo, ColumnAttribute>();
            }

            Dictionary<PropertyInfo, ColumnAttribute> dictionary = new Dictionary<PropertyInfo, ColumnAttribute>();
            PropertyInfo[] protertys = modelT.GetProperties();
            foreach (PropertyInfo pro in protertys) {
                ColumnAttribute columnAttr = ReflexHelper.FindAttributesOnly<ColumnAttribute>(pro);
                if (CheckData.IsObjectNull(columnAttr))
                    continue;
                dictionary[pro] = columnAttr;
            }
            return dictionary;
        }

        /// <summary>
        /// 获取ID标识列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public PropertyInfo PrimaryKeyColumn() {
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                if (item.Value.IsPrimaryKey)
                    return item.Key;
            }
            return null;
        }
        /// <summary>
        /// 获取ID标识列
        /// </summary>
        /// <returns>若无,返回null</returns>
        public PropertyInfo IDentityColumn() {
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                if (item.Value.IsPrimaryKey && item.Value.IsDbGenerated)
                    return item.Key;
            }
            return null;
        }

        /// <summary>
        /// 获取能执行插入语句的列
        /// </summary>
        /// <returns></returns>
        public Dictionary<PropertyInfo, ColumnAttribute> CanInsertColumns() {
            Dictionary<PropertyInfo, ColumnAttribute> dictionary = new Dictionary<PropertyInfo, ColumnAttribute>();
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in this.ALLTypeColumns) {
                if (!item.Value.IsDbGenerated)
                    dictionary[item.Key] = item.Value;
            }
            return dictionary;
        }
        /// <summary>
        /// 获取能执行更新语句的列
        /// </summary>
        /// <returns></returns>
        public Dictionary<PropertyInfo, ColumnAttribute> CanUpdateColumns() {
            return CanInsertColumns();
        }
        /// <summary>
        /// 获取能执行删除语句的列
        /// </summary>
        /// <returns></returns>
        public Dictionary<PropertyInfo, ColumnAttribute> CanDeleteColumns() {
            return CanInsertColumns();
        }
        #endregion
    }
}