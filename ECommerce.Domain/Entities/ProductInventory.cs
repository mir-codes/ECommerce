namespace ECommerce.Domain.Entities
{
    public class ProductInventory
    {
        public int OnHand { get; set; }

        public int Reserved { get; set; }

        public int LowStockThreshold { get; set; }
    }
}
