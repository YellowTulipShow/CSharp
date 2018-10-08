using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsBLL<M, D, W, P, PI> : IBLL<M, D, W, P, PI>
        where M : AbsShineUpon
        where D : AbsDAL<M, W, P, PI>
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        public D SelfDAL = null;

        public AbsBLL() {
            this.SelfDAL = ReflexHelp.CreateNewObject<D>();
        }

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

        public M[] Select(int top, W where, KeyBoolean[] sorts) {
            return this.SelfDAL.Select(top, where, sorts);
        }

        public M[] Select(int pageCount, int pageIndex, out int recordCount, W where, KeyBoolean[] sorts) {
            return this.SelfDAL.Select(pageCount, pageIndex, out recordCount, where, sorts);
        }

        public int GetRecordCount(W where) {
            return this.SelfDAL.GetRecordCount(where);
        }

        public M GetModel(W where, KeyBoolean[] sorts) {
            return this.SelfDAL.GetModel(where, sorts);
        }
    }
}
