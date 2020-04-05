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
using Microsoft.AspNetCore.Authorization;

namespace YTS.AdminWebApi.Controllers
{
    public class MenusController : BaseApiController
    {
        protected YTSEntityContext db;
        public MenusController(YTSEntityContext db)
        {
            this.db = db;
        }

        private readonly static string[] _menu_status = new string[] { "closed", "open" };

        public class _menu
        {
            public int? id { get; set; }
            public string text { get; set; }
            public string code { get; set; }
            public string url { get; set; }
            public string state { get; set; }
            public bool? ishide { get; set; }
            public int? childrenCount { get; set; }
            public IEnumerable<_menu> children { get; set; }
        }

        [HttpGet]
        public object GetMenusList(
            int? page = null, int? rows = null,
            string sort = null, string order = null,
            string NameSpaces = null,
            int? id = null)
        {
            var list = db.Menus.AsQueryable();
            if (!string.IsNullOrWhiteSpace(NameSpaces))
            {
                list = list.Where(a => a.NameSpaces == NameSpaces);
            }
            int total = 0;
            var result = list
                .Where(a => a.ParentID == id)
                .ToOrderBy(sort, order)
                .ToPager(page, rows, a => total = a)
                .ToList()
                .Select(m => new _menu()
                {
                    id = m.ID,
                    text = m.Name,
                    code = m.Code,
                    url = m.Url,
                    state = _menu_status[0],
                    childrenCount = list
                        .Where(a => a.ParentID == m.ID)
                        .Count(),
                });
            return result;
        }

        [HttpPost]
        public Result UploadMenus(string NameSpaces, IEnumerable<_menu> models)
        {
            var result = new Result();
            if (models == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "上传数据为空!";
                return result;
            }

            int needDBCount = UpdateDBDatas(NameSpaces, models);

            result.Code = ResultCode.OK;
            if (needDBCount <= 0)
                result.Message = "需要上传的菜单为空!";
            else
                result.Message = $"完成上传的菜单数量: {needDBCount}!";
            return result;
        }

        private int UpdateDBDatas(string NameSpaces, IEnumerable<_menu> models, int? parentID = null)
        {
            var menager = GetManager(db);
            int needDBCount = 0;
            if (models == null)
                return needDBCount;
            foreach (_menu item in models)
            {
                var menu = ToDBDatas(NameSpaces, item, parentID);
                if (menu == null)
                {
                    continue;
                }
                needDBCount += 1;
                if (menu.ID <= 0)
                {
                    menu.AddTime = DateTime.Now;
                    menu.AddManagerID = menager.ID;
                    db.Menus.Add(menu);
                }
                else
                {
                    db.Menus.Attach(menu);
                    EntityEntry<Menus> entry = db.Entry(menu);
                    entry.State = EntityState.Modified;
                    entry.Property(gp => gp.AddTime).IsModified = false;
                    entry.Property(gp => gp.AddManagerID).IsModified = false;
                }
                db.SaveChanges();

                if (item.children != null)
                {
                    needDBCount += UpdateDBDatas(NameSpaces, item.children, menu.ID);
                }
            }
            return needDBCount;
        }

        private Menus ToDBDatas(string NameSpaces, _menu _menu, int? parentID = null)
        {
            if (string.IsNullOrWhiteSpace(_menu.code))
                return null;
            var model = db.Menus
                .Where(a => a.NameSpaces == NameSpaces && a.Code == _menu.code)
                .FirstOrDefault();
            if (model == null)
            {
                return new Menus()
                {
                    ID = 0,
                    NameSpaces = NameSpaces,
                    ParentID = parentID,
                    Code = _menu.code,
                    Name = _menu.text,
                    Url = _menu.url,
                    IsHide = _menu.ishide ?? false,
                    Ordinal = (db.Menus
                        .Where(a => a.NameSpaces == NameSpaces && a.ParentID == parentID)
                        .Max(a => (int?)a.Ordinal) ?? 0) + 1,
                };
            }
            else
            {
                model.IsHide = _menu.ishide ?? false;
                model.Name = _menu.text;
                model.Url = _menu.url;
                return model;
            }
        }
    }
}
