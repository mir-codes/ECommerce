using System;
using System.Collections.Generic;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Customer? Customers { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Draft;
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
        public List<OrderPayment> Payments { get; set; } = new();

    }
}
