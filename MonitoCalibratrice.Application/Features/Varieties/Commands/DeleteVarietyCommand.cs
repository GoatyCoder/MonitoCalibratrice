using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Commands
{
    public record DeleteVarietyCommand(Guid Id) : IRequest<Result>;

    public class DeleteVarietyCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory) : IRequestHandler<DeleteVarietyCommand, Result>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Result> Handle(DeleteVarietyCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.Varieties.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result.Failure(
                    new AppError(ErrorCode.NotFound, "Variety not found.", $"Id: {request.Id}")
                );
            }

            context.Varieties.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
