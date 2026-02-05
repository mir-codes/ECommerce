using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetOrCreateCartAsync(int customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart != null)
            {
                return cart;
            }

            cart = new Cart { CustomerId = customerId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> AddItemAsync(int customerId, int productId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(customerId);
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem == null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> RemoveItemAsync(int customerId, int productId)
        {
            var cart = await GetOrCreateCartAsync(customerId);
            var item = cart.Items.FirstOrDefault(cartItem => cartItem.ProductId == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
