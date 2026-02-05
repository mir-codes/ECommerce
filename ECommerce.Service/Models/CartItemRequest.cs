namespace ECommerce.Service.Models
{
    public class CartItemRequest
    {
        public int ProductId { get; set; }
        public int? ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
