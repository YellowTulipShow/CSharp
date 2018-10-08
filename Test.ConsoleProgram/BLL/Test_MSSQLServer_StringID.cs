using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using YTS.Engine.IOAccess;
using YTS.Model.DB;
using YTS.Tools;

namespace Test.ConsoleProgram.BLL
{
    public class Test_MSSQLServer_StringID : CaseModel
    {
        public Test_MSSQLServer_StringID() {
            this.NameSign = @"测试 Microsoft SQL Server 2008 业务逻辑层 数据访问器 字符串类型ID标识";
            this.SonCases = new CaseModel[] {
                Func_IDInsert(),
            };
        }

        /// <summary>
        /// 数据模型类: 文章
        /// </summary>
        [Explain(@"文章")]
        [EntityTable]
        public class TestArticle : AbsTable_StringID
        {
            public override YTS.Model.Const.Enums.UseCodeMark GetUseCode() {
                return YTS.Model.Const.Enums.UseCodeMark.Article;
            }

            public override string GetTableName() {
                return @"dt_Test_Article";
            }

            #region === Model ===
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
            /// 内容
            /// </summary>
            [Explain(@"内容")]
            [Column]
            public string Content { get { return _Content; } set { _Content = value; } }
            private string _Content = string.Empty;

            /// <summary>
            /// 发布时间
            /// </summary>
            [Explain(@"发布时间")]
            [Column]
            public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
            private DateTime _TimeRelease = DateTime.Now;
            #endregion
        }

        public CaseModel Func_IDInsert() {
            return new CaseModel() {
                NameSign = @"与ID相关的 插入方法",
                ExeEvent = () => {
                    BLL_MSSQLServer_StringID<DAL_MSSQLServer_StringID<TestArticle>, TestArticle> bll = new BLL_MSSQLServer_StringID<DAL_MSSQLServer_StringID<TestArticle>, TestArticle>();

                    TestArticle model = new TestArticle() {
                        Content = RandomData.GetChineseString(600),
                        CreateUserSID = string.Empty,
                        Name = RandomData.GetChineseString(10),
                        Remark = RandomData.GetString(CommonData.ASCII_WordText()),
                        TimeAdd = DateTime.Now,
                        TimeRelease = RandomData.GetDateTime(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value),
                    };
                    string sid = string.Empty;
                    if (!bll.IDInsert(model, out sid)) {
                        Console.WriteLine("插入数据失败!");
                        return false;
                    }
                    model.SID = sid;

                    TestArticle r_model = bll.IDGetModel(model.SID);
                    if (CheckData.IsObjectNull(r_model)) {
                        Console.WriteLine("查询数据为空!");
                        return false;
                    }
                    if (!r_model.SID.Equals(model.SID)) {
                        Console.WriteLine("SID 错误!");
                        return false;
                    }
                    if (!r_model.CreateUserSID.Equals(model.CreateUserSID)) {
                        Console.WriteLine("创建用户 错误!");
                        return false;
                    }
                    if (!r_model.Content.Equals(model.Content)) {
                        Console.WriteLine("内容 错误!");
                        return false;
                    }
                    if (!r_model.Name.Equals(model.Name)) {
                        Console.WriteLine("名称 错误!");
                        return false;
                    }
                    const string TF = YTS.Model.Const.Format.DATETIME_SECOND;
                    if (r_model.TimeRelease.ToString(TF) != model.TimeRelease.ToString(TF)) {
                        Console.WriteLine("发布时间 错误!");
                        return false;
                    }
                    if (r_model.TimeAdd.ToString(TF) != model.TimeAdd.ToString(TF)) {
                        Console.WriteLine("添加时间 错误!");
                        return false;
                    }
                    return true;
                },
            };
        }
    }
}
