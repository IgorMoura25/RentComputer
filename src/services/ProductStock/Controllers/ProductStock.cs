using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStock : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello Word");
        }
    }
}
