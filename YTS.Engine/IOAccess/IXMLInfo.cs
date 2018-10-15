using System;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 接口: XML 文件信息
    /// </summary>
    public interface IXMLInfo : IFileInfo
    {
        /// <summary>
        /// 获取文档根节点名称
        /// </summary>
        /// <returns>名称</returns>
        string GetRootNodeName();
    }
}
