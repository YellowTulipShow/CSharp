using System;
using System.Collections.Generic;

namespace Test.ConsoleProgram
{
    public class Libray
    {
        public Libray() { }

        /// <summary>
        /// 在这里面手动设置要测试的实例
        /// </summary>
        public CaseModel[] GetALLCases() {
            List<CaseModel> list = new List<CaseModel>();

            list.AddRange(GetList_Normal());
            if (8 <= DateTime.Now.Hour && DateTime.Now.Hour <= 19) {
                list.AddRange(GetList_NeedUseDataBase());
            } else {
                Console.WriteLine("[-]家用电脑数据库不支持, 不能测试!");
            }

            return list.ToArray();
        }

        public CaseModel[] GetList_Normal() {
            return new CaseModel[] {
                new Tools.Test_ReflexHelp(),
                new Model.Test_EnumInfo(),
                new Engine.Test_AbsShineUponParser(),
            };
        }
        public CaseModel[] GetList_NeedUseDataBase() {
            return new CaseModel[] {
                new BLL.Test_MSSQLServer_StringID(),
            };
        }
    }
}
