using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Queries
{
    public record GetFinishedProductPalletByPalletCodeQuery(string PalletCode) : IRequest<Result<FinishedProductPalletDto>>;

    public class GetFinishedProductPalletByPalletCodeQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetFinishedProductPalletByPalletCodeQuery, Result<FinishedProductPalletDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductPalletDto>> Handle(GetFinishedProductPalletByPalletCodeQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.FinishedProductPallets
                .AsNoTracking()
                .Where(p => p.PalletCode == request.PalletCode)
                .ProjectTo<FinishedProductPalletDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {

            }
                //return Result<FinishedProductPalletDto>.Failure("FinishedProductPallet not found.");

            return Result<FinishedProductPalletDto>.Success(dto);
        }
    }
}
