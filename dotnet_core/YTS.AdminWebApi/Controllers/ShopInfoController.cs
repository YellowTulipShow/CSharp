using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.WebApi;
using YTS.Shop;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace YTS.AdminWebApi.Controllers
{
    [AllowAnonymous]
    public class ShopInfoController : BaseApiController
    {
        protected YTSShopContext db;
        public ShopInfoController(YTSShopContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetShopInfoList()
        {
            var list = db.ShopInfo.AsQueryable();
            int total = list.Count();
            var result = list
                .OrderByDescending(m => m.ID)
                .ToList();
            return new
            {
                rows = result,
                total,
            };
        }

        [HttpGet]
        public Result<object> GetShopInfo(int? ID)
        {
            if ((ID ?? 0) <= 0)
            {
                return new Result<object>()
                {
                    Code = ResultCode.BadRequest,
                    Message = @"ID为空!",
                };
            }
            var model = db.ShopInfo.Where(m => m.ID == ID).FirstOrDefault();
            return new Result<object>()
            {
                Code = ResultCode.OK,
                Data = model,
                Message = model != null ? @"获取成功!" : @"数据获取为空!",
            };
        }

        [HttpPost]
        public Result<object> EditShopInfo([FromBody] ShopInfo model)
        {
            var result = new Result<object>();
            if (model == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "模型为空!";
                return result;
            }
            var ID = model.ID;
            if (ID <= 0)
            {
                model.AddTime = DateTime.Now;
                model.AddUserID = 1;
                db.ShopInfo.Add(model);
            }
            else
            {
                db.ShopInfo.Attach(model);
                EntityEntry<ShopInfo> entry = db.Entry(model);
                entry.State = EntityState.Modified;
                entry.Property(gp => gp.AddTime).IsModified = false;
                entry.Property(gp => gp.AddUserID).IsModified = false;
            }
            db.SaveChanges();
            result.Data = model.ID;
            result.Message = (ID == 0 ? "添加" : "修改") + "成功!";
            return result;
        }

        [HttpPost]
        public Result DeleteShopInfos(int[] IDs)
        {
            var result = new Result();
            if (IDs == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "删除失败, IDs为空!";
                return result;
            }

            db.ShopInfo.RemoveRange(db.ShopInfo.Where(a => IDs.Contains(a.ID)).ToList());

            db.SaveChanges();
            result.Code = ResultCode.OK;
            result.Message = "删除成功！IDs:" + ConvertTool.ToString(IDs, ",");
            return result;
        }
    }
}
