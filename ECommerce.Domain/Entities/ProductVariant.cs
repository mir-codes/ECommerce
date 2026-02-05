using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        [Column(TypeName = "money")]
        public decimal PriceDelta { get; set; }

        public int InventoryQuantity { get; set; }

        public Product Product { get; set; }
    }
}
