using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.Tools.Model;

namespace Test.ConsoleProgram.BLL
{
    public class Test_URLReWriter : CaseModel
    {
        public Test_URLReWriter() {
            this.NameSign = @"URL重写";
            this.SonCases = new CaseModel[] {
                Func_GetURLSiteName(),
            };
        }

        public CaseModel Func_GetURLSiteName() {
            return new CaseModel() {
                NameSign = @"获取 url 站点名",
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

                    foreach (KeyString ks in kss) {
                        string site_name = YTS.BLL.URLReWriter.GetURLSiteName(ks.Key);
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
