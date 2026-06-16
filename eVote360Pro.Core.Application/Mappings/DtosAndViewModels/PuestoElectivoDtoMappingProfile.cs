using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.ViewModels.PuestoElectivo;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class PuestoElectivoDtoMappingProfile : Profile
    {
        public PuestoElectivoDtoMappingProfile()
        {
            CreateMap<PuestoElectivoDto, PuestoElectivoViewModel>().ReverseMap();
        }
    }
}
