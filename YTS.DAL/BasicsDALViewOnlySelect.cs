using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using YTS.Tools;
using YTS.Model;

namespace YTS.DAL
{
    /// <summary>
    /// 基础DAL 用于视图, 只能查询的DAL
    /// </summary>
    public class BasicsDALViewOnlySelect<M> : BasicsDAL<M> where M : AbsTableModel
    {
        public BasicsDALViewOnlySelect(M model) : base(model) { }

        #region === View Can Not Method: ===
        public override bool Exists(int id) {
            return false;
        }
        public override bool Transaction(List<string> strlist) {
            return false;
        }
        public override int Add(M model) {
            return 0;
        }
        public override bool Delete(int id) {
            return false;
        }
        public override bool Update(M model) {
            return false;
        }
        public override bool UpdateField(int id, string strValue) {
            return false;
        }
        public override M GetModel(int id) {
            return null;
        }
        #endregion
    }
}
