using System;
using YTS.Model.Attribute;

namespace YTS.Model.Table.Attribute
{
    /// <summary>
    /// 基础数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BasicTableAttribute : AbsBasicAttribute
    {
        /// <summary>
        /// 是否检查结构体
        /// </summary>
        /// <returns></returns>
        public virtual bool IsCheckStructure() {
            return true;
        }
    }
}
