using AHuiHou.Application.DTOs.Requests;
using FluentValidation;

namespace AHuiHou.Application.Validators;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(x => x.TableId)
            .GreaterThan(0).WithMessage("Table ID must be greater than zero");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("Start time cannot be in the past");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time");
    }
}

