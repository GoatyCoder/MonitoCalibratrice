using FluentValidation;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.Queries;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Validators
{
    public class GetFinishedProductPalletByPalletCodeQueryValidator : AbstractValidator<GetFinishedProductPalletByPalletCodeQuery>
    {
        public GetFinishedProductPalletByPalletCodeQueryValidator()
        {
            RuleFor(x => x.PalletCode)
                .NotEmpty().WithMessage("PalletCode is required.")
                .Matches("^P\\d{2}-\\d{3}-\\d{4}").WithMessage("PalletCode is not correct.");
        }
    }
}
