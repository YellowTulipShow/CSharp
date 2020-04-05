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
    /// 用户消费记录
    /// </summary>
    public class UserExpensesRecordController : BaseApiController
    {
        protected YTSEntityContext db;
        public UserExpensesRecordController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetUserExpensesRecordList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.UserExpensesRecord.AsQueryable();
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
        public Result AddUserExpensesRecords(UserExpensesRecord[] models)
        {
            var result = new Result();
            models = new UserExpensesRecord[] {};
            var manager = GetManager(db);

            UserProductOperate userProductOperate = new UserProductOperate(db, GetManager(db));
            string BatchNo = OrderForm.CreateOrderNumber();
            foreach (var model in models)
            {
                if (model.ID > 0)
                {
                    result.Code = ResultCode.Forbidden;
                    result.Message = "不能修改消费记录!";
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

                userProductOperate.AddUserExpensesRecord(BatchNo, user, product, model);
            }
            db.SaveChanges();

            result.Message = "添加成功!";
            return result;
        }
    }
}
