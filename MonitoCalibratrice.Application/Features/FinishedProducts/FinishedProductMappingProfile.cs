using AutoMapper;
using MonitoCalibratrice.Application.Features.FinishedProducts.Commands;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Domain.Entities;

namespace MonitoCalibratrice.Application.Features.FinishedProducts
{
    public class FinishedProductMappingProfile : Profile
    {
        public FinishedProductMappingProfile()
        {
            CreateMap<FinishedProduct, FinishedProductDto>();
            CreateMap<CreateFinishedProductCommand, FinishedProduct>();
            CreateMap<UpdateFinishedProductCommand, FinishedProduct>();
        }
    }
}
