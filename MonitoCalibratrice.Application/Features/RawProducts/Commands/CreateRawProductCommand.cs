using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.RawProducts.Commands
{
    public record CreateRawProductCommand(
        string Code,
        string Name,
        string Description
    ) : IRequest<Result<RawProductDto>>;

    public class CreateRawProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<CreateRawProductCommand, Result<RawProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<RawProductDto>> Handle(CreateRawProductCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            if (await context.RawProducts.AnyAsync(r => r.Code == request.Code, cancellationToken))
            {
                return Result<RawProductDto>.Failure(
                    new AppError(ErrorCode.DuplicateCode, $"RawProduct with code '{request.Code}' already exists.", $"Code: {request.Code}")
                );
            }

            var entity = _mapper.Map<RawProduct>(request);

            await context.RawProducts.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<RawProductDto>(entity);
            return Result<RawProductDto>.Success(dto);
        }
    }
}
