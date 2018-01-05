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
                //Console.WriteLine(String.Format("Column DbType: {0}", item.Value.DbType));
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

    public class AbsTableDAL_ICreateSQL : ITestCase
    {
        public string TestNameSign() {
            return @"基础SQL语句";
        }

        public void TestMethod() {
            BLLArticles bll = new BLLArticles();
            ModelArticles model = new ModelArticles() {
                Money = 287.88M,
                VipDiscountRate = 80,
                TimeAdd = DateTime.Now,
                Remark = @"测试数据,备注"
            };
            Console.WriteLine("原始数据:");
            Console.WriteLine(model.ToString());
            Console.WriteLine("Insert 语句:");
            Console.WriteLine(bll.TableDAL.SQLInsert(model));

            model.id = 8;
            Console.WriteLine("Delete 语句:");
            Console.WriteLine(bll.TableDAL.SQLDelete(model));
            Console.WriteLine("Updata 语句:");
            Console.WriteLine(bll.TableDAL.SQLUpdate(model));
        }
    }
}
