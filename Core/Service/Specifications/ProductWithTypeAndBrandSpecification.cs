using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;

namespace Service.Specifications
{
    class ProductWithTypeAndBrandSpecification : BaseSpecification<Product, int>
    {
        // Get All
        /// the criteria Cases:
        ///1. brandId = null and typeId = null => (criteria) --> true && true
        ///2. brandId != null and typeId = null => (criteria) --> p.BrandId == brandId && true
        ///3. brandId = null and typeId != null => (criteria) --> true && p.TypeId == typeId
        ///4. brandId != null and typeId != null => (criteria) --> p.BrandId == brandId && p.TypeId == typeId 
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || queryParams.brandId == 0 || P.BrandId == queryParams.brandId) 
                     && (!queryParams.typeId.HasValue || queryParams.typeId == 0 || P.TypeId == queryParams.typeId)
                     && (string.IsNullOrWhiteSpace(queryParams.searchValue) || P.Name.ToLower().Contains(queryParams.searchValue.ToLower().Trim())))
        {
            AddInclude(p => p.Brand!);
            AddInclude(p => p.Type!);

            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderByAscending(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderByAscending(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    AddOrderByAscending(p => p.Name);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.pageIndex);
        }

        // Get By Id
        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand!);
            AddInclude(p => p.Type!);
        }

    }
}
