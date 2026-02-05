namespace ECommerce.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public string VariantName { get; set; } = string.Empty;

        public string? VariantSku { get; set; }

        public decimal? PriceOverride { get; set; }

        public ProductInventory Inventory { get; set; } = new();
    }
}
