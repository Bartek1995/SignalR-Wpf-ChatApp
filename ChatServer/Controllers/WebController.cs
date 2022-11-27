using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WebController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVersion()
        {
            return Ok("Chat server v1.0");
        }
    }
}
