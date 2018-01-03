using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 数据库表-子表  同一程序不能多个解释。无法继承此类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SonTableAttribute : TableAttribute { }
}
