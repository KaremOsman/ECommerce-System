using Shared.DataTransferObject.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderModule
{
    // This DTO is used to create an order. It contains the necessary information to create an order, such as the basket ID, delivery method ID, and shipping address.
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public AddressDto Address { get; set; } = default!;
    }
}
