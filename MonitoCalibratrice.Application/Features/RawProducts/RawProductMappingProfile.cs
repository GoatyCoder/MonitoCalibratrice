using AutoMapper;
using MonitoCalibratrice.Application.Features.RawProducts.Commands;
using MonitoCalibratrice.Application.Features.RawProducts.DTOs;
using MonitoCalibratrice.Domain.Entities;

namespace MonitoCalibratrice.Application.Features.RawProducts
{
    public class RawProductMappingProfile : Profile
    {
        public RawProductMappingProfile()
        {
            CreateMap<RawProduct, RawProductDto>();
            CreateMap<CreateRawProductCommand, RawProduct>();
            CreateMap<UpdateRawProductCommand, RawProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
