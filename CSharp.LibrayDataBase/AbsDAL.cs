using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-访问类
    /// </summary>
    public abstract class AbsDAL<M> : ITableName, IBasicDataAccess<M> where M : AbsModelNull
    {
        private readonly string tableName = string.Empty;
        public readonly ColumnModelParser<M> modelParser = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsDAL() {
            this.modelParser = new ColumnModelParser<M>();
            this.tableName = this.modelParser.CreateDefaultModel().GetTableName();
        }

        /// <summary>
        /// 默认模型实例
        /// </summary>
        public M CreateDefaultModel() {
            return this.modelParser.CreateDefaultModel();
        }

        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public string GetTableName() {
            return this.tableName;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        public abstract bool Insert(M model);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="wheres">删除的条件</param>
        /// <returns>是否成功</returns>
        public abstract bool Delete(WhereModel wheres);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fielvals">更新的内容和其值</param>
        /// <param name="wheres">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        public abstract bool Update(FieldValueModel[] fielvals, WhereModel wheres);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="wheres">查询条件</param>
        /// <param name="fieldOrders">排序条件</param>
        /// <returns></returns>
        public abstract M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="wheres">条件筛选</param>
        /// <returns></returns>
        public virtual M GetModel(WhereModel wheres, FieldOrderModel[] fieldOrders = null) {
            M[] list = Select(1, wheres);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }
        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="fielvals">条件筛选</param>
        /// <returns></returns>
        public virtual M GetModel(FieldValueModel fielvals, FieldOrderModel[] fieldOrders = null) {
            return (FieldValueModel.CheckIsCanUse(fielvals)) ? null :
                GetModel(new WhereModel() { FielVals = new FieldValueModel[] { fielvals }, }, fieldOrders);
        }
    }
}