using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Queries
{
    public record SearchFinishedProductsQuery(string Filter) : IRequest<Result<IEnumerable<FinishedProductDto>>>;

    public class SearchFinishedProductsQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper
    ) : IRequestHandler<SearchFinishedProductsQuery, Result<IEnumerable<FinishedProductDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<FinishedProductDto>>> Handle(
            SearchFinishedProductsQuery request,
            CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var filter = request.Filter?.Trim().ToLower() ?? string.Empty;

            var query = context.FinishedProducts
                .AsNoTracking()
                .Where(fp => fp.Code.ToLower().Contains(filter)
                          || fp.Name.ToLower().Contains(filter));

            var dtos = await query
                .ProjectTo<FinishedProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<FinishedProductDto>>.Success(dtos);
        }
    }
}
