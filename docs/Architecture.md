# E-Commerce Architecture & Implementation Guide

## Onion Architecture Overview
- **Domain**: Entities, value objects, and domain events (business rules).
- **Service (Application)**: Use cases, DTOs, CQRS handlers, validation, and orchestration.
- **Persistence/Infrastructure**: EF Core, external integrations (Stripe, email), caching, logging.
- **API**: HTTP controllers, middleware, authentication, filters, and versioning.

## Repository Structure
```
ECommerce.API/                 # ASP.NET Core Web API
ECommerce.Domain/              # Entities, enums, domain events
ECommerce.Service/             # Application services and use cases
ECommerce.Persistence/         # EF Core DbContext + repositories
ECommerce.Infrastructure/      # External services (email, JWT, Stripe)
ECommerce.Test.Unit/           # Unit tests
ECommerce.Test.Integration/    # Integration tests
docs/                          # Architecture and design references
```

## Key Code Samples

### Domain: Product with variants, inventory, and images
The domain entity captures product lifecycle, stock, and variant pricing.

```csharp
public class Product : BaseEntity
{
    public string ProductName { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int CategoryId { get; set; }
    public ProductInventory Inventory { get; set; } = new();
    public List<ProductVariant> Variants { get; set; } = new();
    public List<ProductImage> Images { get; set; } = new();
}
```

### Domain: Order with status, totals, and payments
Orders retain a full audit trail of totals and payment attempts.

```csharp
public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Shipping { get; set; }
    public decimal Total { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new();
    public List<OrderPayment> Payments { get; set; } = new();
}
```

### Service (Application): Checkout orchestration (example shape)
The application layer should be idempotent for payment flows:
1. Create payment intent.
2. Persist payment record.
3. Confirm payment with Stripe webhook.
4. Transition order state only once.

```csharp
public record CheckoutRequest(int CustomerId, int OrderId, string PaymentMethodId);

public interface ICheckoutService
{
    Task<Order> StartCheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string providerPaymentId, CancellationToken cancellationToken);
}
```

### API: RESTful controller (example shape)
The API layer focuses on HTTP semantics and delegates to services.

```csharp
[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly ICheckoutService _checkoutService;

    [HttpPost("{orderId:int}/checkout")]
    public async Task<IActionResult> Checkout(int orderId, CheckoutRequest request, CancellationToken ct)
    {
        var order = await _checkoutService.StartCheckoutAsync(request, ct);
        return Accepted(order);
    }
}
```

## Frontend Architecture (React)
```
src/
  api/                      # Axios clients and interceptors
  app/                      # App shell, routes, providers
  features/
    catalog/
    cart/
    checkout/
    orders/
    auth/
  components/               # Shared UI components
  hooks/                    # Reusable hooks
  types/                    # TypeScript models
```

### Axios configuration
```
const api = axios.create({ baseURL: "/api/v1" });
api.interceptors.request.use((config) => {
  const token = authStore.getState().accessToken;
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});
```

## Scalability, Resilience, and Failure Handling
- **Idempotency**: use idempotency keys for checkout and Stripe confirmation.
- **Retry safety**: ensure Stripe webhooks can be retried without duplicating side effects.
- **Async background work**: queue email notifications to a background processor (Hangfire/Quartz).
- **Observability**: structured logs, correlation IDs, and metrics for API latency.
- **Caching**: cache catalog data with invalidation on product updates (Redis-ready).
- **API versioning**: `/api/v1` for backward compatibility.

## Security Practices
- JWT authentication with refresh token rotation.
- Password hashing (bcrypt/argon2) with lockout on repeated failures.
- Stripe webhook signature verification.
- Input validation and rate limiting.

