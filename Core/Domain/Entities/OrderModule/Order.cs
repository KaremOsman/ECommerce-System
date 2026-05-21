namespace Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
        }

        // Parameterless constructor for EF Core

        // Constructor to initialize the Order with necessary properties
        public Order(string userEmail, OrderAddress address, DeliveryMethod delivaryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = delivaryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress Address { get; set; } = default!;
        public int DeliveryMethodId { get; set; } // Foreign key for DeliveryMethod
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public string PaymentIntentId { get; set; } = default!;
        public decimal SubTotal { get; set; }
        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Price; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price; // as method to calculate total
    }
}
