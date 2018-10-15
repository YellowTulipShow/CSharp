using System;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 接口: 业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="D">接口-数据访问层(Data Access Layer)</typeparam>
    /// <typeparam name="W">查询条件</typeparam>
    /// <typeparam name="P">解析器</typeparam>
    /// <typeparam name="PI">解析信息数据模型</typeparam>
    public interface IBLL<M, D, W, P, PI> :
        IDAL<M, W, P, PI>
        where M : AbsShineUpon
        where D : IDAL<M, W, P, PI>
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
    }
}
