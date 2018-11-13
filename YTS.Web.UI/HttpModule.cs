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

            // 检查请求的文件是否存在 如:存在,跳出,没必要做任何处理
            string request_absfilepath = PathHelp.ToAbsolute(request_path);
            request_absfilepath = ConvertTool.ToString(request_absfilepath);
            if (File.Exists(request_absfilepath)) {
                return;
            }

            // 执行生成执行页面内容
            string redirect_path = GenerateTemplatePage(context.Request.Url);
            if (!CheckData.IsStringNull(redirect_path)) {
                // HTTP请求重写路径
                context.RewritePath(redirect_path);
            }
        }


        private string GenerateTemplatePage(Uri uri) {
            BLL.URLReWriter bllurl = new BLL.URLReWriter();
            Model.URLReWriter urlmodel = bllurl.GetItem_RequestURI(uri);
            if (CheckData.IsObjectNull(urlmodel)) {
                string resource_file = bllurl.GetReSourceFilePath(uri);
                string abs_resource_file = PathHelp.ToAbsolute(resource_file);
                if (File.Exists(abs_resource_file)) {
                    return resource_file;
                }

                // 当得到的对象 为空的时候,证明没有此数据内容,当然也就不用生成了
                return string.Empty;
            }

            FileInfo FItemp = new FileInfo(bllurl.GetFilePath_Templet(urlmodel));
            if (!FItemp.Exists) {
                // 模板文件都不存在的话，就不用生成了
                return string.Empty;
            }

            SystemConfig sys_config = GlobalSystemService.GetInstance().Config.Get<SystemConfig>();
            FileInfo FItarget = new FileInfo(bllurl.GetFilePath_Target(urlmodel));
            if (sys_config.Is_DeBug || !FItarget.Exists || FItemp.LastWriteTime > FItarget.LastWriteTime) {
                // 生成模板
                HtmlToAspx hta = new HtmlToAspx(urlmodel, FItemp.FullName, FItarget.FullName);
                hta.Generate();
            }

            return bllurl.HTTPRedirectPath(uri, urlmodel);
        }
    }
}
