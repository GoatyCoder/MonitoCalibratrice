using AutoMapper;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Models;

namespace MonitoCalibratrice.Mapping
{
    public class SecondaryPackagingModelProfile : Profile
    {
        public SecondaryPackagingModelProfile()
        {
            CreateMap<SecondaryPackagingDto, SecondaryPackagingModel>().ReverseMap();
        }
    }
}
