using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.AsignacionDirigente;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class AsignacionDirigenteDtoMappingProfile : Profile
    {
        public AsignacionDirigenteDtoMappingProfile()
        {
            CreateMap<AsignacionDirigenteDto, AsignacionDirigenteViewModel>().ReverseMap();
            CreateMap<AsignacionDirigenteDto, SaveAsignacionDirigenteViewModel>().ReverseMap();
        }
    }
}
