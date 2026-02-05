# ECommerce

ECommerce Website with .NET 6 and React.Js. Using Onion Architecture and WebApi

## Features
- Product, Order, Customer, Supplier management
- JWT Authentication
- Email notifications
- RESTful API
- Unit and Integration Tests

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

## Contributing
Pull requests are welcome. For major changes, please open an issue first.
