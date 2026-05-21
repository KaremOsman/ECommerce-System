using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.DataTransferObject.BasketModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketModel = _mapper.Map<Basket>(basket);
            var CreatedOrUpdatedBasket = _basketRepository.CreateOrUpdateBasketAsync(basketModel);
            if (CreatedOrUpdatedBasket is null)
                throw new Exception("An error occurred while creating or updating the basket, try Again Later.");
            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket = await _basketRepository.GetBasketAsync(key);
            if (basket == null)
                throw new BasketNotFoundException(key);
            return _mapper.Map<BasketDto>(basket);
        }
    }
}
