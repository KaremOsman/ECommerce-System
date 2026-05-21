using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository,
                                UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IFileService _fileService) : IServiceManager
    {
        // 1. Product Service
        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper, _fileService));
        public IProductService ProductService => _LazyproductService.Value ;
        
        // 2. Basket Service
        private readonly Lazy<IBasketService> _LazybasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketService => _LazybasketService.Value ;
        
        // 3. Authentication Service
        private readonly Lazy<IAuthenticationService> _LazyauthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration, _mapper));
        public IAuthenticationService AuthenticationService => _LazyauthenticationService.Value;
        
        // 4. Order Service
        private readonly Lazy<IOrderService> _LazyorderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepository, _unitOfWork));
        public IOrderService OrderService => _LazyorderService.Value;

        // 5. Paymetn Service
        private readonly Lazy<IPaymentService> _paymentService = new Lazy<IPaymentService>(() => new PaymentService (_configuration, _basketRepository, _unitOfWork, _mapper ));
        public IPaymentService PaymentService => _paymentService.Value;
        // 6. Brands Service
        private readonly Lazy<IBrandService> _LazyBrandService = new Lazy<IBrandService>(() => new  BrandService(_unitOfWork, _mapper));
        public IBrandService brandService => _LazyBrandService.Value;

        // 7. Types Service
        private readonly Lazy<ITypeService> _LazyTypeService = new Lazy<ITypeService>(() => new TypeService(_unitOfWork, _mapper));
        public ITypeService typeService => _LazyTypeService.Value;

    }
}
