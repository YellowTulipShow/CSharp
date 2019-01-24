using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YTS.SystemService;
using YTS.Tools;

namespace Test.ConsoleProgram.Learn
{
    public class Test_URL_or_URI : CaseModel
    {
        public Test_URL_or_URI() {
            this.NameSign = @"URL/URI";
            this.SonCases = new CaseModel[] {
                Func_ViewAttributes(),
            };
        }

        public CaseModel Func_ViewAttributes() {
            return new CaseModel() {
                NameSign = @"查看 Uri 属性",
                ExeEvent = () => {
                    string[] urls = new string[] {
                        @"",
                        @"C:\A\B\C\D\E\F\G\H\",
                        @"C:\A\B\C\D\X\Y\Z\test.txt",
                        @"..\..\D\X\Y",
                        @"..\..\D\X\Y\Z\test.txt",
                        @"http://127.0.0.1/error_url",
                        @"https://blog.csdn.net/tigerzx/article/details/60335505",
                        @"https://blog.csdn.net:8512/tigerzx/article/details/60335505",
                        @"https://blog.csdn.net:aadwwd/tigerzx/article/details/60335505",
                        @"https://cn.bing.com/search?q=C%23+%E8%A7%A3%E6%9E%90+url+%E7%B1%BB&qs=n&form=QBLH&sp=-1&pq=c%23+%E8%A7%A3%E6%9E%90+url+%E7%B1%BB&sc=0-11&sk=&cvid=162FAA1FDF8347078C9AD231E3966A5E",
                        @"http://yellowtulipshow.site/aspx/YTSTemp/Main_Arena.aspx",
                        @"https://github.com/YellowTulipShow/gitalias",
                        @"http://192.168.1.161:5236/index.html",
                        @"http://192.168.1.161:5236",
                        @"http://127.0.0.1:2684/",
                        @"ftp://ygdy8:ygdy8@yg45.dydytt.net:3122/的士速递5.BD.720p.法语中字.mkv",
                        @"ftp://yellowtulipshow.site/aspx/YTSTemp/Main_Arena.aspx",
                        @"ftp://github.com/YellowTulipShow/gitalias",
                        @"ftp://192.168.1.161:5236/index.html",
                        @"ftp://192.168.1.161:5236",
                        @"ftp://127.0.0.1:2684/",
                        @"file:///D:/ZRQWork/JianGuoYunFolder/WorkFolder/StaticPage/SpreadPage_20180807Dev_RedTheme/index.html",
                        @"file://yellowtulipshow.site/aspx/YTSTemp/Main_Arena.aspx",
                        @"file://github.com/YellowTulipShow/gitalias",
                        @"file://192.168.1.161:5236/index.html",
                        @"file://192.168.1.161:5236",
                        @"file://127.0.0.1:2684/",
                    };

                    /*
                        文档信息正则:

                        /?/?
                        // 摘要:
                        //     (.*)
                        //
                        // 返回结果:
                        //     (.*)(
                        //
                        // 异常:
                        //   (.*)
                        //     (.*))?
                        public (.+) (\w+) { get; }
                     */

                    StringBuilder str = new StringBuilder();
                    foreach (string url in urls) {
                        Uri uri;
                        try {
                            uri = new Uri(url);
                        } catch (Exception ex) {
                            str.AppendFormat("url: {0} 无法解析! 原因: {1} \n\n\n", url, ex.Message);
                            continue;
                        }
                        str.Append(ConvertTool.ToString(new string[] {
                            string.Format("url: {0}", url),
                            string.Format("AbsolutePath: {0}", uri.AbsolutePath),
                            string.Format("AbsoluteUri: {0}", uri.AbsoluteUri),
                            string.Format("Authority: {0}", uri.Authority),
                            string.Format("DnsSafeHost: {0}", uri.DnsSafeHost),
                            string.Format("Fragment: {0}", uri.Fragment),
                            string.Format("Host: {0}", uri.Host),
                            string.Format("HostNameType: {0}", uri.HostNameType),
                            string.Format("IsAbsoluteUri: {0}", uri.IsAbsoluteUri),
                            string.Format("IsDefaultPort: {0}", uri.IsDefaultPort),
                            string.Format("IsFile: {0}", uri.IsFile),
                            string.Format("IsLoopback: {0}", uri.IsLoopback),
                            string.Format("IsUnc: {0}", uri.IsUnc),
                            string.Format("LocalPath: {0}", uri.LocalPath),
                            string.Format("OriginalString: {0}", uri.OriginalString),
                            string.Format("PathAndQuery: {0}", uri.PathAndQuery),
                            string.Format("Port: {0}", uri.Port),
                            string.Format("Query: {0}", uri.Query),
                            string.Format("Scheme: {0}", uri.Scheme),
                            string.Format("Segments: {0}", uri.Segments),
                            string.Format("UserEscaped: {0}", uri.UserEscaped),
                            string.Format("UserInfo: {0}", uri.UserInfo),
                        }, "\n\t").Trim());
                        str.Append("\n\n\n\n");
                    }

                    string abs_file_path = PathHelp.CreateUseFilePath(@"/auto/Learn/Test_URL_or_URI", @"Func_ViewAttributes.txt");
                    ClearAndWriteFile(abs_file_path, str.ToString());
                    return true;
                },
            };
        }
    }
}
