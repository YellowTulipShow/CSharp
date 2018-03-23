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
        public DataChar.LogicChar KeyChar { get { return this._keychar_; } }
        protected DataChar.LogicChar _keychar_ = DataChar.LogicChar.AND;
        public WhereModel() { }
        public WhereModel(DataChar.LogicChar keychar) {
            this._keychar_ = keychar;
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
        /// 默认值: 是否允许字段重复 默认允许
        /// </summary>
        public const bool DEFAULT_ISALLOWFIELDREPEAT =  true;
        /// <summary>
        /// 是否允许字段重复
        /// </summary>
        public bool IsAllowFieldRepeat { get { return _isAllowFieldRepeat; } set { _isAllowFieldRepeat = value; } }
        private bool _isAllowFieldRepeat = DEFAULT_ISALLOWFIELDREPEAT;

        #region ====== method: static check is can use ======
        /// <summary>
        /// 检查是否可以使用 (单个)
        /// </summary>
        public static bool CheckIsCanUse(WhereModel wheres) {
            return CheckData.IsCanUseModel(wheres, ErrorMethod);
        }
        /// <summary>
        /// 检查是否可以使用 (多个)
        /// </summary>
        public static bool CheckIsCanUse(WhereModel[] wheres) {
            return CheckData.IsCanUseModel(wheres, ErrorMethod);
        }
        private static bool ErrorMethod(WhereModel wheres) {
            return !FieldValueModel.CheckIsCanUse(wheres.FielVals) && !CheckIsCanUse(wheres.Wheres);
        }
        #endregion
    }

    /// <summary>
    /// 字段/值模型
    /// </summary>
    public class FieldValueModel : AbsBasicDataModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public DataChar.OperChar KeyChar { get { return this._keychar_; } }
        private DataChar.OperChar _keychar_ = DataChar.OperChar.EQUAL;
        public FieldValueModel() { }
        public FieldValueModel(DataChar.OperChar keychar) {
            this._keychar_ = keychar;
        }

        /// <summary>
        /// 设置关键字符
        /// </summary>
        public void SetKeyChar(DataChar.OperChar keychar) {
            this._keychar_ = keychar;
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
            return CheckData.IsCanUseModel(fielvals, m => CheckData.IsStringNull(m.Name));
        }
        /// <summary>
        /// 检查是否可以使用 (多个)
        /// </summary>
        public static bool CheckIsCanUse(FieldValueModel[] fielvals) {
            return CheckData.IsCanUseModel(fielvals, m => CheckData.IsStringNull(m.Name));
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
            return CheckData.IsCanUseModel(fielOrders, m => CheckData.IsStringNull(m.Name));
        }
        /// <summary>
        /// 检查是否可以使用 (多个)
        /// </summary>
        public static bool CheckIsCanUse(FieldOrderModel[] fielOrders) {
            return CheckData.IsCanUseModel(fielOrders, m => CheckData.IsStringNull(m.Name));
        }
    }
}
