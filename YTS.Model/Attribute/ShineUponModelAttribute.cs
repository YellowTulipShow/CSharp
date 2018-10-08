using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Model.Attribute
{
    /// <summary>
    /// 映射-模型-特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ShineUponModelAttribute : Tools.AbsBasicAttribute
    {
    }
}
