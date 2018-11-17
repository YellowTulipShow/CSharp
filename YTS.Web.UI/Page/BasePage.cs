using System;
using YTS.Tools;

namespace YTS.Web.UI.Page
{
    /// <summary>
    /// 模板中的页面基础类文件
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        #region === Property ===
        /// <summary>
        /// 业务逻辑层: Web 站点
        /// </summary>
        public BLL.WebSite BLLWebSite {
            get {
                if (CheckData.IsObjectNull(_bll_website)) {
                    _bll_website = new BLL.WebSite();
                }
                return _bll_website;
            }
            set { _bll_website = value; }
        }
        private BLL.WebSite _bll_website = null;

        /// <summary>
        /// 业务逻辑层: URL 重写
        /// </summary>
        public BLL.URLReWriter BLLURL {
            get {
                if (CheckData.IsObjectNull(_bll_url)) {
                    _bll_url = new BLL.URLReWriter(new Model.WebSite());
                }
                return _bll_url;
            }
            set { _bll_url = value; }
        }
        private BLL.URLReWriter _bll_url = null;
        #endregion

        public BasePage() {
            //this.bllwebsite = new BLL.WebSite();
            //Model.WebSite modelwebsite = bllwebsite.GetModel(SiteName);
            //this.bllurl = new BLL.URLReWriter(modelwebsite);
            ShowPage();
        }

        public override void ProcessRequest(System.Web.HttpContext context) {
            base.ProcessRequest(context);
        }



        /// <summary>
        /// ShowPage Virtual Method
        /// </summary>
        public virtual void ShowPage() { }

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
    }
}
