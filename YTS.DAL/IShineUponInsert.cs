using System;

namespace YTS.DAL
{
    public interface IShineUponInsert<M> where M : Model.AbsShineUpon
    {
        bool Insert(M model);
        bool Insert(M[] model);
    }
}
