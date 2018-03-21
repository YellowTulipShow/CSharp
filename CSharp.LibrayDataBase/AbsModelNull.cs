﻿using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 表-基础-模型
    /// </summary>
    [Serializable]
    public abstract class AbsModelNull : AbsBasicDataModel, ITableName
    {
        public AbsModelNull() { }

        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public abstract string GetTableName();
    }

    /// <summary>
    /// 表-基础-模型 添加时间
    /// </summary>
    [Serializable]
    public abstract class AbsModel_TimeAdd : AbsModelNull
    {
        /// <summary>
        /// 添加时间
        /// </summary>
        [Explain(@"添加时间")]
        //[Column(MSSFieldTypeDefault.Datetime, MSSDefalutValues.DateTimeNow, SortIndex = ushort.MinValue + 1)]
        [Column(MSQLServerDTParser.DTEnum.DateTime, DefaultValue = ConstData.ConstEnum.DateTimeNow, SortIndex = ushort.MinValue + 1)]
        public DateTime TimeAdd { get { return _timeAdd; } set { _timeAdd = value; } }
        private DateTime _timeAdd = DateTime.Now;
    }
    /// <summary>
    /// 表-基础-模型 备注
    /// </summary>
    [Serializable]
    public abstract class AbsModel_Remark : AbsModel_TimeAdd
    {
        /// <summary>
        /// 备注
        /// </summary>
        [Explain(@"备注")]
        [Column(MSQLServerDTParser.DTEnum.NVarChar, CharLength = 500, SortIndex = ushort.MinValue + 1)]
        public string Remark { get { return _remark; } set { _remark = value; } }
        private string _remark = String.Empty;
    }
    /// <summary>
    /// 表-基础-模型 记录ID
    /// </summary>
    [Serializable]
    public abstract class AbsModel_ID : AbsModel_Remark
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Explain(@"自增ID")]
        [Column(MSQLServerDTParser.DTEnum.Int, IsPrimaryKey = true, IsIDentity = true, SortIndex = ushort.MinValue)]
        public int id { get { return _id; } set { _id = value; } }
        private int _id = 0;
    }
}
