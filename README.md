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

## Local Development Setup (On Windows)

### 1. Clone the Repository
```bash
git clone <repository-url>
cd property-rent-spaces-display
```

### 2. Environment Setup

Create a `.env` file in the root directory with the following content:
```
AZURE_BLOB_URL=https://*********.blob.core.windows.net/dev-test/dev-test.json
AZURE_SAS_TOKEN=?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=******************
```

### 3. Backend Setup

```bash
# Navigate to the solution directory
cd src

# Navigate to the solution directory
cd PropertyRentSpaces.API

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

dotnet run
```

The API will be available at:
- API: http://localhost:5000/api/Properties
<!-- - Swagger UI: http://localhost:5000/swagger/index.html -->

### 4. Frontend Setup

```bash
# Navigate to the client directory
cd client

# Install dependencies
npm install

# Run the development server (optional, if not using Docker)
npm start
```

The frontend will be available at:
- http://localhost:3000

### Application Screenshot
![Property Rent Spaces Application](Media/app-screenshot.png)

### 5. Running with Docker (Not Tested)

```bash
# Build and start the containers
docker-compose up --build
```

This will start both the API and frontend services. The application will be available at:
- Frontend: http://localhost:3000
- API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

To stop the containers:
```bash
docker-compose down
```

## Testing (Not Tested)

### Backend Tests
The backend uses xUnit for testing and includes tests for:
- Azure Blob Storage Service
- API Controllers
- Data Models

To run backend tests:
```bash
cd src/PropertyRentSpaces.Tests
dotnet test
```

Key test files:
- `AzureBlobStorageServiceTests.cs`: Tests for blob storage integration
  - Constructor validation
  - Configuration requirements
  - Error handling
  - Cache functionality

### Frontend Tests
The frontend uses Jest and React Testing Library for testing and includes tests for:
- PropertyList component
- PropertyCard component
- Data fetching and error handling

To run frontend tests:
```bash
cd client
npm test
```

Key test files:
- `PropertyList.test.tsx`: Tests for the main property list component
  - Loading states
  - Data fetching
  - Error handling
  - API response handling

- `PropertyCard.test.tsx`: Tests for individual property cards
  - Rendering of property details
  - Expand/collapse functionality
  - UI state changes
  - Component interaction

### Test Coverage
To generate test coverage reports:

Backend:
```bash
cd src/PropertyRentSpaces.Tests
dotnet test --collect:"XPlat Code Coverage"
```

Frontend:
```bash
cd client
npm test -- --coverage
```

## Troubleshooting

### Common Issues

1. Port Conflicts
   - If port 3000 or 5000 is already in use, you can modify the ports in `docker-compose.yml`
   - For local development, you can change the ports in the respective project configurations

2. Docker Issues
   - Ensure Docker Desktop is running
   - Try rebuilding the containers: `docker-compose up --build --force-recreate`

3. API Connection Issues
   - Verify the Azure Blob Storage URL and SAS token in the `.env` file
   - Check if the API is running and accessible
   - Ensure CORS is properly configured

4. Frontend Build Issues
   - Clear node_modules and reinstall: `rm -rf node_modules && npm install`
   - Clear npm cache: `npm cache clean --force`

## Architecture

The application follows a clean architecture pattern with the following layers:
- API Layer: Handles HTTP requests and responses
- Core Layer: Contains business logic and domain models
- Infrastructure Layer: Implements external service integrations
- Presentation Layer: React frontend application

## License

MIT
