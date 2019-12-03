using System;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.BLL
{
    public class Test_WebSite : CaseModel
    {
        public Test_WebSite() {
            NameSign = @"Web站点";
            SonCases = new CaseModel[] {
                Func_MatchSiteName(),
            };
        }

        public CaseModel Func_MatchSiteName() {
            return new CaseModel() {
                NameSign = @"匹配站点名称",
                ExeEvent = () => {
                    KeyString[] kss = new KeyString[] {
                        new KeyString(@"/TS-Admin/index.aspx", @"Admin"),
                        new KeyString(@"/TS-Admin/", @"Admin"),
                        new KeyString(@"/TS-Admin", @"Admin"),
                        new KeyString(@"http://www.baidu.com/TS-Admin/index.aspx", @"Admin"),
                        new KeyString(@"http://www.baidu.com/TS-Admin/", @"Admin"),
                        new KeyString(@"http://www.baidu.com/TS-Admin", @"Admin"),
                        new KeyString(@"TS-Admin/index.aspx", @"Admin"),
                        new KeyString(@"TS-Admin/", @"Admin"),
                        new KeyString(@"TS-Admin", @"Admin"),
                        new KeyString(@"/Admin/index.aspx", @""),
                        new KeyString(@"/Admin/", @""),
                        new KeyString(@"/Admin", @""),
                        new KeyString(@"http://www.baidu.com/Admin/index.aspx", @""),
                        new KeyString(@"http://www.baidu.com/Admin/", @""),
                        new KeyString(@"http://www.baidu.com/Admin", @""),
                        new KeyString(@"Admin/index.aspx", @""),
                        new KeyString(@"Admin/", @""),
                        new KeyString(@"Admin", @""),
                    };
                    YTS.BLL.WebSite bllwebsite = new YTS.BLL.WebSite();
                    foreach (KeyString ks in kss) {
                        string site_name = bllwebsite.MatchSiteName(ks.Key);
                        if (site_name != ks.Value) {
                            Console.WriteLine("结果不一致! site_name: {0}  ks.Value: {1}", site_name, ks.Value);
                            throw new Exception(@"结果不一致!");
                        }
                    }
                    return true;
                },
            };
        }
    }
}
