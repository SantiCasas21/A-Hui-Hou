using AHuiHou.Application.DTOs.Requests;
using FluentValidation;

namespace AHuiHou.Application.Validators;

public class RedeemPointsRequestValidator : AbstractValidator<RedeemPointsRequest>
{
    public RedeemPointsRequestValidator()
    {
        RuleFor(x => x.Points)
            .GreaterThan(0).WithMessage("Points to redeem must be greater than zero");
    }
}

