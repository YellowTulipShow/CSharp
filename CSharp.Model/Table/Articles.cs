using System;
using CSharp.LibrayFunction;

namespace CSharp.Model.Table
{
    /// <summary>
    /// 数据模型类: 文章
    /// </summary>
    [Serializable]
    public class Articles : AbsTableModelID
    {
        public Articles() { }

        #region Model
        #endregion

        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public override string GetTableName() {
            return @"dt_Articles";
        }
    }
}
