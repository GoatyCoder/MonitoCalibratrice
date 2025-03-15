using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Queries
{
    public record GetFinishedProductByCodeQuery(string Code) : IRequest<Result<FinishedProductDto>>;

    public class GetFinishedProductByCodeQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetFinishedProductByCodeQuery, Result<FinishedProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductDto>> Handle(GetFinishedProductByCodeQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.FinishedProducts
                .AsNoTracking()
                .Where(fp => fp.Code == request.Code)
                .ProjectTo<FinishedProductDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<FinishedProductDto>.Failure(
                    new AppError(ErrorCode.NotFound, "FinishedProduct not found.", $"Code: {request.Code}")
                );
            }

            return Result<FinishedProductDto>.Success(dto);
        }
    }
}
