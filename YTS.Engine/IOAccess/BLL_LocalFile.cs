using System;
using System.IO;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 本地文件-业务逻辑层(Business Logic Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    /// <typeparam name="D">抽象-数据访问层(Data Access Layer)</typeparam>
    public class BLL_LocalFile<M, D> :
        AbsBLL<M, D, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>
        where M : AbsShineUpon, IFileInfo, new()
        where D : DAL_LocalFile<M>, new()
    {
        public BLL_LocalFile() : base() { }
        public BLL_LocalFile(FileShare fileShare)
            : base() {
            this.SelfDAL.FileShare = fileShare;
        }
    }
}
