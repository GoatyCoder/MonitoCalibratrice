using FluentValidation;
using MonitoCalibratrice.Application.Features.ProductionBatches.Commands;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Validators
{
    public class CreateProductionBatchCommandValidator : AbstractValidator<CreateProductionBatchCommand>
    {
        public CreateProductionBatchCommandValidator()
        {
            RuleFor(x => x.RawProductId)
                .NotEqual(Guid.Empty).WithMessage("RawProductId is required.");

            RuleFor(x => x.VarietyId)
                .NotEqual(Guid.Empty).WithMessage("VarietyId is required.");

            RuleFor(x => x.FinishedProductId)
                .NotEqual(Guid.Empty).WithMessage("FinishedProductId is required.");

            RuleFor(x => x.SecondaryPackagingId)
                .NotEqual(Guid.Empty).WithMessage("SecondaryPackagingId is required.");

            RuleFor(x => x.StartedTime)
                .NotEmpty().WithMessage("StartTime is required.");
        }
    }
}
