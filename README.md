# microservices-dotnet

## Overview

This project demonstrates a microservices architecture built with modern technologies for building scalable, cloud-native e-commerce applications. The system is designed to showcase best practices in distributed systems, containerization, and enterprise-grade integration patterns.

## Technology Stack

### Backend
- **.NET** - Microservices implementation using ASP.NET Core
- **Entity Framework Core** - ORM for database operations
- **JWT Authentication** - Secure token-based authentication

### Frontend
- **Angular** - Modern SPA framework for the e-commerce interface

### Current Microservices
- **Products Service** - Manages product catalog, inventory, and stock operations
- **Users Service** - Handles user authentication, authorization, and profile management

## Architecture & Deployment

### Current Implementation
- RESTful API design
- Microservices pattern with independent services
- Entity Framework Core with migrations
- Exception handling middleware
- Input validation

### Planned Enhancements

#### Cloud Infrastructure
- **Azure Kubernetes Service (AKS)** - Container orchestration and deployment
- **Docker** - Containerization of all services

#### Messaging & Communication
- **RabbitMQ** - Message broker for asynchronous communication between services
- **Event-Driven Architecture** - Implementing event sourcing and CQRS patterns

#### Security & Identity
- **Microsoft Entra ID** (formerly Azure AD) - Enterprise identity and access management

#### Resilience
- **Polly** - Resilience and transient-fault-handling library
  - Retry policies
  - Circuit breaker patterns
  - Timeout policies
  - Fallback strategies

## Project Structure

```
├── eCommerce/                          # Angular frontend application
├── eCommerce.ProductsService/          # Products microservice
│   ├── eCommerce.Api/                  # API layer
│   ├── eCommerce.Core/                 # Domain entities and interfaces
│   └── eCommerce.Infraestructure/      # Data access and external services
└── eCommerce.UsersService/             # Users microservice
    ├── eCommerce.Api/                  # API layer
    ├── eCommerce.Core/                 # Domain entities and interfaces
    └── eCommerce.Infraestructure/      # Data access and external services
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Node.js and npm
- SQL Server (or configured database provider)
- Docker (for containerization)

### Running the Services

#### Products Service
```bash
cd eCommerce.ProductsService/eCommerce.Api
dotnet restore
dotnet run
```

#### Users Service
```bash
cd eCommerce.UsersService/eCommerce.Api
dotnet restore
dotnet run
```

#### Frontend Application
```bash
cd eCommerce
npm install
ng serve
```

## Development Roadmap

- [x] Basic microservices architecture with .NET
- [x] Angular frontend
- [x] RESTful APIs
- [x] Entity Framework Core integration
- [ ] Docker containerization
- [ ] Kubernetes deployment to Azure
- [ ] RabbitMQ integration
- [ ] Event-Driven Architecture implementation
- [ ] Microsoft Entra ID integration
- [ ] Polly resilience patterns
- [ ] API Gateway
- [ ] Distributed tracing and monitoring

## Contributing

This is a learning project showcasing modern microservices patterns and cloud-native development practices.

## License

See LICENSE file for details.