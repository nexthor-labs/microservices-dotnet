# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy Directory.Packages.props for centralized package management
COPY Directory.Packages.props ./

# Copy solution and project files
COPY eCommerce.OrdersService/eCommerce.OrdersService.sln ./eCommerce.OrdersService/
COPY eCommerce.OrdersService/eCommerce.Api/eCommerce.Api.csproj ./eCommerce.OrdersService/eCommerce.Api/
COPY eCommerce.OrdersService/eCommerce.Core/eCommerce.Core.csproj ./eCommerce.OrdersService/eCommerce.Core/
COPY eCommerce.OrdersService/eCommerce.Infraestructure/eCommerce.Infraestructure.csproj ./eCommerce.OrdersService/eCommerce.Infraestructure/

# Restore dependencies
WORKDIR /src/eCommerce.OrdersService
RUN dotnet restore

# Copy all source files
COPY eCommerce.OrdersService/eCommerce.Api/ ./eCommerce.Api/
COPY eCommerce.OrdersService/eCommerce.Core/ ./eCommerce.Core/
COPY eCommerce.OrdersService/eCommerce.Infraestructure/ ./eCommerce.Infraestructure/

# Build the application
WORKDIR /src/eCommerce.OrdersService/eCommerce.Api
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.dll"]
