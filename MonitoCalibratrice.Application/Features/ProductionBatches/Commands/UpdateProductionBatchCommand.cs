using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Commands
{
    public record UpdateProductionBatchCommand(
        Guid Id,
        Guid RawProductId,
        Guid VarietyId,
        string? Caliber,
        Guid FinishedProductId,
        Guid SecondaryPackagingId,
        DateTime StartTime,
        DateTime? EndTime
    ) : IRequest<Result<ProductionBatchDto>>;

    public class UpdateProductionBatchCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<UpdateProductionBatchCommand, Result<ProductionBatchDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductionBatchDto>> Handle(UpdateProductionBatchCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.RawProducts.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {

            }
                //return Result<ProductionBatchDto>.Failure("ProductionBatch not found.");

            _mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductionBatchDto>(entity);
            return Result<ProductionBatchDto>.Success(dto);
        }
    }
}
