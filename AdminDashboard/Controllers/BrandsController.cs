using AdminDashboard.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Controllers
{
    public class BrandsController(IBrandApiService _brandApiService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands = await _brandApiService.GetAllBrandsAsync();
            return View(brands);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                TempData["ErrorMessage"] = "Validation Error: " + errors;
                return RedirectToAction(nameof(Index));
            }
            var success = await _brandApiService.CreateBrandAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "Brand added successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add brand. Please check your input.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BrandDto model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var result = await _brandApiService.UpdateBrandAsync(id, model);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Update failed!";
                }
                else
                {
                    TempData["SuccessMessage"] = "Brand updated successfully!";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _brandApiService.DeleteBrandAsync(id);
            if (result.success)
            {
                return Json(new { success = true, message = "brand deleted successfully" });
            }
            return Json(new
            {
                success = false,
                message = !string.IsNullOrEmpty(result.message) ? result.message : "Could not delete brand because it's linked to other products."
            });
        }
    }
}
