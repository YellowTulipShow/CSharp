using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 抽象-数据库基础 '特性'
    /// </summary>
    public abstract class AbsBasicsAttribute : System.Attribute
    {
        /// <summary>
        /// 获取或设置 '对象' 名称。
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = String.Empty;
    }
}
