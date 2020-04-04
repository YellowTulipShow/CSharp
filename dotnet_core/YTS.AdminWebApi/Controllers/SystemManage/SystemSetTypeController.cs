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
using System.Collections.Generic;

namespace YTS.AdminWebApi.Controllers
{
    public class SystemSetTypeController : BaseApiController
    {
        protected YTSEntityContext db;
        public SystemSetTypeController(YTSEntityContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public object GetSystemSetTypeList(
            int? page = null, int? rows = null,
            string sort = null, string order = null)
        {
            var list = db.SystemSetType.AsQueryable();
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
        public Result<object> GetSystemSetType(int? ID)
        {
            if ((ID ?? 0) <= 0)
            {
                return new Result<object>()
                {
                    Code = ResultCode.BadRequest,
                    Message = @"ID为空!",
                };
            }
            var model = db.SystemSetType.Where(m => m.ID == ID).FirstOrDefault();
            return new Result<object>()
            {
                Code = ResultCode.OK,
                Data = model,
                Message = model != null ? @"获取成功!" : @"数据获取为空!",
            };
        }

        [HttpPost]
        public Result<object> EditSystemSetType(SystemSetType model)
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
                db.SystemSetType.Add(model);
            }
            else
            {
                db.SystemSetType.Attach(model);
                EntityEntry<SystemSetType> entry = db.Entry(model);
                entry.State = EntityState.Modified;
            }
            db.SaveChanges();
            result.Data = model.ID;
            result.Message = (ID == 0 ? "添加" : "修改") + "成功！";
            return result;
        }

        [HttpPost]
        public Result DeleteSystemSetType(int[] IDs)
        {
            var result = new Result();
            if (IDs == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "删除失败, IDs为空!";
                return result;
            }

            db.SystemSetType.RemoveRange(db.SystemSetType.Where(a => IDs.Contains(a.ID)).ToList());
            db.SaveChanges();

            result.Code = ResultCode.OK;
            result.Message = "删除成功！IDs:" + ConvertTool.ToString(IDs, ",");
            return result;
        }

        [HttpPost]
        public Result InitSystemSetType()
        {
            var result = new Result();

            var dict = new Dictionary<string, List<EnumInfo>>();


            result.Code = ResultCode.OK;
            result.Message = "初始化字典成功!";
            return result;
        }

        // private List<SystemSetType> EnumToSetType<T>() where T : Enum
        // {
        // }
    }
}
