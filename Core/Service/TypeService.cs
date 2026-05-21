using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstractions;
using Shared.DataTransferObject.ProductModules;

namespace Service
{
    public class TypeService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITypeService
    {
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var types = await repo.GetAllAsync();
            var typesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return typesDto;
        }
        public async Task<bool> CreateTypeAsync(TypeDto typeDto)
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var type = _mapper.Map<ProductType>(typeDto);

            await repo.AddAsync(type);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateTypeAsync(int id, TypeDto typeDto)
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var type = await repo.GetByIdAsync(id);

            if (type == null) return false;

            _mapper.Map(typeDto, type); 
            repo.Update(type);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteTypeAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var type = await repo.GetByIdAsync(id);

            if (type == null) return false;

            repo.Delete(type);
            try
            {
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
