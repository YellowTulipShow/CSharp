using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using CSharp.LibrayFunction;

namespace CSharp.WebPageTemp.Page
{
    /// <summary>
    /// 模板中的页面基础类文件
    /// </summary>
    public partial class BasePage : System.Web.UI.Page
    {
        #region ****************PageInitFunction****************
        public BasePage()
        {
            ShowPage();
        }

        /// <summary>
        /// ShowPage Virtual Method
        /// </summary>
        protected virtual void ShowPage() { }
        #endregion
        /*===================================================================================*/
        #region 页面通用方法==========================================
        /// <summary>
        /// 返回URL重写统一链接地址
        /// </summary>
        public string linkurl(string _key, params object[] _params)
        {
            string returnstring = "";

            Template.UrlRewriteModel urlReModel = new Template.UrlRewriteDAL().GetInfo(_key);
            if (urlReModel==null) { return ""; }

            returnstring = "/" + LibrayConfigKey.FolderName_VisitPage + "/" + LibrayConfigKey.FolderName_MainSite + "/" + urlReModel.page;

            return returnstring;
        }
        /// <summary>
        /// 返回一个当前日期加1000以内的随机数的字符串
        /// [主要用于引用文件的强制不进行缓存]
        /// </summary>
        /// <returns>结果</returns>
        public string GetStringDatePlusRandom()
        {
            return ConvertTool.CombinationContent(DateTime.Now.Year, DateTime.Now.Second, new Random().Next(DateTime.Now.Millisecond));
        }
        #endregion
    }
}
