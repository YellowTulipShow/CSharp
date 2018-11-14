using System;
using YTS.Engine.IOAccess;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.Model
{
    public class Site : AbsShineUpon, IFileInfo
    {
        public Site() { }

        public string GetPathFolder() {
            return string.Empty;
        }

        public string GetFileName() {
            return @"Site.config";
        }

        #region === Model Property ===
        /// <summary>
        /// 站点名称
        /// </summary>
        [Explain(@"站点名称")]
        [ShineUponProperty]
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = @"Main";
        #endregion
    }
}
