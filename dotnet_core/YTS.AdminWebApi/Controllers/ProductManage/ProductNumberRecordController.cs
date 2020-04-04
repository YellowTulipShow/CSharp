using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using YTS.Tools;
using YTS.WebApi;
using YTS.Shop;
using YTS.Shop.Models;
using YTS.Shop.Tools;

namespace YTS.AdminWebApi.Controllers
{
    /// <summary>
    /// 产品数量修改记录
    /// </summary>
    public class ProductNumberRecordController : BaseApiController
    {
        protected YTSEntityContext db;
        public ProductNumberRecordController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetProductNumberRecordList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.ProductNumberRecord.AsQueryable();
            int total = 0;
            var result = list
                .ToOrderBy(sort, order)
                .ToPager(page, rows, a => total = a)
                .ToList();
            return new
            {
                rows = result,
                total,
            };
        }
    }
}
