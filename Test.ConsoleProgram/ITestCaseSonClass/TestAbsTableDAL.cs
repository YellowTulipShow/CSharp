using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using CSharp.ApplicationData;
using CSharp.LibrayDataBase;
using CSharp.LibrayDataBase.Utils;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.ITestCaseSonClass
{
    public class AbsTableDAL_AnalysisPropertyColumns : ITestCase
    {
        public string TestNameSign() {
            return @"基础的DAL 实现解析属性的'行'特性";
        }

        public void TestMethod() {
            BLLArticles bll = new BLLArticles();
            ColumnInfo[] dic = bll.AnalysisPropertyColumns();

            foreach (ColumnInfo item in dic) {
                Console.WriteLine(String.Format("PropertyInfo Name: {0}", item.Property.Name));
                Console.WriteLine(String.Format("Column TypeId: {0}", item.Attribute.TypeId));
                Console.WriteLine(String.Format("Column IsIDentity: {0}", item.Attribute.IsIDentity));
                Console.WriteLine(String.Format("Column IsCanBeNull: {0}", item.Attribute.IsCanBeNull));
                Console.WriteLine(String.Format("Column IsDbGenerated: {0}", item.Attribute.IsDbGenerated));
                Console.WriteLine(String.Format("Column IsPrimaryKey: {0}", item.Attribute.IsPrimaryKey));
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
            ModelArticles model = System.Activator.CreateInstance<ModelArticles>();
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
            Console.WriteLine(string.Empty);

            Console.Write("Insert 语句:");
            Console.WriteLine(bll.SQLInsert(model));
            Console.WriteLine(string.Empty);

            model.id = 8;
            Console.Write("Delete 语句:");
            Console.WriteLine(bll.SQLDelete(model));
            Console.WriteLine(string.Empty);

            Console.Write("Update 语句:");
            Console.WriteLine(bll.SQLUpdate(model));
        }
    }

    public class DALSQLServer_IAutoTable : ITestCase
    {
        public string TestNameSign() {
            return @"DALSQLServer IAutoTable 自动化表";
        }

        public void TestMethod() {
            //BLLArticles bllArticles = new BLLArticles();
            DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();

            Console.WriteLine("创建 数据表 SQL:");
            string createsql = dal.SQLCreateTable();
            Console.WriteLine(createsql);
            Console.WriteLine(string.Empty);

            Console.WriteLine("执行创建:");
            Console.WriteLine(DbHelperSQL.GetSingle(createsql));
            Console.WriteLine(string.Empty);

            Console.WriteLine("清空 数据表 SQL:");
            Console.WriteLine(dal.SQLClearTable());
            Console.WriteLine(string.Empty);

            Console.WriteLine("'清除' 数据表 SQL:");
            Console.WriteLine(dal.SQLKillTable());
        }
    }

    public class AbsTableDAL_ITableBasicFunction : ITestCase
    {
        public string TestNameSign() {
            return @"DALSQLServer 类及其继承AbsTableDAL类的测试";
        }

        private void Test_SQLCreateTable() {
            DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();

            Console.WriteLine("执行清除数据表");
            DALSQLServer<ModelArticles>.Transaction(new string[] {
                dal.SQLCreateTable(),
            });
        }

        private void Test_AddDatas() {
            DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();

            Console.WriteLine("循环添加大量测试数据");
            for (int i = 0; i < 100000; i++) {
                int sign = 0;
                Console.Write(string.Format("标识: {1}, 结果: {0}", dal.Insert(new ModelArticles() {
                    id = i,
                    Money = i * 2.5M,
                    Remark = string.Format("测试数据: {0} 条", i + 1),
                    TimeAdd = DateTime.Now
                }, out sign), sign));
            }
            Console.WriteLine("结束了!");
        }

        private void Test_InsertMethod_out_parmeter() {
            DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
            ModelArticles model = new ModelArticles() {
                id = 5555,
                Money = 66.89M,
                Remark = ConvertTool.CombinationContent(DateTime.Now.Year, DateTime.Now.Second, new Random().Next(DateTime.Now.Millisecond)),
                TimeAdd = DateTime.Now,
                VipDiscountRate = 95
            };
            int sign = 0;
            Console.WriteLine(dal.Insert(model, out sign));
            model.id = sign;
            Console.WriteLine(string.Format("最终的模型数据: {0}", model.ToString()));
            Console.WriteLine(string.Empty);

            model.id = model.id - 3;
            Console.Write("删掉刚添加的id小3的数据");
            Console.WriteLine(dal.Delete(model));
            model.id = 8;
            Console.Write("删掉id为8的数据");
            Console.WriteLine(dal.Delete(model));
            model.id = 10;
            Console.Write("删掉id为10的数据");
            Console.WriteLine(dal.Delete(model));

            model.id = 4;
            Console.Write("更新id为4的数据");
            Console.WriteLine(dal.Update(model));
        }

        private void Test_GetModelList() {
            DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
            Console.WriteLine("查询所有数据:");
            DataTable dt = dal.GetList(0, string.Empty, null);
            foreach (ModelArticles item in dal.GetModelList(dt)) {
                Console.WriteLine(item.ToString());
            }
        }

        public void TestMethod() {
            Test_InsertSpeed();
        }

        private void Test_InsertSpeed() {
            DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
            Console.WriteLine("循环添加大量测试数据");
            List<string> sqls = new List<string>();
            for (int i = 0; i < 100000; i++) {
                sqls.Add(dal.SQLInsert(new ModelArticles() {
                    id = i,
                    Money = i * 2.5M,
                    Remark = string.Format("测试数据: {0} 条", i + 1),
                    TimeAdd = DateTime.Now
                }));
            }
            Console.WriteLine(string.Format("生成完成sqls: 个数: {0}", sqls.Count));
            Console.WriteLine("结束了!");
        }
    }
}
