using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public bool TrackInventory { get; set; } = true;
        public int ReorderPoint { get; set; } = 5;

        public Category Category { get; set; }
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
    }
}
