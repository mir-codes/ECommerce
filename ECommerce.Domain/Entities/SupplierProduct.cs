namespace ECommerce.Domain.Entities
{
    public class SupplierProduct : BaseEntity
    {
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string SupplierSku { get; set; }
    }
}
