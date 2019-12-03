using System;
using YTS.Engine.DataBase;
using YTS.Model.DB;
using YTS.Tools;
using YTS.Tools.Const;

namespace YTS.Model
{
    /// <summary>
    /// 数据模型类: 文章
    /// </summary>
    [Explain(@"文章")]
    [EntityTable]
    public class Article : AbsTable_StringID
    {
        public override Enums.UseCodeMark GetUseCode() {
            return Enums.UseCodeMark.Article;
        }

        public override string GetTableName() {
            return @"dt_Article";
        }

        #region === Model ===
        /// <summary>
        /// 创建者用户SID
        /// </summary>
        [Explain(@"创建者用户SID")]
        [Column]
        public string CreateUserSID { get { return _CreateUserSID; } set { _CreateUserSID = value; } }
        private string _CreateUserSID = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [Explain(@"名称")]
        [Column]
        public string Name { get { return _Name; } set { _Name = value; } }
        private string _Name = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [Explain(@"内容")]
        [Column]
        public string Content { get { return _Content; } set { _Content = value; } }
        private string _Content = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        [Explain(@"发布时间")]
        [Column]
        public DateTime TimeRelease { get { return _TimeRelease; } set { _TimeRelease = value; } }
        private DateTime _TimeRelease = DateTime.Now;
        #endregion
    }
}
