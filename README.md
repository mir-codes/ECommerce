# ECommerce

ECommerce Website with .NET 6 and React.Js. Using Onion Architecture and WebApi

## Features
- Product, Order, Customer, Supplier management
- JWT Authentication
- Email notifications
- RESTful API
- Unit and Integration Tests
- Stripe-ready checkout flow with idempotency
- Soft delete + status management
- Caching and health checks

## Getting Started
1. Clone the repository
2. Update `appsettings.json` with your database, JWT, and email settings (see `appsettings.Sample.json`)
3. Build and run the solution
4. Access Swagger UI at `/swagger`

## Project Structure
- **API**: ASP.NET Core Web API
- **Domain**: Entity and value objects
- **Service**: Business logic
- **Persistence**: Data access
- **Infrastructure**: External services (email, JWT, etc.)
- **Test.Unit**: Unit tests
- **Test.Integration**: Integration tests
- **docs**: Architecture guide and implementation notes

## Architecture Guide
See [docs/Architecture.md](docs/Architecture.md) for the Onion Architecture breakdown, sample flows, and frontend organization.

```
ECommerce.API
  Controllers
  Middleware
ECommerce.Domain
  Entities
  Interfaces
  Settings
ECommerce.Service
  Models
ECommerce.Persistence
  ECommerceDbContext.cs
ECommerce.Infrastructure
  StripePaymentGateway.cs
```

## Core Domain Model (sample)
```csharp
public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public string OrderNumber { get; set; }
    public decimal GrandTotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
```

## Application Services (sample)
```csharp
public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
}
```

## API Controllers (sample)
```csharp
[HttpPost]
public async Task<ActionResult<OrderDto>> Create(CreateOrderRequest request, CancellationToken cancellationToken = default)
{
    var order = await _orderService.CreateOrderAsync(request, cancellationToken);
    return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
}
```

## Best Practices & Design Decisions
- Onion architecture: Domain holds entities/interfaces; Infrastructure contains external integrations; API stays thin.
- Async/await across repositories and services to support high concurrency.
- Soft delete and status fields to preserve history and enable audit flows.
- Correlation IDs and global exception middleware for observability and consistent error handling.
- Stripe integration abstracted behind `IPaymentGateway` for easy provider swaps.
- Repository + unit of work pattern for transaction boundaries.

## Scalability, Resilience & Failure Handling
- Stateless APIs with JWT allow horizontal scaling.
- Query pagination and filtering in repositories reduce load.
- Idempotency keys on checkout flows avoid double charges.
- Email notifications are async-ready and can be offloaded to background jobs.
- Health checks are exposed at `/health` for liveness/readiness probes.

## Suggested Frontend Layout (React)
```
src/
  app/
    api/
    hooks/
  features/
    products/
    checkout/
    orders/
  shared/
```

## Testing Strategy
- Unit tests target domain and service layers (business rules, pricing, coupons).
- Integration tests cover API endpoints and database persistence.
- External services (Stripe, Email) are mocked in tests.

## Contributing
Pull requests are welcome. For major changes, please open an issue first.
