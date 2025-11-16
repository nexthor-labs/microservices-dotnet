using System;
using eCommerce.Core.DTOs;
using FluentValidation;

namespace eCommerce.Infraestructure.Validators;

public class ProductUpdateStockValidator : AbstractValidator<ProductUpdateStockRequest>
{
    public ProductUpdateStockValidator()
    {
        RuleFor(p => p.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock must be zero or greater.");
    }
}
