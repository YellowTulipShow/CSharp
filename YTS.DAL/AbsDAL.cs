using System;
using System.Collections.Generic;
using System.Reflection;
using YTS.Engine.DataBase;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Model.Table;
using YTS.Model.Table.Attribute;
using YTS.Tools;

namespace YTS.DAL
{
    /// <summary>
    /// 抽象-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public abstract class AbsDAL<M> : IBasicDataAccess<M>, ISupplementaryStructure where M : AbsTable
    {
        /// <summary>
        /// 映射模型的列信息集合
        /// </summary>
        public ColumnInfo[] modelColumns = null;

        public AbsDAL() {
            this.modelColumns = AnalysisMappingModel();
        }

        /// <summary>
        /// 分析映射模型
        /// </summary>
        /// <returns>列信息集合</returns>
        public ColumnInfo[] AnalysisMappingModel() {
            Type modelT = typeof(M);
            if (!modelT.IsDefined(typeof(BasicTableAttribute), false)) {
                return new ColumnInfo[] { };
            }
            List<ColumnInfo> colms = new List<ColumnInfo>();
            PropertyInfo[] protertys = modelT.GetProperties();
            foreach (PropertyInfo property in protertys) {
                ColumnAttribute attr_column = ReflexHelp.AttributeFindOnly<ColumnAttribute>(property);
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

        #region ====== using:IBasicDataAccess<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        public abstract bool Insert(M model);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns>是否成功</returns>
        public abstract bool Delete(string where);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="where">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        public abstract bool Update(KeyString[] keyvaluedic, string where);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型列表</returns>
        public abstract M[] Select(int top = 0, string where = null, string sort = null);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="where">定义: 查询条件</param>
        /// <param name="sort">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>映射数据模型列表</returns>
        public abstract M[] Select(int pageCount, int pageIndex, out int recordCount, string where = null, string sort = null);

        /// <summary>
        /// 统计符合查询条件的记录总数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public abstract int GetRecordCount(string where = null);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型</returns>
        public virtual M GetModel(string where = null, string sort = null) {
            M[] list = Select(1, where, sort);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }
        #endregion

        #region ====== using:ISupplementaryStructure ======
        /// <summary>
        /// 是否需要补全
        /// </summary>
        /// <returns>是(True), 否(False)</returns>
        public virtual bool IsNeedSupplementary() {
            return false;
        }

        /// <summary>
        /// 执行补全操作
        /// </summary>
        public virtual void ExecutionSupplementary() { }
        #endregion
    }
}
