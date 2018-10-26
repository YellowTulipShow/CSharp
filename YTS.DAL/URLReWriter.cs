﻿using System;
using YTS.SystemService;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.DAL
{
    public class URLReWriter : DAL_LocalXML<Model.URLReWriter>
    {
        public readonly string SiteName = string.Empty;

        public URLReWriter(string sitename)
            : base() {
            this.SiteName = ConvertTool.ObjToString(sitename);
        }

        public void ReSetSiteName(string sitename) {
        }

        public override string GetPathFolder() {
            GlobalSystemService Gsys = GlobalSystemService.GetInstance();
            return string.Format("/{0}/{1}", Gsys.Config.Path_Template, this.SiteName.ToString().Trim('/'));
        }
    }
}
