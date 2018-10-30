using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using YTS.Engine.DataBase;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Model.DB;
using YTS.Tools;
using YTS.Tools.Const;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.Engine
{
    public class Test_IDAL_IDAL : CaseModel
    {
        public Test_IDAL_IDAL() {
            NameSign = @"接口: IDALorIDAL";
            SonCases = new CaseModel[] {
                //Error_DateTimeType(),
                MSSQLServer(),
                LocalTXT(),
                LocalXML(),
                //ErrorReShow(),
            };
        }

        #region === show use ===
        public class Test_ShowUse_IiDALorIDAL : CaseModel
        {
            [EntityTable]
            public class TestID : AbsTable_IntID
            {
                public override string GetTableName() {
                    return @"dt_test_TestModel";
                }
            }

            [Obsolete(@"只用于展示调用方法, 并且因条件全为空, 调用会出现意外的错误", true)]
            public bool ShowUseMethod() {
                TestID model = new TestID();
                TestID[] models = new TestID[] { };
                int id = 0;
                string where = string.Empty;
                KeyObject[] kos = new KeyObject[] { };
                int top = 0;
                KeyBoolean[] kbs = new KeyBoolean[] { };
                int pcount = 0;
                int pindex = 0;
                int psum = 0;
                DataSet ds = null;
                DataRow dr = null;
                string sort = string.Empty;
                string[] sqls = new string[] { };

                // BLL
                YTS.BLL.MSSQLServer_IntID<TestID, YTS.DAL.MSSQLServer_IntID<TestID>> bll = new YTS.BLL.MSSQLServer_IntID<TestID, YTS.DAL.MSSQLServer_IntID<TestID>>();
                bll.Insert(model);
                bll.Insert(models);
                bll.Delete(where);
                bll.Update(kos, where);
                bll.Select(top, where, kbs);
                bll.Select(pcount, pindex, out psum, where, kbs);
                bll.GetRecordCount(where);
                bll.GetModel(where, kbs);
                bll.GetTableName();
                bll.QueryRecords(top, where, sort);
                bll.QueryRecords(pcount, pindex, out psum, where, sort);
                bll.DataRowToModel(dr);
                bll.DataSetToModels(ds);
                bll.IDInsert(model, out id);
                bll.IDDelete(id);
                bll.IDUpdate(kos, id);
                bll.IDGetModel(id);

                BLL_MSSQLServer<TestID, YTS.DAL.MSSQLServer_IntID<TestID>> ms_bll = bll;
                ms_bll.Insert(model);
                ms_bll.Insert(models);
                ms_bll.Delete(where);
                ms_bll.Update(kos, where);
                ms_bll.Select(top, where, kbs);
                ms_bll.Select(pcount, pindex, out psum, where, kbs);
                ms_bll.GetRecordCount(where);
                ms_bll.GetModel(where, kbs);
                ms_bll.GetTableName();
                ms_bll.QueryRecords(top, where, sort);
                ms_bll.QueryRecords(pcount, pindex, out psum, where, sort);
                ms_bll.DataRowToModel(dr);
                ms_bll.DataSetToModels(ds);

                AbsBLL<TestID, YTS.DAL.MSSQLServer_IntID<TestID>, string, ColumnModelParser<TestID>, ColumnInfo> abs_bll = ms_bll;
                abs_bll.Insert(model);
                abs_bll.Insert(models);
                abs_bll.Delete(where);
                abs_bll.Update(kos, where);
                abs_bll.Select(top, where, kbs);
                abs_bll.Select(pcount, pindex, out psum, where, kbs);
                abs_bll.GetRecordCount(where);
                abs_bll.GetModel(where, kbs);

                AbsBLL_OnlyQuery<TestID, YTS.DAL.MSSQLServer_IntID<TestID>, string, ColumnModelParser<TestID>, ColumnInfo> abs_bll_onlquery = abs_bll;
                abs_bll_onlquery.Select(top, where, kbs);
                abs_bll_onlquery.Select(pcount, pindex, out psum, where, kbs);
                abs_bll_onlquery.GetRecordCount(where);
                abs_bll_onlquery.GetModel(where, kbs);

                IBLL<TestID, YTS.DAL.MSSQLServer_IntID<TestID>, string, ColumnModelParser<TestID>, ColumnInfo> i_bll = ms_bll;
                i_bll.Insert(model);
                i_bll.Insert(models);
                i_bll.Delete(where);
                i_bll.Update(kos, where);
                i_bll.Select(top, where, kbs);
                i_bll.Select(pcount, pindex, out psum, where, kbs);
                i_bll.GetRecordCount(where);
                i_bll.GetModel(where, kbs);

                IBLL_OnlyQuery<TestID, YTS.DAL.MSSQLServer_IntID<TestID>, string, ColumnModelParser<TestID>, ColumnInfo> i_bll_onlyquery = abs_bll_onlquery;
                i_bll_onlyquery.Select(top, where, kbs);
                i_bll_onlyquery.Select(pcount, pindex, out psum, where, kbs);
                i_bll_onlyquery.GetRecordCount(where);
                i_bll_onlyquery.GetModel(where, kbs);


                // DAL
                YTS.DAL.MSSQLServer_IntID<TestID> dal = bll.SelfDAL;
                dal.Insert(model);
                dal.Insert(models);
                dal.Delete(where);
                dal.Update(kos, where);
                dal.Select(top, where, kbs);
                dal.Select(pcount, pindex, out psum, where, kbs);
                dal.GetRecordCount(where);
                dal.GetModel(where, kbs);
                dal.GetTableName();
                dal.QueryRecords(top, where, sort);
                dal.QueryRecords(pcount, pindex, out psum, where, sort);
                dal.DataRowToModel(dr);
                dal.DataSetToModels(ds);
                dal.IsNeedSupplementary();
                dal.ExecutionSupplementary();
                dal.IDInsert(model, out id);
                dal.IDDelete(id);
                dal.IDUpdate(kos, id);
                dal.IDGetModel(id);
                dal.Transaction(sqls);

                DAL_MSSQLServer<TestID> ms_dal = dal;
                ms_dal.Insert(model);
                ms_dal.Insert(models);
                ms_dal.Delete(where);
                ms_dal.Update(kos, where);
                ms_dal.Select(top, where, kbs);
                ms_dal.Select(pcount, pindex, out psum, where, kbs);
                ms_dal.GetRecordCount(where);
                ms_dal.GetModel(where, kbs);
                ms_dal.GetTableName();
                ms_dal.QueryRecords(top, where, sort);
                ms_dal.QueryRecords(pcount, pindex, out psum, where, sort);
                ms_dal.DataRowToModel(dr);
                ms_dal.DataSetToModels(ds);
                ms_dal.IsNeedSupplementary();
                ms_dal.ExecutionSupplementary();
                ms_dal.Transaction(sqls);

                AbsDAL<TestID, string, ColumnModelParser<TestID>, ColumnInfo> abs_dal = ms_dal;
                abs_dal.Insert(model);
                abs_dal.Insert(models);
                abs_dal.Delete(where);
                abs_dal.Update(kos, where);
                abs_dal.Select(top, where, kbs);
                abs_dal.Select(pcount, pindex, out psum, where, kbs);
                abs_dal.GetRecordCount(where);
                abs_dal.GetModel(where, kbs);

                AbsDAL_OnlyQuery<TestID, string, ColumnModelParser<TestID>, ColumnInfo> abs_dal_onlyquery = abs_dal;
                abs_dal_onlyquery.Select(top, where, kbs);
                abs_dal_onlyquery.Select(pcount, pindex, out psum, where, kbs);
                abs_dal_onlyquery.GetRecordCount(where);
                abs_dal_onlyquery.GetModel(where, kbs);

                IDAL<TestID, string, ColumnModelParser<TestID>, ColumnInfo> i_dal = abs_dal;
                i_dal.Insert(model);
                i_dal.Insert(models);
                i_dal.Delete(where);
                i_dal.Update(kos, where);
                i_dal.Select(top, where, kbs);
                i_dal.Select(pcount, pindex, out psum, where, kbs);
                i_dal.GetRecordCount(where);
                i_dal.GetModel(where, kbs);

                IDAL_OnlyQuery<TestID, string, ColumnModelParser<TestID>, ColumnInfo> i_dal_onlyquery = i_dal;
                i_dal_onlyquery.Select(top, where, kbs);
                i_dal_onlyquery.Select(pcount, pindex, out psum, where, kbs);
                i_dal_onlyquery.GetRecordCount(where);
                i_dal_onlyquery.GetModel(where, kbs);

                return true;
            }
        }
        #endregion

        #region === test model ===
        [EntityTable]
        public class TestModel : AbsShineUpon, ITableName, IFileInfo
        {
            #region === interface ===
            public string GetTableName() {
                return @"dt_test_TestModel";
            }

            public string GetPathFolder() {
                return @"/auto/test";
            }

            public string GetFileName() {
                return @"TestModel.file";
            }
            #endregion

            /// <summary>
            /// 记录索引
            /// </summary>
            [Explain(@"记录索引")]
            [Column(SortIndex = 1)]
            public int RecordIndex { get { return _RecordIndex; } set { _RecordIndex = value; } }
            private int _RecordIndex = 0;

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

            /// <summary>
            /// 发布
            /// </summary>
            [Explain(@"发布")]
            [ShineUponProperty]
            public SonModel Release { get { return _Release; } set { _Release = value; } }
            private SonModel _Release = new SonModel() {
                ReleaseTime = DateTime.Now,
            };
        }

        public class SonModel : AbsShineUpon
        {
            /// <summary>
            /// 发布日期
            /// </summary>
            [Explain(@"发布日期")]
            [Column]
            public DateTime ReleaseTime { get { return _ReleaseTime; } set { _ReleaseTime = value; } }
            private DateTime _ReleaseTime = DateTime.Now;
        }

        #endregion

        public CaseModel MSSQLServer() {
            Test_WhereIDAL<TestModel, string, ColumnModelParser<TestModel>, ColumnInfo> mdoel = new Test_WhereIDAL<TestModel, string, ColumnModelParser<TestModel>, ColumnInfo>() {
                NameSign = @"微软数据库",
                iDAL = new YTS.Engine.IOAccess.DAL_MSSQLServer<TestModel>(),
                w_dal = @"RecordIndex % 3 = 0",
                w_model = (model) => model.RecordIndex % 3 == 0,
                update_content_dal = new KeyObject[] {
                    new KeyObject() { Key = @"Name", Value = @"3的倍数名称" },
                },
                update_content_model = (model) => {
                    model.Name = @"3的倍数名称";
                    return model;
                },
                sorts = new KeyBoolean[] {
                    new KeyBoolean() { Key = @"RecordIndex", Value = true },
                },
            };
            return mdoel;
        }

        public CaseModel LocalTXT() {
            Func<TestModel, bool> where = (model) => model.RecordIndex % 3 == 0;
            Test_WhereIDAL<TestModel, Func<TestModel, bool>, ShineUponParser<TestModel, ShineUponInfo>, ShineUponInfo> mdoel = new Test_WhereIDAL<TestModel, Func<TestModel, bool>, ShineUponParser<TestModel, ShineUponInfo>, ShineUponInfo>() {
                NameSign = @"行文本文件",
                iDAL = (IDAL<TestModel, Func<TestModel, bool>, ShineUponParser<TestModel, ShineUponInfo>, ShineUponInfo>)new YTS.Engine.IOAccess.DAL_LocalTXT<TestModel>(),
                w_dal = where,
                w_model = where,
                update_content_dal = new KeyObject[] {
                    new KeyObject() { Key = @"Name", Value = @"3的倍数名称" },
                },
                update_content_model = (model) => {
                    model.Name = @"3的倍数名称";
                    return model;
                },
            };
            return mdoel;
        }

        public CaseModel LocalXML() {
            Func<TestModel, bool> where = (model) => model.RecordIndex % 3 == 0;
            Test_WhereIDAL<TestModel, Func<TestModel, bool>, ShineUponParser<TestModel, ShineUponInfo>, ShineUponInfo> mdoel = new Test_WhereIDAL<TestModel, Func<TestModel, bool>, ShineUponParser<TestModel, ShineUponInfo>, ShineUponInfo>() {
                NameSign = @"XML格式文件",
                iDAL = (IDAL<TestModel, Func<TestModel, bool>, ShineUponParser<TestModel, ShineUponInfo>, ShineUponInfo>)new YTS.Engine.IOAccess.DAL_LocalXML<TestModel>(),
                w_dal = where,
                w_model = where,
                update_content_dal = new KeyObject[] {
                    new KeyObject() { Key = @"Name", Value = @"3的倍数名称" },
                },
                update_content_model = (model) => {
                    model.Name = @"3的倍数名称";
                    return model;
                },
            };
            return mdoel;
        }

        public class Test_WhereIDAL<M, W, P, PI> : CaseModel
            where M : TestModel
            where W : class
            where P : ShineUponParser<M, PI>
            where PI : ShineUponInfo
        {
            public Func<TestModel, bool> w_model;
            public W w_dal;
            public Func<TestModel, TestModel> update_content_model;
            public KeyObject[] update_content_dal;
            public KeyBoolean[] sorts;
            public IDAL<M, W, P, PI> iDAL;

            public Test_WhereIDAL() {
                SonCases = new CaseModel[] {
                    Func_Insert(),
                    Func_Insert_List(),
                    Func_Update(),
                    Func_Delete(),
                    Func_GetRecordCount(),
                    Func_GetModel(),
                    Func_Select(),
                    Func_Select_Pagination(),
                };
            }

            public bool TestModelIsEqual(TestModel a, TestModel s) {
                if (a.RecordIndex != s.RecordIndex) {
                    return false;
                }
                if (a.Age != s.Age) {
                    return false;
                }
                if (a.Name != s.Name) {
                    return false;
                }
                if (a.Sex != s.Sex) {
                    return false;
                }

                string a_dt = a.DateOfBirth.ToString(Format.DATETIME_SECOND);
                string s_dt = s.DateOfBirth.ToString(Format.DATETIME_SECOND);
                if (a_dt != s_dt) {
                    Console.WriteLine("时间不一致!  a:{0}  s:{1}", a, s);
                    return false;
                }
                return true;
            }
            public void ErrorValuePrint(TestModel answer, TestModel source, string name, Func<TestModel, object> getvalue) {
                Console.WriteLine("ValueError: {0} answer={1} source={2}", name, getvalue(answer), getvalue(source));
            }

            public TestModel[] GetRandomDatas(int rewrite_sum = 0) {
                if (rewrite_sum <= 0) {
                    rewrite_sum = RandomData.GetInt(30, 81);
                }
                TestModel[] array = new TestModel[rewrite_sum];
                for (int i = 0; i < array.Length; i++) {
                    string name = string.Format("第{0}条 - ", i);
                    array[i] = new TestModel() {
                        RecordIndex = i,
                        Age = RandomData.GetInt(7828, 546822),
                        DateOfBirth = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                        Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                    };
                }
                return array;
            }

            public CaseModel Func_Insert() {
                return new CaseModel() {
                    NameSign = @"添加单条",
                    ExeEvent = () => {
                        TestModel[] array = GetRandomDatas(1);

                        iDAL.Delete(null);
                        iDAL.Insert((M)array[0]);
                        TestModel[] query_result = iDAL.Select(0, null, sorts);
                        return new VerifyIList<TestModel, TestModel>(CalcWayEnum.DoubleCycle) {
                            Answer = array,
                            Source = query_result,
                            Func_isEquals = TestModelIsEqual,
                        }.Calc();
                    },
                };
            }
            public CaseModel Func_Insert_List() {
                return new CaseModel() {
                    NameSign = @"添加多条",
                    ExeEvent = () => {
                        TestModel[] array = GetRandomDatas();
                        // 排序:
                        // List<TestModel> list = new List<TestModel>(array);
                        // list.Sort((one, two) => one.IID == two.IID ? 0 : one.IID < two.IID ? -1 : 1);
                        // array = list.ToArray();

                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);
                        TestModel[] query_result = iDAL.Select(0, null, sorts);
                        bool isby = new VerifyIList<TestModel, TestModel>(CalcWayEnum.DoubleCycle) {
                            Answer = array,
                            Source = query_result,
                            Func_isEquals = TestModelIsEqual,
                        }.Calc();
                        if (!isby) {
                            throw new Exception("错误");
                        }
                        return isby;
                    },
                };
            }
            public CaseModel Func_Delete() {
                TestModel[] array = GetRandomDatas();
                return new CaseModel() {
                    NameSign = string.Format("删除 数据总数: {0}", array.Length),
                    ExeEvent = () => {
                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);

                        double runtime = RunHelp.GetRunTime(() => {
                            iDAL.Delete(w_dal);
                        });
                        Console.WriteLine("Delete run time: {0}", runtime);

                        TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                            return w_model(model) ? null : model;
                        }, null);

                        TestModel[] query_result = iDAL.Select(0, null, sorts);
                        return new VerifyIList<TestModel, TestModel>(CalcWayEnum.Random) {
                            Answer = answer_array,
                            Source = query_result,
                            Func_isEquals = TestModelIsEqual,
                        }.Calc();
                    },
                };
            }
            public CaseModel Func_Update() {
                TestModel[] array = GetRandomDatas();
                return new CaseModel() {
                    NameSign = string.Format("更改 数据总数: {0}", array.Length),
                    ExeEvent = () => {
                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);

                        double runtime = RunHelp.GetRunTime(() => {
                            iDAL.Update(update_content_dal, w_dal);
                        });
                        Console.WriteLine("Update run time: {0}", runtime);

                        TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                            if (w_model(model)) {
                                return update_content_model(model);
                            }
                            return model;
                        }, null);

                        TestModel[] query_result = iDAL.Select(0, null, sorts);
                        return new VerifyIList<TestModel, TestModel>(CalcWayEnum.Random) {
                            Answer = answer_array,
                            Source = query_result,
                            Func_isEquals = TestModelIsEqual,
                        }.Calc();
                    },
                };
            }
            public CaseModel Func_GetRecordCount() {
                return new CaseModel() {
                    NameSign = @"记录总数",
                    ExeEvent = () => {
                        TestModel[] array = GetRandomDatas();
                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);

                        if (array.Length != iDAL.GetRecordCount(null)) {
                            return false;
                        }
                        TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                            return w_model(model) ? null : model;
                        }, null);

                        return answer_array.Length != iDAL.GetRecordCount(w_dal);
                    },
                };
            }
            public CaseModel Func_GetModel() {
                return new CaseModel() {
                    NameSign = @"获取一个",
                    ExeEvent = () => {
                        TestModel[] array = GetRandomDatas();
                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);

                        TestModel result_model = iDAL.GetModel(null, sorts);
                        if (!TestModelIsEqual(array[0], result_model)) {
                            Console.WriteLine("空条件错误");
                            return false;
                        }

                        TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                            return w_model(model) ? model : null;
                        }, null);

                        result_model = iDAL.GetModel(w_dal, sorts);
                        if (!TestModelIsEqual(answer_array[0], result_model)) {
                            Console.WriteLine("有条件错误");
                            return false;
                        }
                        return true;
                    },
                };
            }
            public CaseModel Func_Select() {
                TestModel[] array = GetRandomDatas(RandomData.GetInt(30, 81));
                return new CaseModel() {
                    NameSign = string.Format("查询 数据总数: {0}", array.Length),
                    ExeEvent = () => {
                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);

                        VerifyIList<TestModel, TestModel> verify = new VerifyIList<TestModel, TestModel>(CalcWayEnum.DoubleCycle);
                        verify.Func_isEquals = TestModelIsEqual;

                        verify.Answer = array;
                        verify.Source = iDAL.Select(0, null, sorts);
                        if (!verify.Calc()) {
                            Console.WriteLine("0条, 空条件 : 错误");
                            throw new Exception("错误");
                        }

                        TestModel[] answer_array = ConvertTool.ListConvertType<TestModel, TestModel>(array, model => {
                            return w_model(model) ? model : null;
                        }, null);
                        verify.Answer = answer_array;
                        verify.Source = iDAL.Select(0, w_dal, sorts);
                        if (!verify.Calc()) {
                            Console.WriteLine("0条, 有条件 : 错误");
                            throw new Exception("错误");
                        }

                        TestModel[] top_answer_array = new TestModel[8];
                        for (int i = 0; i < top_answer_array.Length; i++) {
                            top_answer_array[i] = answer_array[i];
                        }
                        verify.Answer = top_answer_array;
                        verify.Source = iDAL.Select(top_answer_array.Length, w_dal, sorts);
                        if (!verify.Calc()) {
                            Console.WriteLine("{0}条, 有条件 : 错误", top_answer_array.Length);
                            throw new Exception("错误");
                        }
                        return true;
                    },
                };
            }
            public CaseModel Func_Select_Pagination() {
                int sum = RandomData.GetInt(300, 8001);
                sum = RandomData.GetInt(116, 265); // 循环遍历内容太多次,百位是极限了
                TestModel[] array = GetRandomDatas(sum);
                return new CaseModel() {
                    NameSign = string.Format("分页查询 数据总数: {0}", sum),
                    ExeEvent = () => {
                        iDAL.Delete(null);
                        iDAL.Insert((M[])array);

                        Func<int, bool> method = (page_size) => {
                            TestModel[] answer_list = ConvertTool.ListConvertType(array, model => {
                                return w_model(model) ? model : null;
                            }, null);

                            for (int page_index = -2; page_index <= answer_list.Length / page_size + 3; page_index++) {
                                int record_count = 0;
                                TestModel[] result = iDAL.Select(page_size, page_index, out record_count, w_dal, sorts);
                                TestModel[] answer_array = ConvertTool.ToRangeList(answer_list, page_index, page_size);

                                bool isby = new VerifyIList<TestModel, TestModel>(CalcWayEnum.Random) {
                                    Answer = answer_array,
                                    Source = result,
                                    Func_isEquals = TestModelIsEqual,
                                    Func_lengthNotEquals = (al, sl) => {
                                        Console.WriteLine("page_size: {0}, page_index: {1}  列表长度不一样:  answer.Length: {2}, source.Length: {3}", page_size, page_index, al, sl);
                                    },
                                    Func_notFind = am => {
                                        Console.WriteLine("page_size: {0}, page_index: {1} answer model: {2}", page_size, page_index, JSON.Serializer(am));
                                    },
                                }.Calc();
                                if (!isby) {
                                    throw new Exception("错误");
                                }
                            }

                            return true;
                        };

                        for (int i = 10; i <= 11; i++) {
                            if (!method(i)) {
                                return false;
                            }
                        }

                        return true;
                    },
                };
            }
        }


        public CaseModel Error_DateTimeType() {
            return new CaseModel() {
                NameSign = @"时间类型",
                ExeEvent = () => {
                    DAL_MSSQLServer<TestModel> dal = new DAL_MSSQLServer<TestModel>();
                    TestModel[] array = new TestModel[999];
                    for (int i = 0; i < array.Length; i++) {
                        string name = string.Format("第{0}条 - ", i);
                        array[i] = new TestModel() {
                            RecordIndex = i,
                            Age = RandomData.GetInt(7828, 546822),
                            DateOfBirth = new DateTime(2018, 10, 30, 14, 42, 33, i),
                            Name = name + RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                        };
                    }
                    dal.Delete(null);
                    dal.Insert(array);
                    TestModel[] result = dal.Select(0, null, new KeyBoolean[] {
                        new KeyBoolean("RecordIndex", true),
                    });

                    VerifyIList<TestModel, TestModel> verify = new VerifyIList<TestModel, TestModel>(CalcWayEnum.SingleCycle) {
                        Answer = array,
                        Source = result,
                        Func_isEquals = (a, s) => {
                            if (a.RecordIndex != s.RecordIndex) {
                                return false;
                            }
                            if (a.Age != s.Age) {
                                return false;
                            }
                            if (a.Name != s.Name) {
                                return false;
                            }
                            if (a.Sex != s.Sex) {
                                return false;
                            }

                            string a_dt = a.DateOfBirth.ToString(Format.DATETIME_SECOND);
                            string s_dt = s.DateOfBirth.ToString(Format.DATETIME_SECOND);
                            if (a_dt != s_dt) {
                                Console.WriteLine("时间不一致!  a:{0}  s:{1}", a, s);
                                return false;
                            }
                            return true;
                        },
                    };

                    bool isby = verify.Calc();
                    if (!isby) {
                        return false;
                    }
                    return true;
                },
            };
        }
    }
}
