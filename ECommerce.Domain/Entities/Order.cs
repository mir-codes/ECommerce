using System;
using System.Collections.Generic;
using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string Currency { get; set; } = "USD";

        public decimal Subtotal { get; set; }

        public decimal DiscountTotal { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal GrandTotal { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new();

        public Payment Payment { get; set; }
    }
}
