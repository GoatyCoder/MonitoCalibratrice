using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Queries
{
    public record GetProductionBatchByIdQuery(Guid Id) : IRequest<Result<ProductionBatchDto>>;

    public class GetProductionBatchByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetProductionBatchByIdQuery, Result<ProductionBatchDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductionBatchDto>> Handle(GetProductionBatchByIdQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.ProductionBatches
                .Include(pb => pb.Pallets)
                .AsNoTracking()
                .Where(pb => pb.Id == request.Id)
                .ProjectTo<ProductionBatchDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<ProductionBatchDto>.Failure(
                    new AppError(ErrorCode.NotFound, "ProductionBatch not found.", $"Id: {request.Id}")
                );
            }

            return Result<ProductionBatchDto>.Success(dto);
        }
    }
}
