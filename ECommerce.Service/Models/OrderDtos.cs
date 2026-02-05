using System.Collections.Generic;

namespace ECommerce.Service.Models
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }
        public string CouponCode { get; set; }
        public IReadOnlyList<CreateOrderItemRequest> Items { get; set; }
    }

    public class CreateOrderItemRequest
    {
        public int ProductId { get; set; }
        public int? ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public decimal GrandTotal { get; set; }
        public IReadOnlyList<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
