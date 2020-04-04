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
        public Result<object> AddUserExpensesRecords(UserExpensesRecord[] models)
        {
            var result = new Result<object>();
            if (models == null)
            {
                result.Code = ResultCode.Forbidden;
                result.Message = "增加记录为空!";
                return result;
            }

            result.Data = 0;
            result.Message = "添加成功!";
            return result;
        }
    }
}
