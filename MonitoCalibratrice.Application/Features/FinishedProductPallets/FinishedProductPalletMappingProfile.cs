using AutoMapper;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.Commands;
using MonitoCalibratrice.Application.Features.FinishedProductPallets.DTOs;
using MonitoCalibratrice.Domain.Entities;

namespace MonitoCalibratrice.Application.Features.FinishedProductPallets
{
    public class FinishedProductPalletMappingProfile : Profile
    {
        public FinishedProductPalletMappingProfile()
        {
            CreateMap<FinishedProductPallet, FinishedProductPalletDto>();
            CreateMap<CreateFinishedProductPalletCommand, FinishedProductPallet>()
                .ForMember(dest => dest.PalletCode, opt => opt.Ignore());
            CreateMap<UpdateFinishedProductPalletCommand, FinishedProductPallet>()
               .ForMember(dest => dest.PalletCode, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
