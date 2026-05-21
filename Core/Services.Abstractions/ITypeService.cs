using Shared.DataTransferObject.ProductModules;

namespace Services.Abstractions
{
    public interface ITypeService
    {

        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        // Create New Type
        Task<bool> CreateTypeAsync(TypeDto typeDto);
        // Update Type
        Task<bool> UpdateTypeAsync(int id, TypeDto typeDto);
        // Delete Type
        Task<bool> DeleteTypeAsync(int id);
    }
}
