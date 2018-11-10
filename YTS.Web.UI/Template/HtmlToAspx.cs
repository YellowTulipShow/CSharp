using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
    }
}
