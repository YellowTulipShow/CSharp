using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.DAL
{
    public class AbsDAL_File<M>
        where M : Model.AbsShineUpon
    {
        public AbsDAL_File() { }

        //public abstract bool Insert(M model);

        //public abstract bool Update(KeyString[] keyvaluedic, string where);

        //public abstract M[] Select(int top = 0, string where = null, string sort = null);

        //public abstract M[] Select(int pageCount, int pageIndex, out int recordCount, string where = null, string sort = null);

        //public abstract int GetRecordCount(string where = null);

        //public virtual M GetModel(string where = null, string sort = null) {
        //    M[] list = Select(1, where, sort);
        //    return (CheckData.IsSizeEmpty(list)) ? null : list[0];
        //}
    }
}
