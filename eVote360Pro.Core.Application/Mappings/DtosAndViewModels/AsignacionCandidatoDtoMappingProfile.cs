using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.AsignacionCandidato;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class AsignacionCandidatoDtoMappingProfile : Profile
    {
        public AsignacionCandidatoDtoMappingProfile()
        {
            CreateMap<AsignacionCandidatoViewModel, AsignacionCandidatoDto>().ReverseMap();
            CreateMap<SaveAsignacionCandidatoViewModel, AsignacionCandidatoDto>().ReverseMap();
        }
    }
}
