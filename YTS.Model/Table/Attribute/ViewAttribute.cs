using System;

namespace YTS.Model.Table.Attribute
{
    /// <summary>
    /// 视图数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewAttribute : BasicTableAttribute
    {
        /// <summary>
        /// 作为视图, 不需要检查结构体
        /// </summary>
        public override bool IsCheckStructure() {
            return false;
        }
    }
}
