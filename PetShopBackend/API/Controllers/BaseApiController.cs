using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    abstract public class BaseApiController : ControllerBase
    {
    }
}