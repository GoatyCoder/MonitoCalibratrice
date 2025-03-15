using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Commands
{
    public record CreateProductionBatchCommand(
        Guid ProductionLineId,
        Guid RawProductId,
        Guid VarietyId,
        string? Caliber,
        Guid FinishedProductId,
        Guid SecondaryPackagingId,
        DateTime StartedTime
    ) : IRequest<Result<ProductionBatchDto>>;

    public class CreateProductionBatchCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<CreateProductionBatchCommand, Result<ProductionBatchDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductionBatchDto>> Handle(CreateProductionBatchCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = _mapper.Map<ProductionBatch>(request);

            await context.ProductionBatches.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductionBatchDto>(entity);
            return Result<ProductionBatchDto>.Success(dto);
        }
    }
}
