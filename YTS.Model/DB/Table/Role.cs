using System;
using YTS.Tools;

namespace YTS.Model.DB.Table
{
    /// <summary>
    /// 数据模型类: 角色
    /// </summary>
    [Explain(@"角色")]
    [EntityTable]
    public class Role : AbsTable_StringID
    {
        public override Const.Enums.UseCodeMark GetUseCode() {
            return Const.Enums.UseCodeMark.Role;
        }

        public override string GetTableName() {
            return @"dt_Role";
        }

        #region === Model ===
        /// <summary>
        /// 名称
        /// </summary>
        [Explain(@"名称")]
        [Column]
        public string Name { get { return _Name; } set { _Name = value; } }
        private string _Name = string.Empty;
        #endregion
    }
}
