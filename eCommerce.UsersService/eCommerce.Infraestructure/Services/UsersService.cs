using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Infraestructure.Services;

public class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public UsersService(IMapper mapper,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    JwtTokenGenerator jwtTokenGenerator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.", nameof(request.Email));

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Password is required.", nameof(request.Password));

        var user = await _userManager.FindByEmailAsync(request.Email);
        var result = await _signInManager.CheckPasswordSignInAsync(user!, request.Password, false);
        if (result.Succeeded && user != null)
        {
            var response = _mapper.Map<AuthenticationResponse>(user);
            response.Success = true;
            response.Token = _jwtTokenGenerator.GenerateToken(user);
            return response;
        }
        
        return null;
    }

    public async Task<ApplicationUser?> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.", nameof(request.Email));
        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Password is required.", nameof(request.Password));

        var appUser = _mapper.Map<ApplicationUser>(request);

        var result = await _userManager.CreateAsync(appUser, request.Password);

        if (!result.Succeeded)
            return null;
        return appUser;
    }
}
