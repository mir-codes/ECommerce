using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetCustomer(int customerId, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerAsync(customerId, cancellationToken);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{customerId:int}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] Customer customer, CancellationToken cancellationToken)
        {
            if (customerId != customer.Id)
            {
                return BadRequest();
            }

            await _customerService.UpdateCustomerAsync(customer, cancellationToken);
            return NoContent();
        }

        [HttpGet("{customerId:int}/orders")]
        public async Task<IActionResult> GetOrderHistory(int customerId, CancellationToken cancellationToken)
        {
            var orders = await _customerService.GetOrderHistoryAsync(customerId, cancellationToken);
            return Ok(orders);
        }
    }
}
