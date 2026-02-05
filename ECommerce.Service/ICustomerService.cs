using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Service
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Order>> GetOrderHistoryAsync(int customerId, CancellationToken cancellationToken);
        Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    }
}
