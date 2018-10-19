using System;
using System.Collections.Generic;

namespace Test.ConsoleProgram
{
    public class Libray
    {
        public Libray() { }

        #region === Rule ===
        /// <summary>
        /// 在这里面手动设置要测试的实例
        /// </summary>
        public CaseModel[] GetALLCases() {
            List<CaseModel> list = new List<CaseModel>();

            list.AddRange(GetList_Learn());

            list.AddRange(GetList_Tools());

            list.AddRange(GetList_Data());

            return list.ToArray();
        }
        #endregion

        /// <summary>
        /// 学习
        /// </summary>
        public CaseModel[] GetList_Learn() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new Learn.Test_Linq(),
                new Learn.Test_Path(),
                new Learn.Test_FileDataOperating(),
                new Learn.Test_XML(),
            };
        }

        /// <summary>
        /// 工具
        /// </summary>
        public CaseModel[] GetList_Tools() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new Tools.Test_ConvertTool(),
                new Tools.Test_ReflexHelp(),
                new Tools.Test_EnumInfo(),
                new Engine.Test_AbsShineUponParser(),
                new Tools.Test_PathHelp(),
                new Tools.Test_SerializerDeserialize(),
            };
        }

        /// <summary>
        /// 数据
        /// </summary>
        public CaseModel[] GetList_Data() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new BLL.Test_ini(),
                new BLL.Test_IDAL_IDAL(),
            };
        }
    }
}
