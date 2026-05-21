using AutoMapper;
using Domain.Entities.OrderModule;
using Shared.DataTransferObject.IdentityModules;
using Shared.DataTransferObject.OrderModule;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
