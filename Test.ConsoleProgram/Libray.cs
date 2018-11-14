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

            list.AddRange(GetList_Learn());

            list.AddRange(GetList_Tools());

            list.AddRange(GetList_Engine());

            list.AddRange(GetList_SystemService());

            list.AddRange(GetList_BLL());

            return list.ToArray();
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        /// <summary>
        /// 学习
        /// </summary>
        public CaseModel[] GetList_Learn() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new Learn.Test_ObjectComparison(),
                new Learn.Test_Linq(),
                new Learn.Test_Path(),
                new Learn.Test_FileDataOperating(),
                new Learn.Test_XML(),
                new Learn.Test_RegularExpression(),
                new Learn.Test_URL_or_URI(),
                new Learn.Test_RatePossibility(),
            };
        }

        /// <summary>
        /// 工具
        /// </summary>
        public CaseModel[] GetList_Tools() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new Tools.Test_CheckData(),
                new Tools.Test_ConvertTool(),
                new Tools.Test_ReflexHelp(),
                new Tools.Test_EnumInfo(),
                new Tools.Test_PathHelp(),
                new Tools.Test_SerializerDeserialize(),
                new Tools.Test_UseSystemLog(),
            };
        }

        /// <summary>
        /// 引擎
        /// </summary>
        public CaseModel[] GetList_Engine() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new Engine.Test_AbsShineUponParser(),
                new Engine.Test_ini(),
                new Engine.Test_IDAL_IDAL(),
            };
        }

        /// <summary>
        /// 系统服务
        /// </summary>
        public CaseModel[] GetList_SystemService() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new SystemService.Test_GlobalSystemService(),
            };
        }

        /// <summary>
        /// 业务逻辑层
        /// </summary>
        public CaseModel[] GetList_BLL() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new BLL.Test_WebSite(),
                new BLL.Test_URLReWriter(),
            };
        }
    }
}
