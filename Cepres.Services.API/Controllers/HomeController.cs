using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepres.Service.API
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Content("API Is Up");
        }
    }
}
