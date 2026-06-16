using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.ViewModels.Usuario;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDto, UsuarioIndexViewModel>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioEditViewModel>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioCreateViewModel>().ReverseMap();
        }
    }
}