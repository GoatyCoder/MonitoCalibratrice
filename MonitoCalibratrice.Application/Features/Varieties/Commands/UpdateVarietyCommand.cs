using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Commands
{
    public record UpdateVarietyCommand(
        Guid Id,
        string Code,
        string Name,
        string Description,
        Guid RawProductId
    ) : IRequest<Result<VarietyDto>>;

    public class UpdateVarietyCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<UpdateVarietyCommand, Result<VarietyDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<VarietyDto>> Handle(UpdateVarietyCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.Varieties.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result<VarietyDto>.Failure(
                    new AppError(ErrorCode.NotFound, "Variety not found.", $"Id: {request.Id}")
                );
            }

            if (!string.Equals(entity.Code, request.Code, StringComparison.OrdinalIgnoreCase) &&
                await context.Varieties.AnyAsync(v => v.RawProductId == request.RawProductId && v.Code == request.Code, cancellationToken))
            {
                return Result<VarietyDto>.Failure(
                    new AppError(ErrorCode.DuplicateCode, $"Variety with code '{request.Code}' already exists for the specified RawProduct.", $"RawProductId: {request.RawProductId}")
                );
            }

            _mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<VarietyDto>(entity);
            return Result<VarietyDto>.Success(dto);
        }
    }
}
