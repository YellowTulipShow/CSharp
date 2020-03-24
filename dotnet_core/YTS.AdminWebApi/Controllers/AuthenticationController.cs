using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTS.WebApi;

namespace YTS.AdminWebApi.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticateService _authService;
        public AuthenticationController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        [AllowAnonymous]
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
    }
}
