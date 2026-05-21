using Shared.DataTransferObject.ProductModules;

namespace Service
{
    public interface IBrandService
    {
        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        // Create New Brand
        Task<bool> CreateBrandAsync(BrandDto brandDto); 
        // Update Brand
        Task<bool> UpdateBrandAsync(int id, BrandDto brandDto);
        // Delete Brand
        Task<bool> DeleteBrandAsync(int id);
    }
}
