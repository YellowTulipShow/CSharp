using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Model.Table
{
    /// <summary>
    /// 表 基础 模型
    /// </summary>
    [Serializable]
    public abstract class AbsTableModel : AbsBasicsDataModel
    {
        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public abstract string GetTableName();
    }

    /// <summary>
    /// 表 基础 模型 ID 版本
    /// </summary>
    [Serializable]
    public abstract class AbsTableModelID : AbsTableModel
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id { set { _id = value; } get { return _id; } }
        private int _id;


        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime TimeAdd { get { return _timeAdd; } set { _timeAdd = value; } }
        private DateTime _timeAdd;


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get { return _remark; } set { _remark = value; } }
        private string _remark;
    }
}
