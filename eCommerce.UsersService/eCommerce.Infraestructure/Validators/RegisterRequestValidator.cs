using System;
using eCommerce.Core.DTOs;
using FluentValidation;

namespace eCommerce.Infraestructure.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.PersonName)
            .NotEmpty().NotNull().WithMessage("Person name is required.")
            .MaximumLength(100).WithMessage("Person name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(20).WithMessage("Password cannot exceed 50 characters.");

        RuleFor(x => x.Gender)
            .NotEmpty().NotNull().WithMessage("Gender is required.");
    }
}
