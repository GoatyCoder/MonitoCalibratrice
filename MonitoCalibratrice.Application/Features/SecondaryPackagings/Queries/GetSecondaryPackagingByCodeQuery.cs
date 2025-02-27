using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Queries
{
    public record GetSecondaryPackagingByCodeQuery(string Code) : IRequest<Result<SecondaryPackagingDto>>;

    public class GetSecondaryPackagingByCodeQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetSecondaryPackagingByCodeQuery, Result<SecondaryPackagingDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SecondaryPackagingDto>> Handle(GetSecondaryPackagingByCodeQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.SecondaryPackagings
                .AsNoTracking()
                .Where(sp => sp.Code == request.Code)
                .ProjectTo<SecondaryPackagingDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<SecondaryPackagingDto>.Failure(
                    new AppError(ErrorCode.NotFound, "SecondaryPackaging not found.", $"Code: {request.Code}")
                );
            }

            return Result<SecondaryPackagingDto>.Success(dto);
        }
    }
}
