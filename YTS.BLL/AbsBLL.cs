using System;
using YTS.DAL;
using YTS.Model;
using YTS.Tools;

namespace YTS.BLL
{
    /// <summary>
    /// 抽象-业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">数据映射模型</typeparam>
    public abstract class AbsBLL<D, M> :
        IBasicDataAccess<M>,
        ISupplementaryStructure,
        IDefaultRecord<M>
        where D : AbsDAL<M>
        where M : AbsShineUpon
    {
        /// <summary>
        /// 当前DAL对象
        /// </summary>
        public D SelfDAL { get { return _SelfDAL; } }
        private D _SelfDAL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsBLL() {
            this._SelfDAL = ReflexHelp.CreateNewObject<D>();
        }

        #region ====== using:IBasicDataAccess<M> Use:AbsDAL<M> ======
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        public virtual bool Insert(M model) {
            return this.SelfDAL.Insert(model);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns>是否成功</returns>
        public virtual bool Delete(string where) {
            return this.SelfDAL.Delete(where);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="where">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        public virtual bool Update(KeyString[] keyvaluedic, string where) {
            return this.SelfDAL.Update(keyvaluedic, where);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型列表</returns>
        public virtual M[] Select(int top = 0, string where = null, string sort = null) {
            return this.SelfDAL.Select(top, where, sort);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="where">定义: 查询条件</param>
        /// <param name="sort">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>映射数据模型列表</returns>
        public virtual M[] Select(int pageCount, int pageIndex, out int recordCount, string where = null, string sort = null) {
            return this.SelfDAL.Select(pageCount, pageIndex, out recordCount, where, sort);
        }

        /// <summary>
        /// 统计符合查询条件的记录总数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public virtual int GetRecordCount(string where = null) {
            return this.SelfDAL.GetRecordCount(where);
        }

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型</returns>
        public virtual M GetModel(string where = null, string sort = null) {
            return this.SelfDAL.GetModel(where, sort);
        }
        #endregion

        #region ====== using:ISupplementaryStructure ======
        /// <summary>
        /// 是否需要补全
        /// </summary>
        /// <returns>是(True), 否(False)</returns>
        public virtual bool IsNeedSupplementary() {
            return this.IsNeedSupplementary();
        }

        /// <summary>
        /// 执行补全操作
        /// </summary>
        public virtual void ExecutionSupplementary() {
            this.SelfDAL.ExecutionSupplementary();
        }
        #endregion

        #region ====== using:IDefaultRecord<M> ======
        /// <summary>
        /// 是否需要默认记录
        /// </summary>
        /// <returns>是(True), 否(False)</returns>
        public virtual bool IsNeedDefaultRecord() {
            return false;
        }

        /// <summary>
        /// 获取固定的默认记录集合
        /// </summary>
        /// <returns>映射数据模型列表</returns>
        public virtual M[] GetDefaultRecordGather() {
            return new M[] { };
        }

        /// <summary>
        /// 填充默认数据
        /// </summary>
        public virtual void FillDefaultRecordGather() {
            if (!IsNeedDefaultRecord()) {
                return;
            }
            M[] defaultMs = GetDefaultRecordGather();
            if (CheckData.IsSizeEmpty(defaultMs) || GetRecordCount() > 0) {
                return;
            }
            foreach (M item in defaultMs) {
                Insert(item);
            }
        }
        #endregion
    }
}
