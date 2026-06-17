using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class PuestoElectivoProfile : Profile
    {
        public PuestoElectivoProfile()
        {
            CreateMap<PuestoElectivo, PuestoElectivoDto>()
                .ReverseMap();
        }
    }
}
