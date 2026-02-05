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
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("session")]
        public async Task<ActionResult<CheckoutSessionDto>> CreateSession(CreateCheckoutRequest request, CancellationToken cancellationToken = default)
        {
            var session = await _checkoutService.CreateCheckoutAsync(request, cancellationToken);
            return Ok(session);
        }

        [HttpPost("confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmPayment(ConfirmPaymentRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _checkoutService.ConfirmPaymentAsync(request, cancellationToken);
            return result ? Ok() : BadRequest();
        }
    }
}
