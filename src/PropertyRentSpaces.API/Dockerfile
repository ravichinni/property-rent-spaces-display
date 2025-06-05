FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["PropertyRentSpaces.API/PropertyRentSpaces.API.csproj", "PropertyRentSpaces.API/"]
COPY ["PropertyRentSpaces.Core/PropertyRentSpaces.Core.csproj", "PropertyRentSpaces.Core/"]
COPY ["PropertyRentSpaces.Infrastructure/PropertyRentSpaces.Infrastructure.csproj", "PropertyRentSpaces.Infrastructure/"]
RUN dotnet restore "PropertyRentSpaces.API/PropertyRentSpaces.API.csproj"

# Copy the rest of the code
COPY . .
WORKDIR "/src/PropertyRentSpaces.API"

# Build and publish
RUN dotnet build "PropertyRentSpaces.API.csproj" -c Release -o /app/build
RUN dotnet publish "PropertyRentSpaces.API.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "PropertyRentSpaces.API.dll"] 