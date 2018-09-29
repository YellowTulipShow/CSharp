﻿using System;
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

            // 常规测试
            list.AddRange(GetList_Normal());

            // 早8:00 - 晚7:00才能执行数据库测试
            if (8 <= DateTime.Now.Hour && DateTime.Now.Hour <= 19) {
                list.AddRange(GetList_NeedUseDataBase());
            } else {
                Console.WriteLine("[-] 家用电脑数据库不支持, 不能测试!");
            }

            // 学习测试
            list.AddRange(GetList_Learn());

            return list.ToArray();
        }
        #endregion

        /// <summary>
        /// 常规测试
        /// </summary>
        public CaseModel[] GetList_Normal() {
            return new CaseModel[] {
                new Tools.Test_ReflexHelp(),
                new Model.Test_EnumInfo(),
                new Engine.Test_AbsShineUponParser(),
                new BLL.Test_LocalFileServer(),
                new Tools.Test_PathHelp(),
                new BLL.Test_AbsFile(),
            };
        }

        /// <summary>
        /// 数据库测试
        /// </summary>
        public CaseModel[] GetList_NeedUseDataBase() {
            return new CaseModel[] { };
            return new CaseModel[] {
                new BLL.Test_MSSQLServer_StringID(),
            };
        }

        /// <summary>
        /// 学习测试
        /// </summary>
        public CaseModel[] GetList_Learn() {
            //return new CaseModel[] { };
            return new CaseModel[] {
                new Learn.Test_Linq(),
                new Learn.Test_Path(),
                new Learn.Test_FileDataOperating(),
            };
        }
    }
}
