using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.WebApi;

namespace YTS.AdminWebApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// 获取用户列表数据信息
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取一个用户信息
        /// </summary>
        /// <param name="Id">用户标识ID</param>
        /// <returns></returns>
        [HttpGet]
        public object GetItem(int? Id)
        {
            return RandomGetUser(Id ?? 0);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="Id">用户标识ID, 如果为0则为增加一个用户</param>
        /// <param name="model">编辑的用户信息</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public Result<object> EditUser(int Id, User model)
        {
            var result = new Result<object>();
            if (model == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "模型为空!";
                return result;
            }
            if (CheckData.IsStringNull(model.UserName))
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "用户名称为空!";
                return result;
            }
            result.Code = ResultCode.OK;
            result.Data = model;
            result.Message = (Id == 0 ? "添加" : "修改") + "成功!";
            return result;
        }

        /// <summary>
        /// 删除多个用户信息
        /// </summary>
        /// <param name="Ids">用户标识Id列表</param>
        /// <returns>返回执行结果</returns>
        [HttpDelete]
        public Result DeleteUsers(int[] Ids)
        {
            var result = new Result();
            if (Ids == null)
            {
                result.Code = ResultCode.BadRequest;
                result.Message = "删除失败, Ids为空!";
                return result;
            }
            result.Code = ResultCode.OK;
            result.Message = "删除成功！Ids:" + ConvertTool.ToString(Ids, ",");
            return result;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public int? Age { get; set; }
        public decimal? Money { get; set; }
    }
}
