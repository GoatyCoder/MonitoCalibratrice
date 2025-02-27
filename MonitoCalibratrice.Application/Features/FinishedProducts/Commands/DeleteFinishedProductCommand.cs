using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Commands
{
    public record DeleteFinishedProductCommand(Guid Id)
        : IRequest<Result>;

    public class DeleteFinishedProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory) : IRequestHandler<DeleteFinishedProductCommand, Result>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Result> Handle(DeleteFinishedProductCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.FinishedProducts.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
                //return Result.Failure("FinishedProduct not found.");

            context.FinishedProducts.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
