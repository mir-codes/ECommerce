using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? RequiredDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.PendingPayment;

        public string Currency { get; set; } = "USD";

        public decimal Subtotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal GrandTotal { get; set; }

        public int? ShippingAddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
    }
}
