using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using CSharp.LibrayFunction;
using CSharp.SystemService;

namespace CSharp.WebPageTemp
{
    /// <summary>
    /// 客户端请求服务器执行内容模块:实现类提供模块初始化和处置事件。
    /// ClientRequest ServerFunctionModule HttpModule InitManagementEvent
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        #region Extends System.Web.IHttpModule InterfaceFunction
        public void Dispose() { }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(FunctionPageRequest);
        }
        #endregion

        #region InitFunctionCodeRegion
        private void FunctionPageRequest(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;

            // 获得请求页面路径页面(含目录)
            string str_requestPagePath = context.Request.Path.ToLower();

            // 检查请求的文件是否存在 如:存在,跳出,没必要做任何处理
            //if (FileHelper.IsExistFile(str_requestPagePath)) { return; }

            // 获取重写对象
            Template.UrlRewriteDAL urlReDAl = new Template.UrlRewriteDAL();
            Template.UrlRewriteModel urlReModel = urlReDAl.GetInfoModel_RequestPagePath(str_requestPagePath);

            // 判断是否需要生成模板文件
            if (IsNeedGenerateTemplate(urlReModel)) {
                // 生成模板
                Template.PageTemplate.GetTemplate(urlReModel);
            }
        }
        #endregion

        #region CustomFunctionCodeRegion
        /// <summary>
        /// 是否需要生成模板文件
        /// </summary>
        /// <param name="urlReModel"></param>
        /// <returns></returns>
        private bool IsNeedGenerateTemplate(Template.UrlRewriteModel urlReModel)
        {
            if (urlReModel==null) { return false; } // 当得到的对象 为空的时候,证明没有此数据内容,当然也就不用生成了

            string tempPath = HttpRuntime.AppDomainAppPath + "\\" + LibrayConfigKey.FolderName_Template + "\\" + LibrayConfigKey.FolderName_MainSite + "\\" + urlReModel.templet;
            string pagePath = HttpRuntime.AppDomainAppPath+"\\" + LibrayConfigKey.FolderName_VisitPage + "\\" + LibrayConfigKey.FolderName_MainSite + "\\" + urlReModel.page;
            
            if (!FileHelper.IsExistFile(tempPath)) // 模板文件都不存在的话，就不用生成了
                return false;
            if (!FileHelper.IsExistFile(pagePath)) // 生成的访问文件不存在，需要生成
                return true;

            if (GlobalSystemService.GetInstance().ConfigModel.IsDeBug) {
                return true; // 如果是测试阶段的话需要执行
            }

            FileInfo tempFif = new FileInfo(tempPath);
            FileInfo pageFif = new FileInfo(pagePath);
            if (tempFif.LastWriteTime > pageFif.LastWriteTime)
                return true;

            return true;
        }
        #endregion
    }
}
