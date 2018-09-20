using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Model.File;
using YTS.Model.Attribute;
using YTS.Model.File.Attribute;
using YTS.Tools;
using YTS.Model;

namespace Test.ConsoleProgram.BLL
{
    public class Test_LocalFileServer : CaseModel
    {
        public YTS.BLL.LocalFileServer<YTS.DAL.LocalFileServer<TestModel>, TestModel> bll = null;
        public readonly string ColumnName_IID = ReflexHelp.Name(() => new TestModel().IID);
        public readonly string ColumnName_Name = ReflexHelp.Name(() => new TestModel().Name);
        public readonly string ColumnName_TimeRelease = ReflexHelp.Name(() => new TestModel().TimeRelease);
        public readonly string ColumnName_Sex = ReflexHelp.Name(() => new TestModel().Sex);

        public Test_LocalFileServer() {
            NameSign = @"本地文件";
            SonCases = new CaseModel[] {
                //Func_Insert(),
                //Func_Delete(),
                //Func_Update(),
                //Func_Select(),
            };

            bll = new YTS.BLL.LocalFileServer<YTS.DAL.LocalFileServer<TestModel>, TestModel>();
        }

        #region === Model ===
        [EntityFile]
        public class TestModel : AbsFile
        {
            public override string GetPathFolder() {
                return @"/Test_File_LocalFileServer";
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

            /// <summary>
            /// 性别
            /// </summary>
            [Explain(@"性别")]
            [Field]
            public YTS.Model.Const.Enums.SexEnum Sex { get { return _sex; } set { _sex = value; } }
            private YTS.Model.Const.Enums.SexEnum _sex = YTS.Model.Const.Enums.SexEnum.Secrecy;
        }

        public TestModel GetValue_Insert() {
            return new TestModel() {
                IID = 2324,
                Name = @"润肤霜",
                Sex = YTS.Model.Const.Enums.SexEnum.Female,
                TimeRelease = new DateTime(2015, 12, 4, 6, 33, 55, 123),
            };
        }
        public TestModel GetValue_Update() {
            return new TestModel() {
                IID = 666,
                Name = @"新说法",
                Sex = YTS.Model.Const.Enums.SexEnum.Male,
                TimeRelease = new DateTime(3054, 3, 5, 4, 56, 11, 22),
            };
        }
        #endregion

        public CaseModel Func_Insert() {
            return new CaseModel() {
                NameSign = @"插入",
                ExeEvent = () => {
                    return bll.Insert(GetValue_Insert());
                },
            };
        }

        public CaseModel Func_Delete() {
            return new CaseModel() {
                NameSign = @"删除",
                ExeEvent = () => {
                    string where = string.Empty;
                    return bll.Delete(where);
                },
            };
        }

        public CaseModel Func_Update() {
            return new CaseModel() {
                NameSign = @"更新",
                ExeEvent = () => {
                    TestModel update_model = GetValue_Update();
                    string where = string.Empty;
                    return bll.Update(new KeyString[] {
                        new KeyString() { Key = ColumnName_IID, Value = update_model.IID.ToString() },
                        new KeyString() { Key = ColumnName_Name, Value = update_model.Name.ToString() },
                        new KeyString() { Key = ColumnName_Sex, Value = update_model.Sex.ToString() },
                        new KeyString() { Key = ColumnName_TimeRelease, Value = update_model.TimeRelease.ToString() },
                    }, where);
                },
            };
        }

        public CaseModel Func_Select() {
            return new CaseModel() {
                NameSign = @"选择查询",
                ExeEvent = () => {
                    string where = string.Empty;
                    return bll.Delete(where);
                },
            };
        }


        public class Group : AbsBasicDataModel
        {
        }

        public class Where : AbsBasicDataModel
        {
            public List<Expression> expression_list = new List<Expression>();
        }

        public class Expression : AbsBasicDataModel
        {
            public string key_name = string.Empty;
            public string join_symbol = string.Empty;
            public object content_value = string.Empty;
        }
    }
}
