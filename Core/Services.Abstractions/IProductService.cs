using Shared;
using Shared.DataTransferObject.ProductModules;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // Get all products
        Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        // Get a product by id
        Task<ProductDto> GetProductByIdAsync(int id);
        // Create Product
        Task<ProductDto> CreateProductAsync(CreateOrUpdateProductDto productDto);
        // Update Product
        Task<ProductDto> UpdateProductAsync(int id, CreateOrUpdateProductDto productDto);
        // Delete Product
        Task<bool> DeleteProductAsync(int id);
    }
}
