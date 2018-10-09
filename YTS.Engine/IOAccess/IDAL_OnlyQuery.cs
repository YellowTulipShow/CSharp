using System;
using YTS.Engine.ShineUpon;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 接口-数据访问层(Data Access Layer)-只提供查询功能
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="W">查询条件</typeparam>
    /// <typeparam name="P">解析器</typeparam>
    /// <typeparam name="PI">解析信息数据模型</typeparam>
    public interface IDAL_OnlyQuery<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="top">查询记录数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        M[] Select(int top, W where, KeyBoolean[] sorts);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageCount">每页展现记录数</param>
        /// <param name="pageIndex">浏览页面索引</param>
        /// <param name="recordCount">查询结果总记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">结果排序键值集合</param>
        /// <returns>数据映射模型集合结果</returns>
        M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts);

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        int GetRecordCount(W where);

        /// <summary>
        /// 获取单个记录模型
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sorts">数据映射模型集合结果</param>
        /// <returns>单个记录数据映射模型</returns>
        M GetModel(W where, KeyBoolean[] sorts);
    }
}
