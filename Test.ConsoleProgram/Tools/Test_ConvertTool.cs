﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using YTS.Tools;

namespace Test.ConsoleProgram.Tools
{
    public class Test_ConvertTool : CaseModel
    {
        public Test_ConvertTool() {
            base.NameSign = @"转化工具";
            base.SonCases = new CaseModel[] {
                Func_GetIListRange(),
            };
        }

        #region old method
        #region === DataTypeConvert ===
        public CaseModel DataTypeConvert() {
            return new CaseModel() {
                NameSign = @"数据类型转换",
                ExeEvent = () => {
                    Print.WriteLine(@"ConvertTool 的类型转化工具");
                    return true;
                },
                SonCases = new CaseModel[] {
                    new StringToInt(),
                    new StringToInt_ErrorValue(),
                    new StringToString(),
                    new StringToString_ErrorValue(),
                    new StringToFloat(),
                    new StringToDecimal(),
                    new StringToBoolean(),
                    new DataTableToString(),
                    new DictionaryToString(),
                },
            };
        }
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

            public bool Method() {
                string str = Srource();
                Print.WriteLine("SourceData: {0}", str);
                int[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToInt(s, 0));
                Print.WriteLine("Result: {0}", array.ToJson());
                return true;
            }
        }
        public class StringToInt_ErrorValue : CaseModel
        {
            public StringToInt_ErrorValue() {
                base.NameSign = @"测试 String 转 Int 加入检测 故意的排除值错误值: -1";
                base.ExeEvent = Method;
            }

            public bool Method() {
                string str = Srource();
                Print.WriteLine("SourceData: {0}", str);
                int[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToInt(s, 0), errorValue: -1);
                Print.WriteLine("Result: {0}", array.ToJson());
                return true;
            }
        }

        public class StringToString : CaseModel
        {
            public StringToString() {
                base.NameSign = @"测试 String 转 String";
                base.ExeEvent = Method;
            }

            public bool Method() {
                string str = Srource();
                Print.WriteLine("SourceData: {0}", str);
                string[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => s);
                Print.WriteLine("Result: {0}", array.ToJson());
                return true;
            }
        }
        public class StringToString_ErrorValue : CaseModel
        {
            public StringToString_ErrorValue() {
                base.NameSign = @"测试 String 转 String 加入检测 故意的排除值错误值: '0'";
                base.ExeEvent = Method;
            }

            public bool Method() {
                string str = Srource();
                Print.WriteLine("SourceData: {0}", str);
                string[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => s, "0");
                Print.WriteLine("Result: {0}", array.ToJson());
                return true;
            }
        }

        public class StringToFloat : CaseModel
        {
            public StringToFloat() {
                base.NameSign = @"测试 String 转 Float 故意的排除值错误值: '0f'";
                base.ExeEvent = Method;
            }

            public bool Method() {
                string str = Srource();
                Print.WriteLine("SourceData: {0}", str);
                float[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToFloat(s, 0f), errorValue: 0f);
                Print.WriteLine("Result: {0}", array.ToJson());
                return true;
            }
        }
        public class StringToDecimal : CaseModel
        {
            public StringToDecimal() {
                base.NameSign = @"测试 String 转 Decimal 故意的排除值错误值: '0m'";
                base.ExeEvent = Method;
            }

            public bool Method() {
                string str = Srource();
                Print.WriteLine("SourceData: {0}", str);
                decimal[] array = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToDecimal(s, 0m), errorValue: 0m);
                Print.WriteLine("Result: {0}", array.ToJson());
                return true;
            }
        }
        public class StringToBoolean : CaseModel
        {
            public StringToBoolean() {
                base.NameSign = @"测试 String 转 Boolean";
                base.ExeEvent = Method;
            }

            public bool Method() {
                string str = @"true,false,flsea,0,1,sliw,trus";

                Print.WriteLine("SourceData: {0}", str);
                bool[] array_1 = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToBool(s, true));
                Print.WriteLine("Default: true Result: {0}", array_1.ToJson());

                bool[] array_2 = ConvertTool.ListConvertType(str.ToArrayList(','), s => ConvertTool.ObjToBool(s, false));
                Print.WriteLine("Default: false Result: {0}", array_2.ToJson());
                return true;
            }
        }
        public class DataTableToString : CaseModel
        {
            public DataTableToString() {
                base.NameSign = @"测试 DataTable 转 String";
                base.ExeEvent = Method;
            }

            public bool Method() {
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
                return true;
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

            public bool Method() {
                Dictionary<string, int> dic = new Dictionary<string, int>() {
                    { "key1", 343 },
                    { "key2_ss", 41243 },
                };
                Print.WriteLine(@"转换 Dictionary<string, int> 的 Key 键值前面加上 ***: ");
                string[] keys = ConvertTool.ListConvertType(dic, d => string.Format(@"***{0}", d.Key));
                foreach (string k in keys) {
                    Print.WriteLine("Key Array: {0}", k);
                }
                return true;
            }
        }
        #endregion
        #endregion

        public CaseModel Event_Test_Unicode_Format_String() {
            return new CaseModel() {
                NameSign = @"测试 Unicode 格式字符转换",
                ExeEvent = () => {
                    char[] word_chars = CommonData.ASCII_WordText();
                    List<string> Sour_Str = new List<string>();
                    for (int i = 0; i < 10; i++) {
                        Sour_Str.Add(RandomData.GetString(word_chars, RandomData.R.Next(3, 15)));
                    }
                    foreach (string item in Sour_Str) {
                        string sour = string.Empty;
                        sour = item;
                        string repl = ConvertTool.StringToHexadecimal(sour);
                        string unicode = ConvertTool.UnicodeFormatString(repl);
                        Print.WriteLine("{0} : {1} : {2}", item, repl, unicode);
                    }
                    return true;
                },
            };
        }
        #endregion

        public CaseModel Func_GetIListRange() {
            return new CaseModel() {
                NameSign = @"获取列表范围",
                ExeEvent = () => {
                    int sumcount = RandomData.GetInt(35, 57);
                    string[] source = new string[sumcount];
                    for (int i = 0; i < source.Length; i++) {
                        source[i] = string.Format("第{0}项", i);
                    }
                    
                    string absfile = PathHelp.CreateUseFilePath(@"/auto/tools/Test_ConvertTool", @"Func_GetIListRange.txt");
                    using (FileStream fileshream = new FileStream(absfile, FileMode.OpenOrCreate)) {
                        using (StreamWriter writer = new StreamWriter(fileshream, Encoding.UTF8)) {
                            for (int count = 9; count <= 11; count++) {
                                for (int index = -1; index < 11; index++) {
                                    string[] result = ConvertTool.GetIListRange(source, index, count);
                                    writer.WriteLine("sum: {0}  index: {1}  count: {2}  result: {3}", source.Length, index, count, JSON.SerializeObject(result));
                                }
                                writer.WriteLine();
                            }
                            writer.Flush();
                        }
                    }
                    Console.WriteLine("结果已写入文件: {0}", absfile);
                    return true;
                },
            };
        }
    }
}
