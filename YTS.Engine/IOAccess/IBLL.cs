using System;
using YTS.Model;

namespace YTS.Engine.IOAccess
{
    public interface IBLL<M, D, W> : IDAL<M, W>
        where M : AbsShineUpon
        where D : IDAL<M, W>
    {
    }
}
