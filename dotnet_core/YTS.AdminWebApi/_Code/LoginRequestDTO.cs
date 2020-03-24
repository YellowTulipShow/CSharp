using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace YTS.WebApi
{
    public class LoginRequestDTO
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录用户密码
        /// </summary>
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
