using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Service.Specifications;
using Services.Abstractions;
using Shared.DataTransferObject.IdentityModules;
using Shared.DataTransferObject.OrderModule;

namespace Service
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string userEmail)
        {
            // Map the Address Dto to an Order Address entity
            var address = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);
            // Get the basket from the repository using the BasketId from the orderDto
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);

            var existingOrderSpec = new OrderWithPaymentIntentSpecification(basket.PaymentIntetId);
            var existingOrder = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(existingOrderSpec);
            if (existingOrder != null)
            {
                _unitOfWork.GetRepository<Order, Guid>().Delete(existingOrder); 
            }
            // Create a list of OrderItems to hold the items that will be added to the order
            List<OrderItem> orderItems = new();
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var ProductItem = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem
                {
                    Product = new ProductItemOrdered
                    {
                        ProductId = ProductItem.Id,
                        ProductName = ProductItem.Name,
                        PictureUrl = ProductItem.PictureUrl
                    },
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }
            //Get the delivery method from the repository using the DeliveryMethodId from the orderDto
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            // Calculate the subtotal by summing the price of each order item multiplied by its quantity
            var subtotal = orderItems.Sum(OI => OI.Price * OI.Quantity);
            // Create a new Order entity using the userEmail, address, delivery method, order items, and subtotal
            var order = new Order(userEmail, address, deliveryMethod, orderItems, subtotal, basket.PaymentIntetId);
            // Add the order to the repository
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();


            // Map the Order entity to an OrderToReturnDto and return it
            return _mapper.Map<Order, OrderToReturnDto>(order);

        }

        public async Task<OrderToReturnDto> GetOrderAsync(Guid id)
        {
            var specifications = new OrderSpecifications(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specifications);
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var specifications = new OrderSpecifications(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(specifications);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }
    }
}
