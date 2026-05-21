using AdminDashboard.Models;
using AutoMapper;
using Domain.Entities.ProductModule;

namespace AdminDashboard.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}
