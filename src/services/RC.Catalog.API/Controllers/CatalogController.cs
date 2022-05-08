using System.Net;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using RC.Catalog.API.Application.Commands;
using RC.Catalog.API.Application.Queries;
using RC.WebAPI.Core;
using RC.MessageBus;
using RC.MessageBus.EasyNetQ;

namespace RC.Catalog.API.Controllers
{
    [Route("catalog")]
    public class CatalogController : MainController
    {
        private readonly IProductQueries _productQueries;
        private IEasyNetQBus _bus { get; set; }

        public CatalogController(IProductQueries productQueries, IEasyNetQBus bus)
        {
            _productQueries = productQueries;
            _bus = bus;
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
            var result = await _bus.RequestAsync<AddProductCommand, ValidationResult>(new AddProductCommand(0, "Produto 1", "Descrição produto 1", 25.53m, 3));

            return CustomResponse(result, HttpStatusCode.Created);
        }
    }
}
