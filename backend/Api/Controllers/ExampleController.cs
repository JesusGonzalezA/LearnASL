using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : BaseController
    {
        public ExampleController()
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world");
        }
    }
}
