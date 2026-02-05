using ECommerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var cart = await _cartService.GetOrCreateCartAsync(customerId);
            return Ok(cart);
        }

        [HttpPost("{customerId:int}/items")]
        public async Task<IActionResult> AddItem(int customerId, [FromQuery] int productId, [FromQuery] int quantity = 1)
        {
            var cart = await _cartService.AddItemAsync(customerId, productId, quantity);
            return Ok(cart);
        }

        [HttpDelete("{customerId:int}/items/{productId:int}")]
        public async Task<IActionResult> RemoveItem(int customerId, int productId)
        {
            var cart = await _cartService.RemoveItemAsync(customerId, productId);
            return Ok(cart);
        }
    }
}
