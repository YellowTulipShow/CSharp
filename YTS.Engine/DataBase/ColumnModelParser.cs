﻿using System;
using System.Collections.Generic;
using System.Reflection;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Model.Table;
using YTS.Model.Table.Attribute;
using YTS.Tools;

namespace YTS.Engine.DataBase
{
    /// <summary>
    /// 模型解析器
    /// </summary>
    public class ColumnModelParser<M> where M : AbsTable
    {
        /// <summary>
        /// 列字段列表数组
        /// </summary>
        private ColumnInfo[] columninfo_array = null;

        public ColumnModelParser() {
            this.columninfo_array = AnalyticalMappingModel();
        }

        #region === ColumnInfo ===
        /// <summary>
        /// 分析映射模型
        /// </summary>
        /// <returns>映射模型的列信息集合</returns>
        public ColumnInfo[] AnalyticalMappingModel() {
            Type modelT = typeof(M);
            if (!modelT.IsDefined(typeof(BasicTableAttribute), false)) {
                return new ColumnInfo[] { };
            }
            List<ColumnInfo> colms = new List<ColumnInfo>();
            PropertyInfo[] protertys = modelT.GetProperties();
            foreach (PropertyInfo property in protertys) {
                ColumnAttribute attr_column = ReflexHelp.AttributeFindOnly<ColumnAttribute>(property);
                // 剔除不表示为列的属性项
                if (CheckData.IsObjectNull(attr_column)) {
                    continue;
                }
                ExplainAttribute attr_explain = ExplainAttribute.Extract(property);
                colms.Add(new ColumnInfo() {
                    Name = property.Name,
                    Property = property,
                    Attribute = attr_column,
                    Explain = attr_explain,
                });
            }
            colms.Sort(ColumnInfo.SortMethod);
            return colms.ToArray();
        }

        /// <summary>
        /// 获得列信息 全部
        /// </summary>
        public ColumnInfo[] GetColumn_ALL() {
            return this.columninfo_array;
        }
        /// <summary>
        /// 获得列信息 可写
        /// </summary>
        public ColumnInfo[] GetColumn_CanWrite() {
            return ConvertTool.ListConvertType(this.columninfo_array, colinfo => {
                if (colinfo.Attribute.IsAutoGenerated) {
                    return null;
                }
                return colinfo;
            }, null);
        }
        #endregion

        #region === Data: Get/Set ===
        /// <summary>
        /// 获取_模型_数据
        /// </summary>
        /// <param name="column_info">行信息</param>
        /// <param name="source_model">数据来源</param>
        /// <returns>键值数据</returns>
        public KeyObject GetModelValue(ColumnInfo column_info, M source_model) {
            if (CheckData.IsObjectNull(column_info) || CheckData.IsObjectNull(source_model)) {
                return null;
            }
            object value = column_info.Property.GetValue(source_model, null);
            return new KeyObject() {
                Key = column_info.Property.Name,
                Value = value,
            };
        }

        /// <summary>
        /// 设置_模型_数据
        /// </summary>
        /// <param name="colmodel">行信息</param>
        /// <param name="targetModel">目标模型</param>
        /// <param name="value">数据</param>
        /// <returns>目标模型</returns>
        public M SetModelValue(ColumnInfo colmodel, M targetModel, object value) { // @1
            if (!CheckData.IsObjectNull(value) && colmodel.Property.CanWrite) {
                colmodel.Property.SetValue(targetModel, value, null);
            }
            return targetModel;
        }
        #endregion
    }
}
