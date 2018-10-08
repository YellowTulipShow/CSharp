using System;

namespace YTS.Engine.IOAccess
{
    public class DAL_LocalFile<M> : AbsDAL<M, Func<M, bool>>
        where M : Model.AbsShineUpon
    {
        public DAL_LocalFile() : base() { }

        public override bool Insert(M model) {
            throw new NotImplementedException();
        }

        public override bool Insert(M[] models) {
            throw new NotImplementedException();
        }

        public override bool Delete(Func<M, bool> where) {
            throw new NotImplementedException();
        }

        public override bool Update(Model.KeyObject[] kos, Func<M, bool> where) {
            throw new NotImplementedException();
        }

        public override M[] Select(int top, Func<M, bool> where, Model.KeyBoolean[] sorts = null) {
            throw new NotImplementedException();
        }

        public override M[] Select(int pageCount, int pageIndex, out int recordCount, Func<M, bool> where, Model.KeyBoolean[] sorts) {
            throw new NotImplementedException();
        }

        public override int GetRecordCount(Func<M, bool> where) {
            throw new NotImplementedException();
        }
    }
}
