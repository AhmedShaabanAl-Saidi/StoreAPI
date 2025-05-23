# StoreAPI

A scalable and modular .NET RESTful API built with **Union Architecture**, designed for clean, maintainable, and extensible back-end development. This API is structured to support store operations such as product management, order processing, and secure payments, with support for modern practices like JWT authentication, Redis caching, and Stripe integration.

## 🧱 Architecture

This project follows the **Union Architecture** pattern, which separates the application into clearly defined layers:

- **Domain** – Core business logic and entities
- **Persistence** – Data access logic with Entity Framework Core
- **Services** – Business logic and service implementations
- **Services.Abstractions** – Interfaces for business services
- **Presentation** – API controllers and HTTP request handling
- **Store.API** – Application entry point and configuration
- **Shared** – Common utilities, constants, and helpers

## 🚀 Features

- ✅ JWT Authentication & Authorization
- 🚚 Stripe Payment Integration
- ⚡ Redis Caching
- 🔍 Specification Pattern for advanced querying
- 🧪 Unit-of-Work & Repository Pattern
- 🌐 Swagger UI for API testing and documentation
- 🗂 Pagination, Filtering, and Sorting
- 📦 Dependency Injection
- 🧪 Middleware for global error handling

## 🛠️ Tech Stack

- **.NET Core 8**
- **C#**
- **Entity Framework Core**
- **Redis**
- **Stripe.NET**
- **SQL Server**
- **Swagger (Swashbuckle)**

## 📁 Project Structure

```bash
StoreAPI/
│
├── Domain/                 # Core business models
├── Persistence/            # EF Core DbContext and repositories
├── Services/               # Service implementations
├── Services.Abstractions/ # Service interfaces
├── Shared/                 # Shared constants and helpers
├── Presentation/          # Controllers and request pipelines
├── Store.API/             # Main application setup
````

## ⚙️ Getting Started

### Prerequisites

* [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server)
* [Redis](https://redis.io/)
* [Stripe Account](https://stripe.com/) (for payment integration)

### Installation

1. **Clone the repository:**

```bash
git clone https://github.com/AhmedShaabanAl-Saidi/StoreAPI.git
cd StoreAPI
```

## 🙋‍♂️ Author

**Ahmed Shaaban Al-Saidi**
📧 [ahmedshaaban123321@gmail.com](mailto:ahmedshaaban123321@gmail.com)
🌐 [GitHub Profile](https://github.com/AhmedShaabanAl-Saidi)
