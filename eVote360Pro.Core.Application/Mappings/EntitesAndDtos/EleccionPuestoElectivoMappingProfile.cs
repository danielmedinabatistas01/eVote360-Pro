using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings.EntitiesAndDtos
{
    public class EleccionPuestoElectivoProfile : Profile
    {
        public EleccionPuestoElectivoProfile()
        {
            CreateMap<EleccionPuestoElectivo, EleccionPuestoElectivoDTO>().ReverseMap();
        }
    }
}