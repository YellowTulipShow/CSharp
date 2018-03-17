using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    public interface IBasicDataAccess<M> where M : AbsModelNull
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
        /// <param name="wheres">删除的条件</param>
        /// <returns>是否成功</returns>
        bool Delete(WhereModel wheres);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="fielvals">更新的内容和其值</param>
        /// <param name="wheres">筛选更新的条件</param>
        /// <returns>是否成功</returns>
        bool Update(FieldValueModel[] fielvals, WhereModel wheres);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="wheres">查询条件</param>
        /// <param name="fieldOrders">排序条件</param>
        /// <returns></returns>
        M[] Select(int top = 0, WhereModel wheres = null, FieldOrderModel[] fieldOrders = null);

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="wheres">条件筛选</param>
        /// <returns></returns>
        M GetModel(WhereModel wheres, FieldOrderModel[] fieldOrders = null);
        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <param name="fielvals">条件筛选</param>
        /// <returns></returns>
        M GetModel(FieldValueModel fielvals, FieldOrderModel[] fieldOrders = null);
    }
}
