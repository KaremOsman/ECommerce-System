using AdminDashboard.Models;
using AdminDashboard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Controllers
{
    [Authorize]
    public class HomeController(IProductApiService _productApiService,
                                IBrandApiService _brandApiService) : Controller
    {
        public async Task<IActionResult> Index(ProductQueryParams queryParams)
        {
            var model = new HomeViewModel
            {
                TotalProducts = (await _productApiService.GetAllProductsAsync(queryParams)).totalCount,
                TotalUsers = 50,
                TodaySales = 1500.75m,
                TotalBrands = (await _brandApiService.GetAllBrandsAsync()).Count(),
                RecentProducts = new List<ProductDto>
                {
                    await _productApiService.GetProductByIdAsync(1),
                    await _productApiService.GetProductByIdAsync(2),
                }
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
