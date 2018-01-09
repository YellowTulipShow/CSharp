using System;
using CSharp.LibrayFunction;
using CSharp.LibrayDataBase;

namespace CSharp.ApplicationData
{
    /// <summary>
    /// 数据模型类: 文章
    /// </summary>
    [Explain("文章表")]
    [Table]
    public class ModelArticles : AbsModel_ID
    {
        /// <summary>
        /// 获得当前表 全名 名称
        /// </summary>
        public override string GetTableName() {
            return @"dt_Articles";
        }

        #region Model
        /// <summary>
        /// 文章金额
        /// </summary>
        [Explain("文章金额")]
        [Column("money")]
        public decimal Money { get { return _money; } set { _money = value; } }
        private decimal _money = 0M;

        /// <summary>
        /// VIP打折比例
        /// </summary>
        [Explain("VIP打折比例")]
        public int VipDiscountRate { get { return _vipDiscountRate; } set { _vipDiscountRate = value; } }
        private int _vipDiscountRate = 80;
        #endregion
    }

    /// <summary>
    /// 数据逻辑类: 文章
    /// </summary>
    public class BLLArticles : BLLSQLServer<ModelArticles>
    {
        public BLLArticles() : base(new DALSQLServer<ModelArticles>()) { }
    }
}