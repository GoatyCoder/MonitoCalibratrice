using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Queries
{
    public record GetVarietyByCodeQuery(string Code, Guid RawProductId) : IRequest<Result<VarietyDto>>;

    public class GetVarietyByCodeQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetVarietyByCodeQuery, Result<VarietyDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<VarietyDto>> Handle(GetVarietyByCodeQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.Varieties
                .AsNoTracking()
                .Where(v => v.Code == request.Code && v.RawProductId == request.RawProductId)
                .ProjectTo<VarietyDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<VarietyDto>.Failure(
                    new AppError(ErrorCode.NotFound, "Variety not found.", $"Code: {request.Code}")
                );
            }

            return Result<VarietyDto>.Success(dto);
        }
    }
}
