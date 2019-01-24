using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using YTS.Tools;

namespace YTS.Web.UI.Template
{
    /// <summary>
    /// HTML 页面内容 转 Aspx 文件输出
    /// </summary>
    public class HtmlToAspx
    {
        public HtmlToAspx() { }

        public class Params
        {
            public Params() { }
            public Model.URLReWriter URLReWriter = null;
            public FileInfo PathTemplet = null;
            public FileInfo PathTarget = null;
        }

        public Params ParamsMerge(Model.URLReWriter modelurl, FileInfo path_templet, FileInfo path_target) {
            return new Params() {
                URLReWriter = modelurl,
                PathTemplet = path_templet,
                PathTarget = path_target,
            };
        }

        /// <summary>
        /// 生成创建
        /// </summary>
        public void Generate(Params par) {
            if (CheckData.IsObjectNull(par)) {
                return;
            }
            string html = GetPage(par);
            // 处理模板的代码, 并写入到执行页面
            SetPage(par, html);
        }

        public void SetPage(Params par, string content) {
            FileHelp.OnlyWrite(par.PathTarget.FullName, content);
        }
        public string GetPage(Params par) {
            string html = FileHelp.OnlyRead(par.PathTemplet.FullName);

            string aspx = html;
            Func<string, Params, string>[] funcs = AnalysisFunctions();
            for (int i = 0; i < funcs.Length; i++) {
                Func<string, Params, string> item = funcs[i];
                if (CheckData.IsObjectNull(item)) {
                    continue;
                }
                aspx = item(aspx, par);
            }
            return aspx;
        }

        public Func<string, Params, string>[] AnalysisFunctions() {
            return new Func<string, Params, string>[] {
                AF_AspxPageStatement,
                AF_UsingOtherFileContent,
            };
        }

        /// <summary>
        /// 解析方法: 添加 Aspx 页面声明
        /// </summary>
        public string AF_AspxPageStatement(string html, Params par) {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("<%@ Page Language=\"C#\" AutoEventWireup=\"true\" Inherits=\"{0}\" %>\n", par.URLReWriter.Inherit);
            str.Append("<script type=\"text/javascript\">alert(\"hello world\")</script>\n");
            str.AppendFormat("\n{0}", html);
            return str.ToString();
        }

        /// <summary>
        /// 解析方法: 引用其他文件内容
        /// </summary>
        public string AF_UsingOtherFileContent(string html, Params par) {
            return html;
        }
    }
}
