using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Queries
{
    public record GetAllVarietiesQuery() : IRequest<Result<IEnumerable<VarietyDto>>>;

    public class GetAllVarietiesQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetAllVarietiesQuery, Result<IEnumerable<VarietyDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<VarietyDto>>> Handle(GetAllVarietiesQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dtos = await context.Varieties
                .AsNoTracking()
                .ProjectTo<VarietyDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<VarietyDto>>.Success(dtos);
        }
    }
}
