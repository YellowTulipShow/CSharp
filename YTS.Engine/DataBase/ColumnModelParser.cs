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
    /// 映射结果信息-数据表列
    /// </summary>
    /// <typeparam name="M">数据表映射模型</typeparam>
    public class ColumnModelParser<M> : AbsParser_ShineUpon<M, ColumnInfo> where M : AbsTable
    {
        public ColumnModelParser() : base() { }

        #region === Info ===
        public override ColumnInfo[] Analytical() {
            ColumnInfo[] base_result = base.Analytical();
            if (CheckData.IsSizeEmpty(base_result)) {
                return new ColumnInfo[] { };
            }
            List<ColumnInfo> list = new List<ColumnInfo>(base_result);
            for (int i = list.Count - 1; i >= 0; i--) {
                ColumnAttribute attr_column = ReflexHelp.AttributeFindOnly<ColumnAttribute>(list[i].Property);
                if (CheckData.IsObjectNull(attr_column)) {
                    list.RemoveAt(i);
                    continue;
                }
                list[i].Attribute = attr_column;
            }
            return list.ToArray();
        }
        /// <summary>
        /// 获得列信息 可写
        /// </summary>
        public ColumnInfo[] GetColumn_CanWrite() {
            return ConvertTool.ListConvertType(this.GetColumn_ALL(), colinfo => {
                if (colinfo.Attribute.IsAutoGenerated) {
                    return null;
                }
                return colinfo;
            }, null);
        }
        #endregion

        #region === Sort ===
        /// <summary>
        /// 钩子: 判断是否需要排序
        /// </summary>
        /// <returns>是否需要布尔值</returns>
        public override bool IsNeedSort() {
            return true;
        }

        /// <summary>
        /// 执行排序比较方法
        /// </summary>
        /// <param name="array">需要排序的信息模型列表</param>
        /// <returns>排序完成信息模型列表</returns>
        public override ColumnInfo[] SortComparison(ColumnInfo[] array) {
            if (CheckData.IsSizeEmpty(array)) {
                return new ColumnInfo[] { };
            }
            List<ColumnInfo> list = new List<ColumnInfo>(array);
            list.Sort(SortMethod);
            return list.ToArray();
        }

        /// <summary>
        /// 排序比较 实现委托: Comparison泛型
        /// </summary>
        public static int SortMethod(ColumnInfo x, ColumnInfo y) {
            // 主键
            if (x.Attribute.IsPrimaryKey != y.Attribute.IsPrimaryKey) {
                return y.Attribute.IsPrimaryKey ? 1 : -1;
            }
            // 标识列
            if (x.Attribute.IsIDentity != y.Attribute.IsIDentity) {
                return y.Attribute.IsIDentity ? 1 : -1;
            }
            // 自动生成的值
            if (x.Attribute.IsAutoGenerated != y.Attribute.IsAutoGenerated) {
                return y.Attribute.IsAutoGenerated ? 1 : -1;
            }
            // 允许为空的值
            if (x.Attribute.IsCanBeNull != y.Attribute.IsCanBeNull) {
                return y.Attribute.IsCanBeNull ? 1 : -1;
            }
            // 排序序列
            int indexResult = Sort.Int(x.Attribute.SortIndex, y.Attribute.SortIndex);
            return indexResult == 0 ? Sort.String(x.Property.Name, y.Property.Name) : indexResult;
        }
        #endregion
    }
}
