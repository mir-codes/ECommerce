using System.Threading;
using System.Threading.Tasks;
using ECommerce.API.Services;
using ECommerce.Service;
using ECommerce.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IEmailQueue _emailQueue;

        public OrdersController(IOrderService orderService, IEmailQueue emailQueue)
        {
            _orderService = orderService;
            _emailQueue = emailQueue;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderService.CheckoutAsync(request, cancellationToken);

            await _emailQueue.QueueEmailAsync(
                to: "customer@example.com",
                subject: "Order placed",
                body: $"Your order #{order.Id} has been placed.",
                cancellationToken: cancellationToken);

            return Accepted(new { order.Id, order.Status, order.GrandTotal });
        }

        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetOrder(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderAsync(orderId, cancellationToken);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}
