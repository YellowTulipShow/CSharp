using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YTS.Engine.DataBase;
using YTS.Engine.ShineUpon;
using YTS.Model.DB;
using YTS.Model.File;
using YTS.Tools;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.Engine
{
    public class Test_AbsShineUponParser : CaseModel
    {
        public Test_AbsShineUponParser() {
            NameSign = @"映射模型解析器";
            SonCases = new CaseModel[] {
                Func_ColumnModelParser(),
                Func_FieldModelParser(),
                Func_GetValue_SetValue(),
            };
        }

        #region Test Model
        public class T : YTS.Model.DB.AbsTable
        {
            /// <summary>
            /// 创建者用户SID
            /// </summary>
            [Explain(@"创建者用户SID")]
            [Column]
            public string CreateUserSID { get { return _CreateUserSID; } set { _CreateUserSID = value; } }
            private string _CreateUserSID = string.Empty;

            /// <summary>
            /// 名称
            /// </summary>
            [Explain(@"名称")]
            [Column]
            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            /// <summary>
            /// 发布时间
            /// </summary>
            [Column(IsShineUpon = false)]
            public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
            private DateTime _TimeRelease = DateTime.Now;

            /// <summary>
            /// 内容
            /// </summary>
            [Explain(@"内容")]
            public string Content { get { return _Content; } set { _Content = value; } }
            private string _Content = string.Empty;

            public static KeyString[] Answer_ColumnInfos() {
                return new KeyString[] {
                    new KeyString() {
                        Key = @"CreateUserSID",
                        Value = @"创建者用户SID",
                    },
                    new KeyString() {
                        Key = @"Name",
                        Value = @"名称",
                    },
                };
            }

            public override string GetTableName() {
                return @"dt_Test_T";
            }
        }

        public class F : YTS.Model.File.AbsFile
        {
            /// <summary>
            /// 创建者用户SID
            /// </summary>
            [Explain(@"创建者用户SID")]
            [ShineUponProperty]
            public string CreateUserSID { get { return _CreateUserSID; } set { _CreateUserSID = value; } }
            private string _CreateUserSID = string.Empty;

            /// <summary>
            /// 名称
            /// </summary>
            [Explain(@"名称")]
            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            /// <summary>
            /// 发布时间
            /// </summary>
            [ShineUponProperty(IsShineUpon = false)]
            public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
            private DateTime _TimeRelease = DateTime.Now;

            /// <summary>
            /// 描述
            /// </summary>
            [Explain(@"描述")]
            [ShineUponProperty]
            public string Description { get { return _Description; } set { _Description = value; } }
            private string _Description = string.Empty;

            /// <summary>
            /// 内容
            /// </summary>
            [Explain(@"内容")]
            public string Content { get { return _Content; } set { _Content = value; } }
            private string _Content = string.Empty;

            public static KeyString[] Answer_FieldInfos() {
                return new KeyString[] {
                    new KeyString() {
                        Key = @"CreateUserSID",
                        Value = @"创建者用户SID",
                    },
                    new KeyString() {
                        Key = @"Description",
                        Value = @"描述",
                    },
                };
            }

            public override string GetPathFolder() {
                return @"/Test";
            }

            public override string GetFileName() {
                return @"F_model";
            }
        }

        public class GS : AbsShineUpon
        {
            /// <summary>
            /// 价格(人贩子(-_^)嘿嘿..)
            /// </summary>
            [Explain(@"价格")]
            [Column]
            public double Price { get { return _Price; } set { _Price = value; } }
            private double _Price = 0;

            /// <summary>
            /// 名称
            /// </summary>
            [Explain(@"名称")]
            [Column]
            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            /// <summary>
            /// 年龄
            /// </summary>
            [Explain(@"年龄")]
            [Column]
            public int Age { get { return _Age; } set { _Age = value; } }
            private int _Age = 0;

            /// <summary>
            /// 出生日期
            /// </summary>
            [Explain(@"出生日期")]
            [Column]
            public DateTime DateOfBirth { get { return _DateOfBirth; } set { _DateOfBirth = value; } }
            private DateTime _DateOfBirth = DateTime.Now;

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
                Male = 1,
                /// <summary>
                /// 女
                /// </summary>
                [Explain(@"女")]
                Female = 81,
            }
            /// <summary>
            /// 性别
            /// </summary>
            [Explain(@"性别")]
            [Column]
            public SexEnum Sex { get { return _sex; } set { _sex = value; } }
            private SexEnum _sex = SexEnum.Secrecy;
        }

        #endregion

        public CaseModel Func_ColumnModelParser() {
            return new CaseModel() {
                NameSign = @"列",
                ExeEvent = () => {
                    ColumnModelParser<T> parser = new ColumnModelParser<T>();
                    KeyString[] keys = T.Answer_ColumnInfos();
                    ColumnInfo[] columns = parser.GetList();

                    return new VerifyIList<KeyString, ColumnInfo>(CalcWayEnum.DoubleCycle) {
                        Answer = keys,
                        Source = columns,
                        Func_isEquals = (item, info) => {
                            return item.Key == info.Name &&
                                item.Key == info.Property.Name &&
                                item.Value == info.Explain.Text &&
                                !CheckData.IsObjectNull(info.Attribute);
                        },
                        Func_lengthNotEquals = (len_key, len_column) => {
                            Console.WriteLine("keys.Length: {0} columns.Length: {1} 不相等", len_key, len_column);
                        },
                        Func_notFind = (item) => {
                            Console.WriteLine("Key: {0} Value: {1} 没找到", item.Key, item.Value);
                        },
                    }.Calc();
                },
            };
        }

        public CaseModel Func_FieldModelParser() {
            return new CaseModel() {
                NameSign = @"字段",
                ExeEvent = () => {
                    ShineUponParser parser = new ShineUponParser(typeof(F));
                    KeyString[] keys = F.Answer_FieldInfos();

                    return new VerifyIList<YTS.Tools.Model.KeyString, ShineUponInfo>(CalcWayEnum.DoubleCycle) {
                        Answer = keys,
                        Source = new List<ShineUponInfo>(parser.GetDictionary().Values),
                        Func_isEquals = (item, info) => {
                            return item.Key == info.Name &&
                                item.Key == info.Property.Name &&
                                item.Value == info.Explain.Text;
                        },
                        Func_lengthNotEquals = (len_key, len_column) => {
                            Console.WriteLine("keys.Length: {0} columns.Length: {1} 不相等", len_key, len_column);
                        },
                        Func_notFind = (item) => {
                            Console.WriteLine("Key: {0} Value: {1} 没找到", item.Key, item.Value);
                        },
                    }.Calc();
                },
            };
        }

        public CaseModel Func_GetValue_SetValue() {
            return new CaseModel() {
                NameSign = @"获取/设置值",
                ExeEvent = () => {
                    GS answer = new GS() {
                        Age = RandomData.GetInt(),
                        DateOfBirth = RandomData.GetDateTime(),
                        Name = RandomData.GetChineseString(),
                        Price = RandomData.GetDouble(),
                        Sex = RandomData.Item(EnumInfo.GetALLItem<GS.SexEnum>()),
                    };

                    GS result = new GS();

                    ShineUponParser parser = new ShineUponParser(typeof(GS));
                    foreach (ShineUponInfo info in parser.GetDictionary().Values) {
                        KeyString ks = parser.GetValue_KeyString(info, answer);
                        parser.SetValue_Object(info, result, ks.Value);
                    }

                    return true;
                },
            };
        }
        public bool IsGSEqual(GS m1, GS m2) {
            if (m1.Age != m2.Age) {
                Console.WriteLine("不相等:  m1.Age:{0}  m2.Age:{1}", m1.Age, m2.Age);
                return false;
            }
            if (m1.DateOfBirth != m2.DateOfBirth) {
                Console.WriteLine("不相等:  m1.DateOfBirth:{0}  m2.DateOfBirth:{1}", m1.DateOfBirth, m2.DateOfBirth);
                return false;
            }
            if (m1.Name != m2.Name) {
                Console.WriteLine("不相等:  m1.Name:{0}  m2.Name:{1}", m1.Name, m2.Name);
                return false;
            }
            if (m1.Price != m2.Price) {
                Console.WriteLine("不相等:  m1.Price:{0}  m2.Price:{1}", m1.Price, m2.Price);
                return false;
            }
            if (m1.Sex != m2.Sex) {
                Console.WriteLine("不相等:  m1.Sex:{0}  m2.Sex:{1}", m1.Sex, m2.Sex);
                return false;
            }
            return true;
        }
    }
}
