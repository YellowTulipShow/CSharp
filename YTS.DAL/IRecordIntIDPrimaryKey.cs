using System;
using YTS.Model;
using YTS.Model.Table;

namespace YTS.DAL
{
    /// <summary>
    /// 接口 - 记录IntID主键相应的方法
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public interface IRecordIntIDPrimaryKey<M> where M : AbsTable_IntID
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="id">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        bool IDInsert(M model, out int id);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        bool IDDelete(int id);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="id">ID条件</param>
        /// <returns>是否成功</returns>
        bool IDUpdate(KeyString[] keyvaluedic, int id);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="id">ID条件</param>
        /// <returns>映射数据模型</returns>
        M IDGetModel(int id);
    }
}
