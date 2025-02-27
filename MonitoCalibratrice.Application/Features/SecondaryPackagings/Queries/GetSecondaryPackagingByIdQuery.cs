using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Queries
{
    public record GetSecondaryPackagingByIdQuery(Guid Id) : IRequest<Result<SecondaryPackagingDto>>;

    public class GetSecondaryPackagingByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetSecondaryPackagingByIdQuery, Result<SecondaryPackagingDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SecondaryPackagingDto>> Handle(GetSecondaryPackagingByIdQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.SecondaryPackagings
                .AsNoTracking()
                .Where(sp => sp.Id == request.Id)
                .ProjectTo<SecondaryPackagingDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<SecondaryPackagingDto>.Failure(
                    new AppError(ErrorCode.NotFound, "SecondaryPackaging not found.", $"Id: {request.Id}")
                );
            }

            return Result<SecondaryPackagingDto>.Success(dto);
        }
    }
}
