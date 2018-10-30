using System;
using System.IO;
using System.Text.RegularExpressions;
using YTS.Engine.IOAccess;
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
                    Type = @"index",
                    Inherit = typeof(System.Web.UI.Page).FullName,
                    Page = @"index.aspx",
                    Templet = @"index.html",
                    Items = new Model.URLReWriter.Item[] {
                        new Model.URLReWriter.Item() {
                            Path = @"index.aspx",
                            Pattern = @"index.aspx",
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

        public Model.URLReWriter GetItem_RequestURI(Uri uri) {
            if (CheckData.IsObjectNull(uri)) {
                return null;
            }
            string site_name = GetURLSiteName(uri.AbsolutePath);

            return null;
        }
    }
}
