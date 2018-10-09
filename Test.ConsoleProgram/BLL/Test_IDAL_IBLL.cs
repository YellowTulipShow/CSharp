using System;
using System.Data;
using YTS.Engine.DataBase;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.BLL
{
    public class Test_IDAL_IBLL : CaseModel
    {
        public Test_IDAL_IBLL() {
            NameSign = @"接口IDAL IBLL";
            ExeEvent = Method;
        }

        public class TestModel : YTS.Model.DB.AbsTable_IntID
        {
            public override string GetTableName() {
                return @"dt_TestModel";
            }
        }

        public bool Method() {
            TestModel model = new TestModel();
            TestModel[] models = new TestModel[] { };
            int id = 0;
            string where = string.Empty;
            KeyObject[] kos = new KeyObject[] { };
            int top = 0;
            KeyBoolean[] kbs = new KeyBoolean[] {};
            int pcount = 0;
            int pindex = 0;
            int psum = 0;
            DataSet ds = null;
            DataRow dr = null;
            string sort = string.Empty;
            string[] sqls = new string[] { };

            // BLL
            YTS.BLL.MSSQLServer_IntID<YTS.DAL.MSSQLServer_IntID<TestModel>, TestModel> bll = new YTS.BLL.MSSQLServer_IntID<YTS.DAL.MSSQLServer_IntID<TestModel>, TestModel>();
            bll.Insert(model);
            bll.Insert(models);
            bll.Delete(where);
            bll.Update(kos, where);
            bll.Select(top, where, kbs);
            bll.Select(pcount, pindex, out psum, where, kbs);
            bll.GetRecordCount(where);
            bll.GetModel(where, kbs);
            bll.GetTableName();
            bll.QueryRecords(top, where, sort);
            bll.QueryRecords(pcount, pindex, out psum, where, sort);
            bll.DataRowToModel(dr);                                                                             
            bll.DataSetToModels(ds);
            bll.IDInsert(model, out id);
            bll.IDDelete(id);
            bll.IDUpdate(kos, id);
            bll.IDGetModel(id);

            BLL_MSSQLServer<YTS.DAL.MSSQLServer_IntID<TestModel>, TestModel> ms_bll = bll;
            ms_bll.Insert(model);
            ms_bll.Insert(models);
            ms_bll.Delete(where);
            ms_bll.Update(kos, where);
            ms_bll.Select(top, where, kbs);
            ms_bll.Select(pcount, pindex, out psum, where, kbs);
            ms_bll.GetRecordCount(where);
            ms_bll.GetModel(where, kbs);
            ms_bll.GetTableName();
            ms_bll.QueryRecords(top, where, sort);
            ms_bll.QueryRecords(pcount, pindex, out psum, where, sort);
            ms_bll.DataRowToModel(dr);
            ms_bll.DataSetToModels(ds);

            AbsBLL<TestModel, YTS.DAL.MSSQLServer_IntID<TestModel>, string, ColumnModelParser<TestModel>, ColumnInfo> abs_bll = ms_bll;
            abs_bll.Insert(model);
            abs_bll.Insert(models);
            abs_bll.Delete(where);
            abs_bll.Update(kos, where);
            abs_bll.Select(top, where, kbs);
            abs_bll.Select(pcount, pindex, out psum, where, kbs);
            abs_bll.GetRecordCount(where);
            abs_bll.GetModel(where, kbs);

            AbsBLL_OnlyQuery<TestModel, YTS.DAL.MSSQLServer_IntID<TestModel>, string, ColumnModelParser<TestModel>, ColumnInfo> abs_bll_onlquery = abs_bll;
            abs_bll_onlquery.Select(top, where, kbs);
            abs_bll_onlquery.Select(pcount, pindex, out psum, where, kbs);
            abs_bll_onlquery.GetRecordCount(where);
            abs_bll_onlquery.GetModel(where, kbs);

            IBLL<TestModel, YTS.DAL.MSSQLServer_IntID<TestModel>, string, ColumnModelParser<TestModel>, ColumnInfo> i_bll = ms_bll;
            i_bll.Insert(model);
            i_bll.Insert(models);
            i_bll.Delete(where);
            i_bll.Update(kos, where);
            i_bll.Select(top, where, kbs);
            i_bll.Select(pcount, pindex, out psum, where, kbs);
            i_bll.GetRecordCount(where);
            i_bll.GetModel(where, kbs);

            IBLL_OnlyQuery<TestModel, YTS.DAL.MSSQLServer_IntID<TestModel>, string, ColumnModelParser<TestModel>, ColumnInfo> i_bll_onlyquery = abs_bll_onlquery;
            i_bll_onlyquery.Select(top, where, kbs);
            i_bll_onlyquery.Select(pcount, pindex, out psum, where, kbs);
            i_bll_onlyquery.GetRecordCount(where);
            i_bll_onlyquery.GetModel(where, kbs);


            // DAL
            YTS.DAL.MSSQLServer_IntID<TestModel> dal = bll.SelfDAL;
            dal.Insert(model);
            dal.Insert(models);
            dal.Delete(where);
            dal.Update(kos, where);
            dal.Select(top, where, kbs);
            dal.Select(pcount, pindex, out psum, where, kbs);
            dal.GetRecordCount(where);
            dal.GetModel(where, kbs);
            dal.GetTableName();
            dal.QueryRecords(top, where, sort);
            dal.QueryRecords(pcount, pindex, out psum, where, sort);
            dal.DataRowToModel(dr);
            dal.DataSetToModels(ds);
            dal.IsNeedSupplementary();
            dal.ExecutionSupplementary();
            dal.IDInsert(model, out id);
            dal.IDDelete(id);
            dal.IDUpdate(kos, id);
            dal.IDGetModel(id);
            dal.Transaction(sqls);

            DAL_MSSQLServer<TestModel> ms_dal = dal;
            ms_dal.Insert(model);
            ms_dal.Insert(models);
            ms_dal.Delete(where);
            ms_dal.Update(kos, where);
            ms_dal.Select(top, where, kbs);
            ms_dal.Select(pcount, pindex, out psum, where, kbs);
            ms_dal.GetRecordCount(where);
            ms_dal.GetModel(where, kbs);
            ms_dal.GetTableName();
            ms_dal.QueryRecords(top, where, sort);
            ms_dal.QueryRecords(pcount, pindex, out psum, where, sort);
            ms_dal.DataRowToModel(dr);
            ms_dal.DataSetToModels(ds);
            ms_dal.IsNeedSupplementary();
            ms_dal.ExecutionSupplementary();
            ms_dal.Transaction(sqls);

            AbsDAL<TestModel, string, ColumnModelParser<TestModel>, ColumnInfo> abs_dal = ms_dal;
            abs_dal.Insert(model);
            abs_dal.Insert(models);
            abs_dal.Delete(where);
            abs_dal.Update(kos, where);
            abs_dal.Select(top, where, kbs);
            abs_dal.Select(pcount, pindex, out psum, where, kbs);
            abs_dal.GetRecordCount(where);
            abs_dal.GetModel(where, kbs);

            AbsDAL_OnlyQuery<TestModel, string, ColumnModelParser<TestModel>, ColumnInfo> abs_dal_onlyquery = abs_dal;
            abs_dal_onlyquery.Select(top, where, kbs);
            abs_dal_onlyquery.Select(pcount, pindex, out psum, where, kbs);
            abs_dal_onlyquery.GetRecordCount(where);
            abs_dal_onlyquery.GetModel(where, kbs);

            IDAL<TestModel, string, ColumnModelParser<TestModel>, ColumnInfo> i_dal = abs_dal;
            i_dal.Insert(model);
            i_dal.Insert(models);
            i_dal.Delete(where);
            i_dal.Update(kos, where);
            i_dal.Select(top, where, kbs);
            i_dal.Select(pcount, pindex, out psum, where, kbs);
            i_dal.GetRecordCount(where);
            i_dal.GetModel(where, kbs);

            IDAL_OnlyQuery<TestModel, string, ColumnModelParser<TestModel>, ColumnInfo> i_dal_onlyquery = i_dal;
            i_dal_onlyquery.Select(top, where, kbs);
            i_dal_onlyquery.Select(pcount, pindex, out psum, where, kbs);
            i_dal_onlyquery.GetRecordCount(where);
            i_dal_onlyquery.GetModel(where, kbs);

            return true;
        }
    }
}
