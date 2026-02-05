using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }
    }
}
