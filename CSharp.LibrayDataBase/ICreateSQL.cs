using System;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 创建 SQL 语句
    /// </summary>
    public interface ICreateSQL<M> where M: AbsModel_Null
    {
        string CreateInsert(M model);
        string CreateDelete(string where);
        string CreateUpdata(M model, string where);
        string CreateSelect(string where);
    }
}
