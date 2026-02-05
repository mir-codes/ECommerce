# ECommerce

Production-ready E-Commerce platform blueprint using ASP.NET Core 6, React, Onion Architecture, EF Core (SQL Server), and JWT authentication. This repository includes a runnable API foundation with domain entities, persistence, and service layers, plus guidance for the React front end and platform hardening.

## ✅ Core Functional Modules (Design + Implementation Skeleton)
- **Product Management**: Categories, variants, pricing, inventory, soft delete, status, images
- **Customer Management**: Registration/login (JWT), profile, addresses, order history
- **Supplier Management**: Supplier onboarding + supplier/product mapping
- **Order Management**: Cart/checkout, order lifecycle, order history
- **Payments**: Stripe-ready payment tracking with idempotency keys
- **Notifications**: Email service hooks + background job friendly

---

## 🧱 Onion Architecture (Layered Structure)

```
ECommerce.API/            # Controllers, middleware, startup
ECommerce.Domain/         # Entities, enums, value objects, interfaces
ECommerce.Service/        # Application services (business logic)
ECommerce.Persistence/    # EF Core DbContext + repositories
ECommerce.Infrastructure/ # Email, JWT, Stripe clients, external services
ECommerce.Test.Unit/      # Unit tests
ECommerce.Test.Integration/# API + DB integration tests
```

### Key Design Decisions
- **Domain-first** with explicit entities and enums.
- **Application services** isolate orchestration (cart/order flow, inventory, payments).
- **Infrastructure adapters** encapsulate external integrations (Stripe, email, caching).
- **API layer** is thin — controllers delegate to services.

---

## 📦 Entity & Domain Samples

### Product + Variants + Images (Domain)
```
public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public string Sku { get; set; }
    public decimal UnitPrice { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Draft;
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}
```

### Order Lifecycle (Domain + Enum)
```
public enum OrderStatus
{
    Pending = 0,
    AwaitingPayment = 1,
    Paid = 2,
    Processing = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6,
    Refunded = 7
}
```

---

## ✅ Service Layer Sample

### Order Service (Cart → Order → Totals)
```
public async Task<Order> CreateOrderFromCartAsync(int customerId)
{
    var cart = await _context.Carts.Include(c => c.Items)
        .FirstOrDefaultAsync(c => c.CustomerId == customerId);

    if (cart == null || !cart.Items.Any())
        throw new InvalidOperationException("Cart is empty.");

    var order = new Order
    {
        CustomerId = customerId,
        OrderDate = DateTime.UtcNow,
        Status = OrderStatus.AwaitingPayment,
        OrderDetails = cart.Items.Select(item => new OrderDetail
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            LineTotal = item.UnitPrice * item.Quantity
        }).ToList()
    };

    order.Subtotal = order.OrderDetails.Sum(d => d.LineTotal);
    order.GrandTotal = order.Subtotal + order.TaxTotal - order.DiscountTotal;

    _context.Orders.Add(order);
    _context.CartItems.RemoveRange(cart.Items);
    await _context.SaveChangesAsync();

    return order;
}
```

---

## ✅ API Sample

### Order Controller
```
[HttpPost("{customerId:int}")]
public async Task<IActionResult> CreateOrder(int customerId)
{
    var order = await _orderService.CreateOrderFromCartAsync(customerId);
    return Ok(order);
}
```

### Global Middleware
```
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

---

## 🔐 Security
- JWT-based auth with issuer + signing key
- Password hashing (BCrypt / Identity)
- Refresh token rotation
- Role & policy-based authorization
- Account lockout + audit logging
- Stripe webhook signature validation

---

## ⚡ Performance & Scalability
- Async/await everywhere
- Pagination / filtering / sorting in APIs
- Caching abstraction (in-memory now, Redis-ready)
- EF Core indexing on SKU and composite keys
- Stateless API ready for horizontal scaling

---

## 📊 Observability
- Correlation IDs in every request
- Global exception handling
- Health checks endpoint at `/health`
- Serilog / OpenTelemetry-ready (addable via Infrastructure)

---

## ✅ Testing Strategy
- Unit tests for domain + services (pure logic)
- Integration tests for API/DB
- Mock external services (Stripe/email)

---

## 🌐 React Frontend (Hooks-Based Structure)

**Suggested Feature-Based Structure**
```
/src
  /features
    /catalog
    /cart
    /checkout
    /orders
  /components
  /services
  /hooks
```

### Axios Client (JWT + retry)
```
const api = axios.create({ baseURL: import.meta.env.VITE_API_URL });
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});
```

### Checkout UX Example
```
const { mutateAsync, isLoading } = useCheckout();
const onCheckout = async () => {
  try { await mutateAsync(payload); }
  catch (err) { toast.error("Payment failed."); }
};
```

---

## 🚀 Configuration
- `appsettings.json` holds DB + JWT
- Environment-based config supported
- Swagger enabled by default

---

## ✅ Next Steps (Optional Enhancements)
- Add Stripe client + webhook handlers
- Background job processing (Hangfire/Quartz)
- Redis distributed caching
- API versioning + gateway readiness
- Domain events + CQRS (MediatR)

---

## Running
1. Update `appsettings.json` with your DB + JWT configuration.
2. Run the API project and open Swagger `/swagger`.

---

## Contributing
Pull requests welcome.
