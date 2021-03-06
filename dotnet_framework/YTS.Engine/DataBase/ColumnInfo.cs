﻿using System;
using System.Reflection;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.Engine.DataBase
{
    /// <summary>
    /// 列信息模型
    /// </summary>
    public class ColumnInfo : ShineUponInfo
    {
        /// <summary>
        /// 数据列特性
        /// </summary>
        public ColumnAttribute Attribute { get { return _attribute; } set { _attribute = value; } }
        private ColumnAttribute _attribute = null;
    }
}
