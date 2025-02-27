using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings.Commands
{
    public record CreateSecondaryPackagingCommand(string Code, string Name, string Description)
        : IRequest<Result<SecondaryPackagingDto>>;

    public class CreateSecondaryPackagingCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<CreateSecondaryPackagingCommand, Result<SecondaryPackagingDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SecondaryPackagingDto>> Handle(CreateSecondaryPackagingCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            if (await context.SecondaryPackagings.AnyAsync(sp => sp.Code == request.Code, cancellationToken))
            {
                
            }
                //return Result<SecondaryPackagingDto>.Failure("SecondaryPackaging Code must be unique.");

            var entity = _mapper.Map<SecondaryPackaging>(request);

            await context.SecondaryPackagings.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<SecondaryPackagingDto>(entity);
            return Result<SecondaryPackagingDto>.Success(dto);
        }
    }
}
