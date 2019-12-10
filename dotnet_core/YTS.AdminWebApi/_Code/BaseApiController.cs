using Microsoft.AspNetCore.Mvc;

namespace YTS.AdminWebApi
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}