using AutoMapper;
using MonitoCalibratrice.Application.Features.ProductionBatches.Commands;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Domain.Entities;

namespace MonitoCalibratrice.Application.Features.ProductionBatches
{
    public class ProductionBatchMappingProfile : Profile
    {
        public ProductionBatchMappingProfile()
        {
            CreateMap<ProductionBatch, ProductionBatchDto>();

            CreateMap<CreateProductionBatchCommand, ProductionBatch>()
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(_ => (DateTime?)null));

            CreateMap<UpdateProductionBatchCommand, ProductionBatch>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
