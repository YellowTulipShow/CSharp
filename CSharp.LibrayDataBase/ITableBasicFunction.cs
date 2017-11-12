using System;
using System.Data;
using System.Collections.Generic;
using CSharp.LibrayFunction;
using CSharp.Model.Table;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 接口 - 数据表基础方法
    /// </summary>
    /// <typeparam name="M">BasicsModel数据模型 并且限定为 BasicsDataModel 数据模型</typeparam>
    public interface ITableBasicFunction<M> where M : AbsTableModel
    {
        #region ====== Tools Function ======
        bool Exists(int id);
        #endregion

        #region ====== SQL Language Execute ======
        int GetCount(string strWhere);

        int Add(M model);

        bool Delete(int id);

        bool Update(M model);

        bool UpdateField(int id, string strValue);

        bool Transaction(string[] strlist);
        #endregion

        #region ====== SQL Select Data ======
        M GetModel(int id);

        M DataRowToModel(DataRow row);

        DataSet GetList(int Top, string strWhere, string filedOrder);

        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);

        List<M> GetModelList(DataTable dt);
        #endregion

        #region ====== SQL String Get ======
        string SQLStringModelAdd(M model);

        string SQLStringModelUpdate(int id, M Model);
        #endregion
    }
}
