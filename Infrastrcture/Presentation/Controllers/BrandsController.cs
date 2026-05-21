using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DataTransferObject.ProductModules;

namespace Presentation.Controllers
{
    public class BrandsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Get All Brands
        [HttpGet]
        // GET: baseUrl/api/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.brandService.GetAllBrandsAsync();
            return Ok(brands);
        }
        //Create Brand
        [HttpPost]
        // POST: baseUrl/api/brands
        public async Task<ActionResult> CreateBrand([FromBody] BrandDto brandDto)
        {
            var result = await _serviceManager.brandService.CreateBrandAsync(brandDto);
            if (result) return Ok(new { Message = "Brand created successfully" });

            return BadRequest(new { Message = "Database logic failed or Name duplicated" });
        }
        //Update Brand
        [HttpPut("{id}")]
        // PUT: baseUrl/api/brands/{id}
        public async Task<ActionResult> UpdateBrand(int id, [FromBody] BrandDto brandDto)
        {
            if (brandDto == null) return BadRequest("Data is null");

            var result = await _serviceManager.brandService.UpdateBrandAsync(id, brandDto);

            if (result)
                return Ok(new { Message = "Brand updated successfully" });

            return NotFound(new { Message = "Brand not found or update failed" });
        }
        // Delete Brand
        [HttpDelete("{id}")]
        // DELETE: baseUrl/api/brands/{id}
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var result = await _serviceManager.brandService.DeleteBrandAsync(id);

            if (result)
                return Ok(new { Message = "Brand deleted successfully" });

            return BadRequest(new { Message = "Cannot delete brand. It might be linked to existing products." });
        }
    }
}
