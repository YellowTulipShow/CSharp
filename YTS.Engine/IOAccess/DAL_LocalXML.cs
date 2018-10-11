using System;
using YTS.Engine.ShineUpon;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public class DAL_LocalXML<M> :
        AbsDAL<M, Func<M, bool>, ShineUponParser<M, ShineUponInfo>, ShineUponInfo>,
        IFileInfo
        where M : AbsShineUpon, IFileInfo
    {
        public DAL_LocalXML() : base() { }

        #region ====== using:IFileInfo ======
        public string GetPathFolder() {
            return this.DefaultModel.GetPathFolder();
        }

        public string GetFileName() {
            return this.DefaultModel.GetPathFolder();
        }
        #endregion

        #region ====== using:AbsDAL<Model, Where, Parser, ParserInfo> ======
        public override bool Insert(M model) {
            throw new NotImplementedException();
        }

        public override bool Insert(M[] models) {
            throw new NotImplementedException();
        }

        public override bool Delete(Func<M, bool> where) {
            throw new NotImplementedException();
        }

        public override bool Update(KeyObject[] kos, Func<M, bool> where) {
            throw new NotImplementedException();
        }

        public override M[] Select(int top, Func<M, bool> where, KeyBoolean[] sorts) {
            throw new NotImplementedException();
        }

        public override M[] Select(int pageCount, int pageIndex, out int recordCount, Func<M, bool> where, KeyBoolean[] sorts) {
            throw new NotImplementedException();
        }

        public override int GetRecordCount(Func<M, bool> where) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
