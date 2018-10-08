using System;

namespace YTS.Model.DB
{
    /// <summary>
    /// 视图数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewTableAttribute : BasicTableAttribute
    {
        /// <summary>
        /// 作为视图, 不需要检查结构体
        /// </summary>
        public override bool IsCheckStructure() {
            return false;
        }
    }
}
