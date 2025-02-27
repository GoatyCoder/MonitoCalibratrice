﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Application.Common;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Infrastructure;

namespace MonitoCalibratrice.Application.Features.FinishedProducts.Commands
{
    public record CreateFinishedProductCommand(string Code, string Name, string Description, string? Ean)
        : IRequest<Result<FinishedProductDto>>;

    public class CreateFinishedProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper) : IRequestHandler<CreateFinishedProductCommand, Result<FinishedProductDto>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<FinishedProductDto>> Handle(CreateFinishedProductCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            if (await context.FinishedProducts.AnyAsync(fp => fp.Code == request.Code, cancellationToken))
            {
                
            }
                //return Result<FinishedProductDto>.Failure("FinishedProduct Code must be unique.");

            var entity = _mapper.Map<FinishedProduct>(request);

            await context.FinishedProducts.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<FinishedProductDto>(entity);
            return Result<FinishedProductDto>.Success(dto);
        }
    }
}
