using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据表-调用类
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">Model数据映射模型</typeparam>
    public abstract class AbsTableBLL<D, M> :
        IPropertyColumn, IBasicsSQL<M>, ITableBasicFunction<M>
        where D : AbsTableDAL<M>
        where M : AbsModel_Null
    {
        private D TableDAL;
        public AbsTableBLL(D dal) {
            this.TableDAL = dal;
        }

        #region === IPropertyColumn ===
        public ColumnModel[] AnalysisPropertyColumns() {
            return TableDAL.AnalysisPropertyColumns();
        }
        #endregion


        #region === IBasicsSQL<M> ===
        public string SQLInsert(M model) {
            return TableDAL.SQLInsert(model);
        }

        public string SQLDelete(M model) {
            return TableDAL.SQLDelete(model);
        }

        public string SQLUpdate(M model) {
            return TableDAL.SQLUpdate(model);
        }
        #endregion


        #region === ITableBasicFunction<M> ===
        public int GetRecordCount(string strWhere) {
            return TableDAL.GetRecordCount(strWhere);
        }

        public bool Insert(M model, out int IDentity) {
            return TableDAL.Insert(model, out IDentity);
        }

        public bool Delete(M model) {
            return TableDAL.Delete(model);
        }

        public bool Update(M model) {
            return TableDAL.Update(model);
        }

        public M GetModel(int IDentity) {
            return TableDAL.GetModel(IDentity);
        }

        public M DataRowToModel(DataRow row) {
            return TableDAL.DataRowToModel(row);
        }

        public M[] GetModelList(DataTable dt) {
            return TableDAL.GetModelList(dt);
        }

        public DataTable GetList(int top, string strWhere, Dictionary<string, bool> fieldOrders) {
            return TableDAL.GetList(top, strWhere, fieldOrders);
        }

        public DataTable GetList(int pageCount, int pageIndex, out int recordCount, string strWhere, Dictionary<string, bool> fieldOrders) {
            return TableDAL.GetList(pageCount, pageIndex, out recordCount, strWhere, fieldOrders);
        }
        #endregion
    }
}
