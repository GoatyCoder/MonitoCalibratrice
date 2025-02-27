using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Queries
{
    public record GetAllSecondaryPackagingsQuery() : IRequest<Result<IEnumerable<SecondaryPackagingDto>>>;

    public class GetAllSecondaryPackagingsQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetAllSecondaryPackagingsQuery, Result<IEnumerable<SecondaryPackagingDto>>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<SecondaryPackagingDto>>> Handle(GetAllSecondaryPackagingsQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dtos = await context.SecondaryPackagings
                .AsNoTracking()
                .ProjectTo<SecondaryPackagingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<SecondaryPackagingDto>>.Success(dtos);
        }
    }
}
