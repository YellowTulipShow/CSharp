using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.Data.Models;

namespace YTS.AdminWebApi.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public object GetUsers()
        {
            var list = Enumerable.Range(1, 5)
                .Select(index => RandomGetUser(index))
                .ToArray();
            return new
            {
                rows = list,
                total = list.Count(),
            };
        }

        private User RandomGetUser(int Id)
        {
            return new User
            {
                Id = Id,
                UserName = RandomData.GetChineseString(3),
                Sex = RandomData.GetBoolean() ? "男" : "女",
                Phone = "1" + RandomData.GetString(CommonData.ASCII_Number(), 10),
                Age = RandomData.GetInt(10, 40),
                Money = (decimal)RandomData.GetDouble(50),
            };
        }

        [HttpGet]
        public User GetItem()
        {
            return RandomGetUser(RandomData.GetInt(1, 5));
        }

        [HttpPost]
        public Result<int> EditUser(int Id, User model)
        {
            var result = new Result<int>();
            if (model == null)
            {
                result.ErrorCode = 1;
                result.Message = "模型为空!";
                return result;
            }
            if (CheckData.IsStringNull(model.UserName))
            {
                result.ErrorCode = 1;
                result.Message = "用户名称为空!";
                return result;
            }
            result.ErrorCode = 0;
            result.Data = RandomData.GetInt(1, 5);
            result.Message = (Id == 0 ? "添加" : "修改") + "成功!";
            return result;
        }

        [HttpPost]
        public Result DeleteUsers(int[] Ids)
        {
            var result = new Result();
            if (Ids == null)
            {
                result.ErrorCode = 1;
                result.Message = "删除失败, Ids为空!";
                return result;
            }
            result.ErrorCode = 0;
            result.Message = "删除成功！";
            return result;
        }
    }
}
