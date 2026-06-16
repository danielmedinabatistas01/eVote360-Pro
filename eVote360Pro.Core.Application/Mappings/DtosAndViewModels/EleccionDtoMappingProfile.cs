using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.ViewModels.Eleccion;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class EleccionProfile : Profile
    {
        public EleccionProfile()
        {
            CreateMap<EleccionDTO, EleccionIndexViewModel>().ReverseMap();
            CreateMap<EleccionDTO, EleccionCreateViewModel>().ReverseMap();
            CreateMap<EleccionDTO, EleccionEditViewModel>().ReverseMap();
            CreateMap<EleccionDTO, EleccionActivarViewModel>().ReverseMap();
            CreateMap<EleccionDTO, EleccionFinalizarViewModel>().ReverseMap();
        }
    }
}