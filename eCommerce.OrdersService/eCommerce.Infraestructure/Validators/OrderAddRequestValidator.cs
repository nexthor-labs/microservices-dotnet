using System;
using System.Data;
using eCommerce.Core.DTOs;
using FluentValidation;

namespace eCommerce.Infraestructure.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .MaximumLength(100).WithMessage("UserId cannot exceed 100 characters.");  

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item.");
    }
}
