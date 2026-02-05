namespace ECommerce.Domain.Enum
{
    public enum OrderStatus
    {
        Pending = 0,
        AwaitingPayment = 1,
        Paid = 2,
        Processing = 3,
        Shipped = 4,
        Delivered = 5,
        Cancelled = 6,
        Refunded = 7
    }
}
