using System;
using YTS.Tools;
using YTS.AlgorithmLogic.Global;

namespace Test.ConsoleProgram.Base
{
    public class TestDbInstance : CaseModel
    {
        public TestDbInstance()
        {
            this.NameSign = "测试全局单例数据实例!";
            this.ExeEvent = () =>
            {
                DbInstance dbInstance = DbInstance.GetInstance();
                Console.WriteLine("dbInstance.HashCode: {0}", dbInstance.GetHashCode());
                return true;
            };
        }
    }
}
