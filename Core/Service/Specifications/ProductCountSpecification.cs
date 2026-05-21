using Domain.Entities.ProductModule;
using Shared;

namespace Service.Specifications
{
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId)
                     && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
                     && (string.IsNullOrWhiteSpace(queryParams.searchValue) || P.Name.ToLower().Contains(queryParams.searchValue))){}
    }
}
