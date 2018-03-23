using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 接口-数据类型
    /// </summary>
    public interface IDataType
    {
        /// <summary>
        /// 输入转换
        /// </summary>
        object OutputConvert(object sourceValue, ColumnItemModel colmodel);
        /// <summary>
        /// 输出转换
        /// </summary>
        object InputConvert(object sourceValue, ColumnItemModel colmodel);
    }

    /// <summary>
    /// 抽象-C# 数据类型映射
    /// </summary>
    public abstract class AbsCSType : IDataType
    {
        public AbsCSType() { }

        public abstract object OutputConvert(object sourceValue, ColumnItemModel colmodel);

        public abstract object InputConvert(object sourceValue, ColumnItemModel colmodel);
    }

    /// <summary>
    /// 抽象-数据系统 数据类型映射
    /// </summary>
    public abstract class AbsDBType : IDataType
    {
        public AbsDBType() { }

        /// <summary>
        /// Object 字符串描述
        /// </summary>
        public override string ToString() {
            return TypeName();
        }

        /// <summary>
        /// 字段类型名称
        /// </summary>
        public abstract string TypeName();
        /// <summary>
        /// 输出转换
        /// </summary>
        public virtual object OutputConvert(object sourceValue, ColumnItemModel colmodel) {
            return sourceValue;
        }
        /// <summary>
        /// 输入转换
        /// </summary>
        public abstract object InputConvert(object sourceValue, ColumnItemModel colmodel);

        #region === Char Length ===
        /// <summary>
        /// 错误字符长度
        /// </summary>
        protected const ushort CHARLENGTH_ERROR = 0;
        /// <summary>
        /// 表示使用 MAX 字符标识
        /// </summary>
        public const ushort CHARLENGTH_MAX_SIGN = ushort.MaxValue;
        /// <summary>
        /// 最小字符长度
        /// </summary>
        protected virtual ushort MinCharLength { get { return CHARLENGTH_ERROR; } }
        /// <summary>
        /// 最小字符长度
        /// </summary>
        protected virtual ushort MaxCharLength { get { return CHARLENGTH_ERROR; } }
        /// <summary>
        /// 是否启用默认字符长度最大值的选项
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsDefaultCharLengthMAX() {
            return false;
        }

        /// <summary>
        /// 字符长度
        /// </summary>
        internal ushort CharLength { get { return _charLength; } set { _charLength = value; } }
        protected ushort _charLength = CHARLENGTH_ERROR;

        /// <summary>
        /// 设置字符长度
        /// </summary>
        public void SetCharLength(ushort clen) {
            ushort min = MinCharLength;
            ushort max = MaxCharLength >= CHARLENGTH_MAX_SIGN ? (ushort)(CHARLENGTH_MAX_SIGN - 1) : MaxCharLength;

            this.CharLength = (min <= clen && clen <= max) ? clen :
                (clen == CHARLENGTH_MAX_SIGN || IsDefaultCharLengthMAX()) ? CHARLENGTH_MAX_SIGN :
                CHARLENGTH_ERROR;
        }
        /// <summary>
        /// 获取字符长度字符串标识
        /// </summary>
        public string GetCharLengthStrSign() {
            return this.CharLength == CHARLENGTH_MAX_SIGN ? @"(MAX)" :
                this.CharLength == CHARLENGTH_ERROR ? string.Empty :
                string.Format("({0})", this.CharLength);
        }
        /// <summary>
        /// 获取字符长度字符串标识, 使用自定义的格式输出
        /// </summary>
        public string GetCharLengthStrSign(CharLengthFormatOutput formatOutput) {
            return CheckData.IsObjectNull(formatOutput) ? formatOutput(this.CharLength) : GetCharLengthStrSign();
        }
        /// <summary>
        /// 字符长度格式输出自定义方法
        /// </summary>
        public delegate string CharLengthFormatOutput(ushort charlength);
        /// <summary>
        /// 截取符合字符限制的内容
        /// </summary>
        protected string LimitCharLengthContent(object datasource) {
            string result = string.Empty;
            if (!CheckData.IsObjectNull(datasource)) {
                result = datasource.ToString();
            }
            if (this.CharLength == CHARLENGTH_ERROR || this.CharLength == CHARLENGTH_MAX_SIGN || result.Length < this.CharLength) {
                return result;
            }
            return result.Substring(0, this.CharLength);
        }

        /// <summary>
        /// 字符/字符串类型的基本转化过程
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <returns></returns>
        protected string CharStringTypeConvert(object sourceValue) {
            string result = LimitCharLengthContent(sourceValue);
            if (CheckData.IsStringNull(result)) {
                return GetDefaultValueString();
            }
            return result;
        }
        #endregion

        #region === Default Value ===
        /// <summary>
        /// 默认值
        /// </summary>
        internal ConstData.ConstEnum DefaultValue { get { return _defaultValue; } set { _defaultValue = value; } }
        protected ConstData.ConstEnum _defaultValue = ConstData.ConstEnum.Empty;
        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <param name="defaultValue"></param>
        public void SetDefaultValue(ConstData.ConstEnum defaultValue) {
            this.DefaultValue = defaultValue;
        }
        /// <summary>
        /// 获取默认值字符串
        /// </summary>
        public string GetDefaultValueString() {
            return ConstData.ConstOutput(this.DefaultValue);
        }
        #endregion
    }
}
