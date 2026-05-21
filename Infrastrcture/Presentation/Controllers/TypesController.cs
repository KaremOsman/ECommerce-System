using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DataTransferObject.ProductModules;

namespace Presentation.Controllers
{
    //[Authorize]
    public class TypesController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Get All Types
        [HttpGet]
        // GET: baseUrl/api/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.typeService.GetAllTypesAsync();
            return Ok(types);
        }
        //Create Type
        [HttpPost]
        // POST: baseUrl/api/types
        public async Task<ActionResult> CreateType([FromBody] TypeDto typeDto)
        {
            if (typeDto == null)
                return BadRequest(new { Message = "Invalid data provided." });
            var result = await _serviceManager.typeService.CreateTypeAsync(typeDto);
            if (result) return Ok(new { Message = "Type created successfully" });

            return BadRequest(new { Message = "Database logic failed or Name duplicated" });
        }
        //Update Type
        [HttpPut("{id}")]
        // PUT: baseUrl/api/Types/{id}
        public async Task<ActionResult> UpdateType(int id, [FromBody] TypeDto typeDto)
        {
            if (typeDto == null) return BadRequest("Data is null");

            var result = await _serviceManager.typeService.UpdateTypeAsync(id, typeDto);
            if (result)
                return Ok(new { Message = "Type updated successfully" });

            return NotFound(new { Message = "Brand not found or update failed" });
        }
        // Delete Type
        [HttpDelete("{id}")]
        // DELETE: baseUrl/api/types/{id}
        public async Task<ActionResult> DeleteType(int id)
        {
            var result = await _serviceManager.typeService.DeleteTypeAsync(id);

            if (result)
                return Ok(new { Message = "Type deleted successfully" });

            return BadRequest(new { Message = "Cannot delete type. It might be linked to existing products." });
        }
    }
}
