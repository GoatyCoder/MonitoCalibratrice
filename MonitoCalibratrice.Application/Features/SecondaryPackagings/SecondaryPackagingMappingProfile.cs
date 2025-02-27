using AutoMapper;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.Commands;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.DTOs;
using MonitoCalibratrice.Domain.Entities;

namespace MonitoCalibratrice.Application.Features.SecondaryPackagings
{
    public class SecondaryPackagingMappingProfile : Profile
    {
        public SecondaryPackagingMappingProfile()
        {
            CreateMap<SecondaryPackaging, SecondaryPackagingDto>();
            CreateMap<CreateSecondaryPackagingCommand, SecondaryPackaging>();
            CreateMap<UpdateSecondaryPackagingCommand, SecondaryPackaging>();
        }
    }
}
