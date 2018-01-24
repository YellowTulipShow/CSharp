using System;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 表-基础-模型
    /// </summary>
    [Serializable]
    public abstract class AbsModel_Null : AbsBasicsDataModel, ITableName
    {
        public AbsModel_Null() { }

        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public abstract string GetTableName();
    }
}
