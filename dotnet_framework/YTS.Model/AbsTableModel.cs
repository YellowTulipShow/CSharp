using System;
using YTS.Tools;

namespace YTS.Model
{
    /// <summary>
    /// 表 基础 模型
    /// </summary>
    [Serializable]
    public abstract class AbsTableModel : AbsBasicDataModel
    {
        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public abstract string GetTableName();
    }

    public abstract class AbsTableModelCommonlyUsed : AbsTableModel
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public int id { get { return _id; } set { _id = value; } }
        private int _id = 0;

        /// <summary>
        /// 记录添加时间
        /// </summary>
        public DateTime TimeAdd { get { return _TimeAdd; } set { _TimeAdd = value; } }
        private DateTime _TimeAdd = DateTime.Now;

        /// <summary>
        /// 记录备注
        /// </summary>
        public string Remark { get { return _Remark; } set { _Remark = value; } }
        private string _Remark = string.Empty;
    }
}
