using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.AlianzaPolitica;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class AlianzaPoliticaDtoMappingProfile : Profile
    {
        public AlianzaPoliticaDtoMappingProfile()
        {
            CreateMap<AlianzaPoliticaViewModel, AlianzaPoliticaDto>().ReverseMap();
            CreateMap<SaveAlianzaPoliticaViewModel, AlianzaPoliticaDto>().ReverseMap();
            CreateMap<AlianzaPoliticaDto,DeleteAlianzaPoliticaViewModel>().ReverseMap();
        }
    }
}
