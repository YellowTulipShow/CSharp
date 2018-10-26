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
                    GlobalSystemService Gsys = GlobalSystemService.GetInstance();
                    Console.WriteLine("Config:");
                    ShineUponParser<SystemConfig, ShineUponInfo> parser = new ShineUponParser<SystemConfig, ShineUponInfo>();
                    foreach (ShineUponInfo info in parser.GetSortResult()) {
                        KeyObject ko = parser.GetModelValue(info, Gsys.Config);
                        Console.WriteLine("info.Name: {0}  ko.Key: {1}  ko.Value: {2}", info.Name, ko.Key, ko.Value);
                    }
                    return true;
                },
            };
        }
    }
}
