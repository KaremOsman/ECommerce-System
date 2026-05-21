using Domain.Entities.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string basketId);
        Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
