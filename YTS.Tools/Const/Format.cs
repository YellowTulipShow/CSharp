using System;

namespace YTS.Tools.Const
{
    /// <summary>
    /// 格式
    /// </summary>
    public static class Format
    {
        /// <summary>
        /// 时间格式 精确至: 毫秒 (取七位)
        /// </summary>
        public const string DATETIME_MILLISECOND_SEVEN = "yyyy-MM-dd HH:mm:ss.fffffff";
        /// <summary>
        /// 时间格式 精确至: 毫秒 (取三位)
        /// </summary>
        public const string DATETIME_MILLISECOND = "yyyy-MM-dd HH:mm:ss.fff";
        /// <summary>
        /// 时间格式 精确至: 秒
        /// </summary>
        public const string DATETIME_SECOND = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 时间格式 精确至: 分钟
        /// </summary>
        public const string DATETIME_MINUTE = "yyyy-MM-dd HH:mm";
        /// <summary>
        /// 时间格式 精确至: 小时
        /// </summary>
        public const string DATETIME_HOUR = "yyyy-MM-dd HH";
        /// <summary>
        /// 时间格式 精确至: 天
        /// </summary>
        public const string DATETIME_DAY = "yyyy-MM-dd";
        /// <summary>
        /// 时间格式 精确至: 月
        /// </summary>
        public const string DATETIME_MONTH = "yyyy-MM";
        /// <summary>
        /// 时间格式 精确至: 年
        /// </summary>
        public const string DATETIME_YEAR = "yyyy";
    }
}
