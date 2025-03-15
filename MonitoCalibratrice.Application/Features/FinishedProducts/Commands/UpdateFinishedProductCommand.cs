using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Commands
{
    public record UpdateFinishedProductCommand(
        Guid Id,
        string Code,
        string Name,
        string? Description,
        string? Ean
    ) : IRequest<Result<FinishedProductDto>>;

    public class UpdateFinishedProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<UpdateFinishedProductCommand, Result<FinishedProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductDto>> Handle(UpdateFinishedProductCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.FinishedProducts.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result<FinishedProductDto>.Failure(
                    new AppError(ErrorCode.NotFound, "FinishedProduct not found.", $"Id: {request.Id}")
                );
            }

            if (!string.Equals(entity.Code, request.Code, StringComparison.OrdinalIgnoreCase) &&
                await context.FinishedProducts.AnyAsync(fp => fp.Code == request.Code, cancellationToken))
            {
                return Result<FinishedProductDto>.Failure(
                    new AppError(ErrorCode.DuplicateCode, $"FinishedProduct with code '{request.Code}' already exists.", $"Code: {request.Code}")
                );
            }

            _mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FinishedProductDto>(entity);
            return Result<FinishedProductDto>.Success(dto);
        }
    }
}
