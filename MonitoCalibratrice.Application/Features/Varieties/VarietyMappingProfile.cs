using AutoMapper;
using MonitoCalibratrice.Application.Features.Varieties.Commands;
using MonitoCalibratrice.Application.Features.Varieties.DTOs;
using MonitoCalibratrice.Domain.Entities;

namespace MonitoCalibratrice.Application.Features.Varieties
{
    public class VarietyMappingProfile : Profile
    {
        public VarietyMappingProfile()
        {
            CreateMap<Variety, VarietyDto>()
                .ForMember(dest => dest.RawProductName, opt => opt.MapFrom(src => src.RawProduct.Name));
            CreateMap<CreateVarietyCommand, Variety>();
            CreateMap<UpdateVarietyCommand, Variety>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
