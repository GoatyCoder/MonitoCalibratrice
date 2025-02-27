using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.RawProducts.Queries
{
    public record GetRawProductByCodeQuery(string Code) : IRequest<Result<RawProductDto>>;

    public class GetRawProductByCodeQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<GetRawProductByCodeQuery, Result<RawProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<RawProductDto>> Handle(GetRawProductByCodeQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var dto = await context.RawProducts
                .AsNoTracking()
                .Where(r => r.Code == request.Code)
                .ProjectTo<RawProductDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (dto == null)
            {
                return Result<RawProductDto>.Failure(
                    new AppError(ErrorCode.NotFound, "RawProduct not found.", $"Code: {request.Code}")
                );
            }

            return Result<RawProductDto>.Success(dto);
        }
    }
}
