using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DataTransferObject.BasketModules;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost("{basketId}")] // POST : baseUrl/api/payments/{basketId}
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(String basketId)
        {
            var basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(basket);
        }

        [HttpPost("WebHook")] // POST : baseUrl/api/payment/WebHook
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await _serviceManager.PaymentService.UpdateOrderPaymentStatusAsync(json, Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }
    }
}
