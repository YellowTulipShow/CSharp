using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-调用类
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">Model数据映射模型</typeparam>
    public abstract class AbsBLL<D, M> :
        ITableName,
        IBasicDataAccess<M>,
        IDefaultRecord<M>,
        ISupplementaryStructure
        where D : AbsDAL<M>
        where M : AbsModelNull
    {
        /// <summary>
        /// 当前DAL对象
        /// </summary>
        protected D SelfDAL { get { return _selfDAL; } }
        private D _selfDAL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsBLL(D dal) {
            this._selfDAL = dal;
            Supplementary();
        }

        #region ====== using:ITableName ======
        public string GetTableName() {
            return this.SelfDAL.GetTableName();
        }
        #endregion

        #region ====== override:IBasicDataAccess<M> Use:AbsDAL<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        public bool Insert(M model) {
            return this.SelfDAL.Insert(model);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="wheres">删除的条件</param>
        /// <returns>是否成功</returns>
        public bool Delete(WhereModel wheres) {
            return this.SelfDAL.Delete(wheres);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fielvals">更新的内容和其值</param>
        /// <param name="wheres">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        public bool Update(FieldValueModel[] fielvals, WhereModel wheres) {
            return this.SelfDAL.Update(fielvals, wheres);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="wheres">查询条件</param>
        /// <param name="fieldOrders">排序条件</param>
        /// <returns></returns>
        public M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            return this.SelfDAL.Select(top, wheres, fieldOrders);
        }
        /// <summary>
        /// 分页 查
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="wheres">定义: 查询条件</param>
        /// <param name="fieldOrders">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        public M[] Select(int pageCount, int pageIndex, out int recordCount, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null) {
            return this.SelfDAL.Select(pageCount, pageIndex, out recordCount, wheres, fieldOrders);
        }

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="wheres">条件筛选</param>
        /// <returns></returns>
        public M GetModel(WhereModel wheres, FieldOrderModel[] fieldOrders = null) {
            return this.SelfDAL.GetModel(wheres, fieldOrders);
        }
        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="fielvals">条件筛选</param>
        /// <returns></returns>
        public M GetModel(FieldValueModel fielvals, FieldOrderModel[] fieldOrders = null) {
            return this.SelfDAL.GetModel(fielvals, fieldOrders);
        }

        /// <summary>
        /// 获得条件的记录总数
        /// </summary>
        /// <param name="wheres">条件</param>
        public int GetRecordCount(WhereModel wheres) {
            return this.SelfDAL.GetRecordCount(wheres);
        }
        #endregion

        #region ====== using:IDefaultRecord<M> ======
        /// <summary>
        /// 默认的数据记录
        /// </summary>
        public virtual M[] DefaultDataModel() {
            return new M[] { };
        }
        #endregion

        #region ====== using:ISupplementaryStructure ======
        /// <summary>
        /// 执行补全
        /// </summary>
        public void Supplementary() {
            this.SelfDAL.Supplementary();
        }
        #endregion
    }
}
