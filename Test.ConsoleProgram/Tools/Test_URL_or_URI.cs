using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.LibrayFunction;
using CSharp.SystemService;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_URL_or_URI : CaseModel
    {
        public Test_URL_or_URI() {
            this.NameSign = @"学习开发 URL URI 解析";
            this.ExeEvent = Method;
        }

        public void Method() {
            string[] urls = new string[] {
                @"",
                @"http://127.0.0.1/error_url",
                @"https://blog.csdn.net/tigerzx/article/details/60335505",
                @"https://blog.csdn.net:8512/tigerzx/article/details/60335505",
                @"https://blog.csdn.net:aadwwd/tigerzx/article/details/60335505",
                //@"https://cn.bing.com/search?q=C%23+%E8%A7%A3%E6%9E%90+url+%E7%B1%BB&qs=n&form=QBLH&sp=-1&pq=c%23+%E8%A7%A3%E6%9E%90+url+%E7%B1%BB&sc=0-11&sk=&cvid=162FAA1FDF8347078C9AD231E3966A5E",
                //@"http://yellowtulipshow.site/aspx/YTSTemp/Main_Arena.aspx",
                //@"https://github.com/YellowTulipShow/gitalias",
                //@"http://192.168.1.161:5236/index.html",
                //@"http://192.168.1.161:5236",
                //@"http://127.0.0.1:2684/",
                //@"file:///D:/ZRQWork/JianGuoYunFolder/WorkFolder/StaticPage/SpreadPage_20180807Dev_RedTheme/index.html",
                //@"ftp://ygdy8:ygdy8@yg45.dydytt.net:3122/阳光电影www.ygdy8.com.的士速递5.BD.720p.法语中字.mkv",
            };

            foreach (string item in urls) {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("\n\nURL: {0}", item);
                try {
                    Uri uri = new Uri(item);
                    str.AppendFormat("\n\tAbsolutePath: {0}", uri.AbsolutePath);
                    str.AppendFormat("\n\tAbsoluteUri: {0}", uri.AbsoluteUri);
                    str.AppendFormat("\n\tAuthority: {0}", uri.Authority);
                    str.AppendFormat("\n\tDnsSafeHost: {0}", uri.DnsSafeHost);
                    str.AppendFormat("\n\tFragment: {0}", uri.Fragment);
                    str.AppendFormat("\n\tHost: {0}", uri.Host);
                    str.AppendFormat("\n\tHostNameType: {0}", uri.HostNameType);
                    str.AppendFormat("\n\tIsAbsoluteUri: {0}", uri.IsAbsoluteUri);
                    str.AppendFormat("\n\tIsDefaultPort: {0}", uri.IsDefaultPort);
                    str.AppendFormat("\n\tIsFile: {0}", uri.IsFile);
                    str.AppendFormat("\n\tIsLoopback: {0}", uri.IsLoopback);
                    str.AppendFormat("\n\tIsUnc: {0}", uri.IsUnc);
                    str.AppendFormat("\n\tLocalPath: {0}", uri.LocalPath);
                    str.AppendFormat("\n\tOriginalString: {0}", uri.OriginalString);
                    str.AppendFormat("\n\tPathAndQuery: {0}", uri.PathAndQuery);
                    str.AppendFormat("\n\tPort: {0}", uri.Port);
                    str.AppendFormat("\n\tQuery: {0}", uri.Query);
                    str.AppendFormat("\n\tScheme: {0}", uri.Scheme);
                    str.AppendFormat("\n\tSegments: {0}", uri.Segments);
                    str.AppendFormat("\n\tUserEscaped: {0}", uri.UserEscaped);
                    str.AppendFormat("\n\tUserInfo: {0}", uri.UserInfo);
                } catch (Exception ex) {
                    str.AppendFormat("\n\tError Msg: {0}", ex.Message);
                }
                str.AppendFormat("\n\r\n\r\n\r\n ");

                SystemLog.Write(@"Test_URL_or_URI: ", str.ToString());
                Print.WriteLine(str.ToString());
            }
        }
    }
}
