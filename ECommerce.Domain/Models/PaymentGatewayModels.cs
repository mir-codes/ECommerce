namespace ECommerce.Domain.Models
{
    public class PaymentCheckoutRequest
    {
        public int OrderId { get; set; }
        public string IdempotencyKey { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }

    public class PaymentCheckoutSession
    {
        public string SessionId { get; set; }
        public string CheckoutUrl { get; set; }
    }

    public class PaymentConfirmation
    {
        public string PaymentIntentId { get; set; }
        public string IdempotencyKey { get; set; }
        public bool IsSuccess { get; set; }
        public string FailureReason { get; set; }
    }
}
