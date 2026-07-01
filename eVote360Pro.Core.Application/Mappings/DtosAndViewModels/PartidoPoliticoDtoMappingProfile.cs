using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.PartidoPolitico;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class PartidoPoliticoDtoMappingProfile : Profile
    {
        public PartidoPoliticoDtoMappingProfile()
        {
            CreateMap<PartidoPoliticoDto, PartidoPoliticoViewModel>().ReverseMap();
            CreateMap<PartidoPoliticoDto, SavePartidoPoliticoViewModel>().ReverseMap();
        }
    }
}
