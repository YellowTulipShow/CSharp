using System;
using System.Data;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 接口 - 数据表基础方法
    /// </summary>
    /// <typeparam name="M">BasicsModel数据模型 并且限定为 BasicsDataModel 数据模型</typeparam>
    public interface ITableBasicFunction<M> where M : AbsModel_Null
    {
        #region ====== Tools Function ======
        int GetRecordCount(string strWhere);
        #endregion

        #region ====== SQL Language Execute ======
        bool Insert(M model, out int IDentity);

        bool Delete(M model);

        bool Update(M model);
        #endregion

        #region ====== SQL Select Data ======
        M GetModel(int IDentity);

        M DataRowToModel(DataRow row);

        M[] GetModelList(DataTable dt);

        DataTable GetList(int top = 0, string strWhere = "", Dictionary<string, bool> fieldOrders = null);

        DataTable GetList(int pageCount, int pageIndex, out int recordCount, string strWhere, Dictionary<string, bool> fieldOrders);
        #endregion
    }
}
