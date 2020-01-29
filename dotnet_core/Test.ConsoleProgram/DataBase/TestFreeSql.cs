using System;
using YTS.Tools;
using YTS.AlgorithmLogic.Global;
using FreeSql.DataAnnotations;

namespace Test.ConsoleProgram.Base
{
    public class TestFreeSql : CaseModel
    {
        public TestFreeSql()
        {
            this.NameSign = "测试 FreeSql ORM 使用方法";
            this.ExeEvent = () =>
            {
                IFreeSql fsql = DbInstance.GetInstance().testDb;
                FreeSql.IInsert<Topic> sin = fsql.Insert(new Topic()
                {
                    Id = 0,
                    Clicks = 52,
                    CreateTime = DateTime.Now,
                    Title = "测试第一个模型",
                });
                string sql = sin.ToSql();
                return true;
            };
        }

        [Table(Name = "tb_topic")]
        class Topic
        {
            [Column(IsIdentity = true, IsPrimary = true)]
            public int Id { get; set; }
            public int Clicks { get; set; }
            public string Title { get; set; }
            public DateTime CreateTime { get; set; }
        }
    }
}
