using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.RawProducts.Queries
{
    public record GetRawProductByIdQuery(Guid Id) : IRequest<Result<RawProductDto>>;

    public class GetRawProductByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetRawProductByIdQuery, Result<RawProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<RawProductDto>> Handle(GetRawProductByIdQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.RawProducts
                .AsNoTracking()
                .Where(r => r.Id == request.Id)
                .ProjectTo<RawProductDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<RawProductDto>.Failure(
                    new AppError(ErrorCode.NotFound, "RawProduct not found.", $"Id: {request.Id}")
                );
            }

            return Result<RawProductDto>.Success(dto);
        }
    }
}
