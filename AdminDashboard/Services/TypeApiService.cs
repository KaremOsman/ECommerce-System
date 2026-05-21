using AdminDashboard.Helper;
using Shared.DataTransferObject.ProductModules;
using System.Net.Http.Headers;

namespace AdminDashboard.Services
{
    public class TypeApiService(HttpClient _httpClient) : ITypeApiService
    {
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            //var token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            //_httpClient.DefaultRequestHeaders.Authorization =
            //new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TypeDto>>("Types");
            return response ?? Enumerable.Empty<TypeDto>();
        }
        public async Task<bool> CreateTypeAsync(TypeDto type)
        {
            var response = await _httpClient.PostAsJsonAsync("Types", type);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTypeAsync(int id, TypeDto type)
        {
            var response = await _httpClient.PutAsJsonAsync($"Types/{id}", type);
            return response.IsSuccessStatusCode;
        }
        public async Task<(bool success, string message)> DeleteTypeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Types/{id}");

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();

            return (response.IsSuccessStatusCode, result?.Message ?? "Done");
        }
    }
}
