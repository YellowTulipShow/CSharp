using System;
using YTS.Tools;

namespace YTS.Web.UI.Page
{
    /// <summary>
    /// 模板中的页面基础类文件
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName = null;

        public readonly BLL.URLReWriter bllurl = null;

        public BasePage() {
            this.bllurl = new BLL.URLReWriter(this.SiteName);
            ShowPage();
        }

        /// <summary>
        /// ShowPage Virtual Method
        /// </summary>
        public virtual void ShowPage() { }

        #region 页面通用方法==========================================
        /// <summary>
        /// 返回URL重写统一链接地址
        /// </summary>
        public string linkurl(string key, params object[] paramslist) {
            string returnstring = "";

            Template.UrlRewriteModel urlReModel = new Template.UrlRewriteDAL().GetInfo(key);
            if (urlReModel == null) { return ""; }

            returnstring = "/aspx/YTSTemp/" + urlReModel.page;

            return returnstring;
        }

        /// <summary>
        /// 返回一个当前日期加1000以内的随机数的字符串
        /// [主要用于引用文件的强制不进行缓存]
        /// </summary>
        /// <returns>结果</returns>
        public string GetStringDatePlusRandom() {
            DateTime now = DateTime.Now;
            return ConvertTool.CombinationContent(now.Year, now.Second, RandomData.GetInt(now.Millisecond));
        }
        #endregion
    }
}
