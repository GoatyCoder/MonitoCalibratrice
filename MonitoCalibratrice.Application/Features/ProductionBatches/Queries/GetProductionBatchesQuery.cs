using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Queries
{
    public record GetProductionBatchesQuery() : IRequest<Result<IEnumerable<ProductionBatchDto>>>;

    public class GetAllProductionBatchesQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetProductionBatchesQuery, Result<IEnumerable<ProductionBatchDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<ProductionBatchDto>>> Handle(GetProductionBatchesQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dtos = await context.ProductionBatches
                .AsNoTracking()
                .ProjectTo<ProductionBatchDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<ProductionBatchDto>>.Success(dtos);
        }
    }
}
