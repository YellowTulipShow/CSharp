using System;
using YTS.Model;
using YTS.Model.Table;

namespace YTS.DAL
{
    /// <summary>
    /// 接口 - 记录ID主键相应的方法
    /// </summary>
    /// <typeparam name="M">数据模型</typeparam>
    /// <typeparam name="T">ID键的数据类型</typeparam>
    public interface IRecordIDPrimaryKey<M, T> where M : AbsTable
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="id">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        bool IDInsert(M model, out T id);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        bool IDDelete(T id);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="kos">更新的内容和其值</param>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        bool IDUpdate(KeyObject[] kos, T id);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>映射数据模型</returns>
        M IDGetModel(T id);
    }
}
