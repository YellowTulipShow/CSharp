using System;
using YTS.Tools.Model;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    public interface IDAL<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        bool Insert(M model);

        bool Insert(M[] models);

        bool Delete(W where);

        bool Update(KeyObject[] kos, W where);

        M[] Select(int top, W where, KeyBoolean[] sorts);

        M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts);

        int GetRecordCount(W where);

        M GetModel(W where, KeyBoolean[] sorts);
    }
}
