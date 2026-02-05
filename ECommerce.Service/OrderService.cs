using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain;
using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using ECommerce.Service.Models;
using ECommerce.Service.Payments;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IPaymentService _paymentService;

        public OrderService(ECommerceDbContext dbContext, IPaymentService paymentService)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        public async Task<Order> CheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .Where(product => request.Items.Select(item => item.ProductId).Contains(product.Id))
                .ToListAsync(cancellationToken);

            var order = new Order
            {
                CustomerId = request.CustomerId,
                ShippingAddressId = request.ShippingAddressId,
                Currency = string.IsNullOrWhiteSpace(request.Currency) ? "USD" : request.Currency,
                Status = OrderStatus.PendingPayment
            };

            foreach (var item in request.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                {
                    continue;
                }

                var lineTotal = product.UnitPrice * item.Quantity;
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.UnitPrice,
                    LineTotal = lineTotal
                });
            }

            order.Subtotal = order.OrderDetails.Sum(detail => detail.LineTotal);
            order.GrandTotal = order.Subtotal + order.TaxTotal + order.ShippingTotal - order.DiscountTotal;

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var idempotencyKey = string.IsNullOrWhiteSpace(request.IdempotencyKey)
                ? Guid.NewGuid().ToString("N")
                : request.IdempotencyKey;

            var payment = await _paymentService.CreatePaymentIntentAsync(order, idempotencyKey, cancellationToken);
            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }

        public Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            return _dbContext.Orders
                .Include(order => order.OrderDetails)
                .Include(order => order.Payments)
                .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
        }
    }
}
