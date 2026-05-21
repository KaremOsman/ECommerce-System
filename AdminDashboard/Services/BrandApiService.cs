using AdminDashboard.Helper;
using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Services
{
    public class BrandApiService(HttpClient _httpClient) : IBrandApiService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<BrandDto>>("Brands");
            return response ?? Enumerable.Empty<BrandDto>();
        }

        public async Task<bool> CreateBrandAsync(BrandDto brand)
        {
            var response = await _httpClient.PostAsJsonAsync("Brands", brand);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBrandAsync(int id, BrandDto brand)
        {
            var response = await _httpClient.PutAsJsonAsync($"Brands/{id}", brand);
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool success, string message)> DeleteBrandAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Brands/{id}");

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();

            return (response.IsSuccessStatusCode, result?.Message ?? "Done");
        }
    }
}
