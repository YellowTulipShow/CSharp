using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Engine.Template
{
    /// <summary>
    /// HTML 页面内容 转 Aspx 文件输出
    /// </summary>
    public class HtmlToAspx
    {
        /// <summary>
        /// 绝对文件路径_Html
        /// </summary>
        public readonly string AbsFilePath_Html = string.Empty;
        /// <summary>
        /// 绝对文件路径_Apsx
        /// </summary>
        public readonly string AbsFilePath_Apsx = string.Empty;

        public HtmlToAspx(string afp_html, string afp_aspx) {
            this.AbsFilePath_Html = afp_html;
            this.AbsFilePath_Apsx = afp_aspx;
        }

        /// <summary>
        /// 生成创建
        /// </summary>
        public void Generate() {
        }
    }
}
