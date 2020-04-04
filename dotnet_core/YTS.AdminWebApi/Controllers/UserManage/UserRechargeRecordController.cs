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
    /// 用户充值记录
    /// </summary>
    public class UserRechargeRecordController : BaseApiController
    {
        protected YTSEntityContext db;
        public UserRechargeRecordController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetUserRechargeRecordList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.UserRechargeRecord.AsQueryable();
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
        public Result<object> AddUserRechargeRecord(UserRechargeRecord model)
        {
            var result = new Result<object>();
            if (model.ID > 0)
            {
                result.Code = ResultCode.Forbidden;
                result.Message = "不能修改报损记录!";
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

            // 充值设置
            UserRechargeSet userRechargeSet = db.UserRechargeSet.Where(a => a.ID == model.UserRechargeSetID).FirstOrDefault();
            if (userRechargeSet == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "充值设置查询为空!";
                return result;
            }

            var userOperate = new UserOperate(db, GetManager(db));
            userOperate.AddUserRechargeRecord(user, userRechargeSet, model);

            result.Data = model.ID;
            result.Message = "添加成功!";
            return result;
        }
    }
}
