using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace YTS.WebApi
{
    public class LoginRequestDTO
    {
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
