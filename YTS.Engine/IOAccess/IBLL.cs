using System;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    public interface IBLL<M, D, W, P, PI> : IDAL<M, W, P, PI>
        where M : AbsShineUpon
        where D : IDAL<M, W, P, PI>
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
    }
}
