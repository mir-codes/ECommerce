# ECommerce

Production-ready E-Commerce platform built with .NET 6 (Web API) and React, following Onion Architecture.

## Features
- Product management with categories, variants, inventory, soft deletes, and images
- Customer profiles, address book, order history, wishlist, and recently viewed products
- Supplier onboarding and supplier-product mapping
- Order lifecycle with checkout and payment intent handling
- JWT authentication, role-based authorization, and correlation IDs
- Background email notifications (non-blocking)
- Caching, pagination readiness, and health checks

## Getting Started
1. Clone the repository
2. Update `appsettings.json` with your database, JWT, and email settings (see `appsettings.Sample.json`)
3. Build and run the solution
4. Access Swagger UI at `/swagger`

## Project Structure
```
ECommerce.API/            # ASP.NET Core Web API (controllers, middleware, background services)
ECommerce.Domain/         # Entities, value objects, enums, auth models
ECommerce.Service/        # Application services, DTOs, payment abstractions
ECommerce.Persistence/    # EF Core DbContext + repositories
ECommerce.Infrastructure/ # External services (email, JWT, Stripe)
ECommerce.Frontend/       # React app (features-based structure)
ECommerce.Test.Unit/      # Unit tests
ECommerce.Test.Integration/# Integration tests
```

## Architectural Notes
- Onion Architecture keeps Domain isolated; Service depends on Domain and Persistence.
- Infrastructure implements Service abstractions (e.g., payment gateway).
- API layer composes everything with DI, middleware, and versioned routes.

## Contributing
Pull requests are welcome. For major changes, please open an issue first.
