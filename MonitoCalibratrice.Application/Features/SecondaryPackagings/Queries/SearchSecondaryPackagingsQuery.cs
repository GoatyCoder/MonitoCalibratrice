using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Queries
{
    public record SearchSecondaryPackagingsQuery(string Filter) : IRequest<Result<IEnumerable<SecondaryPackagingDto>>>;

    public class SearchSecondaryPackagingsQueryHandler(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IMapper mapper)
                : IRequestHandler<SearchSecondaryPackagingsQuery, Result<IEnumerable<SecondaryPackagingDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<SecondaryPackagingDto>>> Handle(
            SearchSecondaryPackagingsQuery request,
            CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var filter = request.Filter?.Trim().ToLower() ?? string.Empty;

            var query = context.SecondaryPackagings
                .AsNoTracking()
                .Where(sp => sp.Code.ToLower().Contains(filter)
                          || sp.Name.ToLower().Contains(filter));

            var dtos = await query
                .ProjectTo<SecondaryPackagingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<SecondaryPackagingDto>>.Success(dtos);
        }
    }
}
