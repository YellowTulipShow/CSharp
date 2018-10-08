using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTS.DAL;
using YTS.Tools;

namespace YTS.BLL
{
    public class AbsFile<M, D>
        where M : Model.File.AbsFile
        where D : DAL.AbsFile<M>
    {
        /// <summary>
        /// 当前DAL对象
        /// </summary>
        public D SelfDAL { get { return _SelfDAL; } }
        private D _SelfDAL = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsFile() {
            this._SelfDAL = ReflexHelp.CreateNewObject<D>();
        }

        public virtual bool Insert(M model) {
            return SelfDAL.Insert(model);
        }

        public virtual bool Insert(M[] models) {
            return SelfDAL.Insert(models);
        }

        public virtual bool Delete(Func<M, bool> where) {
            return SelfDAL.Delete(where);
        }

        public virtual bool Update(Func<M, M> where) {
            return SelfDAL.Update(where);
        }

        public virtual M[] Select(int top, Func<M, bool> where) {
            return SelfDAL.Select(top, where);
        }

        public virtual M[] Select(int pageCount, int pageIndex, out int recordCount, Func<M, bool> where) {
            return SelfDAL.Select(pageCount, pageIndex, out recordCount, where);
        }

        public virtual int GetRecordCount(Func<M, bool> where) {
            return SelfDAL.GetRecordCount(where);
        }

        public virtual M GetModel(Func<M, bool> where) {
            return SelfDAL.GetModel(where);
        }
    }
}
