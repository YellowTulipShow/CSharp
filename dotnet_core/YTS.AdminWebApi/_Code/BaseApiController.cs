using Microsoft.AspNetCore.Mvc;

namespace YTS.AdminWebApi
{
    [ApiController]
    [Route(ApiConfig.APIRoute)]
    public abstract class BaseApiController : ControllerBase
    {
    }
}