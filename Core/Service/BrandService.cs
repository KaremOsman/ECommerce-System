using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Shared.DataTransferObject.ProductModules;

namespace Service
{
    public class BrandService(IUnitOfWork _unitOfWork,IMapper _mapper) : IBrandService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }
        public async Task<bool> CreateBrandAsync(BrandDto brandDto)
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brand = _mapper.Map<ProductBrand>(brandDto);

            await repo.AddAsync(brand);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateBrandAsync(int id, BrandDto brandDto)
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brand = await repo.GetByIdAsync(id);

            if (brand == null) return false;

            _mapper.Map(brandDto, brand);

            repo.Update(brand);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteBrandAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brand = await repo.GetByIdAsync(id);

            if (brand == null) return false;

            repo.Delete(brand);

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
