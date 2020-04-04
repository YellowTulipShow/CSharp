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
    /// 用户退货记录
    /// </summary>
    public class UserReturnGoodsRecordController : BaseApiController
    {
        protected YTSEntityContext db;
        public UserReturnGoodsRecordController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetUserReturnGoodsRecordList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.UserReturnGoodsRecord.AsQueryable();
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

        [HttpGet]
        public Result<object> GetUserReturnGoodsRecord(int? ID)
        {
            if ((ID ?? 0) <= 0)
            {
                return new Result<object>()
                {
                    Code = ResultCode.BadRequest,
                    Message = @"ID为空!",
                };
            }
            var model = db.UserReturnGoodsRecord.Where(m => m.ID == ID).FirstOrDefault();
            return new Result<object>()
            {
                Code = ResultCode.OK,
                Data = model,
                Message = model != null ? @"获取成功!" : @"数据获取为空!",
            };
        }

        [HttpPost]
        public Result<object> AddUserReturnGoodsRecord(UserReturnGoodsRecord model)
        {
            var result = new Result<object>();
            if (model.ID > 0)
            {
                result.Code = ResultCode.Forbidden;
                result.Message = "不能修改退货记录!";
                return result;
            }

            // 用户查询
            Users user = db.Users.Where(a => a.ID == model.UserID).FirstOrDefault();
            if (user == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "用户查询为空!";
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

            var userProductOperate = new UserProductOperate(db, GetManager(db));
            userProductOperate.AddUserReturnGoodsRecord(user, product, model);
            db.SaveChanges();

            result.Data = model.ID;
            result.Message = "添加成功!";
            return result;
        }
    }
}
