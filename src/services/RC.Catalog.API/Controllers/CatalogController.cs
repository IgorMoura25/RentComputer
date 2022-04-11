using Microsoft.AspNetCore.Mvc;
using RC.Catalog.API.Application.Queries;

namespace RC.Catalog.API.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductQueries _productQueries;

        public CatalogController(IProductQueries productQueries)
        {
            _productQueries = productQueries;
        }

        [HttpGet]
        [Route("products")]
        public ActionResult ListProducts()
        {
            return Ok(_productQueries.GetAll());
        }
    }
}
