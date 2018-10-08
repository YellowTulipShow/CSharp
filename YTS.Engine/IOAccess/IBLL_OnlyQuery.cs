using System;
using YTS.Engine.ShineUpon;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public interface IBLL_OnlyQuery<M, D, W, P, PI> : IDAL_OnlyQuery<M, W, P, PI>
        where M : AbsShineUpon
        where D : IDAL_OnlyQuery<M, W, P, PI>
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
    }
}
