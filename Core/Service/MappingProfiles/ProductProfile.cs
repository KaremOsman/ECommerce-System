using AutoMapper;
using Domain.Entities.ProductModule;
using Shared.DataTransferObject.ProductModules;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand != null ? src.Brand.Name : "Have No Brand"))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type != null ? src.Type.Name : "Have No Type"))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<CreateOrUpdateProductDto, Product>()
                .ForMember(dest => dest.PictureUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProductBrand, BrandDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
            CreateMap<ProductType, TypeDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
