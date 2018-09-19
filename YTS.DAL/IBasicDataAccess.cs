using System;
using YTS.Model;

namespace YTS.DAL
{
    /// <summary>
    /// 基础数据访问接口
    /// </summary>
    /// <typeparam name="M">访问的数据映射模型</typeparam>
    public interface IBasicDataAccess<M> where M : Model.AbsShineUpon
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <returns>是否成功</returns>
        bool Insert(M model);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns>是否成功</returns>
        bool Delete(string where);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="where">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        bool Update(KeyString[] keyvaluedic, string where);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型列表</returns>
        M[] Select(int top = 0, string where = null, string sort = null);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="where">定义: 查询条件</param>
        /// <param name="sort">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>映射数据模型列表</returns>
        M[] Select(int pageCount, int pageIndex, out int recordCount, string where = null, string sort = null);

        /// <summary>
        /// 统计符合查询条件的记录总数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录总数</returns>
        int GetRecordCount(string where = null);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns>映射数据模型</returns>
        M GetModel(string where = null, string sort = null);
    }
}
