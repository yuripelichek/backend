# Ambev Developer Evaluation

A .NET 8 Web API project for the Ambev Developer Evaluation.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [PowerShell](https://github.com/PowerShell/PowerShell/releases) (Windows)
- [Git](https://git-scm.com/downloads)

## Getting Started

1. Clone the repository:

2. Make sure Docker Desktop is running on your machine.

3. Build and start the containers:
```powershell
docker-compose -f docker-compose.yml up --build -d
```

This command will:
- Create a Docker network for the application
- Create necessary volumes for data persistence
- Build and start the following containers:
  - PostgreSQL database (port 5433)
  - Web API (port 5000)

4. Check if all containers are running:
```powershell
docker-compose -f docker-compose.yml ps
```

5. Monitor the API logs:
```powershell
docker-compose -f docker-compose.yml logs -f ambev.developerevaluation.webapi
```

## Development

The application is structured as follows:

```
src/
├── Ambev.DeveloperEvaluation.WebApi/     # Main API project
├── Ambev.DeveloperEvaluation.ORM/        # Database context and migrations
├── Ambev.DeveloperEvaluation.Domain/     # Domain models and interfaces
├── Ambev.DeveloperEvaluation.Application/# Application services
├── Ambev.DeveloperEvaluation.Common/     # Shared utilities
└── Ambev.DeveloperEvaluation.IoC/        # Dependency injection setup
```

### Database Migrations

To create a new migration:
```powershell
cd src/Ambev.DeveloperEvaluation.ORM
dotnet ef migrations add MigrationName --startup-project ../Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
```

### Clean Up

To stop and remove all containers and volumes:
```powershell
docker-compose -f docker-compose.yml down -v
```

To remove the API image:
```powershell
docker rmi ambevdeveloperevaluationwebapi
```

## Environment Variables

The application uses the following environment variables (configured in docker-compose.yml):

- `ASPNETCORE_ENVIRONMENT`: Development
- `ASPNETCORE_URLS`: http://+:5000
- `ConnectionStrings__DefaultConnection`: PostgreSQL connection string

## Health Checks

The API includes health checks for all dependencies:
- Database (PostgreSQL)

Access the health check endpoint at: `http://localhost:5000/health`

## Troubleshooting

If you encounter any issues:

1. Check if Docker Desktop is running
2. Verify all containers are healthy:
```powershell
docker-compose -f docker-compose.yml ps
```

3. Check container logs:
```powershell
docker-compose -f docker-compose.yml logs [service-name]
```

4. If needed, rebuild everything from scratch:
```powershell
docker-compose -f docker-compose.yml down -v
docker rmi ambevdeveloperevaluationwebapi
docker-compose -f docker-compose.yml up --build -d
```