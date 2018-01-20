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
    public class TestDALSQLServer : AbsTestCase {
        public override string TestNameSign() {
            return @"测试 DALSQLServer类";
        }
        public override void TestMethod() {
        }

        public override ITestCase[] SonTestCase() {
            return new ITestCase[] {
                //new Test_CAnalysisPropertyColumns(),
                //new Test_CDefaultModel(),
                //new Test_CICreateSQL(),
                //new Test_CIAutoTable(),
                new Test_CGetModelList(),
                //new Test_CInsertMethod_out_parmeter(),
                //new Test_CInsertSpeed(),
                //new Test_ColumnInfoSort(),
            };
        }

        #region Son Test Case
        private class Test_CAnalysisPropertyColumns : ITestCase
        {
            public string TestNameSign() {
                return @"实现解析属性的'行'特性";
            }
            public void TestMethod() {
                DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                ColumnInfo[] dic = dal.AnalysisPropertyColumns();
                foreach (ColumnInfo item in dic) {
                    Console.WriteLine(String.Format("PropertyInfo Name: {0}", item.Property.Name));
                    Console.WriteLine(String.Format("Column TypeId: {0}", item.Attribute.TypeId));
                    Console.WriteLine(String.Format("Column IsIDentity: {0}", item.Attribute.IsIDentity));
                    Console.WriteLine(String.Format("Column IsCanBeNull: {0}", item.Attribute.IsCanBeNull));
                    Console.WriteLine(String.Format("Column IsDbGenerated: {0}", item.Attribute.IsDbGenerated));
                    Console.WriteLine(String.Format("Column IsPrimaryKey: {0}", item.Attribute.IsPrimaryKey));
                }
            }
        }
        private class Test_CDefaultModel : ITestCase
        {
            public string TestNameSign() {
                return @"获得系统自动生成默认的实例";
            }
            public void TestMethod() {
                DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                Console.WriteLine(dal.DefaultModel().ToString());
            }
        }
        private class Test_CICreateSQL : ITestCase
        {
            public string TestNameSign() {
                return @"基础SQL语句";
            }
            public void TestMethod() {
                //DALSQLServer<ModelArticles> bll = new DALSQLServer<ModelArticles>();
                //ModelArticles model = new ModelArticles() {
                //    Money = 287.88M,
                //    VipDiscountRate = 80,
                //    TimeAdd = DateTime.Now,
                //    Remark = @"测试数据,备注"
                //};

                DALSQLServer<ModelUser> bll = new DALSQLServer<ModelUser>();
                ModelUser model = new ModelUser() {
                    Email = @"1426689530@qq.com",
                    MobilePhone = @"18563920971",
                    NickName = @"YellowTulipShow",
                    Password = @"zrqytspass",
                    RealName = @"赵瑞青",
                    Remark = @"System Developer",
                    TelePhone = string.Empty,
                    TimeAdd = DateTime.Now,
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
        private class Test_CIAutoTable : ITestCase
        {
            public string TestNameSign() {
                return @"IAutoTable 自动化表 SQL字符串";
            }
            public void TestMethod() {
                DALSQLServer<ModelUser> dal = new DALSQLServer<ModelUser>();

                Console.WriteLine("创建 数据表 SQL:");
                string createsql = dal.SQLCreateTable();
                Console.WriteLine(createsql);
                Console.WriteLine(string.Empty);

                Console.WriteLine("清空 数据表 SQL:");
                Console.WriteLine(dal.SQLClearTable());
                Console.WriteLine(string.Empty);

                Console.WriteLine("'清除' 数据表 SQL:");
                Console.WriteLine(dal.SQLKillTable());
            }
        }
        private class Test_CGetModelList : ITestCase
        {
            public string TestNameSign() {
                return @"查询所有数据";
            }
            public void TestMethod() {
                BLLUser dal = new BLLUser();
                DataTable dt = dal.GetList(0, string.Empty, null);
                foreach (ModelUser item in dal.GetModelList(dt)) {
                    Console.WriteLine(item.ToString());
                }
            }
        }
        private class Test_CInsertMethod_out_parmeter : ITestCase
        {
            public string TestNameSign() {
                return @"执行-基础的数据操作方法";
            }
            public void TestMethod() {
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
        }
        private class Test_CInsertSpeed : ITestCase
        {
            public string TestNameSign() {
                return @"执行添加大量测试数据-测试速度";
            }
            public void TestMethod() {
                DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                double count = System.Math.Pow(10, 5);
                Console.WriteLine(@"执行循环次数: {0}", count);
                List<string> sqls = new List<string>();
                for (int i = 0; i < count; i++) {
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
        private class Test_ColumnInfoSort : ITestCase
        {
            public string TestNameSign() {
                return @"列信息的排序问题";
            }
            public void TestMethod() {
                DALSQLServer<ModelUser> dal = new DALSQLServer<ModelUser>();
                ColumnInfo[] ciAttr = dal.AnalysisPropertyColumns();
                for (int i = 0; i < ciAttr.Length; i++) {
                    Console.WriteLine("排序: {0} 列名: {1} 解释: {2} ", i, ciAttr[i].Property.Name, ciAttr[i].Explain.Text);
                }
            }
        }
        #endregion
    }
}
