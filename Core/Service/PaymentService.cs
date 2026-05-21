using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using Services.Abstractions;
using Shared.DataTransferObject.BasketModules;
using Stripe;

namespace Service
{
    internal class PaymentService(IConfiguration _configuration,
                                  IBasketRepository _basketRepository,
                                  IUnitOfWork _unitOfWork, 
                                  IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            var productRepo = _unitOfWork.GetRepository<Domain.Entities.ProductModule.Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }


            var deliveryMethodId = basket.DeliveryMethodId ?? throw new ArgumentNullException();
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(deliveryMethodId)
                                                  ?? throw new DeliveryMethodNotFoundException(deliveryMethodId);
            basket.ShippingPrice = deliveryMethod.Price;
            var amount = (long)(basket.Items.Sum(I => I.Quantity * I.Price) + basket.ShippingPrice) * 100; // 100 because stripe works with cents
            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntetId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "AED",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var intent = await service.CreateAsync(options);
                basket.PaymentIntetId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntetId, options);
            }
            await _basketRepository.CreateOrUpdateBasketAsync(basket);

            return _mapper.Map<Basket, BasketDto>(basket);
        }
         

        public async Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader)
        {
            var WebhookSecret = _configuration.GetSection("Stripe")["WebhookSigningSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeader, WebhookSecret);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    // Update order status to Payment Failed
                    await UpdatePaymentFailedAsync(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    // Update order status to Payment Succeeded
                    await UpdatePaymentRecievedAsync(paymentIntent.Id);
                    break;
                default:
                    Console.WriteLine("Unhandled stripe event type: " + stripeEvent.Type);
                    break;
            }
        }
        private async Task UpdatePaymentRecievedAsync(string PaymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithPaymentIntentSpecification(PaymentIntentId));
            order.Status = OrderStatus.PaymentReceived;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentFailedAsync(string PaymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithPaymentIntentSpecification(PaymentIntentId));
            order.Status = OrderStatus.PaymentFailed;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
