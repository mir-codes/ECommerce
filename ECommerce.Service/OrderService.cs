using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;
using ECommerce.Domain.Interfaces;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}".Substring(0, 23),
                Status = OrderStatus.AwaitingPayment,
                ShippingAddressId = request.ShippingAddressId
            };

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                {
                    throw new InvalidOperationException("Product not found for order.");
                }

                var unitPrice = product.UnitPrice;
                var lineTotal = unitPrice * item.Quantity;
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    LineTotal = lineTotal
                });
            }

            order.Subtotal = order.Items.Sum(i => i.LineTotal);
            order.DiscountTotal = 0;
            order.TaxTotal = Math.Round(order.Subtotal * 0.07m, 2);
            order.ShippingTotal = 10;
            order.GrandTotal = order.Subtotal - order.DiscountTotal + order.TaxTotal + order.ShippingTotal;

            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapOrder(order);
        }

        public async Task<OrderDto> GetByIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdWithItemsAsync(orderId, cancellationToken);
            return order == null ? null : MapOrder(order);
        }

        public async Task<PagedResult<OrderDto>> GetHistoryAsync(int customerId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var skip = (page - 1) * pageSize;
            var orders = await _orderRepository.GetByCustomerAsync(customerId, skip, pageSize, cancellationToken);

            return new PagedResult<OrderDto>
            {
                Items = orders.Select(MapOrder).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = orders.Count
            };
        }

        private static OrderDto MapOrder(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status.ToString(),
                GrandTotal = order.GrandTotal,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.LineTotal
                }).ToList()
            };
        }
    }
}
