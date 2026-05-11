using AHuiHou.Application.DTOs.Requests;
using FluentValidation;

namespace AHuiHou.Application.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be greater than zero");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero");

        RuleFor(x => x.PointsAwarded)
            .GreaterThanOrEqualTo(0).WithMessage("Points awarded cannot be negative");
    }
}

