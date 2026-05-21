using Domain.Entities.OrderModule;

namespace Service.Specifications
{
    internal class OrderWithPaymentIntentSpecification : BaseSpecification<Order, Guid>
    {
        public OrderWithPaymentIntentSpecification(String paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
