using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.Varieties.Queries
{
    public record SearchVarietiesQuery(string Filter, Guid RawProductId) : IRequest<Result<IEnumerable<VarietyDto>>>;

    public class SearchVarietiesQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<SearchVarietiesQuery, Result<IEnumerable<VarietyDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<VarietyDto>>> Handle(SearchVarietiesQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            var filter = request.Filter?.Trim().ToLower() ?? string.Empty;

            var query = context.Varieties
                .AsNoTracking()
                .Where(v => v.RawProductId == request.RawProductId &&
                            (v.Code.ToLower().Contains(filter) || v.Name.ToLower().Contains(filter)));

            var dtos = await query
                .ProjectTo<VarietyDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<VarietyDto>>.Success(dtos);
        }
    }
}
