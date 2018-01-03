using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据库表  同一程序不能多个解释。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : AbsBasicsAttribute { }
}
