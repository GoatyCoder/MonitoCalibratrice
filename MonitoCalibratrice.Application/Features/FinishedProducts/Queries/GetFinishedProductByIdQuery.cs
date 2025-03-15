using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Queries
{
    public record GetFinishedProductByIdQuery(Guid Id) : IRequest<Result<FinishedProductDto>>;

    public class GetFinishedProductByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetFinishedProductByIdQuery, Result<FinishedProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductDto>> Handle(GetFinishedProductByIdQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.FinishedProducts
                .AsNoTracking()
                .Where(v => v.Id == request.Id)
                .ProjectTo<FinishedProductDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<FinishedProductDto>.Failure(
                    new AppError(ErrorCode.NotFound, "FinishedProduct not found.", $"Id: {request.Id}")
                );
            }

            return Result<FinishedProductDto>.Success(dto);
        }
    }
}
