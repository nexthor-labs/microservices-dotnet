using System;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces.Services;

public interface IUsersService
{
    Task<AuthenticationResponse> Login(LoginRequest request);
    Task<ApplicationUser?> Register(RegisterRequest request);
}
