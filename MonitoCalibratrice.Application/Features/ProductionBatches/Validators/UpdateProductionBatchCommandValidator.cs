﻿using FluentValidation;
using MonitoCalibratrice.Application.Features.ProductionBatches.Commands;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Validators
{
    public class UpdateProductionBatchCommandValidator : AbstractValidator<UpdateProductionBatchCommand>
    {
        public UpdateProductionBatchCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("ProductionBatch Id is required.");

            RuleFor(x => x.RawProductId)
                .NotEqual(Guid.Empty).WithMessage("RawProductId is required.");

            RuleFor(x => x.VarietyId)
                .NotEqual(Guid.Empty).WithMessage("VarietyId is required.");

            RuleFor(x => x.FinishedProductId)
                .NotEqual(Guid.Empty).WithMessage("FinishedProductId is required.");

            RuleFor(x => x.SecondaryPackagingId)
                .NotEqual(Guid.Empty).WithMessage("SecondaryPackagingId is required.");

            RuleFor(x => x.StartedAt)
                .NotEmpty().WithMessage("StartTime is required.");

            When(x => x.FinishedAt.HasValue, () =>
            {
                RuleFor(x => x.FinishedAt.Value)
                    .GreaterThan(x => x.StartedAt).WithMessage("EndTime must be greater than StartTime when provided.");
            });
        }
    }
}
