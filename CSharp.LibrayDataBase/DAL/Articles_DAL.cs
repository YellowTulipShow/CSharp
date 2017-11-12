using System;
using CSharp.Model.Table;

namespace CSharp.LibrayDataBase.DAL
{
    /// <summary>
    /// 数据访问类: 文章
    /// </summary>
    internal class Articles_DAL : BasicsDAL<Articles>
    {
        public Articles_DAL() : base(new Articles()) { }
    }
}