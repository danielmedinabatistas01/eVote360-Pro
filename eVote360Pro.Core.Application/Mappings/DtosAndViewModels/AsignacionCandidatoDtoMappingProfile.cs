using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.AsignacionCandidato;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class AsignacionCandidatoDtoMappingProfile : Profile
    {
        public AsignacionCandidatoDtoMappingProfile()
        {
            CreateMap<AsignacionCandidatoViewModel, AsignacionCandidatoDto>().ReverseMap();
            CreateMap<SaveAsignacionCandidatoViewModel, AsignacionCandidatoDto>().ReverseMap();

            CreateMap<AsignacionCandidato, AsignacionCandidatoDto>()
            .ForMember(d => d.NombreCandidato,
                o => o.MapFrom(s => s.Candidato != null
                    ? s.Candidato.Nombre + " " + s.Candidato.Apellido
                    : string.Empty))
            .ForMember(d => d.NombrePuesto,
                o => o.MapFrom(s => s.PuestoElectivo != null
                    ? s.PuestoElectivo.Nombre
                    : string.Empty))
            .ForMember(d => d.NombreEleccion,
                o => o.MapFrom(s => s.Eleccion != null
                    ? s.Eleccion.Nombre
                    : string.Empty))
            .ReverseMap();
        }
    }
}
