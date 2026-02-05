using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderFromCartAsync(int customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.AwaitingPayment,
                OrderDetails = cart.Items.Select(item => new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = item.UnitPrice * item.Quantity
                }).ToList()
            };

            order.Subtotal = order.OrderDetails.Sum(detail => detail.LineTotal);
            order.GrandTotal = order.Subtotal + order.TaxTotal - order.DiscountTotal;

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();

            return order;
        }

        public Task<Order?> GetByIdAsync(int orderId) =>
            _context.Orders
                .Include(order => order.OrderDetails)
                .Include(order => order.Payment)
                .FirstOrDefaultAsync(order => order.Id == orderId);

        public async Task UpdateStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(existing => existing.Id == orderId);
            if (order == null)
            {
                return;
            }

            if (Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
            {
                order.Status = parsedStatus;
            }

            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
