using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsDAL<M, W, P, PI> : IDAL<M, W, P, PI>
        where M : AbsShineUpon
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        public M DefaultModel = null;
        public P Parser = null;

        public AbsDAL() {
            this.DefaultModel = ReflexHelp.CreateNewObject<M>();
            this.Parser = ReflexHelp.CreateNewObject<P>();
        }

        public abstract bool Insert(M model);

        public abstract bool Insert(M[] models);

        public abstract bool Delete(W where);

        public abstract bool Update(KeyObject[] kos, W where);

        public abstract M[] Select(int top, W where, KeyBoolean[] sorts = null);

        public abstract M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts);

        public abstract int GetRecordCount(W where);

        public virtual M GetModel(W where, KeyBoolean[] sorts) {
            M[] list = Select(1, where, null);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }
    }
}
