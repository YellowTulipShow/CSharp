using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 抽象-数据库 字段类型
    /// </summary>
    public abstract class AbsFieldType
    {
        public AbsFieldType() { }

        /// <summary>
        /// 字段类型名称
        /// </summary>
        public abstract string FieldTypeName();

        /// <summary>
        /// 打印 用于保存到数据库的值
        /// </summary>
        public abstract string PrintSaveValue(object programValue);

        public override string ToString() {
            return FieldTypeName();
        }
    }

    /// <summary>
    /// 抽象-数据库 字段类型 字符数量限制
    /// </summary>
    public abstract class AbsFieldTypeCharCount : AbsFieldType
    {
        protected ushort charCount = 0;

        public AbsFieldTypeCharCount(ushort charCount)
            : base() {
            SetCharCount(charCount);
        }

        private void SetCharCount(ushort charCount) {
            ushort min = MinCharCount;
            ushort max = MaxCharCount;
            this.charCount = (charCount < min) ? min : (charCount > max) ? max : charCount;
        }

        /// <summary>
        /// 最小字符长度数量
        /// </summary>
        public abstract ushort MinCharCount { get; }
        /// <summary>
        /// 最小字符长度数量
        /// </summary>
        public abstract ushort MaxCharCount { get; }

        /// <summary>
        /// 打印 用于保存到数据库的值
        /// </summary>
        public override string PrintSaveValue(object programValue) {
            return CheckData.IsObjectNull(programValue) ? string.Empty :
                IsCharExceedsLimit(programValue) ? LimitCharValue(programValue) : programValue.ToString();
        }

        /// <summary>
        /// 是否字符超出限制
        /// </summary>
        protected virtual bool IsCharExceedsLimit(object programValue) {
            if (CheckData.IsObjectNull(programValue))
                return false;
            if (CheckData.IsStringNull(programValue.ToString()))
                return false;
            if (MinCharCount <= programValue.ToString().Length && programValue.ToString().Length <= MaxCharCount)
                return false;
            return true;
        }

        /// <summary>
        /// 截取符合字符限制的内容
        /// </summary>
        protected string LimitCharValue(object programValue) {
            return programValue.ToString().Substring(0, charCount);
        }
    }

    /// <summary>
    /// 抽象-数据库 字段类型 字符数量限制(可包含最多)
    /// </summary>
    public abstract class AbsFieldTypeCharMAX : AbsFieldTypeCharCount
    {
        /// <summary>
        /// 表示使用 MAX 字符标识
        /// </summary>
        public const ushort MAXCHARSIGN = ushort.MaxValue;

        public AbsFieldTypeCharMAX(ushort charCount) : base(charCount) { }

        /// <summary>
        /// 是否字符超出限制
        /// </summary>
        protected override bool IsCharExceedsLimit(object programValue) {
            return charCount == MAXCHARSIGN ? false : base.IsCharExceedsLimit(programValue);
        }
    }

    /// <summary>
    /// 抽象-数据库 字段类型 默认值
    /// </summary>
    public abstract class AbsFieldTypeDefault : AbsFieldType
    {
        public AbsFieldTypeDefault(object defalutValue) { }
    }
}
