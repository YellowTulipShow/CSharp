using System;

namespace YTS.Engine.DataBase
{
    /// <summary>
    /// 实体数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityTableAttribute : BasicTableAttribute
    {
    }
}
