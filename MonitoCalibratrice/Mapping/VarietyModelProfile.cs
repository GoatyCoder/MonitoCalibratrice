using AutoMapper;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Models;

namespace MonitoCalibratrice.Mapping
{
    public class VarietyModelProfile : Profile
    {
        public VarietyModelProfile()
        {
            CreateMap<VarietyDto, VarietyModel>().ReverseMap();
        }
    }
}
