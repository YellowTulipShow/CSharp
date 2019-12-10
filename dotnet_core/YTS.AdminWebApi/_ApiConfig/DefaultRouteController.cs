using Microsoft.AspNetCore.Mvc;

namespace YTS
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class DefaultRouteController : ControllerBase
    {
    }
}