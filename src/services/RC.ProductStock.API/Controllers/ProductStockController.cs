using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductStock.Controllers
{
    [Route("api/product-stock")]
    [ApiController]
    public class ProductStockController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello Word");
        }
    }
}
