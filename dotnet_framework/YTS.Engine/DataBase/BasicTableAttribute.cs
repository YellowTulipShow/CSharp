using System;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.DataBase
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
