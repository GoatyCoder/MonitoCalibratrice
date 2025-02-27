using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Commands
{
    public record CreateVarietyCommand(
        string Code,
        string Name,
        string Description,
        Guid RawProductId
        ) : IRequest<Result<VarietyDto>>;

    public class CreateVarietyCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<CreateVarietyCommand, Result<VarietyDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<VarietyDto>> Handle(CreateVarietyCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            if (await context.Varieties.AnyAsync(v => v.RawProductId == request.RawProductId && v.Code == request.Code, cancellationToken))
            {
                return Result<VarietyDto>.Failure(
                    new AppError(ErrorCode.DuplicateCode, $"Variety with code '{request.Code}' already exists for the specified RawProduct.", $"RawProductId: {request.RawProductId}")
                );
            }

            var entity = _mapper.Map<Variety>(request);

            await context.Varieties.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<VarietyDto>(entity);
            return Result<VarietyDto>.Success(dto);
        }
    }
}
