using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.BasketModules
{
    public class BasketDto
    {
        [Required]
        public string Id { get; set; } = null!;
        public ICollection<BasketItemDto> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
