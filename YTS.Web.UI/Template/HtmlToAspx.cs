using System;
using System.IO;
using System.Text;
using YTS.Tools;

namespace YTS.Web.UI.Template
{
    /// <summary>
    /// HTML 页面内容 转 Aspx 文件输出
    /// </summary>
    public class HtmlToAspx
    {
        public Model.URLReWriter URLModel = null;

        public string AbsPathTemplet = null;
        public string AbsPathTarget = null;

        public HtmlToAspx(Model.URLReWriter urlmodel, string abspath_templet, string abspath_target) {
            this.URLModel = urlmodel;

            this.AbsPathTemplet = abspath_templet;
            this.AbsPathTarget = abspath_target;
        }

        /// <summary>
        /// 生成创建
        /// </summary>
        public void Generate() {
            if (CheckData.IsStringNull(this.AbsPathTemplet)) {
                return;
            }
            if (!File.Exists(this.AbsPathTemplet)) {
                return;
            }
            
            // 处理模板的代码, 并写入到执行页面
            SetPage(this.AbsPathTarget, GetPage(this.AbsPathTemplet));
        }

        public void SetPage(string abs_file_path, string content) {
            FileHelp.OnlyWrite(abs_file_path, content);
        }

        public string GetPage(string abs_file_path) {
            string html = FileHelp.OnlyRead(abs_file_path);
            string aspx = PageContentConvert(html);
            return aspx;
        }

        /// <summary>
        /// 页面内容转化
        /// </summary>
        /// <param name="html">需要转化的 html 页面内容</param>
        /// <returns></returns>
        public string PageContentConvert(string html) {
            if (CheckData.IsStringNull(html)) {
                return string.Empty;
            }
            StringBuilder page = new StringBuilder(html);
            return page.ToString();
        }
    }
}
