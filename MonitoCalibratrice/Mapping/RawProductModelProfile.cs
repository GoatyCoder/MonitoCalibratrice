using AutoMapper;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Models;

namespace MonitoCalibratrice.Mapping
{
    public class RawProductViewModelProfile : Profile
    {
        public RawProductViewModelProfile()
        {
            CreateMap<RawProductDto, RawProductModel>().ReverseMap();
        }
    }
}
