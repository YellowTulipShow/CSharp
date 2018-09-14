using System;

namespace YTS.Model.Table.Attribute
{
    /// <summary>
    /// 实体数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityAttribute : BasicTableAttribute
    {
    }
}
