using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using YTS.Model;
using YTS.Model.Attribute;
using YTS.Model.File;
using YTS.Model.File.Attribute;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_FileDataOperating : CaseModel
    {
        public Test_FileDataOperating() {
            NameSign = @"学习-操作文件";
            SonCases = new CaseModel[] {
                //Func_Insert(),
                //Func_Delete(),
                //Func_Update(),
                //Func_Select(),
            };
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

        public CaseModel Func_Insert() {
            return new CaseModel() {
                NameSign = @"插入数据",
                ExeEvent = () => {
                    string folder_path = new TestModel().GetPathFolder();
                    string file_name = new TestModel().GetFileName();
                    List<TestModel> model_list = new List<TestModel>();
                    for (int i = 0; i < RandomData.GetInt(10, 51); i++) {
                        model_list.Add(new TestModel() {
                            IID = i,
                            Name = RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        });
                    }

                    Func<TestModel, string> get_formatprint = (model) => {
                        return JSON.SerializeObject(model);
                        //StringBuilder str = new StringBuilder();
                        //return str.ToString();
                    };
                    

                    string rel_address = string.Format("{0}/{1}.yts", folder_path, file_name);
                    string abs_address = PathHelp.ToAbsolute(rel_address);
                    
                    foreach (TestModel model in model_list) {
                        string text = get_formatprint(model);
                        File.AppendAllText(abs_address, text + "\n", Encoding.UTF8);
                    }
                    
                    return true;
                },
            };
        }


        public CaseModel Func_Delete() {
            return new CaseModel() {
                NameSign = @"删除",
                ExeEvent = () => {
                    return true;
                },
            };
        }
        public CaseModel Func_Update() {
            return new CaseModel() {
                NameSign = @"更新",
                ExeEvent = () => {
                    return true;
                },
            };
        }
        public CaseModel Func_Select() {
            return new CaseModel() {
                NameSign = @"查询",
                ExeEvent = () => {
                    return true;
                },
            };
        }
    }
}
