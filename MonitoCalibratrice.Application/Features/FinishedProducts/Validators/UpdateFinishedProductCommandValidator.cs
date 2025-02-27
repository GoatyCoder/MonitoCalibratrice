using FluentValidation;
using MonitoCalibratrice.Application.Features.FinishedProducts.Commands;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Validators
{
    public class UpdateFinishedProductCommandValidator : AbstractValidator<UpdateFinishedProductCommand>
    {
        public UpdateFinishedProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(12).WithMessage("Code must not exceed 12 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(60).WithMessage("Name must not exceed 30 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        }
    }
}
