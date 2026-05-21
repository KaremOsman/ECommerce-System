using Shared.DataTransferObject.OrderModule;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        // Crete Order
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string userEmail);
        //Get Delicery Method
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        // Get All Orders
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        // Get Order By Id
        Task<OrderToReturnDto> GetOrderAsync(Guid id);
    }
}
