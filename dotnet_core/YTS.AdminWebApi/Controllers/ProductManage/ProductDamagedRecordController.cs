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
    /// 产品报损记录
    /// </summary>
    public class ProductDamagedRecordController : BaseApiController
    {
        protected YTSEntityContext db;
        public ProductDamagedRecordController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetProductDamagedRecordList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.ProductDamagedRecord.AsQueryable();
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

        [HttpPost]
        public Result<object> AddProductDamagedRecord(ProductDamagedRecord model)
        {
            var result = new Result<object>();
            if (model.ID > 0)
            {
                result.Code = ResultCode.Forbidden;
                result.Message = "不能修改报损记录!";
                return result;
            }

            // 产品查询
            Product product = db.Product.Where(a => a.ID == model.ProductID).FirstOrDefault();
            if (product == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "产品查询为空!";
                return result;
            }

            var productOperate = new ProductOperate(db, GetManager(db));
            productOperate.AddProductDamagedRecord(product, model);

            result.Data = model.ID;
            result.Message = "添加成功!";
            return result;
        }
    }
}
