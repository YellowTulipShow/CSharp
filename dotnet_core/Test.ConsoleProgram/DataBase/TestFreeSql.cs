using System;
using FreeSql.DataAnnotations;
using YTS.Tools;

namespace Test.ConsoleProgram.Base
{
    public class TestFreeSql : CaseModel
    {
        public TestFreeSql()
        {
            this.NameSign = "测试 FreeSql ORM 使用方法";
            this.ExeEvent = () =>
            {
                string connstr = @"";
                IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.Sqlite, connstr)
                    .UseAutoSyncStructure(true) //自动同步实体结构到数据库
                    .Build(); //请务必定义成 Singleton 单例
                fsql.CodeFirst.IsNoneCommandParameter = false;

                FreeSql.IInsert<Topic> sin = fsql.Insert(new Topic() {
                });
                string sql = sin.ToSql();
                return true;
            };
        }

        [Table(Name = "tb_topic")]
        class Topic {
            [Column(IsIdentity = true, IsPrimary = true)]
            public int Id { get; set; }
            public int Clicks { get; set; }
            public string Title { get; set; }
            public DateTime CreateTime { get; set; }
        }
    }
}
