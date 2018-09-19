using System;
using YTS.Model.Attribute;

namespace YTS.Model.Table.Attribute
{
    /// <summary>
    /// 基础数据库表
    /// </summary>
    public class BasicTableAttribute : ShineUponModelAttribute
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
