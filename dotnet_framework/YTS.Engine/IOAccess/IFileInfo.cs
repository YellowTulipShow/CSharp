using System;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 接口: 文件名称
    /// </summary>
    public interface IFileInfo
    {
        /// <summary>
        /// 获取 /(根目录) 相对路径文件夹 格式: /xxx/xxx
        /// </summary>
        /// <returns>相对路径</returns>
        string GetPathFolder();

        /// <summary>
        /// 获取文件名称 (只是名称, 不需要后缀)
        /// </summary>
        /// <returns>文件名</returns>
        string GetFileName();
    }
}
