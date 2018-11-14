using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 抽象-业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="D">抽象-数据访问层(Data Access Layer)</typeparam>
    /// <typeparam name="W">查询条件</typeparam>
    /// <typeparam name="P">解析器</typeparam>
    /// <typeparam name="PI">解析信息数据模型</typeparam>
    public abstract class AbsBLL<M, D, W, P, PI> :
        AbsBLL_OnlyQuery<M, D, W, P, PI>,
        IBLL<M, D, W, P, PI>,
        IDefaultRecord<M>
        where M : AbsShineUpon, new()
        where D : AbsDAL<M, W, P, PI>, new()
        where P : ShineUponParser<M, PI>, new()
        where PI : ShineUponInfo
    {
        public AbsBLL()
            : base() {
            if (IsNeedDefaultRecord()) {
                FillDefaultRecordGather();
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model">数据映射模型</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public bool Insert(M model) {
            return this.SelfDAL.Insert(model);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="models">数据映射模型多条记录</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public bool Insert(M[] models) {
            return this.SelfDAL.Insert(models);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public bool Delete(W where) {
            return this.SelfDAL.Delete(where);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="kos">需要更新的键值</param>
        /// <param name="where">查询条件</param>
        /// <returns>是否成功 是:True 否:False</returns>
        public bool Update(KeyObject[] kos, W where) {
            return this.SelfDAL.Update(kos, where);
        }

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
        /// 填充默认记录集合
        /// </summary>
        public void FillDefaultRecordGather() {
            int count = GetRecordCount(default(W));
            if (count > 0) {
                return;
            }
            M[] list = GetDefaultRecordGather();
            if (CheckData.IsSizeEmpty(list)) {
                return;
            }
            Insert(list);
        }
    }
}
