using AdminDashboard.Helper;
using Shared;
using Shared.DataTransferObject.ProductModules;
using static AdminDashboard.Helper.Converter;

namespace AdminDashboard.Services
{
    public class ProductApiService(HttpClient _httpClient) : IProductApiService
    {
        public async Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var queryString = QueryString(queryParams);

            var url = string.IsNullOrEmpty(queryString)
                ? "Products"
                : $"Products?{queryString}";

            var response = await _httpClient.GetFromJsonAsync<PaginationResult<ProductDto>>(url);

            return response ?? new PaginationResult<ProductDto>(0, 0, 0, Enumerable.Empty<ProductDto>());
        }
        private string QueryString(ProductQueryParams queryParams)
        {
            var query = new Dictionary<string, string>();

            if (queryParams.pageIndex > 0)
                query["pageIndex"] = queryParams.pageIndex.ToString();

            if (queryParams.PageSize > 0)
                query["PageSize"] = queryParams.PageSize.ToString();

            if (!string.IsNullOrWhiteSpace(queryParams.searchValue))
                query["searchValue"] = Uri.EscapeDataString(queryParams.searchValue);

            if (queryParams.brandId.HasValue)
                query["brandId"] = queryParams.brandId.Value.ToString();

            if (queryParams.typeId.HasValue)
                query["typeId"] = queryParams.typeId.Value.ToString();
            if (queryParams.sortingOptions.HasValue)
                query["sortingOptions"] = queryParams.sortingOptions.Value.ToString();

            if (query.Count == 0)
                return string.Empty;

            return string.Join("&", query.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductDto>($"Products/{id}");
            if (response is null)
                throw new Exception($"Product with ID {id} not found or API returned empty response.");

            return response;
        }
        public async Task<bool> CreateProductAsync(CreateOrUpdateProductDto model)
        {
            var content = ConvertDtoToMultipart(model);

            var response = await _httpClient.PostAsync("Products", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProductAsync(int id, CreateOrUpdateProductDto model)
        {
            var content = ConvertDtoToMultipart(model);

            var response = await _httpClient.PutAsync($"Products/{id}", content);

            return response.IsSuccessStatusCode;
        }
        public async Task<(bool success, string message)> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();

            return (response.IsSuccessStatusCode, result?.Message ?? "An error occurred");
        }
    }
}
