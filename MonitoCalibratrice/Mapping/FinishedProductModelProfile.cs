using AutoMapper;
using MonitoCalibratrice.Application.Features.FinishedProducts.DTOs;
using MonitoCalibratrice.Models;

namespace MonitoCalibratrice.Mapping
{
    public class FinishedProductModelProfile : Profile
    {
        public FinishedProductModelProfile()
        {
            CreateMap<FinishedProductDto, FinishedProductModel>().ReverseMap();
        }
    }
}
