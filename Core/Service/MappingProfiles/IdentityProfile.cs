using AutoMapper;
using Domain.Entities.IdentityModule;
using Shared.DataTransferObject.IdentityModules;

namespace Service.MappingProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
