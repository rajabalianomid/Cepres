using Microsoft.AspNetCore.Mvc;

namespace Cepres.Service.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Content("API Is Up");
        }
    }
}
