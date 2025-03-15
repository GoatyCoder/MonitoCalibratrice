using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.ProductionBatches.Commands
{
    public record DeleteProductionBatchCommand(Guid Id) : IRequest<Result>;

    public class DeleteProductionBatchCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory) : IRequestHandler<DeleteProductionBatchCommand, Result>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Result> Handle(DeleteProductionBatchCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.ProductionBatches.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result.Failure(new AppError(ErrorCode.NotFound, "ProductionBatch not found.", $"Id: {request.Id}"));
            }

            context.ProductionBatches.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
