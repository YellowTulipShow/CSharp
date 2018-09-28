using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Model.File;
using YTS.Model.File.Attribute;
using YTS.Tools;

namespace Test.ConsoleProgram.BLL
{
    public class Test_AbsFile : CaseModel
    {

        #region === Model ===
        [EntityFile]
        public class TestModel : AbsFile
        {
            public override string GetPathFolder() {
                return @"/Test_AbsFile";
            }

            public override string GetFileName() {
                return @"TestModel";
            }

            /// <summary>
            /// Int类型ID值
            /// </summary>
            [Explain(@"Int类型ID值")]
            [Field]
            public int IID { get { return _IID; } set { _IID = value; } }
            private int _IID = 0;

            /// <summary>
            /// 名称
            /// </summary>
            [Explain(@"名称")]
            [Field]
            public string Name { get { return _Name; } set { _Name = value; } }
            private string _Name = string.Empty;

            /// <summary>
            /// 发布时间
            /// </summary>
            [Explain(@"发布时间")]
            [Field]
            public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
            private DateTime _TimeRelease = DateTime.Now;

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
            [Field]
            public SexEnum Sex { get { return _sex; } set { _sex = value; } }
            private SexEnum _sex = SexEnum.Secrecy;
        }
        #endregion

        public YTS.BLL.AbsFile<TestModel, YTS.DAL.AbsFile<TestModel>> bll = null;

        public Test_AbsFile() {
            bll = new YTS.BLL.AbsFile<TestModel, YTS.DAL.AbsFile<TestModel>>();

            NameSign = @"文件DAL";
            SonCases = new CaseModel[] {
                Func_Insert(),
                Func_Delete(),
                Func_Update(),
                Func_GetRecordCount(),
                Func_Select(),
                Func_GetModel(),
                Func_Select_Pagination(),
            };
        }

        public bool TestModelIsEqual(TestModel answer, TestModel source) {
            return answer.IID == source.IID &&
                answer.Name == source.Name &&
                answer.Sex == source.Sex &&
                answer.TimeRelease == source.TimeRelease;
        }

        public CaseModel Func_Insert() {
            return new CaseModel() {
                NameSign = @"添加",
                ExeEvent = () => {
                    TestModel[] array = new TestModel[RandomData.GetInt(30, 81)];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            IID = RandomData.GetInt(7828, 546822),
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }
                    // 排序:
                    // List<TestModel> list = new List<TestModel>(array);
                    // list.Sort((one, two) => one.IID == two.IID ? 0 : one.IID < two.IID ? -1 : 1);
                    // array = list.ToArray();

                    bll.Delete(null);
                    bll.Insert(array);
                    TestModel[] query_result = bll.Select(0, model => true);
                    return this.IsIEnumerableEqual(array, query_result, func_isEquals: TestModelIsEqual);
                },
            };
        }
        public CaseModel Func_Delete() {
            return new CaseModel() {
                NameSign = @"删除",
                ExeEvent = () => {
                    TestModel[] array = new TestModel[RandomData.GetInt(30, 81)];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            IID = i,
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }
                    bll.Delete(null);
                    bll.Insert(array);

                    Func<TestModel, bool> delete_where = (model) => {
                        return model.IID % 3 == 0;
                    };

                    bll.Delete(delete_where);

                    TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                        return delete_where(model) ? null : model;
                    }, null);

