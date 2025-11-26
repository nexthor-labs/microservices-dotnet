using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using eCommerce.Infraestructure.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Infraestructure.Services;

public class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public UsersService(IMapper mapper,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    JwtTokenGenerator jwtTokenGenerator,
    IValidator<LoginRequest> loginValidator,
    IValidator<RegisterRequest> registerValidator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest request)
    {
        var validate = await _loginValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

        var user = await _userManager.FindByEmailAsync(request.Email!);
        var result = await _signInManager.CheckPasswordSignInAsync(user!, request.Password!, false);
        if (result.Succeeded && user != null)
        {
            var response = _mapper.Map<AuthenticationResponse>(user);
            response.UserID = Guid.Parse(user.Id);
            response.Success = true;
            var (token, expires) = _jwtTokenGenerator.GenerateToken(user);
            response.Token = token;
            response.Expires = expires;
            return response;
        }
        
        return null;
    }

    public async Task<ApplicationUser?> Register(RegisterRequest request)
    {
        var validate = await _registerValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

        var appUser = _mapper.Map<ApplicationUser>(request);

        var result = await _userManager.CreateAsync(appUser, request.Password!);

        if (!result.Succeeded)
            return null;
        return appUser;
    }
}
