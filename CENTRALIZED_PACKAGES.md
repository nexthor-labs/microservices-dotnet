# Centralized Package Management

This project uses MSBuild's Directory.Build.props and Directory.Packages.props for centralized configuration and package version management across all microservices.

## Files

### Directory.Build.props
Located at the repository root, this file defines common properties for all projects:
- Target framework (net9.0)
- Common compiler settings (ImplicitUsings, Nullable)
- Versioning information
- Build optimization settings

All projects in subdirectories automatically inherit these properties.

### Directory.Packages.props
Located at the repository root, this file centralizes all NuGet package versions:
- Enables Central Package Management (CPM)
- Defines versions for all shared packages
- Ensures version consistency across microservices

## Benefits

✅ **Consistency**: All microservices use the same package versions
✅ **Maintainability**: Update versions in one place
✅ **Simplicity**: Individual `.csproj` files are cleaner
✅ **Safety**: Prevents version conflicts between projects

## Usage

### Adding a New Package

1. Add the package version to `Directory.Packages.props`:
```xml
<PackageVersion Include="PackageName" Version="1.0.0" />
```

2. Reference it in your `.csproj` file WITHOUT a version:
```xml
<PackageReference Include="PackageName" />
```

### Updating Package Versions

Simply update the version in `Directory.Packages.props`:
```xml
<PackageVersion Include="PackageName" Version="2.0.0" />
```

All projects will use the new version after running `dotnet restore`.

### Project-Specific Version Override

If a specific project needs a different version, you can override it:
```xml
<PackageReference Include="PackageName" VersionOverride="1.5.0" />
```

## Common Properties Inherited by All Projects

From `Directory.Build.props`:
- `TargetFramework`: net9.0
- `ImplicitUsings`: enabled
- `Nullable`: enabled
- `LangVersion`: latest

To override a property in a specific project, simply redefine it in that project's `.csproj` file.

## Current Packages

| Package | Version | Used By |
|---------|---------|---------|
| Microsoft.AspNetCore.OpenApi | 9.0.8 | API Projects |
| Swashbuckle.AspNetCore | 6.9.0 | API Projects |
| Microsoft.EntityFrameworkCore.Design | 9.0.10 | Infrastructure |
| Pomelo.EntityFrameworkCore.MySql | 9.0.0 | Infrastructure |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 9.0.10 | Core |
| Microsoft.AspNetCore.Authentication.JwtBearer | 9.0.10 | Infrastructure |
| FluentValidation | 12.1.0 | Core |
| AutoMapper | 15.1.0 | Infrastructure |
| Microsoft.Extensions.Configuration | 9.0.10 | Infrastructure |
| Microsoft.Extensions.Configuration.FileExtensions | 9.0.10 | Infrastructure |
| Microsoft.Extensions.DependencyInjection.Abstractions | 9.0.10 | Core |

## Restoration

After modifying `Directory.Packages.props`, run:
```bash
dotnet restore
```

This will update all projects with the new package versions.

## Documentation

- [MSBuild Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory)
- [Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)
