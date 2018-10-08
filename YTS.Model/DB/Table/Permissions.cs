using System;
using YTS.Engine.DataBase;
using YTS.Tools;
using YTS.Tools.Const;

namespace YTS.Model.DB.Table
{
    /// <summary>
    /// 数据模型类: 权限
    /// </summary>
    [Explain(@"权限")]
    [EntityTable]
    public class Permissions : AbsTable_StringID
    {
        public override Enums.UseCodeMark GetUseCode() {
            return Enums.UseCodeMark.Permissions;
        }

        public override string GetTableName() {
            return @"dt_Permissions";
        }

        #region === Model ===
        /// <summary>
        /// 名称
        /// </summary>
        [Explain(@"名称")]
        [Column]
        public string Name { get { return _Name; } set { _Name = value; } }
        private string _Name = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Explain(@"描述")]
        [Column]
        public string Description { get { return _Description; } set { _Description = value; } }
        private string _Description = string.Empty;
        #endregion
    }
}
