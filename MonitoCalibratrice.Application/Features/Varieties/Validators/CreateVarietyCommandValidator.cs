using FluentValidation;
using MonitoCalibratrice.Application.Features.Varieties.Commands;

namespace MonitoCalibratrice.Application.Features.Varieties.Validators
{
    public class CreateVarietyCommandValidator : AbstractValidator<CreateVarietyCommand>
    {
        public CreateVarietyCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(12).WithMessage("Code must not exceed 12 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");

            RuleFor(x => x.RawProductId)
                .NotEqual(Guid.Empty).WithMessage("RawProductId is required.");
        }
    }
}
