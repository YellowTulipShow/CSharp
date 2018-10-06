using System;

namespace YTS.Engine.IOAccess
{
    public interface IBLL<M, D, W> : IDAL<M, W>
        where M : IModel
        where D : IDAL<M, W>
    {
    }
}
