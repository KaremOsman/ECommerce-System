using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Services
{
    public interface IBrandApiService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<bool> CreateBrandAsync(BrandDto brand);
        Task<bool> UpdateBrandAsync(int id, BrandDto brand);
        Task<(bool success, string message)> DeleteBrandAsync(int id);
    }
}
