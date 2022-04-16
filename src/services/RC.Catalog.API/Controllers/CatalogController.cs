using Microsoft.AspNetCore.Mvc;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Queries;
using RC.Core.Mediator;

namespace RC.Catalog.API.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductQueries _productQueries;
        private readonly IMediatRHandler _mediator;

        public CatalogController(IProductQueries productQueries, IMediatRHandler mediator)
        {
            _productQueries = productQueries;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("products")]
        public ActionResult ListProducts()
        {
            return Ok(_productQueries.GetAll());
        }

        [HttpPost]
        [Route("product")]
        public ActionResult AddProduct()
        {
            var result = _mediator.SendCommandAsync(new AddProductCommand(0, "Produto 1", "Descrição produto 1", 25.53m, 3));

            return Ok(result);
        }
    }
}
