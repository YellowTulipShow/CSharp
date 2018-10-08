using System;
using YTS.Tools;

namespace YTS.Model.DB.Table
{
    /// <summary>
    /// 数据模型类: 组别
    /// </summary>
    [Explain(@"组别")]
    [EntityTable]
    public class Group : AbsTable_StringID
    {
        public override Const.Enums.UseCodeMark GetUseCode() {
            return Const.Enums.UseCodeMark.Group;
        }

        public override string GetTableName() {
            return @"dt_Group";
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
