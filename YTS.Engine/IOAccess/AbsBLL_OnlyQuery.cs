using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;
using YTS.Tools.Model;

namespace YTS.Engine.IOAccess
{
    public abstract class AbsBLL_OnlyQuery<M, D, W, P, PI> : IBLL_OnlyQuery<M, D, W, P, PI>
        where M : AbsShineUpon
        where D : AbsDAL<M, W, P, PI>
        where P : ShineUponParser<M, PI>
        where PI : ShineUponInfo
    {
        public D SelfDAL = null;

        public AbsBLL_OnlyQuery() {
            this.SelfDAL = ReflexHelp.CreateNewObject<D>();
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
