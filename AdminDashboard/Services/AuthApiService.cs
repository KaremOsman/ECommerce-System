using Shared.DataTransferObject.IdentityModules;

namespace AdminDashboard.Services
{
    public class AuthApiService(HttpClient _httpClient, ILogger<AuthApiService> _logger) : IAuthApiService
    {
        public async Task<UserDto?> GetTokenFromApiAsync(LogInDto logInDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Authentication/LogIn", logInDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("API Error: {StatusCode} - {Message}", response.StatusCode, errorContent);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<UserDto?> RegisterApiAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Account/register", registerDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Register failed with status code: {0} - {1}", response.StatusCode, errorContent);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}
