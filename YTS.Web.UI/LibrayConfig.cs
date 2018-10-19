using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Web.UI
{
    public class LibrayConfig { }

    public static class LibrayConfigKey
    {
        /// <summary>
        /// 存放模板文件的文件夹名称 位置：【/】
        /// </summary>
        public const string FolderName_Template = "templets";

        /// <summary>
        /// 存放生成用来访问文件的文件夹名称 位置：【/】
        /// </summary>
        public const string FolderName_VisitPage = "aspx";

        /// <summary>
        /// 默认主站点名称 位置：【/(模板或生成路径)/】
        /// </summary>
        public const string FolderName_MainSite = "YTSTemp";

        /// <summary>
        /// 存放项目系统配置文件的文件夹名称 位置：【/】
        /// </summary>
        public const string FolderName_Config = "xmlconfig";

        /// <summary>
        /// URL页面路径信息信息的配置文件名称 扩展名：*.config 位置：【/(配置文件路径)/*.config】
        /// </summary>
        public const string UrlRewrite_RootNodeName = "urls";
    }
    
}
