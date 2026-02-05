using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/suppliers")]
    [Authorize(Roles = "Admin")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
        {
            var suppliers = await _supplierService.GetSuppliersAsync(cancellationToken);
            return Ok(suppliers);
        }

        [HttpGet("{supplierId:int}")]
        public async Task<IActionResult> GetSupplier(int supplierId, CancellationToken cancellationToken)
        {
            var supplier = await _supplierService.GetSupplierAsync(supplierId, cancellationToken);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] Supplier supplier, CancellationToken cancellationToken)
        {
            await _supplierService.AddSupplierAsync(supplier, cancellationToken);
            return CreatedAtAction(nameof(GetSupplier), new { supplierId = supplier.Id }, supplier);
        }
    }
}
