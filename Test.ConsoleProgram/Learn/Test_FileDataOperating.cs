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
                Func_Insert(),
                Func_Delete(),
                Func_Update(),
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


        public string Get_AbsFilePath() {
            TestModel defmodel = new TestModel();
            string rel_directory = defmodel.GetPathFolder();
            string rel_filename = string.Format("{0}.ytsdb", defmodel.GetFileName());
            string abs_file_path = PathHelp.CreateUseFilePath(rel_directory, rel_filename);
            return abs_file_path;
        }

        public CaseModel Func_Insert() {
            return new CaseModel() {
                NameSign = @"插入数据",
                ExeEvent = () => {
                    List<TestModel> model_list = new List<TestModel>();
                    int record_count = RandomData.GetInt(10, 51);
                    record_count = 15;
                    for (int i = 0; i < record_count; i++) {
                        model_list.Add(new TestModel() {
                            IID = i,
                            Name = RandomData.GetChineseString(RandomData.GetInt(3, 5)),
                            Sex = RandomData.Item(EnumInfo.GetALLItem<TestModel.SexEnum>()),
                            TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                        });
                    }

                    Func<TestModel, string> get_formatprint = (model) => {
                        return JSON.SerializeObject(model);
                    };

                    List<string> lines = new List<string>();
                    foreach (TestModel model in model_list) {
                        string text = get_formatprint(model);
                        lines.Add(text);
                    }
                    string abs_file_path = Get_AbsFilePath();
                    File.Delete(abs_file_path);
                    File.Create(abs_file_path).Close();
                    WriterLine(abs_file_path, lines.ToArray());

                    Func_Select().ExeEvent();
                    return true;
                },
            };
        }

        public CaseModel Func_Delete() {
            return new CaseModel() {
                NameSign = @"删除",
                ExeEvent = () => {
                    string abs_file_path = Get_AbsFilePath();
                    string[] lines = ReaderLines(abs_file_path, line => {
                        if (CheckData.IsStringNull(line)) {
                            return null;
                        }
                        TestModel model = JSON.DeserializeToObject<TestModel>(line);
                        if (CheckData.IsObjectNull(model) || model.IID <= 5) {
                            return null;
                        }
                        return line;
                    });
                    File.Delete(abs_file_path);
                    File.Create(abs_file_path).Close();
                    WriterLine(abs_file_path, lines);

                    Func_Select().ExeEvent();
                    return true;
                },
            };
        }

        public CaseModel Func_Update() {
            return new CaseModel() {
                NameSign = @"更新",
                ExeEvent = () => {
                    string abs_file_path = Get_AbsFilePath();
                    string[] lines = ReaderLines(abs_file_path, line => {
                        if (CheckData.IsStringNull(line)) {
                            return null;
                        }
                        TestModel model = JSON.DeserializeToObject<TestModel>(line);
                        if (CheckData.IsObjectNull(model)) {
                            return null;
                        }

                        if (model.IID % 2 == 0) {
                            model.Name = @"正常名称";
                            return JSON.SerializeObject(model);
                        }

                        return line;
                    });
                    File.Delete(abs_file_path);
                    File.Create(abs_file_path).Close();
                    WriterLine(abs_file_path, lines);

                    Func_Select().ExeEvent();
                    return true;
                },
            };
        }

        public CaseModel Func_Select() {
            return new CaseModel() {
                NameSign = @"查询",
                ExeEvent = () => {
                    string abs_file_path = Get_AbsFilePath();
                    string[] lines = ReaderLines(abs_file_path);
                    foreach (string line in lines) {
                        Console.WriteLine("line: {0}", line);
                    }
                    Console.WriteLine("lines.Length: {0}", lines.Length);
                    return true;
                },
            };
        }

        /// <summary>
        /// 读取文件多行数据
        /// </summary>
        /// <param name="abs_file_path">文件绝对路径</param>
        /// <returns>多行数据</returns>
        public string[] ReaderLines(string abs_file_path) {
            return ReaderLines(abs_file_path, line => line);
        }
        /// <summary>
        /// 读取文件多行数据
        /// </summary>
        /// <param name="abs_file_path">文件绝对路径</param>
        /// <param name="line_rule">判断行规则, false 跳过此行数据</param>
        /// <returns>所有 line_rule == true 的行的集合</returns>
        public T[] ReaderLines<T>(string abs_file_path, Func<string, T> line_rule) {
            List<T> lines = new List<T>();
            using (FileStream fs = File.Open(abs_file_path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null) {
                        // 筛选符合规则的数据行
                        T value = line_rule(line);
                        if (value == null) {
                            continue;
                        }
                        lines.Add(value);
                    }
                }
            }
            return lines.ToArray();
        }

        /// <summary>
        /// 对文件写入行数据
        /// </summary>
        /// <param name="abs_file_path">文件绝对路径</param>
        /// <param name="strline">单行数据</param>
        public void WriterLine(string abs_file_path, string strline) {
            WriterLine(abs_file_path, new string[] { strline });
        }
        /// <summary>
        /// 对文件写入行数据
        /// </summary>
        /// <param name="abs_file_path">文件绝对路径</param>
        /// <param name="lines">写入的行数据集合</param>
        public void WriterLine(string abs_file_path, string[] lines) {
            using (FileStream fs = File.Open(abs_file_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)) {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
                    foreach (string line in lines) {
                        sw.WriteLine(line);
                    }
                    sw.Flush();
                }
            }
        }
    }
}
