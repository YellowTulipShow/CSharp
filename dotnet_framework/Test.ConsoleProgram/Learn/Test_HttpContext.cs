using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Test.ConsoleProgram.Learn
{
    public class Test_HttpContext : CaseModel
    {
        public Test_HttpContext() {
            NameSign = @"学习-HTTP上下文";
            SonCases = new CaseModel[] {
                Func_MapPath(),
            };
        }
        public CaseModel Func_MapPath() {
            return new CaseModel() {
                NameSign = @"组合字符串",
                ExeEvent = () => {
                    string[] paths = new string[] {
                        "",
                        "aaaa",
                        "/bbbb",
                        "/ccc/fff",
                        "ggg/eee/",
                        "/rrrr/ttt/name.txt",
                        "qqq/www.html",
                    };
                    foreach (string path in paths) {
                        //HttpContext context = HttpContext.Current;
                        HttpRequest request = new HttpRequest("test.txt", "yts.site", "id=0");
                        TextWriter writer = null;
                        HttpResponse response = new HttpResponse(writer);
                        HttpContext context = new HttpContext(request, response);
                        HttpServerUtility server = context.Server;
                        string result = server.MapPath(path);
                        Console.WriteLine("result: {0}  path: {1}");
                    }
                    return true;
                },
            };
        }
    }
}
