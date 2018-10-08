using System;
using YTS.Engine.ShineUpon;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public interface IDAL_OnlyQuery<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        M[] Select(int top, W where, KeyBoolean[] sorts);

        M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts);

        int GetRecordCount(W where);

        M GetModel(W where, KeyBoolean[] sorts);
    }
}
