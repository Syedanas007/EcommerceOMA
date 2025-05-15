
# E-Commerce Microservices Project

## Overview

This solution implements a microservices-based architecture for an e-commerce domain with the following components:

- **Category Microservice**  
  - CRUD operations for product categories  
  - Uses Factory Pattern  
  - JWT Authentication  
  - EF Core with Stored Procedures  
  - .NET Core 7.0  
  - Swagger UI disabled for security  

- **Product Microservice**  
  - CRUD operations for products  
  - Uses CQRS Pattern with MediatR  
  - JWT Authentication  
  - EF Core with Stored Procedures  
  - Supports JPEG product images  
  - .NET Core 7.0  
  - Swagger UI disabled for security  

- **Company Microservice**  
  - CRUD operations for companies  
  - Uses Factory Pattern  
  - JWT Authentication  
  - EF Core with Stored Procedures  
  - .NET Core 7.0  
  - Swagger UI disabled for security  

- **API Gateway**  
  - Routes requests to microservices  
  - Centralized JWT Authentication and Authorization  
  - Built using Ocelot  

- **User Interface**  
  - ASP.NET Core MVC/Razor Pages  
  - Consumes APIs via API Gateway  
  - Uses SweetAlert for notifications  

---

## Architecture Diagram

```
[UI] <--> [API Gateway] <--> [Category MS]  
                             [Product MS]  
                             [Company MS]
```

---

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)  
- SQL Server (or compatible database)  
- Visual Studio 2022 or VS Code  
- Node.js & npm (for front-end if applicable)  

---

## Getting Started

### Clone the repository

```bash
git clone https://github.com/yourusername/ecommerce-microservices.git
cd ecommerce-microservices
```

### Database Setup

- Create databases for each microservice.
- Run the provided SQL scripts to create tables and stored procedures.

### Environment Variables / App Settings

Each microservice requires configuration for:

- Database connection string  
- JWT Secret Key and Token settings  

Example in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CategoryDb;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "SecretKey": "YourVeryStrongSecretKeyHere",
    "Issuer": "ECommerceAPI",
    "Audience": "ECommerceClient",
    "ExpiryMinutes": 60
  }
}
```

### Running Microservices

Open each microservice folder and run:

```bash
dotnet run
```

### Running API Gateway

Navigate to the API Gateway folder and run:

```bash
dotnet run
```

Ensure `ocelot.json` is configured correctly for downstream services.

### Running UI

Navigate to the UI project and run:

```bash
dotnet run
```

---

## Features

### Category Microservice

- CRUD with Factory Pattern  
- JWT Authentication  
- Stored procedures for data access  
- Global Exception Handling  

### Product Microservice

- CRUD with CQRS Pattern (using MediatR)  
- JWT Authentication  
- Stored procedures for data access  
- JPEG image upload validation  
- Global Exception Handling  

### Company Microservice

- CRUD with Factory Pattern  
- JWT Authentication  
- Stored procedures for data access  
- Global Exception Handling  

### API Gateway

- Ocelot-based routing  
- Central JWT Authentication & Authorization  
- No Swagger UI exposed  

### User Interface

- ASP.NET Core MVC / Razor Pages  
- Consumes APIs via Gateway  
- SweetAlert notifications for CRUD feedback  

---

## Design Patterns Used

| Microservice | Pattern       |
|--------------|---------------|
| Category     | Factory       |
| Product      | CQRS + MediatR|
| Company      | Factory       |

---

## Exception Handling

- Global exception middleware logs errors and returns formatted JSON responses.
- All microservices have consistent error response formats.

---

## Security

- JWT token issuance and validation
- Authorization handled both at microservices and API Gateway level
- Swagger UI disabled in all microservices to avoid direct API exposure

---

## Versioning and Branching Strategy

- `main` branch: Stable production-ready code  
- `dev` branch: Active development  
- Feature branches named: `feature/category-crud`, `feature/product-cqrs`, etc.

### Git Tags Example:

- `v0.1-category-crud`  
- `v0.2-product-cqrs`

Each commit includes descriptive messages about features and bug fixes.

---

## Future Enhancements

- Implement event-driven communication (e.g., with RabbitMQ)  
- Add integration and unit tests  
- Dockerize each microservice and orchestrate with Kubernetes  
- Implement role-based access control (RBAC)  
- Enhance UI with React or Angular  

---

## Contact

For issues or questions, please contact:  
**Syed Mohammed Anas** – sayedmohammedanas@gmail.com

---

## License

This project is licensed under the MIT License.
