using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shared;
using Shared.DataTransferObject.ProductModules;

namespace Presentation.Controllers
{
    [Authorize]
    public class ProductsController(
        IServiceManager _serviceManager,
        ICacheService _cacheService) : ApiBaseController
    {
        // Get all products
        [HttpGet]
        [Cache("products", 120)]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts(
            [FromQuery] ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        // Get product by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // Create Product
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] CreateOrUpdateProductDto productDto)
        {
            var product = await _serviceManager.ProductService.CreateProductAsync(productDto);

            if (product is null)
                return BadRequest();

            await _cacheService.IncrementAsync("products:version");

            return Ok(product);
        }

        // Update Product
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromForm] CreateOrUpdateProductDto productDto)
        {
            var product = await _serviceManager.ProductService.UpdateProductAsync(id, productDto);

            if (product is null)
                return BadRequest();

            await _cacheService.IncrementAsync("products:version");

            return Ok(product);
        }

        // Delete Product
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleted = await _serviceManager.ProductService.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Product with id '{id}' not found"
                });
            }

            await _cacheService.IncrementAsync("products:version");

            return Ok(new
            {
                statusCode = 200,
                message = $"Product with id '{id}' Deleted Successfully"
            });
        }
    }
}