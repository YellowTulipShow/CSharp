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
    /// 用户金额变动记录
    /// </summary>
    public class UserMoneyRecordController : BaseApiController
    {
        protected YTSEntityContext db;
        public UserMoneyRecordController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetUserMoneyRecordList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.UserMoneyRecord.AsQueryable();
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
