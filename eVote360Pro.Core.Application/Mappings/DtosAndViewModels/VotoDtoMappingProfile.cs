using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.Voto;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class VotoDtoMappingProfile : Profile
    {
        public VotoDtoMappingProfile()
        {
            CreateMap<EmitirVotoViewModel, VotoDto>().ReverseMap();
        }
    }
}
