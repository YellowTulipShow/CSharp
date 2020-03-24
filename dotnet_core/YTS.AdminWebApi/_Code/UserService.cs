using System;

namespace YTS.WebApi
{
    public interface IUserService
    {
        /// <summary>
        /// 判断是否验证通过
        /// </summary>
        bool IsValid(LoginRequestDTO req);
    }

    public class UserService : IUserService
    {
        public bool IsValid(LoginRequestDTO req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.Password))
                return false;
            return req.UserName == "admin" && req.Password == "zrq.yts.pwd";
        }
    }
}
