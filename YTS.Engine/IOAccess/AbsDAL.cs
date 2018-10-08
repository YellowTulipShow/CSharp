using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsDAL<M, W, P, PI> : AbsDAL_OnlyQuery<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        public AbsDAL() : base() { }

        public abstract bool Insert(M model);

        public abstract bool Insert(M[] models);

        public abstract bool Delete(W where);

        public abstract bool Update(KeyObject[] kos, W where);
    }
}
