using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using Services.Abstractions;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            // Register application services here

            Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(AssemblyReference).Assembly);
            //Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            Services.AddScoped<IServiceManager, ServiceManager>();

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(Provider => () => Provider.GetRequiredService<IBasketService>());
            
            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped<Func<IBrandService>>(Provider => () => Provider.GetRequiredService<IBrandService>());
            Services.AddScoped<ITypeService, TypeService>();
            Services.AddScoped<Func<ITypeService>>(Provider => () => Provider.GetRequiredService<ITypeService>());

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(Provider => () => Provider.GetRequiredService<IProductService>());
           
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(Provider => () => Provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(Provider => () => Provider.GetRequiredService<IOrderService>());
           
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(Provider => () => Provider.GetRequiredService<IPaymentService>());
           
            Services.AddScoped<ICacheService, CacheService>();

            return Services;
        }
    }
}
