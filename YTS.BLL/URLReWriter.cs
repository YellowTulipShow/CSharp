using System;
using System.IO;
using System.Text.RegularExpressions;
using YTS.Engine.IOAccess;
using YTS.SystemService;
using YTS.Tools;

namespace YTS.BLL
{
    /// <summary>
    /// URL重写
    /// </summary>
    public class URLReWriter : BLL_LocalFile<Model.URLReWriter, DAL.URLReWriter>
    {
        public URLReWriter()
            : base() {
        }
        public URLReWriter(string sitename)
            : base() {
            this.SelfDAL.SetSiteName(sitename);
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public override DAL.URLReWriter InitCreateDAL() {
            return new DAL.URLReWriter();
        }

        public override bool IsNeedDefaultRecord() {
            return true;
        }

        public override Model.URLReWriter[] GetDefaultRecordGather() {
            return new Model.URLReWriter[] {
                new Model.URLReWriter() {
                    Name = @"index",
                    Inherit = typeof(System.Web.UI.Page).FullName,
                    Templet = @"index.html",
                    ReItems = new Model.URLReWriter.RegularQuery[] {
                        new Model.URLReWriter.RegularQuery() {
                            Pattern = @"index.html",
                            QueryParameter = string.Empty,
                        },
                    },
                },
            };
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        public static string GetURLSiteName(string url) {
            url = ConvertTool.ToString(url);
            Regex re = new Regex(@"/?TS-(\w*)/?.*");
            return re.Match(url).Groups[1].Value.ToString().Trim();
        }

        public static string SetURLSiteName(string sitename, string url) {
            sitename = ConvertTool.ToString(sitename);
            url = ConvertTool.ToString(url);
            return string.Format("/TS-{0}/{1}", sitename, url.Trim('/'));
        }

        /* ================================== ~华丽的间隔线~ ================================== */

        /// <summary>
        /// 获取选项, 根据请求URI信息, 执行过程会动态更改站点名称
        /// </summary>
        /// <param name="uri">用户的请求 uri 信息</param>
        /// <returns>获取唯一匹配的信息数据</returns>
        public Model.URLReWriter GetItem_RequestURI(Uri uri) {
            if (CheckData.IsObjectNull(uri)) {
                return null;
            }
            string site_name = GetURLSiteName(uri.AbsolutePath);
            this.SelfDAL.SetSiteName(site_name);
            Model.URLReWriter result = this.GetModel(model => {
                if (CheckData.IsSizeEmpty(model.ReItems)) {
                    return false;
                }
                Model.URLReWriter.RegularQuery requery = null;
                foreach (Model.URLReWriter.RegularQuery item in model.ReItems) {
                    if (CheckData.IsStringNull(item.Pattern)) {
                        continue;
                    }
                    if (Regex.IsMatch(uri.AbsolutePath, item.Pattern, RegexOptions.IgnoreCase)) {
                        requery = item;
                        break;
                    }
                }
                if (!CheckData.IsObjectNull(requery)) {
                    model.ReItems = new Model.URLReWriter.RegularQuery[] { requery };
                    return true;
                }
                return false;
            }, null);
            return result;
        }

        /// <summary>
        /// 获取模型数据-文件路径-模板
        /// </summary>
        /// <returns>文件绝对路径</returns>
        public string GetFilePath_Templet(Model.URLReWriter model) {
            if (CheckData.IsObjectNull(model)) {
                return string.Empty;
            }
            // 与 URL 重写的配置文件在同一文件夹下
            string directory = this.SelfDAL.GetPathFolder();
            string path = PathHelp.CreateUseFilePath(directory, model.Templet);
            return path;
        }

        /// <summary>
        /// 获取模型数据-文件路径-目标
        /// </summary>
        /// <returns>文件绝对路径</returns>
        public string GetFilePath_Target(Model.URLReWriter model) {
            if (CheckData.IsObjectNull(model)) {
                return string.Empty;
            }
            // 需要指定生成路径文件夹目录
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            string directory = this.SelfDAL.GetSiteNamePathFolder(curl.RootPage);
            string path = PathHelp.CreateUseFilePath(directory, model.Target);
            return path;
        }

        public string HTTPRedirectPath(Uri uri, Model.URLReWriter urlmodel) {
            if (CheckData.IsObjectNull(urlmodel)) {
                return string.Empty;
            }
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            string directory = this.SelfDAL.GetSiteNamePathFolder(curl.RootPage);
            Model.URLReWriter.RegularQuery rq = urlmodel.ReItems[0];
            string querystr = Regex.Replace(uri.AbsolutePath, rq.Pattern, rq.QueryParameter);
            string path = string.Format("{0}/{1}?{2}", directory, urlmodel.Target, querystr);
            return path;
        }
    }
}
