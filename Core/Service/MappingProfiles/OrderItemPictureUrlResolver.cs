using AutoMapper;
using Domain.Entities.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderModule;

namespace Service.MappingProfiles
{
    public class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            return $"{_configuration.GetSection("Urls")["BaseUrl"]}Data/{source.Product.PictureUrl}";
        }
    }
}
