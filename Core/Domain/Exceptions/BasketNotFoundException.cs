namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException(string Id) : NotFoundException($"Basket with id ' {Id} ' not found")
    {

    }
}
