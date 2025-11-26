# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy Directory.Packages.props for centralized package management
COPY Directory.Packages.props ./

# Copy solution and project files
COPY eCommerce.ProductsService/eCommerce.ProductsService.sln ./eCommerce.ProductsService/
COPY eCommerce.ProductsService/eCommerce.Api/eCommerce.Api.csproj ./eCommerce.ProductsService/eCommerce.Api/
COPY eCommerce.ProductsService/eCommerce.Core/eCommerce.Core.csproj ./eCommerce.ProductsService/eCommerce.Core/
COPY eCommerce.ProductsService/eCommerce.Infraestructure/eCommerce.Infraestructure.csproj ./eCommerce.ProductsService/eCommerce.Infraestructure/

# Restore dependencies
WORKDIR /src/eCommerce.ProductsService
RUN dotnet restore

# Copy all source files
COPY eCommerce.ProductsService/eCommerce.Api/ ./eCommerce.Api/
COPY eCommerce.ProductsService/eCommerce.Core/ ./eCommerce.Core/
COPY eCommerce.ProductsService/eCommerce.Infraestructure/ ./eCommerce.Infraestructure/

# Build the application
WORKDIR /src/eCommerce.ProductsService/eCommerce.Api
RUN dotnet tool install --global dotnet-ef --version 9.0.0-rc.1.24451.1
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Add dotnet tools to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy dotnet-ef tool from build stage
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.dll"]