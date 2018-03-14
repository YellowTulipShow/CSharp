using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_ConvertTool : CaseModel
    {
        public Test_ConvertTool() {
            base.NameSign = @"测试 转化工具";
            base.ExeEvent = Method;
            base.SonCases = SonCaseArray();
        }

        public void Method() {
            Print.WriteLine(@"ConvertTool 的类型转化工具");
        }

        public CaseModel[] SonCaseArray() {
            //List<ICase> 
            return new CaseModel[] {
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
        //public class TypeConvert : AbsCase {}

        #region Son Test Case
        private static string Srource() {
            return ",15,8,3,-1,,,r,2,0,ssss,55,5,4,34M,350,0,";
        }
        public class StringToInt : CaseModel
        {
            public StringToInt() {
                base.NameSign = @"测试 String 转 Int";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = Srource();
                Print.WriteLine("SourceData: \n {0}", str);
                int[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToInt(s, 0));
                Print.WriteLine("Result: \n {0}", array.ToJson());
            }
        }
        public class StringToInt_ErrorValue : CaseModel
        {
            public StringToInt_ErrorValue() {
                base.NameSign = @"测试 String 转 Int 加入检测 故意的排除值错误值: -1";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = Srource();
                Print.WriteLine("SourceData: \n {0}", str);
                int[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToInt(s, 0), errorValue: -1);
                Print.WriteLine("Result: \n {0}", array.ToJson());
            }
        }

        public class StringToString : CaseModel
        {
            public StringToString() {
                base.NameSign = @"测试 String 转 String";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = Srource();
                Print.WriteLine("SourceData: \n {0}", str);
                string[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => s);
                Print.WriteLine("Result: \n {0}", array.ToJson());
            }
        }
        public class StringToString_ErrorValue : CaseModel
        {
            public StringToString_ErrorValue() {
                base.NameSign = @"测试 String 转 String 加入检测 故意的排除值错误值: '0'";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = Srource();
                Print.WriteLine("SourceData: \n {0}", str);
                string[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => s, "0");
                Print.WriteLine("Result: \n {0}", array.ToJson());
            }
        }

        public class StringToFloat : CaseModel
        {
            public StringToFloat() {
                base.NameSign = @"测试 String 转 Float 故意的排除值错误值: '0f'";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = Srource();
                Print.WriteLine("SourceData: \n {0}", str);
                float[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToFloat(s, 0f), errorValue: 0f);
                Print.WriteLine("Result: \n {0}", array.ToJson());
            }
        }
        public class StringToDecimal : CaseModel
        {
            public StringToDecimal() {
                base.NameSign = @"测试 String 转 Decimal 故意的排除值错误值: '0m'";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = Srource();
                Print.WriteLine("SourceData: \n {0}", str);
                decimal[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToDecimal(s, 0m), errorValue: 0m);
                Print.WriteLine("Result: \n {0}", array.ToJson());
            }
        }
        public class StringToBoolean : CaseModel
        {
            public StringToBoolean() {
                base.NameSign = @"测试 String 转 Boolean";
                base.ExeEvent = Method;
            }

            public void Method() {
                string str = @"true,false,flsea,0,1,sliw,trus";

                Print.WriteLine("SourceData: \n {0}", str);
                bool[] array_1 = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToBool(s, true));
                Print.WriteLine("Default: true Result: \n {0}", array_1.ToJson());

                bool[] array_2 = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToBool(s, false));
                Print.WriteLine("Default: false Result: \n {0}", array_2.ToJson());
            }
        }
        public class DataTableToString : CaseModel
        {
            public DataTableToString() {
                base.NameSign = @"测试 DataTable 转 String";
                base.ExeEvent = Method;
            }

            public void Method() {
                DataTable dt = DataTableSource(new Dictionary<string, string>() {
                    { "捐赠人姓名", "name:sjifwjelifjwief" },
                    { "捐赠人电话", "telephone:2349873941" },
                    { "发件地址-省", "省将自己死的法恩发觉我:" },
                    { "发件地址_市", "dafwefa" },
                });
                Print.WriteLine(@"转换 DataTable 的 捐赠人电话前面加上 ***: ");
                string[] tels = ConvertTool.ListConvertType(dt, r => string.Format(@"***{0}", r["捐赠人电话"]));
                foreach (string k in tels) {
                    Print.WriteLine("tels Array: {0}", k);
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
        public class DictionaryToString : CaseModel
        {
            public DictionaryToString() {
                base.NameSign = @"测试 Dictionary 转 String";
                base.ExeEvent = Method;
            }

            public void Method() {
                Dictionary<string, int> dic = new Dictionary<string, int>() {
                    { "key1", 343 },
                    { "key2_ss", 41243 },
                };
                Print.WriteLine(@"转换 Dictionary<string, int> 的 Key 键值前面加上 ***: ");
                string[] keys = ConvertTool.ListConvertType(dic, d => string.Format(@"***{0}", d.Key));
                foreach (string k in keys) {
                    Print.WriteLine("Key Array: {0}", k);
                }
            }
        }
        #endregion
    }
}
