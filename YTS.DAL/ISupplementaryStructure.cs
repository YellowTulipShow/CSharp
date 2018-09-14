using System;

namespace YTS.DAL
{
    /// <summary>
    /// 用于补全数据结构(常用于数据库表,列的补全)
    /// </summary>
    public interface ISupplementaryStructure
    {
        /// <summary>
        /// 是否需要补全
        /// </summary>
        /// <returns>是(True), 否(False)</returns>
        bool IsNeedSupplementary();

        /// <summary>
        /// 执行补全操作
        /// </summary>
        void ExecutionSupplementary();
    }
}
