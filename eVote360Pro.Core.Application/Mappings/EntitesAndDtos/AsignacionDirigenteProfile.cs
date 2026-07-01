using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class AsignacionDirigenteProfile : Profile
    {
        public AsignacionDirigenteProfile()
        {
            CreateMap<AsignacionDirigente, AsignacionDirigenteDto>()
                .ReverseMap();

            CreateMap<AsignacionDirigente, AsignacionDirigenteDto>()
                 .ForMember(
                    dest => dest.NombreDirigente,
                    opt => opt.MapFrom(src =>
                        src.Usuario.Nombre + " " + src.Usuario.Apellido))

                .ForMember(
                    dest => dest.NombrePartido,
                    opt => opt.MapFrom(src =>
                        src.PartidoPolitico.Nombre));
        }
    }
}
