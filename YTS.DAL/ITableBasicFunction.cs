using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YTS.Model;

namespace YTS.DAL
{
    /// <summary>
    /// 接口 - 数据表基础方法
    /// </summary>
    /// <typeparam name="M">BasicsModel数据模型</typeparam>
    public interface ITableBasicFunction<M> where M : AbsTableModel
    {
        #region ====== Tools Function ======
        bool Exists(int id);
        #endregion

        #region ====== SQL Language Execute ======
        bool Transaction(List<string> strlist);

        int Add(M model);

        bool Update(M model);

        bool Delete(int id);

        int GetCount(string strWhere);

        bool UpdateField(int id, string strValue);
        #endregion

        #region ====== SQL Select Data ======
        M GetModel(int id);

        DataSet GetList(int Top, string strWhere, string filedOrder);

        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);

        M DataRowToModel(DataRow row);

        List<M> GetModelList(DataTable dt);
        #endregion

        #region ====== SQL String Get ======
        string SQLStringModelAdd(M model);

        string SQLStringModelUpdate(int id, M Model);

        string SQLStringModelDelete(int id);
        #endregion
    }
}
