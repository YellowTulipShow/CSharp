using System;
using System.IO;
using System.Text.RegularExpressions;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.BLL
{
    /// <summary>
    /// 业务逻辑: Web网站站点
    /// </summary>
    public class WebSite : BLL_LocalFile<Model.WebSite, DAL_LocalXML<Model.WebSite>>
    {
        public WebSite() : base() { }

        public override bool IsNeedDefaultRecord() {
            return true;
        }

        public override Model.WebSite[] GetDefaultRecordGather() {
            return new Model.WebSite[] {
                new Model.WebSite() {
                    Name = string.Empty,
                    Domain = @"127.0.0.1",
                },
                new Model.WebSite() {
                    Name = @"Admin",
                    Domain = @"127.0.0.1",
                    Tel = @"18563920971",
                    Mail = @"1426689530@qq.com",
                },
            };
        }

        /// <summary>
        /// 匹配 URL Web 站点信息
        /// </summary>
        /// <param name="url_absolute_path">web绝对链接</param>
        /// <returns>匹配内容</returns>
        public Match MatchWebSiteInfo(string url_absolute_path) {
            string path = ConvertTool.ToTrim(url_absolute_path);
            Regex re = new Regex(@"/?TS-(\w*)/?(.*)", RegexOptions.IgnoreCase);
            Match match = re.Match(path);
            return match;
        }

        /// <summary>
        /// 匹配站点名称
        /// </summary>
        /// <param name="url_absolute_path">web绝对链接</param>
        /// <returns>站点名称</returns>
        public string MatchSiteName(string url_absolute_path) {
            Match match = MatchWebSiteInfo(url_absolute_path);
            return ConvertTool.ToTrim(match.Groups[1].Value);
        }

        /// <summary>
        /// 匹配页面路径
        /// </summary>
        /// <param name="url_absolute_path">web绝对链接</param>
        /// <returns>页面路径</returns>
        public string MatchPagePath(string url_absolute_path) {
            Match match = MatchWebSiteInfo(url_absolute_path);
            return ConvertTool.ToTrim(match.Groups[2].Value);
        }

        /// <summary>
        /// 根据 站点名称 获取站点信息
        /// </summary>
        /// <param name="site_name"></param>
        /// <returns></returns>
        public Model.WebSite GetModel(string site_name) {
            if (CheckData.IsObjectNull(site_name)) {
                return null;
            }
            return GetModel(model => {
                return model.Name == site_name;
            }, null);
        }
    }
}
