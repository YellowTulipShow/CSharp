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
        public URLReWriter(Model.WebSite modelsite) {
            this.SelfDAL.SetSiteName(modelsite);
        }

        /* ================================== ~华丽的间隔线~ ================================== */

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

        /// <summary>
        /// 获取URL重写信息, 根据请求URI信息
        /// </summary>
        /// <param name="uri">用户的请求 uri 信息</param>
        /// <returns>获取唯一匹配的信息数据</returns>
        public Model.URLReWriter GetItem_RequestURI(string url_absolute_path) {
            if (CheckData.IsStringNull(url_absolute_path)) {
                return null;
            }
            Model.URLReWriter result = this.GetModel(model => {
                if (CheckData.IsSizeEmpty(model.ReItems)) {
                    return false;
                }
                Model.URLReWriter.RegularQuery requery = null;
                foreach (Model.URLReWriter.RegularQuery item in model.ReItems) {
                    if (CheckData.IsStringNull(item.Pattern)) {
                        continue;
                    }
                    if (Regex.IsMatch(url_absolute_path, item.Pattern, RegexOptions.IgnoreCase)) {
                        requery = item;
                        break;
                    }
                }
                if (!CheckData.IsObjectNull(requery)) {
                    model.ReItems = new Model.URLReWriter.RegularQuery[] {
                        requery
                    };
                    return true;
                }
                return false;
            }, null);
            return result;
        }

        /// <summary>
        /// 获取资源文件地址
        /// </summary>
        /// <param name="url_absolute_path">url绝对地址路径</param>
        /// <param name="bllwebsite">业务逻辑层: Web站点</param>
        /// <returns>得到的资源文件地址</returns>
        public string GetResourceFilePath(string url_absolute_path, BLL.WebSite bllwebsite) {
            string page = bllwebsite.MatchPagePath(url_absolute_path);
            return string.Format("/{0}/{1}", this.SelfDAL.GetPathFolder(), page);
        }

        /// <summary>
        /// 获取模型数据-文件路径-模板
        /// </summary>
        /// <returns>文件绝对路径</returns>
        public string GetFilePath_Templet(Model.URLReWriter model) {
            if (CheckData.IsObjectNull(model)) {
                return string.Empty;
            }
            model.Templet = ConvertTool.ToString(model.Templet);
            string directory = null;
            if (Regex.IsMatch(model.Templet, @"^/.*")) {
                // 留有从根目录来的绝对路径值
                directory = this.SelfDAL.GetRootTemplatePathFolder();
            } else {
                // 与 URL 重写的配置文件在同一文件夹下
                directory = this.SelfDAL.GetPathFolder();
            }
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

        /// <summary>
        /// HTTP 重定向路径
        /// </summary>
        /// <param name="uri">需要的原始路径各部分信息</param>
        /// <param name="urlmodel">需要的 URL 重定向模型</param>
        /// <returns>最终生成的重定向路径</returns>
        public string HTTPRedirectPath(Uri uri, Model.URLReWriter urlmodel) {
            if (CheckData.IsObjectNull(urlmodel)) {
                return string.Empty;
            }
            Model.URLReWriterConfig curl = GlobalSystemService.GetInstance().Config.Get<Model.URLReWriterConfig>();
            string directory = this.SelfDAL.GetSiteNamePathFolder(curl.RootPage);
            Model.URLReWriter.RegularQuery rq = urlmodel.ReItems[0];
            if (CheckData.IsObjectNull(uri)) {
                return string.Format("{0}/{1}", directory, urlmodel.Target);
            }
            string querystr = Regex.Replace(uri.AbsolutePath, rq.Pattern, rq.QueryParameter);
            if (!CheckData.IsStringNull(uri.Query)) {
                querystr = string.Format("{0}&{1}", querystr, uri.Query.TrimStart('?'));
            }
            string path = string.Format("{0}/{1}?{2}", directory, urlmodel.Target, querystr);
            return path;
        }
    }
}
