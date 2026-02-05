namespace ECommerce.Domain
{
    public enum PaymentStatus
    {
        Pending = 1,
        Authorized = 2,
        Succeeded = 3,
        Failed = 4,
        Refunded = 5
    }
}
