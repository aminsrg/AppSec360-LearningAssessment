# AppSec360-LearningAssessment Microservice

## Description
AppSec360-LearningAssessment microservice following Clean Architecture + CQRS patterns.

## Technology Stack
- .NET 8
- MongoDB
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- Testcontainers (for integration tests)

## Project Structure
- **Domain**: Core business entities and logic
- **Application**: CQRS commands/queries, DTOs, business logic
- **Infrastructure**: Data access, external services
- **Api**: REST API controllers, middleware

## Setup

### Prerequisites
- .NET 8 SDK
- MongoDB 7+
- Docker (optional for running MongoDB, required for integration tests)

### Database Setup

**Using Docker:**
```bash
docker run -d \
  --name appsec360-learningassessment-db \
  -p 27017:27017 \
  mongo:7
```

**Or install MongoDB locally:**
- Download from: https://www.mongodb.com/try/download/community

### Run Application
```bash
cd src/AppSec360-LearningAssessment.Api
dotnet run
```

### API Documentation
Swagger UI: http://localhost:5001/swagger

### Health Check
http://localhost:5001/health

## Development

### Run Tests

**Prerequisites for Integration Tests:**
- Docker Desktop must be running
- Testcontainers will automatically start/stop MongoDB containers

```bash
# Run all tests
dotnet test

# Run unit tests only
dotnet test tests/AppSec360-LearningAssessment.UnitTests

# Run integration tests only
dotnet test tests/AppSec360-LearningAssessment.IntegrationTests
```

### Build Solution
```bash
dotnet build
```

### Restore Packages
```bash
dotnet restore
```

## Architecture Decisions
- Clean Architecture for separation of concerns
- CQRS pattern via MediatR for read/write separation
- MongoDB for flexible document-based storage
- Soft delete for data retention
- Audit fields on all entities
- Current user context from X-User-Id header (set by API Gateway)

## API Conventions
- All endpoints are versioned: `/api/v1/[controller]`
- Health endpoint: `/health`
- API documentation: `/swagger`

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "mongodb://localhost:27017"
  },
  "DatabaseSettings": {
    "DatabaseName": "AppSec360LearningAssessmentDb"
  }
}
```

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Development | Staging | Production
- `ConnectionStrings__DefaultConnection`: Override MongoDB connection string
- `DatabaseSettings__DatabaseName`: Override database name

## Current User Context
This microservice expects the `X-User-Id` header to be set by the API Gateway for authenticated requests. This header is used to populate audit fields (CreatedBy, UpdatedBy, DeletedBy).

Example:
```
X-User-Id: user123
```

## CI/CD
GitHub Actions workflow configured in `.github/workflows/ci.yml`
- Runs on push/PR to main and develop branches
- Builds solution
- Runs all tests

## License
Proprietary - All rights reserved
