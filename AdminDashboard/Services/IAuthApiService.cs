using Shared.DataTransferObject.IdentityModules;

namespace AdminDashboard.Services
{
    public interface IAuthApiService
    {
        Task<UserDto?> RegisterApiAsync(RegisterDto registerDto);
        Task<UserDto?> GetTokenFromApiAsync(LogInDto logInDto);
    }
}