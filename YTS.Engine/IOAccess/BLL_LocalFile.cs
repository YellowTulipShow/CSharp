using System;

namespace YTS.Engine.IOAccess
{
    public class BLL_LocalFile<M, D> : AbsBLL<M, D, Func<M, bool>>
        where M : Model.File.AbsFile
        where D : DAL_LocalFile<M>
    {
        public BLL_LocalFile() : base() { }
    }
}
