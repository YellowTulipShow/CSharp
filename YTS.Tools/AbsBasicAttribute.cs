using System;

namespace YTS.Tools
{
    /// <summary>
    /// 抽象-基础 '特性'
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public abstract class AbsBasicAttribute : System.Attribute { }
}
