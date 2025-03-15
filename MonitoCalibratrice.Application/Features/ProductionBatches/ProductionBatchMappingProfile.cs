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
            CreateMap<ProductionBatch, ProductionBatchDto>()
                .ForMember(dest => dest.ProductionLineName, opt => opt.MapFrom(src => src.ProductionLine.Name))
                .ForMember(dest => dest.RawProductName, opt => opt.MapFrom(src => src.RawProduct.Name))
                .ForMember(dest => dest.VarietyName, opt => opt.MapFrom(src => src.Variety.Name))
                .ForMember(dest => dest.FinishedProductName, opt => opt.MapFrom(src => src.FinishedProduct.Name))
                .ForMember(dest => dest.SecondaryPackagingName, opt => opt.MapFrom(src => src.SecondaryPackaging.Name))
                .ForMember(dest => dest.TotalUnits, opt => opt.MapFrom(src => src.Pallets.Sum(p => p.Units)));
            ;

            CreateMap<CreateProductionBatchCommand, ProductionBatch>()
                .ForMember(dest => dest.FinishedAt, opt => opt.MapFrom(_ => (DateTime?)null));

            CreateMap<UpdateProductionBatchCommand, ProductionBatch>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }
    }
}
