using ECommerce.Domain.Entities;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public interface ICartService
    {
        Task<Cart> GetOrCreateCartAsync(int customerId);

        Task<Cart> AddItemAsync(int customerId, int productId, int quantity);

        Task<Cart> RemoveItemAsync(int customerId, int productId);
    }
}
