using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefultPageSize = 5;

        private const int MaxPageSize = 10;
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public ProductSortingOptions? sortingOptions { get; set; }
        public string? searchValue { get; set; }
        private int _pageIndex = 1;
        public int pageIndex
        {
            get => _pageIndex;
            set => _pageIndex = (value <= 0) ? 1 : value;
        }

        private int pageSize = DefultPageSize;
        public int PageSize
        {
            get => pageSize;
            // if the value is less than or equal to 0, set it to the default page size; if it's greater than the maximum page size, set it to the maximum page size; otherwise, use the provided value.
            set => pageSize = (value <= 0) ? DefultPageSize : (value > MaxPageSize) ? MaxPageSize : value;  
        }
    }
}
