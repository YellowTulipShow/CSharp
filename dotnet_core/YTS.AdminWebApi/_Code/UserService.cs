using System;
using System.Linq;
using YTS.Shop;

namespace YTS.WebApi
{
    public interface IUserService
    {
        /// <summary>
        /// 判断是否验证通过
        /// </summary>
        Manager IsValid(LoginRequestDTO req);
    }

    public class UserService : IUserService
    {
        protected YTSEntityContext db;
        public UserService(YTSEntityContext db)
        {
            this.db = db;
        }

        public Manager IsValid(LoginRequestDTO req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.Password))
                return null;
            var encryPwd = Manager.EncryptionPassword(req.Password);
            return db.Manager
                .Where(m => m.Account == req.UserName && m.Password == encryPwd)
                .FirstOrDefault();
        }
    }
}
