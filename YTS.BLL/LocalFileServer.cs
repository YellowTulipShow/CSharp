using System;

namespace YTS.BLL
{
    /// <summary>
    /// 本地文件存储服务-业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="D">调用的DAL类型</typeparam>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class LocalFileServer<D, M> :
        AbsBLL<D, M>,
        Model.File.IFileInfo
        where D : DAL.LocalFileServer<M>
        where M : Model.File.AbsFile
    {
        public LocalFileServer() : base() { }

        #region ====== using:IFileInfo ======
        /// <summary>
        /// 获取 /(根目录) 相对路径文件夹 格式: /xxx/xxx
        /// </summary>
        /// <returns>相对路径</returns>
        public string GetPathFolder() {
            return this.SelfDAL.GetPathFolder();
        }

        /// <summary>
        /// 获取文件名称 (只是名称, 不需要后缀)
        /// </summary>
        /// <returns>文件名</returns>
        public string GetFileName() {
            return this.SelfDAL.GetFileName();
        }
        #endregion
    }
}
