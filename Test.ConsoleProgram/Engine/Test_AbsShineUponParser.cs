using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Model.Attribute;

namespace Test.ConsoleProgram.Engine
{
    public class Test_AbsShineUponParser : CaseModel
    {
        public Test_AbsShineUponParser() {
            NameSign = @"映射模型解析器";
            SonCases = new CaseModel[] {
                Func_ColumnModelParser(),
                Func_FieldModelParser(),
            };
        }

        #region Test Model
        public class T : YTS.Model.Table.AbsTable
        {
            /// <summary>
            /// 创建者用户SID
            /// </summary>
            [Explain(@"创建者用户SID")]
            [YTS.Model.Table.Attribute.Column]
            public string CreateUserSID { get { return _CreateUserSID; } set { _CreateUserSID = value; } }
            private string _CreateUserSID = string.Empty;

            /// <summary>
            /// 名称
            /// </summary>
            [Explain(@"名称")]
            [YTS.Model.Table.Attribute.Column]
            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            /// <summary>
            /// 发布时间
            /// </summary>
            [YTS.Model.Table.Attribute.Column(IsShineUpon = false)]
            public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
            private DateTime _TimeRelease = DateTime.Now;

            /// <summary>
            /// 内容
            /// </summary>
            [Explain(@"内容")]
            public string Content { get { return _Content; } set { _Content = value; } }
            private string _Content = string.Empty;

            public static YTS.Model.KeyString[] Answer_ColumnInfos() {
                return new YTS.Model.KeyString[] {
                    new YTS.Model.KeyString() {
                        Key = @"CreateUserSID",
                        Value = @"创建者用户SID",
                    },
                    new YTS.Model.KeyString() {
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
            [YTS.Model.File.Attribute.Field]
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
            [YTS.Model.File.Attribute.Field(IsShineUpon = false)]
            public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
            private DateTime _TimeRelease = DateTime.Now;

            /// <summary>
            /// 描述
            /// </summary>
            [Explain(@"描述")]
            [YTS.Model.File.Attribute.Field]
            public string Description { get { return _Description; } set { _Description = value; } }
            private string _Description = string.Empty;

            /// <summary>
            /// 内容
            /// </summary>
            [Explain(@"内容")]
            public string Content { get { return _Content; } set { _Content = value; } }
            private string _Content = string.Empty;

            public static YTS.Model.KeyString[] Answer_FieldInfos() {
                return new YTS.Model.KeyString[] {
                    new YTS.Model.KeyString() {
                        Key = @"CreateUserSID",
                        Value = @"创建者用户SID",
                    },
                    new YTS.Model.KeyString() {
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
        #endregion

        public CaseModel Func_ColumnModelParser() {
            return new CaseModel() {
                NameSign = @"列",
                ExeEvent = () => {
                    YTS.Engine.DataBase.ColumnModelParser<T> parser = new YTS.Engine.DataBase.ColumnModelParser<T>();
                    YTS.Model.KeyString[] keys = T.Answer_ColumnInfos();
                    YTS.Engine.DataBase.ColumnInfo[] columns = parser.GetColumn_ALL();

                    return IsIEnumerableEqual(keys, columns,
                        func_isEquals: (item, info) => {
                            return item.Key == info.Name &&
                                item.Key == info.Property.Name &&
                                item.Value == info.Explain.Text &&
                                !YTS.Tools.CheckData.IsObjectNull(info.Attribute);
                        },
                        func_lengthNotEquals: (len_key, len_column) => {
                            Console.WriteLine("keys.Length: {0} columns.Length: {1} 不相等", len_key, len_column);
                        },
                        func_notFindPrint: (item) => {
                            Console.WriteLine("Key: {0} Value: {1} 没找到", item.Key, item.Value);
                        });
                },
            };
        }

        public CaseModel Func_FieldModelParser() {
            return new CaseModel() {
                NameSign = @"字段",
                ExeEvent = () => {
                    YTS.Engine.LocalFile.FieldModelParser<F> parser = new YTS.Engine.LocalFile.FieldModelParser<F>();
                    YTS.Model.KeyString[] keys = F.Answer_FieldInfos();
                    YTS.Engine.LocalFile.FieldInfo[] columns = parser.GetColumn_ALL();

                    return IsIEnumerableEqual(keys, columns,
                        func_isEquals: (item, info) => {
                            return item.Key == info.Name &&
                                item.Key == info.Property.Name &&
                                item.Value == info.Explain.Text;
                        },
                        func_lengthNotEquals: (len_key, len_column) => {
                            Console.WriteLine("keys.Length: {0} columns.Length: {1} 不相等", len_key, len_column);
                        },
                        func_notFindPrint: (item) => {
                            Console.WriteLine("Key: {0} Value: {1} 没找到", item.Key, item.Value);
                        });
                },
            };
        }
    }
}
