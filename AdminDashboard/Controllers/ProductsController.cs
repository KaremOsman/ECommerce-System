using AdminDashboard.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared;
using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Controllers
{
    [Authorize]
    public class ProductsController(IProductApiService _productApiService,IBrandApiService _brandApiService, 
                                    ITypeApiService _typeApiService) : Controller
    {
        public async Task<IActionResult> Index(ProductQueryParams queryParams)
        {
            var products = await _productApiService.GetAllProductsAsync(queryParams);

            var brands = await _brandApiService.GetAllBrandsAsync();
            var types = await _typeApiService.GetAllTypesAsync();

            ViewBag.Brands = new SelectList(brands, "Id", "Name");
            ViewBag.Types = new SelectList(types, "Id", "Name");

            if (products is null)
                return View(new PaginationResult<ProductDto>(0, 0, 0, Enumerable.Empty<ProductDto>()));

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productApiService.GetProductByIdAsync(id);

            if (product == null)
                throw new ProductNotFoundException(id);

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var brands = await _brandApiService.GetAllBrandsAsync();
            var types = await _typeApiService.GetAllTypesAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "Name");
            ViewBag.Types = new SelectList(types, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrUpdateProductDto model)
        {
            if (ModelState.IsValid)
            {
                var success = await _productApiService.CreateProductAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = "Product created successfully";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Failed to create product. Please try again.");
            }

            var brands = await _brandApiService.GetAllBrandsAsync();
            var types = await _typeApiService.GetAllTypesAsync();

            ViewBag.Brands = new SelectList(brands, "Id", "Name");
            ViewBag.Types = new SelectList(types, "Id", "Name");

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productApiService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            var brands = await _brandApiService.GetAllBrandsAsync();
            var types = await _typeApiService.GetAllTypesAsync();  

            var brandId = brands.FirstOrDefault(b => b.Name == product.BrandName)?.Id;
            var typeId = types.FirstOrDefault(t => t.Name == product.TypeName)?.Id;

            var model = new CreateOrUpdateProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                BrandId = brandId ?? 0,
                TypeId = typeId ?? 0,
                PictureUrl = product.PictureUrl
            };

            ViewBag.Brands = new SelectList(await _brandApiService.GetAllBrandsAsync(), "Id", "Name", model.BrandId);
            ViewBag.Types = new SelectList(await _typeApiService.GetAllTypesAsync(), "Id", "Name", model.TypeId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateOrUpdateProductDto model)
        {
            if (ModelState.IsValid)
            {
                var success = await _productApiService.UpdateProductAsync(id, model);
                if (success)
                {
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Error updating product.");
            }

            ViewBag.Brands = new SelectList(await _brandApiService.GetAllBrandsAsync(), "Id", "Name");
            ViewBag.Types = new SelectList(await _typeApiService.GetAllTypesAsync(), "Id", "Name");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest(new { success = false, message = "No ids provided" });

            var failedIds = new List<int>();

            foreach (var id in ids)
            {
                var (success, message) = await _productApiService.DeleteProductAsync(id);

                if (!success)
                    failedIds.Add(id);
            }

            if (failedIds.Any())
            {
                return Json(new
                {
                    success = false,
                    message = $"Failed to delete some products: {string.Join(", ", failedIds)}"
                });
            }

            return Json(new
            {
                success = true,
                message = "Products deleted successfully"
            });
        }
    }
}
