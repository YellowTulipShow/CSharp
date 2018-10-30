using System;
using YTS.Engine.ShineUpon;
using YTS.SystemService;
using YTS.Tools.Model;

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
                    SystemConfig sys_config = GlobalSystemService.GetInstance().Config.Get<SystemConfig>();
                    Console.WriteLine("Config:");
                    ShineUponParser<SystemConfig, ShineUponInfo> parser = new ShineUponParser<SystemConfig, ShineUponInfo>();
                    foreach (ShineUponInfo info in parser.GetSortResult()) {
                        KeyObject ko = parser.GetValue_KeyObject(info, sys_config);
                        Console.WriteLine("info.Name: {0}  ko.Key: {1}  ko.Value: {2}", info.Name, ko.Key, ko.Value);
                    }
                    return true;
                },
            };
        }

        public CaseModel Func_ConfigParser() {
            return new CaseModel() {
                NameSign = @"配置分析器",
                ExeEvent = () => {
                    return true;
                },
            };
        }
    }
}
