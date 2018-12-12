using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using YTS.Tools;

namespace YTS.BLL
{
    public partial class sysconfig
    {
        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.sysconfig loadConfig()
        {
            Model.sysconfig model = new Model.sysconfig();
            model.Load();
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.sysconfig saveConifg(Model.sysconfig model)
        {
            if (CheckData.IsObjectNull(model)) {
                model = loadConfig();
            }
            model.Save();
            return model;
        }
    }
}
