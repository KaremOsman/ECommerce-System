using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DataTransferObject.OrderModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost] // POST: api/Orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetUserEmail());
            return Ok(order);
        }
        // Get All Orders By Email
        [HttpGet] // GET: api/Orders
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetUserEmail());
            return Ok(orders);
        }
        // Get Order by Id and Email
        [HttpGet("{id:guid}")] // GET: api/Orders/{id}
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderAsync(id);
            return Ok(order);
        }
        // Get Delivery Methods
        [AllowAnonymous]
        [HttpGet("deliveryMethods")] // GET: api/Orders/deliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMotheds()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
