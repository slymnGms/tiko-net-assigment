using Microsoft.AspNetCore.Mvc;

namespace tiko_net_assignment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetHome()
        {
            return Ok("Welcome To API");
        }
    }
}
