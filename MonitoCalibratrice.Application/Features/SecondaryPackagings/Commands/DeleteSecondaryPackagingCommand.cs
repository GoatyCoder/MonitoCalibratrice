using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Commands
{
    public record DeleteSecondaryPackagingCommand(Guid Id) : IRequest<Result>;

    public class DeleteSecondaryPackagingCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory) : IRequestHandler<DeleteSecondaryPackagingCommand, Result>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Result> Handle(DeleteSecondaryPackagingCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.SecondaryPackagings.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result.Failure(
                    new AppError(ErrorCode.NotFound, "SecondaryPackaging not found.", $"Id: {request.Id}")
                );
            }

            context.SecondaryPackagings.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
