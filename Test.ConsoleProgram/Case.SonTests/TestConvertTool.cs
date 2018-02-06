using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class TestConvertTool : AbsCase
    {
        public override string TestNameSign() {
            return @"测试 转化工具";
        }

        public override void TestMethod() {
            Console.WriteLine(@"ConvertTool 的类型转化工具");
        }

        public override ICase[] SonTestCase() {
            return new ICase[] {
                new StringToInt(),
                new StringToInt_ErrorValue(),
                new StringToString(),
                new StringToString_ErrorValue(),
                new StringToFloat(),
                new StringToDecimal(),
                new StringToBoolean(),
                new DataTableToString(),
                new DictionaryToString(),
            };
        }

        #region Son Test Case
        private static string Srource() {
            return ",15,8,3,-1,,,r,2,0,ssss,55,5,4,34M,350,0,";
        }
        private class StringToInt : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                int[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToInt(s, 0));
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToInt_ErrorValue : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Int 加入检测 故意的排除值错误值: -1";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                int[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToInt(s, 0), errorValue: -1);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }

        private class StringToString : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 String";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                string[] array = str.ToArrayList(',').ListConvertType(s => s);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToString_ErrorValue : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 String 加入检测 故意的排除值错误值: '0'";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                string[] array = str.ToArrayList(',').ListConvertType(s => s, "0");
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }

        private class StringToFloat : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Float 故意的排除值错误值: '0f'";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                float[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToFloat(s, 0f), errorValue: 0f);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToDecimal : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Decimal 故意的排除值错误值: '0m'";
            }
            public void TestMethod() {
                string str = Srource();
                Console.WriteLine("SourceData: \n {0}", str);
                decimal[] array = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToDecimal(s, 0m), errorValue: 0m);
                Console.WriteLine("Result: \n {0}", JsonHelper.SerializeObject(array));
            }
        }
        private class StringToBoolean : ICase
        {
            public string TestNameSign() {
                return @"测试 String 转 Boolean";
            }
            public void TestMethod() {
            string str = @"true,false,flsea,0,1,sliw,trus";

            Console.WriteLine("SourceData: \n {0}", str);
            bool[] array_1 = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToBool(s, true));
            Console.WriteLine("Default: true Result: \n {0}", JsonHelper.SerializeObject(array_1));

            bool[] array_2 = str.ToArrayList(',').ListConvertType(s => ConvertTool.ObjToBool(s, false));
            Console.WriteLine("Default: false Result: \n {0}", JsonHelper.SerializeObject(array_2));
            }
        }
        private class DataTableToString : ICase
        {
            public string TestNameSign() {
                return @"测试 DataTable 转 String";
            }

            public void TestMethod() {
                DataTable dt = DataTableSource(new Dictionary<string, string>() {
                    { "捐赠人姓名", "name:sjifwjelifjwief" },
                    { "捐赠人电话", "telephone:2349873941" },
                    { "发件地址-省", "省将自己死的法恩发觉我:" },
                    { "发件地址_市", "dafwefa" },
                });
                Console.WriteLine(@"转换 DataTable 的 捐赠人电话前面加上 ***: ");
                string[] tels = ConvertTool.ListConvertType(dt, r => string.Format(@"***{0}", r["捐赠人电话"]));
                foreach (string k in tels) {
                    Console.WriteLine("tels Array: {0}", k);
                }
            }
            private DataTable DataTableSource(Dictionary<string, string> dic) {
                List<Dictionary<String, String>> dbSource = new List<Dictionary<String, String>>();
                for (int i = 0; i < 10; i++) {
                    dbSource.Add(dic);
                }

                Type StringType = Type.GetType("System.String");
                DataTable newdt = new DataTable("SurveyUserInfo" + DateTime.Now.Day.ToString());
                DataColumn dc = newdt.Columns.Add("id", Type.GetType("System.Int32"));
                dc.AutoIncrement = true;//自动增加
                dc.AutoIncrementSeed = 1;//起始为1
                dc.AutoIncrementStep = 1;//步长为1
                dc.AllowDBNull = false;//是否允许为空
                foreach (KeyValuePair<String, String> keyVal in dbSource[0]) {
                    newdt.Columns.Add(new DataColumn(keyVal.Key, StringType));
                }
                foreach (Dictionary<String, String> dicAry in dbSource) {
                    DataRow dr = newdt.NewRow();
                    foreach (KeyValuePair<String, String> keyVal in dicAry) {
                        dr[keyVal.Key] = keyVal.Value;
                    }
                    newdt.Rows.Add(dr);
                }
                return newdt;
            }
        }
        private class DictionaryToString : ICase
        {
            public string TestNameSign() {
                return @"测试 Dictionary 转 String";
            }

            public void TestMethod() {
                Dictionary<string, int> dic = new Dictionary<string, int>() {
                    { "key1", 343 },
                    { "key2_ss", 41243 },
                };
                Console.WriteLine(@"转换 Dictionary<string, int> 的 Key 键值前面加上 ***: ");
                string[] keys = ConvertTool.ListConvertType(dic, d => string.Format(@"***{0}", d.Key));
                foreach (string k in keys) {
                    Console.WriteLine("Key Array: {0}", k);
                }
            }
        }
        #endregion
    }
}
