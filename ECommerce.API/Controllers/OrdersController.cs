using System.Threading;
using System.Threading.Tasks;
using ECommerce.Service;
using ECommerce.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            var order = await _orderService.CreateOrderAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var order = await _orderService.GetByIdAsync(id, cancellationToken);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("history/{customerId:int}")]
        public async Task<ActionResult<PagedResult<OrderDto>>> GetHistory(int customerId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var history = await _orderService.GetHistoryAsync(customerId, page, pageSize, cancellationToken);
            return Ok(history);
        }
    }
}
