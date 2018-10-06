using System;
using YTS.Model;
using YTS.Tools;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsDAL<M, W> : IDAL<M, W>
        where M : AbsShineUpon
    {
        public M DefaultModel = null;

        public AbsDAL() {
            this.DefaultModel = ReflexHelp.CreateNewObject<M>();
        }

        public abstract bool Insert(M model);

        public abstract bool Delete(W where);

        public abstract bool Update(Model.KeyObject[] kos, W where);

        public abstract M[] Select(int top, W where, Model.KeyBoolean[] sorts);

        public abstract M[] Select(int pageCount, int pageIndex, out int recordCount, W where, Model.KeyBoolean[] sorts);

        public abstract int GetRecordCount(W where);

        public virtual M GetModel(W where, Model.KeyBoolean[] sorts) {
            M[] list = Select(1, where, null);
            return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        }
    }
}
