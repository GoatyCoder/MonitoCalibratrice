using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Commands
{
    public record ConcludeProductionBatchCommand(Guid Id) : IRequest<Result<ProductionBatchDto>>;

    public class ConcludeProductionBatchCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<ConcludeProductionBatchCommand, Result<ProductionBatchDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductionBatchDto>> Handle(ConcludeProductionBatchCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.ProductionBatches.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
                //return Result<ProductionBatchDto>.Failure("ProductionBatch not found.");

            entity.EndTime = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductionBatchDto>(entity);
            return Result<ProductionBatchDto>.Success(dto);
        }
    }
}
