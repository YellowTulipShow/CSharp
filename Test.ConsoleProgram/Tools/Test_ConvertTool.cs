using System;
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
            base.NameSign = @"转化";
            base.SonCases = new CaseModel[] {
                new ObjectToObject(),
                Func_ToRangeIndex(),
                new ToRangeList(),
                DateTimeToString(),
            };
        }

        public class ObjectToObject : CaseModel
        {
            public class OTO : AbsBasicDataModel
            {
                public object Source;
                public Type Aims_Type;
                public object Aims_Value;

                public enum SexEnum
                {
                    /// <summary>
                    /// 保密
                    /// </summary>
                    [Explain(@"保密")]
                    Secrecy,
                    /// <summary>
                    /// 男
                    /// </summary>
                    [Explain(@"男")]
                    Male = 1,
                    /// <summary>
                    /// 女
                    /// </summary>
                    [Explain(@"女")]
                    Female = 81,
                }

                public OTO(object source, Type aims_type, object aims_value) {
                    this.Source = source;
                    this.Aims_Type = aims_type;
                    this.Aims_Value = aims_value;
                }
            }
            public ObjectToObject() {
                this.NameSign = @"对象 转 对象";
                this.ExeEvent = () => {
                    OTO[] list = new OTO[] {
                        //new OTO(null, typeof(int), default(int)),
                        //new OTO(null, typeof(OTO), default(OTO)),
                        //new OTO(null, typeof(string), null),
                        //new OTO(null, typeof(DateTime), default(DateTime)),
                        //new OTO(null, typeof(GS.SexEnum), default(GS.SexEnum)),
                        new OTO(10, typeof(int), 10),
                        new OTO("10", typeof(int), 10),
                        new OTO("rrr10", typeof(int), default(int)),
                        new OTO(OTO.SexEnum.Male, typeof(int), default(int)),
                        new OTO((int)OTO.SexEnum.Male, typeof(OTO.SexEnum), (int)OTO.SexEnum.Male),
                        new OTO(((int)OTO.SexEnum.Male).ToString(), typeof(OTO.SexEnum), (int)OTO.SexEnum.Male),
                        new OTO(OTO.SexEnum.Male, typeof(OTO.SexEnum), (int)OTO.SexEnum.Male),
                        new OTO(10, typeof(float), 10f),
                        new OTO("10", typeof(float), 10f),
                        new OTO(65, typeof(double), 65.00),
                        new OTO("10", typeof(double), 10.0),
                        new OTO("50.25", typeof(double), 50.25),
                        new OTO("s50.25", typeof(double), default(double)),
                        new OTO("s50.25", typeof(string), "s50.25"),
                        new OTO("2018-06-01 14:52:22", typeof(DateTime), new DateTime(2018,06,01,14,52,22)),
                        new OTO(new DateTime(2018,06,01,14,52,22), typeof(DateTime), new DateTime(2018,06,01,14,52,22)),
                    };
                    for (int i = 1; i <= list.Length; i++) {
                        OTO oto = list[i - 1];
                        object result = ConvertTool.ToObject(oto.Aims_Type, oto.Source);
                        if (!oto.Aims_Value.Equals(result)) {
                            string source_type_fullname = oto.Source.GetType().FullName;
                            string aims_value_type_fullname = oto.Aims_Value.GetType().FullName;
                            string result_type_fullname = result.GetType().FullName;
                            Console.WriteLine("i:{0}结果不相等: oto.source:{1}  oto.aims_type:{2}  oto.aims_value:{3}  result:{4}  source_type_fullname:{5}  aims_value_type_fullname:{6}  result_type_fullname:{7}",
                                i, oto.Source, oto.Aims_Type, oto.Aims_Value, result, source_type_fullname, aims_value_type_fullname, result_type_fullname);
                            return false;
                        }
                    }
                    return true;
                };
            }
        }

        public class ToRangeList : CaseModel
        {
            public class ToRangeListItem
            {
                public int index = 1;
                public int count = 10;
                public string[] result = new string[] { };

                public static int DataSumCount = 47;
                public static ToRangeListItem[] ResultAnswer() {
                    return new ToRangeListItem[] {
                    new ToRangeListItem() {
                        index = -1,
                        count = 10,
                        result = new string[] { "第0项","第1项","第2项","第3项","第4项","第5项","第6项","第7项","第8项","第9项" },
                    },
                    new ToRangeListItem() {
                        index = 0,
                        count = 10,
                        result = new string[] { "第0项","第1项","第2项","第3项","第4项","第5项","第6项","第7项","第8项","第9项" },
                    },
                    new ToRangeListItem() {
                        index = 1,
                        count = 10,
                        result = new string[] { "第0项","第1项","第2项","第3项","第4项","第5项","第6项","第7项","第8项","第9项" },
                    },
                    new ToRangeListItem() {
                        index = 2,
                        count = 10,
                        result = new string[] { "第10项","第11项","第12项","第13项","第14项","第15项","第16项","第17项","第18项","第19项" },
                    },
                    new ToRangeListItem() {
                        index = 3,
                        count = 10,
                        result = new string[] { "第20项","第21项","第22项","第23项","第24项","第25项","第26项","第27项","第28项","第29项" },
                    },
                    new ToRangeListItem() {
                        index = 5,
                        count = 10,
                        result = new string[] { "第40项","第41项","第42项","第43项","第44项","第45项","第46项" },
                    },
                    new ToRangeListItem() {
                        index = 6,
                        count = 10,
                        result = new string[] {  },
                    },
                    new ToRangeListItem() {
                        index = 7,
                        count = 10,
                        result = new string[] {  },
                    },
                    new ToRangeListItem() {
                        index = 8,
                        count = 10,
                        result = new string[] {  },
                    },
                    new ToRangeListItem() {
                        index = 2,
                        count = 11,
                        result = new string[] { "第11项","第12项","第13项","第14项","第15项","第16项","第17项","第18项","第19项","第20项","第21项" },
                    },
                    new ToRangeListItem() {
                        index = 3,
                        count = 11,
                        result = new string[] { "第22项","第23项","第24项","第25项","第26项","第27项","第28项","第29项","第30项","第31项","第32项" },
                    },
                    new ToRangeListItem() {
                        index = 4,
                        count = 11,
                        result = new string[] { "第33项","第34项","第35项","第36项","第37项","第38项","第39项","第40项","第41项","第42项","第43项" },
                    },
                    new ToRangeListItem() {
                        index = 6,
                        count = 11,
                        result = new string[] {  },
                    },
                    new ToRangeListItem() {
                        index = 2,
                        count = 9,
                        result = new string[] { "第9项","第10项","第11项","第12项","第13项","第14项","第15项","第16项","第17项" },
                    },
                    new ToRangeListItem() {
                        index = 3,
                        count = 9,
                        result = new string[] { "第18项","第19项","第20项","第21项","第22项","第23项","第24项","第25项","第26项" },
                    },
                    new ToRangeListItem() {
                        index = 5,
                        count = 9,
                        result = new string[] { "第36项","第37项","第38项","第39项","第40项","第41项","第42项","第43项","第44项" },
                    },
                    new ToRangeListItem() {
                        index = 6,
                        count = 9,
                        result = new string[] { "第45项","第46项" },
                    },
                };
                }
            }
            public ToRangeList() {
                this.NameSign = @"获取列表范围";
                this.ExeEvent = () => {
                    int sumcount = ToRangeListItem.DataSumCount;
                    string[] source = new string[sumcount];
                    for (int i = 0; i < source.Length; i++) {
                        source[i] = string.Format("第{0}项", i);
                    }

                    // auto 规则测试
                    VerifyIList<string, string> verify = new VerifyIList<string, string>(CalcWayEnum.DoubleCycle);
                    foreach (ToRangeListItem item in ToRangeListItem.ResultAnswer()) {
                        verify.Answer = item.result;
                        verify.Source = ConvertTool.ToRangePage(source, item.index, item.count);
                        if (!verify.Calc()) {
                            Console.WriteLine("Error: sum: {0}  index: {1}  count: {2}  result: {3}", source.Length, item.index, item.count, JSON.Serializer(item.result));
                        }
                    }
                    Console.WriteLine("自动化程序测试结果成功!");

                    // old 生成结果数据
                    string absfile = PathHelp.CreateUseFilePath(@"/auto/tools/Test_ConvertTool", @"Func_ToRangeList.txt");
                    File.Delete(absfile);
                    using (FileStream fileshream = new FileStream(absfile, FileMode.OpenOrCreate)) {
                        using (StreamWriter writer = new StreamWriter(fileshream, YTS.Tools.Const.Format.FILE_ENCODING)) {
                            for (int count = 9; count <= 11; count++) {
                                for (int index = -1; index < 11; index++) {
                                    string[] result = ConvertTool.ToRangePage(source, index, count);
                                    writer.WriteLine("sum: {0}  index: {1}  count: {2}  result: {3}", source.Length, index, count, JSON.Serializer(result));
                                }
                                writer.WriteLine();
                            }
                            writer.Flush();
                        }
                    }
                    Console.WriteLine("生成结果数据: {0}", absfile);
                    return true;
                };
            }
        }

        public CaseModel DateTimeToString() {
            return new CaseModel() {
                NameSign = @"时间转字符串",
                ExeEvent = () => {
                    for (int i = 0; i < 1000; i++) {
                        DateTime time = new DateTime(2018, 5, 30, 14, 42, 33, i);
                        string str = ConvertTool.ToString(time);
                        //Convert.ToString(time);
                        DateTime ct = ConvertTool.ToDateTime(str, DateTime.Now);
                        if (time != ct) {
                            throw new Exception("error");
                        }
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_ToRangeIndex() {
            return new CaseModel() {
                NameSign = @"指定范围索引长度",
                ExeEvent = () => {
                    StringBuilder str = new StringBuilder();

                    int[] list = new int[] { 0, 1, 2, 3, 4, 5 };
                    str.AppendFormat("list: {0}\n\n", JSON.Serializer(list));
                    for (int index = -2; index < list.Length + 2; index++) {
                        for (int length = -2; length < list.Length + 2; length++) {
                            int[] result = ConvertTool.ToRangeIndex(list, index, length);
                            str.AppendFormat("index: {0} length: {1} result: {2}\n", index, length, JSON.Serializer(result));
                        }
                    }

                    string path = PathHelp.CreateUseFilePath(@"/auto/tools/Test_ConvertTool", @"Func_ToRangeIndex.txt");
                    this.ClearAndWriteFile(path, str.ToString());

                    VerifyIList<int, int> verify = new VerifyIList<int, int>(CalcWayEnum.DoubleCycle);
                    Action<int, int, int[]> method = (index, length, source) => {
                        verify.Answer = source;
                        verify.Source = ConvertTool.ToRangeIndex(list, index, length);
                        if (!verify.Calc()) {
                            Console.WriteLine("Error! index: {0} length: {1} result: {2}", index, length, JSON.Serializer(verify.Source));
                            throw new Exception("错误!");
                        }
                    };

                    method(-2, -2, new int[] { });
                    method(-2, -1, new int[] { });
                    method(-2, 0, new int[] { });
                    method(-2, 1, new int[] { 0 });
                    method(-2, 2, new int[] { 0, 1 });
                    method(-2, 3, new int[] { 0, 1, 2 });
                    method(-2, 4, new int[] { 0, 1, 2, 3 });
                    method(-2, 5, new int[] { 0, 1, 2, 3, 4 });
                    method(-2, 6, new int[] { 0, 1, 2, 3, 4, 5 });
                    method(-2, 7, new int[] { 0, 1, 2, 3, 4, 5 });
                    method(-1, -2, new int[] { });
                    method(-1, -1, new int[] { });
                    method(-1, 0, new int[] { });
                    method(-1, 1, new int[] { 0 });
                    method(-1, 2, new int[] { 0, 1 });
                    method(-1, 3, new int[] { 0, 1, 2 });
                    method(-1, 4, new int[] { 0, 1, 2, 3 });
                    method(-1, 5, new int[] { 0, 1, 2, 3, 4 });
                    method(-1, 6, new int[] { 0, 1, 2, 3, 4, 5 });
                    method(-1, 7, new int[] { 0, 1, 2, 3, 4, 5 });
                    method(0, -2, new int[] { });
                    method(0, -1, new int[] { });
                    method(0, 0, new int[] { });
                    method(0, 1, new int[] { 0 });
                    method(0, 2, new int[] { 0, 1 });
                    method(0, 3, new int[] { 0, 1, 2 });
                    method(0, 4, new int[] { 0, 1, 2, 3 });
                    method(0, 5, new int[] { 0, 1, 2, 3, 4 });
                    method(0, 6, new int[] { 0, 1, 2, 3, 4, 5 });
                    method(0, 7, new int[] { 0, 1, 2, 3, 4, 5 });
                    method(1, -2, new int[] { });
                    method(1, -1, new int[] { });
                    method(1, 0, new int[] { });
                    method(1, 1, new int[] { 1 });
                    method(1, 2, new int[] { 1, 2 });
                    method(1, 3, new int[] { 1, 2, 3 });
                    method(1, 4, new int[] { 1, 2, 3, 4 });
                    method(1, 5, new int[] { 1, 2, 3, 4, 5 });
                    method(1, 6, new int[] { 1, 2, 3, 4, 5 });
                    method(1, 7, new int[] { 1, 2, 3, 4, 5 });
                    method(2, -2, new int[] { });
                    method(2, -1, new int[] { });
                    method(2, 0, new int[] { });
                    method(2, 1, new int[] { 2 });
                    method(2, 2, new int[] { 2, 3 });
                    method(2, 3, new int[] { 2, 3, 4 });
                    method(2, 4, new int[] { 2, 3, 4, 5 });
                    method(2, 5, new int[] { 2, 3, 4, 5 });
                    method(2, 6, new int[] { 2, 3, 4, 5 });
                    method(2, 7, new int[] { 2, 3, 4, 5 });
                    method(3, -2, new int[] { });
                    method(3, -1, new int[] { });
                    method(3, 0, new int[] { });
                    method(3, 1, new int[] { 3 });
                    method(3, 2, new int[] { 3, 4 });
                    method(3, 3, new int[] { 3, 4, 5 });
                    method(3, 4, new int[] { 3, 4, 5 });
                    method(3, 5, new int[] { 3, 4, 5 });
                    method(3, 6, new int[] { 3, 4, 5 });
                    method(3, 7, new int[] { 3, 4, 5 });
                    method(4, -2, new int[] { });
                    method(4, -1, new int[] { });
                    method(4, 0, new int[] { });
                    method(4, 1, new int[] { 4 });
                    method(4, 2, new int[] { 4, 5 });
                    method(4, 3, new int[] { 4, 5 });
                    method(4, 4, new int[] { 4, 5 });
                    method(4, 5, new int[] { 4, 5 });
                    method(4, 6, new int[] { 4, 5 });
                    method(4, 7, new int[] { 4, 5 });
                    method(5, -2, new int[] { });
                    method(5, -1, new int[] { });
                    method(5, 0, new int[] { });
                    method(5, 1, new int[] { 5 });
                    method(5, 2, new int[] { 5 });
                    method(5, 3, new int[] { 5 });
                    method(5, 4, new int[] { 5 });
                    method(5, 5, new int[] { 5 });
                    method(5, 6, new int[] { 5 });
                    method(5, 7, new int[] { 5 });
                    method(6, -2, new int[] { });
                    method(6, -1, new int[] { });
                    method(6, 0, new int[] { });
                    method(6, 1, new int[] { });
                    method(6, 2, new int[] { });
                    method(6, 3, new int[] { });
                    method(6, 4, new int[] { });
                    method(6, 5, new int[] { });
                    method(6, 6, new int[] { });
                    method(6, 7, new int[] { });
                    method(7, -2, new int[] { });
                    method(7, -1, new int[] { });
                    method(7, 0, new int[] { });
                    method(7, 1, new int[] { });
                    method(7, 2, new int[] { });
                    method(7, 3, new int[] { });
                    method(7, 4, new int[] { });
                    method(7, 5, new int[] { });
                    method(7, 6, new int[] { });
                    method(7, 7, new int[] { });

                    return true;
                },
            };
        }
    }
}
