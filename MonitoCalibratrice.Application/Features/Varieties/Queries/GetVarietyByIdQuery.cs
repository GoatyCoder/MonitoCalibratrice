using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Queries
{
    public record GetVarietyByIdQuery(Guid Id) : IRequest<Result<VarietyDto>>;

    public class GetVarietyByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetVarietyByIdQuery, Result<VarietyDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<VarietyDto>> Handle(GetVarietyByIdQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.Varieties
                .AsNoTracking()
                .Where(v => v.Id == request.Id)
                .ProjectTo<VarietyDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<VarietyDto>.Failure(
                    new AppError(ErrorCode.NotFound, "Variety not found.", $"Id: {request.Id}")
                );
            }

            return Result<VarietyDto>.Success(dto);
        }
    }
}
