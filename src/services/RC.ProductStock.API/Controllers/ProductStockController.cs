using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductStock.Models

namespace ProductStock.Controllers
{
    [ApiController]
    [Route("api/product-stock")]
    public class ProductStockController : ControllerBase
    {
        private readonly ProductStockContext _contex;

        public ProductStockController(ProductStockContext context)
        {
            _contex = context;
        }


        [HttpPost("products")]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name) || productDto.Value <= 0)
            {
                return BadRequest("Required fields are missing.");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Value = productDto.Value,
            };

            _contex.Products.Add(product);
            _contex.SaveChanges();

            return Ok(product);
            
        }
    }
}