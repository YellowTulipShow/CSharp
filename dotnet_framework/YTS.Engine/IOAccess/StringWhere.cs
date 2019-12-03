using System;
using YTS.Tools.Const;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 字符串条件
    /// </summary>
    public interface IStringWhere
    {
        void AddItem(string key, CalcEnums.Comparison comparison, object value);
        void AddGroup(CalcEnums.Logic logic, IStringWhere stringwhere);
        string GetMergeWhereString();
    }
}
