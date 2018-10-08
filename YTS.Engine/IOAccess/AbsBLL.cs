using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsBLL<M, D, W, P, PI> : AbsBLL_OnlyQuery<M, D, W, P, PI>
        where M : AbsShineUpon
        where D : AbsDAL<M, W, P, PI>
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        public AbsBLL() : base() { }

        public bool Insert(M model) {
            return this.SelfDAL.Insert(model);
        }

        public bool Insert(M[] models) {
            return this.SelfDAL.Insert(models);
        }

        public bool Delete(W where) {
            return this.SelfDAL.Delete(where);
        }

        public bool Update(KeyObject[] kos, W where) {
            return this.SelfDAL.Update(kos, where);
        }
    }
}
