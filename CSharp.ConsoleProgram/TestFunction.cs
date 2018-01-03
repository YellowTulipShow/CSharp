using System;
using System.Reflection;
using System.Collections.Generic;

using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;
using CSharp.ApplicationData;

namespace CSharp.ConsoleProgram
{
    public class TestFunction
    {
        public TestFunction() { }

        public void Init() {
            TestSQLServerBasicDALDefaultModel();
        }

        #region Test SQLServerBasicDAL
        private void TestSQLServerBasicDALGetColumnsDictionary() {
            BLLArticles bll = new BLLArticles();
            Dictionary<PropertyInfo, ColumnAttribute> dic = bll.TableDAL.AnalysisPropertyColumns();

            Print.WriteLine("=== 解析开始 GetColumnsDictionary(): ===");
            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in dic) {
                Print.WriteLine("");
                Print.WriteLine(String.Format("PropertyInfo Name: {0}", item.Key.Name));
                Print.WriteLine(String.Format("Column TypeId: {0}", item.Value.TypeId));
                Print.WriteLine(String.Format("Column Name: {0}", item.Value.Name));
                Print.WriteLine(String.Format("Column DbType: {0}", item.Value.DbType));
                Print.WriteLine(String.Format("Column IsCanBeNull: {0}", item.Value.IsCanBeNull));
                Print.WriteLine(String.Format("Column IsDbGenerated: {0}", item.Value.IsDbGenerated));
                Print.WriteLine(String.Format("Column IsPrimaryKey: {0}", item.Value.IsPrimaryKey));
                Print.WriteLine("");
            }
            Print.WriteLine("=== GetColumnsDictionary() 解析结束! ===");
        }
        private void TestSQLServerBasicDALDefaultModel() {
            BLLArticles bll = new BLLArticles();
            ModelArticles model = bll.TableDAL.DefaultModel();
            Print.WriteLine(model.ToString());
        }
        #endregion

        #region Test Attribute
        private void TestAttribute() {
            //Type m_type = typeof(CSharp.LibrayDataBase.Model.Articles);
            //PropertyInfo[] pros = m_type.GetProperties();
            //foreach (PropertyInfo item in pros) {
            //    ExplainAttribute[] expattr = ReflexHelper.FindAttributes<ExplainAttribute>(item);
            //    Print.WriteLine(String.Format("{0} have ExplainAttribute: {1}", item.Name, expattr[0].Text));
            //}

            ModelArticles ar = new ModelArticles();

            Type tm = typeof(ModelArticles);

            Print.WriteLine(tm.IsDefined(typeof(TableAttribute), false));
        }
        #endregion
    }
}
