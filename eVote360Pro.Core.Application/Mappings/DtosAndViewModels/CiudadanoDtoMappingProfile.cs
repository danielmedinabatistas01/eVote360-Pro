using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.Ciudadano;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class CiudadanoDtoMappingProfile : Profile
    {
        public CiudadanoDtoMappingProfile()
        {
            CreateMap<CiudadanoDto, CiudadanoViewModel>().ReverseMap();
            CreateMap<CiudadanoDto, SaveCiudadanoViewModel>().ReverseMap();
        }
    }
}
