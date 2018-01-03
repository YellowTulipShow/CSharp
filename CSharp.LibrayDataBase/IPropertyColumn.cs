using System;
using System.Reflection;
using System.Collections.Generic;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 解析-模型属性值为表列
    /// </summary>
    public interface IPropertyColumn
    {
        Dictionary<PropertyInfo, ColumnAttribute> AnalysisPropertyColumns();
    }
}
