using Microsoft.AspNetCore.Mvc;

namespace YTS.Data.Models.WebApi
{
    [ApiController]
    [Route(ApiConfig.APIRoute)]
    public abstract class BaseApiController : ControllerBase
    {
    }
}