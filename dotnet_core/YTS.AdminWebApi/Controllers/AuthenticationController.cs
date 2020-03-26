using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTS.Tools;
using YTS.WebApi;

namespace YTS.AdminWebApi.Controllers
{
    /// <summary>
    /// 认证方式
    /// </summary>
    [AllowAnonymous]
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticateService _authService;
        public AuthenticationController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        /// <summary>
        /// 根据账户密码交换Token信息
        /// </summary>
        /// <param name="request">账户信息</param>
        /// <returns>返回结果</returns>
        [HttpPost]
        public ActionResult RequestToken([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }
            if (_authService.IsAuthenticated(request, out string token))
            {
                return Ok(token);
            }
            return BadRequest("Invalid Request");
        }

        /// <summary>
        /// 获取所有请求代码的详情
        /// </summary>
        [HttpGet]
        public object ResultCodes()
        {
            EnumInfo[] infos = EnumInfo.AnalysisList<ResultCode>();
            var list = infos
                .Select(info => new
                {
                    code = info.IntValue,
                    name = info.Name,
                    remark = info.Explain,
                })
                .ToList();
            return list;
        }
    }
}
