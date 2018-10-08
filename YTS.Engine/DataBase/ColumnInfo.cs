using System;
using System.Reflection;
using YTS.Model.DB;
using YTS.Tools;

namespace YTS.Engine.DataBase
{
    /// <summary>
    /// 列信息模型
    /// </summary>
    public class ColumnInfo : AbsInfo_ShineUpon
    {
        /// <summary>
        /// 数据列特性
        /// </summary>
        public ColumnAttribute Attribute { get { return _attribute; } set { _attribute = value; } }
        private ColumnAttribute _attribute = null;
    }
}
