using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Model.File
{
    /// <summary>
    /// 抽象-文件基础模型
    /// </summary>
    public abstract class AbsFile : AbsShineUpon, IFileInfo
    {
        /// <summary>
        /// 获取 /(根目录) 相对路径文件夹 格式: /xxx/xxx
        /// </summary>
        /// <returns>相对路径</returns>
        public abstract string GetPathFolder();

        /// <summary>
        /// 获取文件名称 (只是名称, 不需要后缀)
        /// </summary>
        /// <returns>文件名</returns>
        public abstract string GetFileName();
    }
}
