using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 抽象-业务逻辑层(Business Logic Layer)-只提供查询功能
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="D">抽象-数据访问层(Data Access Layer)</typeparam>
    /// <typeparam name="W">查询条件</typeparam>
    /// <typeparam name="P">解析器</typeparam>
    /// <typeparam name="PI">解析信息数据模型</typeparam>
    public abstract class AbsBLL_OnlyQuery<M, D, W, P, PI> :
        IBLL_OnlyQuery<M, D, W, P, PI>
        where M : AbsShineUpon, new()
        where D : AbsDAL<M, W, P, PI>, new()
        where P : ShineUponParser<M, PI>, new()
        where PI : ShineUponInfo
    {
        /// <summary>
        /// 当前-数据访问层(Data Access Layer)对象
        /// </summary>
        public D SelfDAL {
            get {
                if (CheckData.IsObjectNull(_selfdal)) {
                    _selfdal = InitCreateDAL();
                }
                return _selfdal;
            }
        }
        private D _selfdal = null;

        public AbsBLL_OnlyQuery() { }

        /// <summary>
        /// 初始化创建 数据访问层DAL 对象
        /// </summary>
        public virtual D InitCreateDAL() {
            return ReflexHelp.CreateNewObject<D>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public M[] Select(int top, W where, KeyBoolean[] sorts) {
            return this.SelfDAL.Select(top, where, sorts);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageCount">每页展现记录数</param>
        /// <param name="pageIndex">浏览页面索引</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        public M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts) {
            return this.SelfDAL.Select(pageCount, pageIndex, out recordCount, where, sorts);
        }

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        public int GetRecordCount(W where) {
            return this.SelfDAL.GetRecordCount(where);
        }

        /// <summary>
        /// 获取单个记录模型
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">数据映射模型集合结果</param>
        /// <returns>单个记录数据映射模型</returns>
        public M GetModel(W where, KeyBoolean[] sorts) {
            return this.SelfDAL.GetModel(where, sorts);
        }
    }
}
