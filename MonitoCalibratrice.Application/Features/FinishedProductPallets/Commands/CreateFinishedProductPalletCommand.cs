using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Commands
{
    public record CreateFinishedProductPalletCommand(
         Guid RawProductId,
         Guid VarietyId,
         string Caliber,
         Guid FinishedProductId,
         Guid SecondaryPackagingId,
         int Units,
         decimal Weight,
         string Annotation
    ) : IRequest<Result<FinishedProductPalletDto>>;

    public class CreateFinishedProductPalletCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<CreateFinishedProductPalletCommand, Result<FinishedProductPalletDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductPalletDto>> Handle(CreateFinishedProductPalletCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = _mapper.Map<FinishedProductPallet>(request);

            entity.PalletCode = await GeneratePalletCode(context, cancellationToken);
            entity.CreatedAt = DateTime.Now;

            await context.FinishedProductPallets.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FinishedProductPalletDto>(entity);
            return Result<FinishedProductPalletDto>.Success(dto);
        }

        private async Task<string> GeneratePalletCode(ApplicationDbContext context, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            string year = (now.Year % 100).ToString("D2");    // ultime due cifre dell'anno
            string dayOfYear = now.DayOfYear.ToString("D3");     // giorno dell'anno su 3 cifre

            // Conta il numero di pedane create oggi
            int countToday = await context.FinishedProductPallets
                .AsNoTracking()
                .CountAsync(p => p.CreatedAt.Date == now.Date, cancellationToken);

            int sequence = countToday + 1;
            string sequenceFormatted = sequence.ToString("D4");

            return $"P{year}-{dayOfYear}-{sequenceFormatted}";
        }
    }
}
