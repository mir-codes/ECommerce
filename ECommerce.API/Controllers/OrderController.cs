using ECommerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{customerId:int}")]
        public async Task<IActionResult> CreateOrder(int customerId)
        {
            var order = await _orderService.CreateOrderFromCartAsync(customerId);
            return Ok(order);
        }

        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPatch("{orderId:int}/status")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromQuery] string status)
        {
            await _orderService.UpdateStatusAsync(orderId, status);
            return NoContent();
        }
    }
}
