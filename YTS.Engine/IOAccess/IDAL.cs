using System;
using YTS.Model;

namespace YTS.Engine.IOAccess
{
    public interface IDAL<M, W>
        where M : AbsShineUpon
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
