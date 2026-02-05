namespace ECommerce.Domain.Entities
{
    public class InventoryItem : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReorderPoint { get; set; }
    }
}
