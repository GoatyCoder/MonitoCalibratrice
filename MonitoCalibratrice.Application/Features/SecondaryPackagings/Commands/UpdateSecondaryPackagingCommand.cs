using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Commands
{
    public record UpdateSecondaryPackagingCommand(
        Guid Id,
        string Code,
        string Name,
        string Description
        ) : IRequest<Result<SecondaryPackagingDto>>;

    public class UpdateSecondaryPackagingCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<UpdateSecondaryPackagingCommand, Result<SecondaryPackagingDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SecondaryPackagingDto>> Handle(UpdateSecondaryPackagingCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.SecondaryPackagings.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result<SecondaryPackagingDto>.Failure(
                    new AppError(ErrorCode.NotFound, "SecondaryPackaging not found.", $"Id: {request.Id}")
                );
            }

            if (!string.Equals(entity.Code, request.Code, StringComparison.OrdinalIgnoreCase) &&
                await context.SecondaryPackagings.AnyAsync(sp => sp.Code == request.Code, cancellationToken))
            {
                return Result<SecondaryPackagingDto>.Failure(
                    new AppError(ErrorCode.DuplicateCode, $"SecondaryPackaging with code '{request.Code}' already exists.", $"Code: {request.Code}")
                );
            }

            _mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<SecondaryPackagingDto>(entity);
            return Result<SecondaryPackagingDto>.Success(dto);
        }
    }
}
