using System.Net;
using Microsoft.AspNetCore.Mvc;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Queries;
using RC.WebAPI.Core;
using RC.MessageBus.Mediator;
using RC.Catalog.API.Models;

namespace RC.Catalog.API.Controllers
{
    [Route("catalog")]
    public class CatalogController : MainController
    {
        private readonly IProductQueries _productQueries;
        private readonly IMediatRHandler _bus;

        public CatalogController(IProductQueries productQueries, IMediatRHandler mediator)
        {
            _productQueries = productQueries;
            _bus = mediator;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult> ListProductsAsync()
        {
            return CustomResponse(await _productQueries.GetAllAsync());
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductViewRequestModel model)
        {
            var result = await _bus.SendCommandAsync(new AddProductCommand(0, model.Name, model.Description, model.Value, model.Quantity));

            return CustomResponse(result, HttpStatusCode.Created);
        }
    }
}
