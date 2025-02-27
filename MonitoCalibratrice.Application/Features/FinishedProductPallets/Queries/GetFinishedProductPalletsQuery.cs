using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Queries
{
    public record GetFinishedProductPalletsQuery() : IRequest<Result<IEnumerable<FinishedProductPalletDto>>>;

    public class GetFinishedProductPalletsQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetFinishedProductPalletsQuery, Result<IEnumerable<FinishedProductPalletDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<FinishedProductPalletDto>>> Handle(GetFinishedProductPalletsQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dtos = await context.FinishedProductPallets
                .AsNoTracking()
                .ProjectTo<FinishedProductPalletDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<FinishedProductPalletDto>>.Success(dtos);
        }
    }
}
