using AutoMapper;
using MonitoCalibratrice.Application.Features.ProductionBatches.DTOs;
using MonitoCalibratrice.Models;

namespace MonitoCalibratrice.Mapping
{
    public class ProductionBatchModelProfile : Profile
    {
        public ProductionBatchModelProfile()
        {
            CreateMap<ProductionBatchDto, ProductionBatchModel>().ReverseMap();
        }
    }
}
