using System;
using System.IO;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    public class BLL_LocalFile<M, D> :
        AbsBLL<M, D, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo
        where D : DAL_LocalFile<M>
    {
        public BLL_LocalFile() : base() { }
        public BLL_LocalFile(FileShare fileShare) : base() {
            this.SelfDAL.FileShare = fileShare;
        }

        #region ====== using:IFileInfo ======
        public string GetPathFolder() {
            return this.SelfDAL.GetPathFolder();
        }

        public string GetFileName() {
            return this.SelfDAL.GetFileName();
        }
        #endregion
    }
}
