using System;
using System.IO;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 本地XML文件-业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="D">抽象-数据访问层(Data Access Layer)</typeparam>
    public class BLL_LocalXML<M, D> :
        AbsBLL<M, D, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo
        where D : DAL_LocalXML<M>
    {
        public BLL_LocalXML() : base() { }
        public BLL_LocalXML(FileShare fileShare)
            : base() {
            this.SelfDAL.FileShare = fileShare;
        }

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
