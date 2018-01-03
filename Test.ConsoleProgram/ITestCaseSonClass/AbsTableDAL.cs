using System;
using System.Collections.Generic;
using System.Reflection;

using CSharp.ApplicationData;
using CSharp.LibrayDataBase;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class AbsTableDAL_AnalysisPropertyColumns : ITestCase
    {
        public string TestNameSign() {
            return @"基础的DAL 实现解析属性的'行'特性";
        }

        public void TestMethod() {
            BLLArticles bll = new BLLArticles();
            Dictionary<PropertyInfo, ColumnAttribute> dic = bll.TableDAL.AnalysisPropertyColumns();

            foreach (KeyValuePair<PropertyInfo, ColumnAttribute> item in dic) {
                Console.WriteLine(String.Format("PropertyInfo Name: {0}", item.Key.Name));
                Console.WriteLine(String.Format("Column TypeId: {0}", item.Value.TypeId));
                Console.WriteLine(String.Format("Column Name: {0}", item.Value.Name));
                Console.WriteLine(String.Format("Column DbType: {0}", item.Value.DbType));
                Console.WriteLine(String.Format("Column IsCanBeNull: {0}", item.Value.IsCanBeNull));
                Console.WriteLine(String.Format("Column IsDbGenerated: {0}", item.Value.IsDbGenerated));
                Console.WriteLine(String.Format("Column IsPrimaryKey: {0}", item.Value.IsPrimaryKey));
                Console.WriteLine("");
            }
        }
    }

    public class AbsTableDAL_DefaultModel : ITestCase
    {
        public string TestNameSign() {
            return @"基础的DAL-获得";
        }

        public void TestMethod() {
            BLLArticles bll = new BLLArticles();
            ModelArticles model = bll.TableDAL.DefaultModel();
            Console.WriteLine(model.ToString());
        }
    }
}
