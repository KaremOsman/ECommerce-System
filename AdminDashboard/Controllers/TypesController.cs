using AdminDashboard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObject.ProductModules;

namespace AdminDashboard.Controllers
{
    public class TypesController(ITypeApiService _typeApiService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var types = await _typeApiService.GetAllTypesAsync();
            return View(types);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TypeDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                TempData["ErrorMessage"] = "Validation Error: " + errors;
                return RedirectToAction(nameof(Index));
            }
            var success = await _typeApiService.CreateTypeAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "Type added successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add Type. Please check your input.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TypeDto model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var result = await _typeApiService.UpdateTypeAsync(id,model);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Update failed!";
                }
                else
                {
                    TempData["SuccessMessage"] = "Type updated successfully!";
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _typeApiService.DeleteTypeAsync(id);
            if (result.success)
            {
                return Json(new { success = true, message = "Type deleted successfully" });
            }
            return Json(new
            {
                success = false,
                message = !string.IsNullOrEmpty(result.message) ? result.message : "Could not delete type because it's linked to other products."
            });
        }
    }
}
