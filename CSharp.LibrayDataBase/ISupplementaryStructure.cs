using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 用于补全数据结构(常用于数据库表,列的补全)
    /// </summary>
    public interface ISupplementaryStructure
    {
        /// <summary>
        /// 执行补全
        /// </summary>
        void Supplementary();
    }
}
