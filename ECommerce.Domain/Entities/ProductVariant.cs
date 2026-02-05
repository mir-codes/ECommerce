using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string VariantName { get; set; }

        public string Sku { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int InventoryQuantity { get; set; }

        public bool TrackInventory { get; set; } = true;
    }
}
