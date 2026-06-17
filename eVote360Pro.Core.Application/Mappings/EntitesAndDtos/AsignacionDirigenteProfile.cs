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
        }
    }
}
