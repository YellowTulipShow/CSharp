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
        Shop_Manager IsValid(LoginRequestDTO req);
    }

    public class UserService : IUserService
    {
        protected YTSShopContext db;
        public UserService(YTSShopContext db)
        {
            this.db = db;
        }

        public Shop_Manager IsValid(LoginRequestDTO req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.Password))
                return null;
            var encryPwd = Shop_Manager.EncryptionPassword(req.Password);
            return db.Shop_Manager
                .Where(m => m.Account == req.UserName && m.Password == encryPwd)
                .FirstOrDefault();
        }
    }
}
