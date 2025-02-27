using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets.Commands
{
    public record DeleteFinishedProductPalletCommand(Guid Id) : IRequest<Result>;

    public class DeleteFinishedProductPalletCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory) : IRequestHandler<DeleteFinishedProductPalletCommand, Result>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Result> Handle(DeleteFinishedProductPalletCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.FinishedProductPallets.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
                //return Result.Failure("FinishedProductPallet not found.");

            context.FinishedProductPallets.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
