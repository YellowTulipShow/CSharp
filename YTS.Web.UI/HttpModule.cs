using System;
using System.IO;
using System.Web;
using YTS.Engine;
using YTS.Engine.IOAccess;
using YTS.SystemService;
using YTS.Tools;
using YTS.Web.UI.Template;

namespace YTS.Web.UI
{
    /// <summary>
    /// 客户端请求服务器执行内容模块:实现类提供模块初始化和处置事件。
    /// ClientRequest ServerFunctionModule HttpModule InitManagementEvent
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        #region Extends System.Web.IHttpModule 扩展实现接口方法
        public void Dispose() { }
        public void Init(HttpApplication context) {
            context.BeginRequest += BeginRequest;
        }
        #endregion

        /// <summary>
        /// 第一个事件执行
        /// </summary>
        private void BeginRequest(object sender, EventArgs e) {
            HttpContext context = ((HttpApplication)sender).Context;

            // 获得请求页面路径页面(含目录)
            string request_path = context.Request.Path.ToLower();

            SystemLog log = new SystemLog() {
                Position = "HttpModule.BeginRequest",
            };
            log.Message = string.Format("request_path: {0}", request_path);
            log.Message = log.Write();
            log.Write();

            // 检查请求的文件是否存在 如:存在,跳出,没必要做任何处理
            string request_absfilepath = PathHelp.ToAbsolute(request_path);
            request_absfilepath = ConvertTool.ToString(request_absfilepath);
            if (File.Exists(request_absfilepath)) {
                return;
            }

            string siteName = BLL.URLReWriter.GetURLSiteName(request_path); // 表示根目录一个
            BLL.URLReWriter bllurl = new BLL.URLReWriter(siteName);
            Model.URLReWriter urlmodel = bllurl.GetItem_RequestURI(context.Request.Url);
            // 判断是否需要生成模板文件
            if (IsNeedGenerateTemplate(urlmodel)) {
                // 生成模板
                //Template.PageTemplate.GetTemplate(urlmodel);
                HtmlToAspx hta = new HtmlToAspx(urlmodel);
                hta.Generate();
            }
        }

        /// <summary>
        /// 是否需要生成模板文件
        /// </summary>
        private bool IsNeedGenerateTemplate(Model.URLReWriter model) {
            if (CheckData.IsObjectNull(model)) {
                // 当得到的对象 为空的时候,证明没有此数据内容,当然也就不用生成了
                return false;
            }

            string tempPath = PathHelp.ToAbsolute(model.Templet);
            if (!FileHelper.IsExistFile(tempPath)) {
                // 模板文件都不存在的话，就不用生成了
                return false;
            }

            SystemConfig sys_config = GlobalSystemService.GetInstance().Config.Get<SystemConfig>();
            if (sys_config.Is_DeBug) {
                // 调试状态, 每一次访问都生成 
                return true;
            }

            string pagePath = PathHelp.ToAbsolute(model.Page);
            if (!FileHelper.IsExistFile(pagePath)) {
                // 生成的访问文件不存在，需要生成
                return true;
            }

            FileInfo tempFif = new FileInfo(tempPath);
            FileInfo pageFif = new FileInfo(pagePath);
            // 比较文件的修改时间
            return tempFif.LastWriteTime > pageFif.LastWriteTime;
        }
    }
}
