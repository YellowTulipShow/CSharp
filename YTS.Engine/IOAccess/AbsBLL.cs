using System;
using YTS.Model;
using YTS.Tools;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsBLL<M, D, W> : IBLL<M, D, W>
        where M : AbsShineUpon
        where D : AbsDAL<M, W>
    {
        public D SelfDAL = null;

        public AbsBLL() {
            this.SelfDAL = ReflexHelp.CreateNewObject<D>();
        }

        public bool Insert(M model) {
            return this.SelfDAL.Insert(model);
        }

        public bool Delete(W where) {
            return this.SelfDAL.Delete(where);
        }

        public bool Update(Model.KeyObject[] kos, W where) {
            return this.SelfDAL.Update(kos, where);
        }

        public M[] Select(int top, W where, Model.KeyBoolean[] sorts) {
            return this.SelfDAL.Select(top, where, sorts);
        }

        public M[] Select(int pageCount, int pageIndex, out int recordCount, W where, Model.KeyBoolean[] sorts) {
            return this.SelfDAL.Select(pageCount, pageIndex, out recordCount, where, sorts);
        }

        public int GetRecordCount(W where) {
            return this.SelfDAL.GetRecordCount(where);
        }

        public M GetModel(W where, Model.KeyBoolean[] sorts) {
            return this.SelfDAL.GetModel(where, sorts);
        }
    }
}
