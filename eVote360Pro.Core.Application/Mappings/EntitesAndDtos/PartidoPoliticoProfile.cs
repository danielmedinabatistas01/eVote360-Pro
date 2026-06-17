using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class PartidoPoliticoProfile : Profile
    {
        public PartidoPoliticoProfile()
        {
            CreateMap<PartidoPolitico, PartidoPoliticoDto>()
                .ReverseMap();
        }
    }
}
