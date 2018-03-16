using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 条件模型
    /// </summary>
    public class WhereModel : AbsBasicDataModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public DataChar.LogicChar KeyWords { get { return this._keywords_; } }
        protected DataChar.LogicChar _keywords_ = DataChar.LogicChar.AND;

        public WhereModel() { }
        public WhereModel(DataChar.LogicChar keywords) {
            this._keywords_ = keywords;
        }

        /// <summary>
        /// 嵌套的条件
        /// </summary>
        public WhereModel[] Wheres { get { return _wheres; } set { _wheres = value; } }
        private WhereModel[] _wheres = new WhereModel[] { };


        /// <summary>
        /// 嵌套的条件
        /// </summary>
        public FieldValueModel[] FielVals { get { return _fielVals; } set { _fielVals = value; } }
        private FieldValueModel[] _fielVals = new FieldValueModel[] { };

        /// <summary>
        /// 检查是否可以使用 (单个)
        /// </summary>
        public static bool CheckIsCanUse(WhereModel wheres) {
            return CheckData.CheckModelCanUseItem(wheres, ErrorMethod);
        }
        /// <summary>
        /// 检查是否可以使用 (多个)
        /// </summary>
        public static bool CheckIsCanUse(WhereModel[] wheres) {
            return CheckData.CheckModelCanUseArray(wheres, ErrorMethod);
        }

        private static bool ErrorMethod(WhereModel wheres) {
            return !FieldValueModel.CheckIsCanUse(wheres.FielVals) && !CheckIsCanUse(wheres.Wheres);
        }
    }

    /// <summary>
    /// 字段/值模型
    /// </summary>
    public class FieldValueModel : AbsBasicDataModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public DataChar.OperChar KeyWords { get { return this._keywords_; } }
        private DataChar.OperChar _keywords_ = DataChar.OperChar.EQUAL;

        public FieldValueModel() { }
        public FieldValueModel(DataChar.OperChar keywords) {
            this._keywords_ = keywords;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 字段值
        /// </summary>
        public string Value { get { return _value; } set { _value = value; } }
        private string _value = string.Empty;

        /// <summary>
        /// 检查是否可以使用 (单个)
        /// </summary>
        public static bool CheckIsCanUse(FieldValueModel fielvals) {
            return CheckData.CheckModelCanUseItem(fielvals, m => CheckData.IsStringNull(m.Name));
        }
        /// <summary>
        /// 检查是否可以使用 (多个)
        /// </summary>
        public static bool CheckIsCanUse(FieldValueModel[] fielvals) {
            return CheckData.CheckModelCanUseArray(fielvals, m => CheckData.IsStringNull(m.Name));
        }
    }

    /// <summary>
    /// 字段排序模型
    /// </summary>
    public class FieldOrderModel : AbsBasicDataModel
    {
        public FieldOrderModel() { }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = string.Empty;

        /// <summary>
        /// 是否升序排列 默认 False
        /// </summary>
        public bool IsAsc { get { return _value; } set { _value = value; } }
        private bool _value = false;

        /// <summary>
        /// 检查是否可以使用 (单个)
        /// </summary>
        public static bool CheckIsCanUse(FieldOrderModel fielOrders) {
            return CheckData.CheckModelCanUseItem(fielOrders, m => CheckData.IsStringNull(m.Name));
        }
        /// <summary>
        /// 检查是否可以使用 (多个)
        /// </summary>
        public static bool CheckIsCanUse(FieldOrderModel[] fielOrders) {
            return CheckData.CheckModelCanUseArray(fielOrders, m => CheckData.IsStringNull(m.Name));
        }
    }
}
