using System;
using CSharp.Model.Table;

namespace CSharp.LibrayDataBase.BLL
{
    /// <summary>
    /// 数据逻辑类: 文章
    /// </summary>
    public class Articles_BLL : BasicsBLL<Articles>
    {
        public Articles_BLL() : base(new DAL.Articles_DAL()) { }
    }

}