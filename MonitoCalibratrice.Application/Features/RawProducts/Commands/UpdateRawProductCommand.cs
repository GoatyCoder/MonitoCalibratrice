using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.RawProducts.Commands
{
    public record UpdateRawProductCommand(
        Guid Id,
        string Code,
        string Name,
        string Description
    ) : IRequest<Result<RawProductDto>>;

    public class UpdateRawProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<UpdateRawProductCommand, Result<RawProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<RawProductDto>> Handle(UpdateRawProductCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.RawProducts.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result<RawProductDto>.Failure(
                    new AppError(ErrorCode.NotFound, "RawProduct not found.", $"Id: {request.Id}")
                );
            }

            if (!string.Equals(entity.Code, request.Code, StringComparison.OrdinalIgnoreCase) &&
                await context.RawProducts.AnyAsync(r => r.Code == request.Code, cancellationToken))
            {
                return Result<RawProductDto>.Failure(
                    new AppError(ErrorCode.DuplicateCode, $"RawProduct with code '{request.Code}' already exists.", $"Code: {request.Code}")
                );
            }

            _mapper.Map(request, entity);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<RawProductDto>(entity);
            return Result<RawProductDto>.Success(dto);
        }
    }
}
