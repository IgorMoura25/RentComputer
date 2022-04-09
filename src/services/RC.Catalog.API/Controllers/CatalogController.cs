using Microsoft.AspNetCore.Mvc;

namespace RC.Catalog.API.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public ActionResult ListProducts()
        {
            return Ok();
        }
    }
}
