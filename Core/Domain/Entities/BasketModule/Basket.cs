namespace Domain.Entities.BasketModule
{
    public class Basket
    {
        public string Id { get; set; } // GUID, Created From Client Side[Frontend].
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? PaymentIntetId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
