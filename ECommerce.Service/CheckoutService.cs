using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentGateway _paymentGateway;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutService(
            IRepository<Payment> paymentRepository,
            IOrderRepository orderRepository,
            IPaymentGateway paymentGateway,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _paymentGateway = paymentGateway;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CheckoutSessionDto> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            var session = await _paymentGateway.CreateCheckoutSessionAsync(new PaymentCheckoutRequest
            {
                OrderId = request.OrderId,
                IdempotencyKey = request.IdempotencyKey,
                SuccessUrl = request.SuccessUrl,
                CancelUrl = request.CancelUrl
            }, cancellationToken);

            var payment = new Payment
            {
                OrderId = order.Id,
                Provider = "Stripe",
                ProviderReference = session.SessionId,
                Amount = order.GrandTotal,
                Status = PaymentStatus.Pending,
                IdempotencyKey = request.IdempotencyKey
            };

            await _paymentRepository.AddAsync(payment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CheckoutSessionDto
            {
                SessionId = session.SessionId,
                CheckoutUrl = session.CheckoutUrl
            };
        }

        public async Task<bool> ConfirmPaymentAsync(ConfirmPaymentRequest request, CancellationToken cancellationToken = default)
        {
            var confirmed = await _paymentGateway.ConfirmPaymentAsync(new PaymentConfirmation
            {
                PaymentIntentId = request.PaymentIntentId,
                IdempotencyKey = request.IdempotencyKey,
                IsSuccess = request.IsSuccess,
                FailureReason = request.FailureReason
            }, cancellationToken);

            if (confirmed)
            {
                await _emailService.SendEmailAsync("customer@example.com", "Payment Success", "Your payment was successful.", cancellationToken);
            }
            else
            {
                await _emailService.SendEmailAsync("customer@example.com", "Payment Failed", "Your payment failed. We will retry.", cancellationToken);
            }

            return confirmed;
        }
    }
}
