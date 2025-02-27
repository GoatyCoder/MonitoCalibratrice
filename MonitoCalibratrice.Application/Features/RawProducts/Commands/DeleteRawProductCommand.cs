using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.RawProducts.Commands
{
    public record DeleteRawProductCommand(Guid Id) : IRequest<Result>;

    public class DeleteRawProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory) : IRequestHandler<DeleteRawProductCommand, Result>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Result> Handle(DeleteRawProductCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.RawProducts.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result.Failure(
                    new AppError(ErrorCode.NotFound, "RawProduct not found.", $"Id: {request.Id}")
                );
            }

            context.RawProducts.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
