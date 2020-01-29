using System;

namespace YTS.AlgorithmLogic.Global
{
    public class DbInstance
    {
        /// <summary>
        /// 获得 实例 对象
        /// </summary>
        public static DbInstance GetInstance()
        {
            return HolderClass.instance;
        }
        private static class HolderClass
        {
            public static DbInstance instance = new DbInstance();
        }

        private DbInstance()
        {
            // this.testDb = OnInitTestDb();
            Console.WriteLine("\nTest DbInstance 初始化程序步骤\n");
        }

        public readonly IFreeSql testDb;

        public IFreeSql OnInitTestDb()
        {
            string connstr = @"";
            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, connstr)
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库
                .Build(); //请务必定义成 Singleton 单例
            fsql.CodeFirst.IsNoneCommandParameter = false;
            return fsql;
        }
    }
}
