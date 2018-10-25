﻿using System;
using System.Text.RegularExpressions;
using YTS.Engine.IOAccess;
using YTS.Tools;

namespace YTS.BLL
{
    public class URLReWriter : BLL_LocalXML<Model.URLReWriter, DAL.URLReWriter>
    {
        public readonly string SiteName = string.Empty;

        public URLReWriter(string sitename)
            : base() {
            this.SiteName = ConvertTool.ObjToString(sitename);
        }

        public override DAL.URLReWriter InitCreateDAL() {
            return new DAL.URLReWriter(this.SiteName);
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

        public static string GetURLSiteName(string url) {
            url = ConvertTool.ObjToString(url);
            Regex re = new Regex(@"/(\w*)/?.*");
            return re.Match(url).Groups[0].Value.ToString().Trim();
        }
    }
}
