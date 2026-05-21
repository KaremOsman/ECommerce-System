using Services.Abstractions;

namespace Service
{
    internal class ServiceManagerWithFactoryDelegate( Func<IProductService> _productServiceFactory,
                                                      Func<IAuthenticationService> _AuthenticationServiceFactory,
                                                      Func<IOrderService> _OrderServiceFactory,
                                                      Func<IBasketService> _BasketServiceFactory,
                                                      Func<IPaymentService> _paymentServiceFactory,
                                                      Func<IBrandService> _brandServiceFactory,
                                                      Func<ITypeService> _typeServiceFactory) : IServiceManager
    { 
        public IAuthenticationService AuthenticationService => _AuthenticationServiceFactory();
        public IProductService ProductService => _productServiceFactory(); // _ProductServiceFactory() = _productServiceFactory.Invoke() with syntax sugar
        public IBasketService BasketService => _BasketServiceFactory();
        public IOrderService OrderService => _OrderServiceFactory();
        public IPaymentService PaymentService => _paymentServiceFactory();
        public IBrandService brandService => _brandServiceFactory();
        public ITypeService typeService => _typeServiceFactory();

    }
}
 