                    TestModel[] query_result = bll.Select(0, model => true);
                    return this.IsIEnumerableEqual(answer_array, query_result, func_isEquals: TestModelIsEqual);
                },
            };
        }
        public CaseModel Func_Update() {
            return new CaseModel() {
                NameSign = @"更改",
                ExeEvent = () => {
                    TestModel[] array = new TestModel[RandomData.GetInt(30, 81)];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            IID = i,
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }
                    bll.Delete(null);
                    bll.Insert(array);

                    DateTime release_time = new DateTime(2111, 1, 1, 1, 1, 1, 1);
                    Func<TestModel, TestModel> update_where = (model) => {
                        TestModel copy_model = ReflexHelp.CloneProperties<TestModel>(model);
                        if (copy_model.IID % 3 == 0) {
                            copy_model.Name = string.Format("第{0}条: 3的倍数IID名称和发布时间更改", model.IID);
                            copy_model.TimeRelease = release_time;
                        }
                        return copy_model;
                    };

                    bll.Update(update_where);

                    TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                        return update_where(model);
                    });

                    TestModel[] query_result = bll.Select(0, model => true);
                    return this.IsIEnumerableEqual(answer_array, query_result, func_isEquals: TestModelIsEqual);
                },
            };
        }
        public CaseModel Func_GetRecordCount() {
            return new CaseModel() {
                NameSign = @"记录总数",
                ExeEvent = () => {
                    TestModel[] array = new TestModel[RandomData.GetInt(30, 81)];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            IID = RandomData.GetInt(7828, 546822),
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }
                    bll.Delete(null);
                    bll.Insert(array);

                    if (array.Length != bll.GetRecordCount(null)) {
                        return false;
                    }

                    Func<TestModel, bool> query_where = (model) => {
                        return model.IID % 3 == 0;
                    };
                    TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                        return query_where(model) ? null : model;
                    }, null);

                    return answer_array.Length != bll.GetRecordCount(query_where);
                },
            };
        }
        public CaseModel Func_Select() {
            return new CaseModel() {
                NameSign = @"查询",
                ExeEvent = () => {
                    TestModel[] array = new TestModel[RandomData.GetInt(30, 81)];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            IID = i,
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }
                    bll.Delete(null);
                    bll.Insert(array);

                    if (!this.IsIEnumerableEqual(array, bll.Select(0, null), func_isEquals: TestModelIsEqual)) {
                        Console.WriteLine("0条, 空条件 : 错误");
                        return false;
                    }

                    Func<TestModel, bool> query_where = (model) => {
                        return model.IID % 3 == 0;
                    };
                    TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                        return query_where(model) ? model : null;
                    }, null);
                    if (!this.IsIEnumerableEqual(answer_array, bll.Select(0, query_where), func_isEquals: TestModelIsEqual)) {
                        Console.WriteLine("0条, 有条件 : 错误");
                        return false;
                    }

                    TestModel[] top_answer_array = new TestModel[8];
                    for (int i = 0; i < top_answer_array.Length; i++) {
                        top_answer_array[i] = answer_array[i];
                    }
                    return this.IsIEnumerableEqual(top_answer_array, bll.Select(top_answer_array.Length, query_where),
                        func_isEquals: TestModelIsEqual);
                },
            };
        }
        public CaseModel Func_GetModel() {
            return new CaseModel() {
                NameSign = @"获取一个",
                ExeEvent = () => {
                    TestModel[] array = new TestModel[RandomData.GetInt(30, 81)];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            IID = i,
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }
                    bll.Delete(null);
                    bll.Insert(array);

                    TestModel result_model = bll.GetModel(null);
                    if (!TestModelIsEqual(array[0], result_model)) {
                        Console.WriteLine("空条件错误");
                        return false;
                    }

                    Func<TestModel, bool> query_where = (model) => {
                        return model.IID == array.Length - 15;
                    };
                    TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                        return query_where(model) ? model : null;
                    }, null);

                    result_model = bll.GetModel(query_where);
                    if (!TestModelIsEqual(answer_array[0], result_model)) {
                        Console.WriteLine("有条件错误");
                        return false;
                    }
                    return true;
                },
            };
        }
        public CaseModel Func_Select_Pagination() {
            return new CaseModel() {
                NameSign = @"分页查询",
                ExeEvent = () => {
                    int sum = RandomData.GetInt(300, 8001);
                    sum = 123; // 循环遍历内容太多次,百位是极限了
                    TestModel[] list = new TestModel[sum];
                    for (int i = 0; i < list.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        list[i] = new TestModel() {
                            IID = RandomData.GetInt(7828, 546822),
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        };
                    }

                    bll.Delete(null);
                    bll.Insert(list);

                    Func<int, Func<TestModel, bool>, bool> method = (page_size, where) => {
                        List<TestModel> answer_list = new List<TestModel>(ConvertTool.ListConvertType(list, model => {
                            return CheckData.IsObjectNull(where) ? model : where(model) ? model : null;
                        }, null));

                        int remainder_num = answer_list.Count % page_size;
                        int sum_page_num = answer_list.Count / page_size;
                        if (remainder_num > 0) {
                            sum_page_num++;
                        }

                        for (int page_index = 0; page_index < sum_page_num; page_index++) {
                            int record_count = 0;
                            TestModel[] result = bll.Select(page_size, page_index + 1, out record_count, where);

                            int range_len = page_size;
                            if (page_index == sum_page_num - 1 && page_size > remainder_num && remainder_num != 0) {
                                range_len = remainder_num;
                            }
                            int range_index = page_index * page_size;
                            TestModel[] answer_array = answer_list.GetRange(range_index, range_len).ToArray();

                            bool isby = IsIEnumerableEqual(answer_array, result, func_isEquals: TestModelIsEqual);
                            if (!isby) {
                                Console.WriteLine("每页{0}条 第{1}页 记录总数:{2} 出了问题!", page_size, page_index, record_count);
                                return false;
                            }
                        }

                        return true;
                    };

                    Func<TestModel, bool>[] wheres = new Func<TestModel, bool>[] {
                        null,
                        model => model.IID % 3 == 0,
                        model => model.IID % 2 == 0,
                    };

                    for (int i = 10; i <= 11; i++) {
                        foreach (Func<TestModel, bool> where in wheres) {
                            if (!method(i, where)) {
                                return false;
                            }
                        }
                    }

                    return true;
                },
            };
        }
    }
}
