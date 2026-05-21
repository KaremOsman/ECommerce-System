using AutoMapper;
using Domain.Entities.BasketModule;
using Shared.DataTransferObject.BasketModules;

namespace Service.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketDto, Basket>();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
