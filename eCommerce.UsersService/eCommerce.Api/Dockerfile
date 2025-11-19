# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY eCommerce.UsersService.sln ./
COPY eCommerce.Api/eCommerce.Api.csproj ./eCommerce.Api/
COPY eCommerce.Core/eCommerce.Core.csproj ./eCommerce.Core/
COPY eCommerce.Infraestructure/eCommerce.Infraestructure.csproj ./eCommerce.Infraestructure/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY eCommerce.Api/ ./eCommerce.Api/
COPY eCommerce.Core/ ./eCommerce.Core/
COPY eCommerce.Infraestructure/ ./eCommerce.Infraestructure/

# Build the application
WORKDIR /src/eCommerce.Api
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
