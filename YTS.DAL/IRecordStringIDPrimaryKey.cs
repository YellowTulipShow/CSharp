using System;
using YTS.Model;
using YTS.Model.Table;

namespace YTS.DAL
{
    /// <summary>
    /// 接口 - 记录StringID主键相应的方法
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public interface IRecordStringIDPrimaryKey<M> where M : AbsTable_StringID
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model">数据来源</param>
        /// <param name="sid">插入成功后需要返回的ID值</param>
        /// <returns>是否成功</returns>
        bool IDInsert(M model, out string sid);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sid">ID条件</param>
        /// <returns>是否成功</returns>
        bool IDDelete(string sid);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="keyvaluedic">更新的内容和其值</param>
        /// <param name="sid">ID条件</param>
        /// <returns>是否成功</returns>
        bool IDUpdate(KeyObject[] keyvaluedic, string sid);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="sid">ID条件</param>
        /// <returns>映射数据模型</returns>
        M IDGetModel(string sid);
    }
}
