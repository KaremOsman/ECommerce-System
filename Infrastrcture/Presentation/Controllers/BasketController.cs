using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DataTransferObject.BasketModules;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get Basket
        [HttpGet] // api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }
        // Create or Update Basket
        [HttpPost] // api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        // Delete Basket
        [HttpDelete("{key}")] // api/Basket/{key}
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        { 
            var IsDeleted = await _serviceManager.BasketService.DeleteBasketAsync(key);
            if (!IsDeleted)
                return NotFound();
            return Ok(IsDeleted);
        }
    }
}
