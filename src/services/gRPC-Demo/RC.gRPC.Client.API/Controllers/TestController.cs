using Microsoft.AspNetCore.Mvc;
using RC.gRPC.Client.API.Services;

namespace RC.gRPC.Client.API.Controllers
{
    [Route("catalog")]
    public class TestController : ControllerBase
    {
        private readonly ICatalogGrpcService _catalogService;

        public TestController(ICatalogGrpcService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult> ListProductsAsync()
        {
            return Ok(await _catalogService.ListProductsAsync());
        }
    }
}
