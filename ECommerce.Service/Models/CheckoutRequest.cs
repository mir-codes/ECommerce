using System.Collections.Generic;

namespace ECommerce.Service.Models
{
    public class CheckoutRequest
    {
        public int CustomerId { get; set; }
        public int? ShippingAddressId { get; set; }
        public string CouponCode { get; set; }
        public string Currency { get; set; } = "USD";
        public List<CartItemRequest> Items { get; set; } = new();
        public string IdempotencyKey { get; set; }
    }
}
