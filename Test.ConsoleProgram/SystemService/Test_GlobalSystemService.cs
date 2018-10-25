using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.SystemService;

namespace Test.ConsoleProgram.SystemService
{
    public class Test_GlobalSystemService : CaseModel
    {
        public Test_GlobalSystemService() {
            this.NameSign = @"全局系统服务";
            this.SonCases = new CaseModel[] {
                Func_SystemConfig(),
            };
        }

        public CaseModel Func_SystemConfig() {
            return new CaseModel() {
                NameSign = @"系统配置",
                ExeEvent = () => {
                    GlobalSystemService Gsys = GlobalSystemService.GetInstance();
                    //Gsys.Config;
                    Console.WriteLine(typeof(System.Collections.Generic.IList<string>).FullName);
                    return true;
                },
            };
        }
    }
}
