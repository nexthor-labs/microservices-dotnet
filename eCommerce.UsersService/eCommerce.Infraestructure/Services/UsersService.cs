using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repository;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest request)
    {
        var user = await _repository.GetUserByEmailAndPassword(request.Email, request.Password);
        if (user is not null)
        {
            var response = _mapper.Map<AuthenticationResponse>(user);
            response.Success = true;
            response.Token = "valid-token";
            return response;
        }
        
        return null;
    }

    public async Task<ApplicationUser?> Register(RegisterRequest request)
    {
        var appUser = _mapper.Map<ApplicationUser>(request);
        var user = await _repository.AddUser(appUser);

        return user;
    }
}
