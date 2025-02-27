using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Queries
{
    public record GetFinishedProductsByEanQuery(string Ean)
        : IRequest<Result<IEnumerable<FinishedProductDto>>>;

    public class GetFinishedProductsByEanQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetFinishedProductsByEanQuery, Result<IEnumerable<FinishedProductDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<FinishedProductDto>>> Handle(GetFinishedProductsByEanQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dtos = await context.FinishedProducts
                .AsNoTracking()
                .Where(fp => fp.Ean == request.Ean)
                .ProjectTo<FinishedProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<FinishedProductDto>>.Success(dtos);
        }
    }
}
