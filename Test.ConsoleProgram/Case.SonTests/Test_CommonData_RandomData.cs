using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_CommonData_RandomData : CaseModel
    {
        public Test_CommonData_RandomData() {
            this.NameSign = @"常用工具的测试";
            this.ExeEvent = () => { };
            this.SonCases = new CaseModel[] {
                //ExeEvent_Section_ASCII_String(),
                //ExeEvent_Section_UnicodeConvert(),
                //ExeEvent_Section_Unicode_Create_Test(),
                //ExeEvent_RandomStrignMethod(),
                //ExeEvent_Random_Select_Item(),
                //ExeEvent_Random_DateTime(),
                //ExeEvent_Random_DayRegion(),
                ExeEvent_Random_DateTime_Region(true),
                ExeEvent_Random_DateTime_Region(false),
                //ExeEvent_Random_Int(),
                //ExeEvent_Random_Double(),
            };
        }

        #region === Random String ===
        private CaseModel ExeEvent_RandomStrignMethod() {
            return new CaseModel() {
                NameSign = @"随机字符串",
                ExeEvent = () => {
                    Print.WriteLine(@"(" + RandomData.GetString(100) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(100) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(100) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(CommonData.ASCII_Number()) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(CommonData.ASCII_Number()) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(CommonData.ASCII_Number()) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(CommonData.ASCII_Special(), 80) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(CommonData.ASCII_Special(), 80) + @")");
                    Print.WriteLine(@"(" + RandomData.GetString(CommonData.ASCII_Special(), 80) + @")");
                },
            };
        }
        #endregion

        #region === Random ASCII Code ===
        private CaseModel ExeEvent_Section_ASCII_String() {
            return new CaseModel() {
                NameSign = @"部分的 ASCII 码字符",
                ExeEvent = () => {
                    PrintMethod(@"所有常用字符:", CommonData.ASCII_ALL());
                    PrintMethod(@"常用文本字符", CommonData.ASCII_WordText());
                    PrintMethod(@"特殊字符:", CommonData.ASCII_Special());
                    PrintMethod(@"阿拉伯数字:", CommonData.ASCII_Number());
                    PrintMethod(@"小写英文:", CommonData.ASCII_LowerEnglish());
                    PrintMethod(@"大写英文:", CommonData.ASCII_UpperEnglish());
                },
            };
        }
        private void PrintMethod(string name, char[] sour) {
            Print.WriteLine(name);
            Print.WriteLine(ConvertTool.IListToString(sour, string.Empty) + @" 个数: " + sour.Length.ToString());
        }
        #endregion

        #region === Random Unicode Code ===
        private CaseModel ExeEvent_Section_UnicodeConvert() {
            return new CaseModel() {
                NameSign = @"Unicode 字符互转",
                ExeEvent = () => {
                    string[] Sour_Chinese = new string[] { "软", "件", "质", "量", "保", "证", "与", "管", "理", "计", "算", "机", "硬", "件", "技", "术", "基", "础", "数", "据", "结", "构", "严", "蔚", "敏", "路", "由", "型", "与", "交", "换", "型", "互", "联", "网", "基", "础", "程", "庆", "梅", "软", "件", "质", "量", "保", "证", "与", "管", "理", "计", "算", "机", "操", "作", "系", "统", "算", "法", "导", "论", "王", "红", "梅", "二", "愣", "子", };
                    foreach (string item in Sour_Chinese) {
                        Print.WriteLine(string.Format("{0} : {1}", item, ConvertTool.GB2312ToUnicode(item)));
                    }

                    string[] Sour_unicode = new string[] { @"\u00A9", @"\u8f6f", @"\u4ef6", @"\u8d28", @"\u91cf", @"\u4fdd", @"\u8bc1", @"\u4e0e", @"\u7ba1", @"\u7406", @"\u8ba1", @"\u7b97", @"\u673a", @"\u786c", @"\u4ef6", @"\u6280", @"\u672f", @"\u57fa", @"\u7840", @"\u6570", @"\u636e", @"\u7ed3", @"\u6784", @"\u4e25", @"\u851a", @"\u654f", @"\u8def", @"\u7531", @"\u578b", @"\u4e0e", @"\u4ea4", @"\u6362", @"\u578b", @"\u4e92", @"\u8054", @"\u7f51", @"\u57fa", @"\u7840", @"\u7a0b", @"\u5e86", @"\u6885", @"\u8f6f", @"\u4ef6", @"\u8d28", @"\u91cf", @"\u4fdd", @"\u8bc1", @"\u4e0e", @"\u7ba1", @"\u7406", @"\u8ba1", @"\u7b97", @"\u673a", @"\u64cd", @"\u4f5c", @"\u7cfb", @"\u7edf", @"\u7b97", @"\u6cd5", @"\u5bfc", @"\u8bba", @"\u738b", @"\u7ea2", @"\u6885", @"\u4e8c", @"\u6123", @"\u5b50" };
                    //foreach (string item in Sour_unicode) {
                    //    Print.WriteLine(string.Format("{0} : {1}", item, ConvertTool.UnicodeToGB2312(item)));
                    //}
                    Print.WriteLine(string.Format("{0} : {1}", Sour_unicode[0], ConvertTool.UnicodeToGB2312(Sour_unicode[0])));
                    Print.WriteLine(string.Format("{0} : {1}", Sour_unicode[1], ConvertTool.UnicodeToGB2312(Sour_unicode[1])));
                },
            };
        }
        private CaseModel ExeEvent_Section_Unicode_Create_Test() {
            return new CaseModel() {
                NameSign = @"Unicode 生成测试",
                ExeEvent = () => {
                    for (int i = 0; i < 2; i++) {
                        int hexa_int_min_sign = CommonData.Unicode_Chinese_MIN_Decimal();
                        int hexa_int_max_sign = CommonData.Unicode_Chinese_MAX_Decimal();
                        int random_value = RandomData.R.Next(hexa_int_min_sign, hexa_int_max_sign + 1);
                        string random_heax_string = ConvertTool.DecimalToHexadecimal(random_value);
                        string unicode_format_str = ConvertTool.UnicodeFormatString(random_heax_string);
                        string chinese_char = ConvertTool.UnicodeToGB2312(unicode_format_str);
                        Print.WriteLine("{0} : {1} : {2} : {3}", random_value, random_heax_string, unicode_format_str, chinese_char);
                    }
                },
            };
        }
        #endregion

        #region === Random DateTime ===
        private CaseModel ExeEvent_Random_DateTime() {
            return new CaseModel() {
                NameSign = @"随机时间",
                ExeEvent = () => {
                    for (int i = 0; i < 20; i++) {
                        Print.WriteLine(RandomData.GetDateTime().ToString(LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND));
                    }
                },
            };
        }

        private CaseModel ExeEvent_Random_DayRegion() {
            return new CaseModel() {
                NameSign = @"每个月的最大天数",
                ExeEvent = () => {
                    foreach (int year in new int[] { 2000, 2001 }) {
                        for (int month = 1; month <= 12; month++) {
                            Print.WriteLine("{0}年{1}月有{2}天", year, month, RandomData.GetMaxDayCount(year, month));
                        }
                    }
                },
            };
        }

        private CaseModel ExeEvent_Random_DateTime_Region(bool isAsc) {
            const string timeFormat = LFKeys.TABLE_DATETIME_FORMAT_MILLISECOND;
            DateTime min_time = new DateTime(2018, 04, 19, 06, 40, 0);
            DateTime max_time = new DateTime(2020, 01, 5, 18, 10, 0);
            return new CaseModel() {
                NameSign = @"随机时间范围",
                ExeEvent = () => {
                    Print.WriteLine("Init: {0}-{1}", min_time.ToString(timeFormat), max_time.ToString(timeFormat));
                    for (int i = 0; i < 20; i++) {
                        DateTime time = RandomData.GetDateTime(min_time, max_time);
                        string symbol = string.Empty;
                        if (isAsc) {
                            min_time = time;
                            symbol = @"↑";
                        } else {
                            max_time = time;
                            symbol = @"↓";
                        }
                        Print.WriteLine(time.ToString(timeFormat) + symbol);
                        //break;
                    }
                },
            };
        }

        //private CaseModel ExeEvent_Random_DateTime_RandomRegionValue() {
        //    return new CaseModel() {
        //        NameSign = @"随机获取区域值",
        //        ExeEvent = () => {
        //            int min = 1;
        //            int max = 9999;
        //            int start = 2018;
        //            int end = 2020;
        //            for (int x = start; x <= end; x++) {
        //                for (int y = start; y <= end; y++) {
        //                    for (int z = 0; z <= 3; z++) {
        //                        int upstatue = z;
        //                        Print.WriteLine("参数: upstatue:{0}, min:{1}, max:{2}, start:{3}, end:{4}", upstatue, min, max, x, y);
        //                        int value = RandomData.TimeRangeSelect(ref upstatue, min, max, x, y);
        //                        Print.WriteLine("结果: value:{0}, upstatue:{1}", value, upstatue);
        //                        Print.WriteLine(string.Empty);
        //                    }
        //                }
        //            }
        //        },
        //    };
        //}
        #endregion

        #region === Random Select Item ===
        private CaseModel ExeEvent_Random_Select_Item() {
            return new CaseModel() {
                NameSign = @"随机选项",
                ExeEvent = () => {
                    TestEnum[] source = ConvertTool.EnumForeachArray<TestEnum>();
                    foreach (TestEnum item in source) {
                        Print.WriteLine(string.Format("Name: {0}  Value: {1}", item.GetName(), item.GetIntValue()));
                    }

                    Print.WriteLine("随机筛选的值: ");
                    TestEnum random_selitem = RandomData.GetItem(source);
                    Print.WriteLine(string.Format("Name: {0}  Value: {1}", random_selitem.GetName(), random_selitem.GetIntValue()));
                },
            };
        }
        #endregion

        #region === Random Number ===
        private CaseModel ExeEvent_Random_Int() {
            return new CaseModel() {
                NameSign = @"随机 Int 值",
                ExeEvent = () => {
                    for (int i = 0; i < 50; i++) {
                        Print.WriteLine(RandomData.GetInt(-20, 20));
                    }
                },
            };
        }
        private CaseModel ExeEvent_Random_Double() {
            return new CaseModel() {
                NameSign = @"随机 Double 值",
                ExeEvent = () => {
                    for (int i = 0; i < 50; i++) {
                        Print.WriteLine(RandomData.GetDouble());
                    }
                },
            };
        }
        #endregion
    }
}
