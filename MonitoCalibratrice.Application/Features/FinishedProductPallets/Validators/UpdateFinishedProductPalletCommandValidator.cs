using FluentValidation;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.Commands;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Validators
{
    public class UpdateFinishedProductPalletCommandValidator : AbstractValidator<UpdateFinishedProductPalletCommand>
    {
        public UpdateFinishedProductPalletCommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Id is required.");
            RuleFor(x => x.RawProductId).NotEqual(Guid.Empty).WithMessage("RawProductId is required.");
            RuleFor(x => x.VarietyId).NotEqual(Guid.Empty).WithMessage("VarietyId is required.");
            RuleFor(x => x.FinishedProductId).NotEqual(Guid.Empty).WithMessage("FinishedProductId is required.");
            RuleFor(x => x.SecondaryPackagingId).NotEqual(Guid.Empty).WithMessage("SecondaryPackagingId is required.");
            RuleFor(x => x.Units).GreaterThan(0).WithMessage("Units must be greater than zero.");
            RuleFor(x => x.Weight).GreaterThan(0).WithMessage("Weight must be greater than zero.");
            RuleFor(x => x.Annotation).MaximumLength(50);
        }
    }
}
