using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Service
{
    public interface ISupplierService
    {
        Task<IReadOnlyList<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken);
        Task<Supplier> GetSupplierAsync(int supplierId, CancellationToken cancellationToken);
        Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
    }
}
