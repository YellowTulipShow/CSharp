using System;

namespace YTS.Model.File.Attribute
{
    /// <summary>
    /// 文件字段特性  同一程序不能多个解释。无法继承此类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FieldAttribute : Model.Attribute.ShineUponPropertyAttribute
    {
        public FieldAttribute() { }
    }
}
