using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Services
{
    public interface ITypeApiService
    {
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<bool> CreateTypeAsync(TypeDto type);
        Task<bool> UpdateTypeAsync(int id, TypeDto type);
        Task<(bool success, string message)> DeleteTypeAsync(int id);
    }
}
