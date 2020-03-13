using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YTS.WebApi
{
    [ApiController]
    [Route(ApiConfig.APIRoute)]
    [EnableCors(ApiConfig.CorsName)]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
