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
            if (request_path == @"/") {
                context.Response.Redirect(@"index.aspx");
                return;
            }

            // 检查请求的文件是否存在 如:存在,跳出,没必要做任何处理
            string request_absfilepath = PathHelp.ToAbsolute(request_path);
            request_absfilepath = ConvertTool.ToString(request_absfilepath);
            if (File.Exists(request_absfilepath)) {
                return;
            }

            // 执行生成执行页面内容
            string redirect_path = ReWriteURLPath(context.Request.Url);
            if (!CheckData.IsStringNull(redirect_path)) {
                SystemLog log = new SystemLog() {
                    Type = SystemLog.LogType.Daily,
                    Position = @"YTS.Web.UI.HttpModule.BeginRequest",
                    Message = string.Format("new redirect path: {0}", redirect_path),
                };
                log.Write();

                // HTTP请求重写路径
                context.RewritePath(redirect_path);
            }
        }

        /// <summary>
        /// 重写 URL 路径
        /// </summary>
        /// <param name="uri">用户原始请求信息</param>
        /// <returns>重定向路径, 如果为空则不处理</returns>
        private string ReWriteURLPath(Uri uri) {
            BLL.WebSite bllsite = new BLL.WebSite();
            string site_name = bllsite.MatchSiteName(uri.AbsolutePath);
            Model.WebSite modelsite = bllsite.GetModel(site_name);

            BLL.URLReWriter bllurl = new BLL.URLReWriter(modelsite);
            Model.URLReWriter modelurl = bllurl.GetItem_RequestURI(uri.AbsolutePath);

            if (CheckData.IsObjectNull(modelurl)) {
                string resource_file = bllurl.GetResourceFilePath(uri.AbsolutePath, bllsite);
                string abs_resource_file = PathHelp.ToAbsolute(resource_file);
                if (File.Exists(abs_resource_file)) {
                    return resource_file;
                }

                // 当得到的对象 为空的时候,证明没有此数据内容,当然也就不用生成了
                return string.Empty;
            }

            FileInfo FItemp = new FileInfo(bllurl.GetFilePath_Templet(modelurl));
            if (!FItemp.Exists) {
                // 模板文件都不存在的话，就不用生成了
                return string.Empty;
            }

            SystemConfig sys_config = GlobalSystemService.GetInstance().Config.Get<SystemConfig>();
            FileInfo FItarget = new FileInfo(bllurl.GetFilePath_Target(modelurl));
            if (sys_config.Is_DeBug || !FItarget.Exists || FItemp.LastWriteTime > FItarget.LastWriteTime) {
                // 生成模板
                HtmlToAspx hta = new HtmlToAspx(modelurl, FItemp.FullName, FItarget.FullName);
                hta.Generate();
            }

            return bllurl.HTTPRedirectPath(uri, modelurl);
        }
    }
}
