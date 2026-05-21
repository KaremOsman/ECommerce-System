using Shared;
using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Services
{
    public interface IProductApiService
    {
        Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<bool> CreateProductAsync(CreateOrUpdateProductDto model);
        Task<bool> UpdateProductAsync(int id, CreateOrUpdateProductDto model);
        Task<(bool success, string message)> DeleteProductAsync(int id);
    }
}
