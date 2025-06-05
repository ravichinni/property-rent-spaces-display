# Property Rent Spaces Display

This project is a web application that displays property and space rental information from Azure Blob Storage.

## Project Structure

- `src/`
  - `PropertyRentSpaces.API/` - ASP.NET Core Web API
  - `PropertyRentSpaces.Core/` - Core business logic and models
  - `PropertyRentSpaces.Infrastructure/` - Infrastructure layer (Azure Blob Storage integration)
  - `PropertyRentSpaces.Tests/` - Unit tests for API and Core
- `client/` - React frontend application
- `docker/` - Docker configuration files

## Prerequisites

- .NET 8.0 SDK
- Node.js 18.x or later
- Docker and Docker Compose
- Azure Blob Storage account with SAS token

## Getting Started

1. Clone the repository
2. Set up environment variables:
   - Create `.env` file in the root directory
   - Add the following variables:
     ```
     AZURE_BLOB_URL=https://nmrkpidev.blob.core.windows.net/dev-test/dev-test.json
     AZURE_SAS_TOKEN=your_sas_token
     ```

3. Run with Docker Compose:
   ```bash
   docker-compose up --build
   ```

4. Access the applications:
   - API: http://localhost:5000
   - Frontend: http://localhost:3000

## Development

### Backend Development
```bash
cd src/PropertyRentSpaces.API
dotnet run
```

### Frontend Development
```bash
cd client
npm install
npm start
```

## Testing

### Backend Tests
```bash
cd src/PropertyRentSpaces.Tests
dotnet test
```

### Frontend Tests
```bash
cd client
npm test
```

## Architecture

The application follows a clean architecture pattern with the following layers:
- API Layer: Handles HTTP requests and responses
- Core Layer: Contains business logic and domain models
- Infrastructure Layer: Implements external service integrations
- Presentation Layer: React frontend application

## License

MIT
