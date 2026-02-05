using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "money")]
        public decimal DiscountTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal ShippingTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public int? ShippingAddressId { get; set; }

        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
