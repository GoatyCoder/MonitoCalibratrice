using FluentValidation;
using MonitoCalibratrice.Application.Features.RawProducts.Commands;

namespace MonitoCalibratrice.Application.Features.RawProducts.Validators
{
    public class UpdateRawProductCommandValidator : AbstractValidator<UpdateRawProductCommand>
    {
        public UpdateRawProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Id is required.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(12).WithMessage("Code must not exceed 12 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        }
    }
}
