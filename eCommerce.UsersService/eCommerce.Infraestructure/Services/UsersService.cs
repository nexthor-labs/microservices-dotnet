using System;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class UsersService : IUsersService
{
    public async Task<AuthenticationResponse> Login(LoginRequest request)
    {
        await Task.CompletedTask;

        return new AuthenticationResponse
        {
            UserID = Guid.NewGuid(),
            Email = request.Email,
            PersonName = "First User",
            Gender = GenderOptions.Male.ToString(),
            Token = "token returned",
            Success = true
        };
    }

    public async Task<ApplicationUser?> Register(RegisterRequest request)
    {
        await Task.CompletedTask;

        return new ApplicationUser
        {
            UserID = Guid.NewGuid(),
            PersonName = request.PersonName,
            Gender = GenderOptions.Male.ToString(),
            Email = request.Email,
            Password = request.Password
        };
    }
}
