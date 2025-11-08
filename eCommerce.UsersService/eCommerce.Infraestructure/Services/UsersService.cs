using System;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repository;

    public UsersService(IUsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest request)
    {
        var user = await _repository.GetUserByEmailAndPassword(request.Email, request.Password);
        if (user is not null)
            return new AuthenticationResponse
            {
                UserID = user.UserID,
                Email = user.Email,
                PersonName = user.PersonName,
                Gender = user.Gender,
                Success = true
            };

        return null;
    }

    public async Task<ApplicationUser?> Register(RegisterRequest request)
    {
        var appUser = new ApplicationUser
        {
            PersonName = request.PersonName,
            Email = request.Email,
            Password = request.Password,
            Gender = request.Gender
        };
        var user = await _repository.AddUser(appUser);

        return user;
    }
}
