using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Commands
{
    public record UpdateFinishedProductPalletCommand(
        Guid Id,
        Guid RawProductId,
        Guid VarietyId,
        string Caliber,
        Guid FinishedProductId,
        Guid SecondaryPackagingId,
        int Units,
        decimal Weight,
        string Annotation
    ) : IRequest<Result<FinishedProductPalletDto>>;

    public class UpdateFinishedProductPalletCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<UpdateFinishedProductPalletCommand, Result<FinishedProductPalletDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductPalletDto>> Handle(UpdateFinishedProductPalletCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.FinishedProductPallets.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (entity == null)
            {
                //return Result<FinishedProductPalletDto>.Failure($"Finished Product Pallet with id {request.Id} not found.");
            }

            _mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FinishedProductPalletDto>(entity);
            return Result<FinishedProductPalletDto>.Success(dto);
        }
    }
}
