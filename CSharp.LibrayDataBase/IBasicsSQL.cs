using System;
using System.Collections.Generic;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 接口-基础-SQL
    /// </summary>
    public interface IBasicsSQL<M> where M : AbsModel_Null
    {
        string SQLInsert(M model);
        string SQLDelete(M model);
        string SQLUpdate(M model);
    }
}
