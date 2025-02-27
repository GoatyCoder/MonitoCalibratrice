using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Queries
{
    public record GetFinishedProductPalletByIdQuery(Guid Id) : IRequest<Result<FinishedProductPalletDto>>;

    public class GetFinishedProductPalletByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetFinishedProductPalletByIdQuery, Result<FinishedProductPalletDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductPalletDto>> Handle(GetFinishedProductPalletByIdQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.FinishedProductPallets
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
                .ProjectTo<FinishedProductPalletDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<FinishedProductPalletDto>.Failure(
                        new AppError(ErrorCode.NotFound, "FinishedProductPallet not found.", $"Id: {request.Id}")
                    );
            }

            return Result<FinishedProductPalletDto>.Success(dto);
        }
    }
}
