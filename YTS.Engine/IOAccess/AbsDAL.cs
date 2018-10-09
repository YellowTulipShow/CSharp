using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 抽象-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="W">查询条件</typeparam>
    /// <typeparam name="P">解析器</typeparam>
    /// <typeparam name="PI">解析信息数据模型</typeparam>
    public abstract class AbsDAL<M, W, P, PI> :
        AbsDAL_OnlyQuery<M, W, P, PI>,
        IDAL<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        public AbsDAL() : base() { }

        public abstract bool Insert(M model);

        public abstract bool Insert(M[] models);

        public abstract bool Delete(W where);

        public abstract bool Update(KeyObject[] kos, W where);
    }
}
