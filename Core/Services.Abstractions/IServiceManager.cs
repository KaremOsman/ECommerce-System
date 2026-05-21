using Service;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IOrderService OrderService { get; }
        public IPaymentService PaymentService { get; }
        public IBrandService brandService { get; }
        public ITypeService typeService { get; }

    }
}
