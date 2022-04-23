using Microsoft.AspNetCore.Mvc;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Queries;
using RC.Core.Mediator;
using RC.WebAPI.Core;
using System.Net;

namespace RC.Catalog.API.Controllers
{
    [Route("catalog")]
    public class CatalogController : MainController
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
        public async Task<ActionResult> ListProductsAsync()
        {
            return CustomResponse(await _productQueries.GetAllAsync());
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> AddProductAsync()
        {
            var result = await _mediator.SendCommandAsync(new AddProductCommand(0, "Produto 1", "Descrição produto 1", 25.53m, 3));

            return CustomResponse(result, HttpStatusCode.Created);
        }
    }
}
