# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy Directory.Packages.props (Central Package Management)
COPY Directory.Packages.props ./

# Copy solution and project files
COPY eCommerce.UsersService/eCommerce.UsersService.sln ./eCommerce.UsersService/
COPY eCommerce.UsersService/eCommerce.Api/eCommerce.Api.csproj ./eCommerce.UsersService/eCommerce.Api/
COPY eCommerce.UsersService/eCommerce.Core/eCommerce.Core.csproj ./eCommerce.UsersService/eCommerce.Core/
COPY eCommerce.UsersService/eCommerce.Infraestructure/eCommerce.Infraestructure.csproj ./eCommerce.UsersService/eCommerce.Infraestructure/

# Restore dependencies
WORKDIR /src/eCommerce.UsersService
RUN dotnet restore

# Copy all source files
COPY eCommerce.UsersService/eCommerce.Api/ ./eCommerce.Api/
COPY eCommerce.UsersService/eCommerce.Core/ ./eCommerce.Core/
COPY eCommerce.UsersService/eCommerce.Infraestructure/ ./eCommerce.Infraestructure/

# Build the application
WORKDIR /src/eCommerce.UsersService/eCommerce.Api
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
