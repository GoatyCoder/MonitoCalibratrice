using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.RawProducts.Queries
{
    public record SearchRawProductsQuery(string Filter) : IRequest<Result<IEnumerable<RawProductDto>>>;

    public class SearchRawProductsQueryHandler(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IMapper mapper
    ) : IRequestHandler<SearchRawProductsQuery, Result<IEnumerable<RawProductDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<RawProductDto>>> Handle(SearchRawProductsQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var filter = request.Filter?.Trim().ToLower() ?? string.Empty;

            var query = context.RawProducts
                .AsNoTracking()
                .Where(r => r.Code.ToLower().Contains(filter)
                         || r.Name.ToLower().Contains(filter));

            var dtos = await query
                .ProjectTo<RawProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<RawProductDto>>.Success(dtos);
        }
    }
}
