using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Models
{
    public class HomeViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public decimal TodaySales { get; set; }
        public int TotalBrands { get; set; }

        public List<ProductDto> RecentProducts { get; set; }
    }
}
