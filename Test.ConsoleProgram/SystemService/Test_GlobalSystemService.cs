using System;
using YTS.Engine.ShineUpon;
using YTS.Model;
using YTS.SystemService;
using YTS.Tools.Model;
using YTS.Web.UI;

namespace Test.ConsoleProgram.SystemService
{
    public class Test_GlobalSystemService : CaseModel
    {
        public Test_GlobalSystemService() {
            this.NameSign = @"全局系统服务";
            this.SonCases = new CaseModel[] {
                Func_SystemConfig(),
                Func_ConfigParser(),
            };
        }

        public CaseModel Func_SystemConfig() {
            return new CaseModel() {
                NameSign = @"系统配置",
                ExeEvent = () => {
                    SystemConfig sys_config = GlobalSystemService.GetInstance().Config.Get<SystemConfig>();
                    Console.WriteLine("Config:");
                    ShineUponParser parser = new ShineUponParser(typeof(SystemConfig));
                    foreach (ShineUponInfo info in parser.GetDictionary().Values) {
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
                    GlobalSystemService GSS = GlobalSystemService.GetInstance();
                    SystemConfig sys_config = GSS.Config.Get<SystemConfig>();
                    UploadConfig upload_config = GSS.Config.Get<UploadConfig>();
                    URLReWriterConfig urlrew_config = GSS.Config.Get<URLReWriterConfig>();

                    sys_config.Is_DeBug = true;
                    sys_config.EncryptedUseString = @"SSSSSSYTS>jfiwjfi";

                    upload_config.File_Size = 43434;
                    upload_config.Watermark_Fontsize = 16;

                    GSS.Config.SaveALLConfig();

                    SystemConfig newsys_config = GSS.Config.Get<SystemConfig>();
                    newsys_config.Load();
                    if (newsys_config.EncryptedUseString != @"SSSSSSYTS>jfiwjfi") {
                        Console.WriteLine("加密字符串错误");
                        throw new Exception("错误");
                        return false;
                    }

                    //ShineUponParser<SystemConfig, ShineUponInfo> parser = new ShineUponParser<SystemConfig, ShineUponInfo>();
                    //ShineUponInfo[] infos = parser.GetSortResult();
                    return true;
                },
            };
        }
    }
}
