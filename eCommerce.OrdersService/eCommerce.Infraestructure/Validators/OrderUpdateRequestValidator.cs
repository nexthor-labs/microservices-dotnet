using System;
using eCommerce.Core.DTOs;
using FluentValidation;

namespace eCommerce.Infraestructure.Validators;

public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .MaximumLength(100).WithMessage("UserId cannot exceed 100 characters.");
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item.");
    }
}
