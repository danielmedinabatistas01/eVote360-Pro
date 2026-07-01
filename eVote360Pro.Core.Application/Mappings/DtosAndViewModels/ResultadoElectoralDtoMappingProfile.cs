using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.ViewModels.PuestoElectivo;
using eVote360Pro.Core.Application.ViewModels.ResultadoElectoral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Mappings.DtosAndViewModels
{
    public class ResultadoElectoralDtoMappingProfile : Profile
    {
        public ResultadoElectoralDtoMappingProfile()
        {
            CreateMap<ResultadoElectoralDTO, ResultadoPorPuestoViewModel>()
    .ForMember(dest => dest.NombrePuesto,
        opt => opt.MapFrom(src => src.NombrePuestoElectivo));
        }
    }
}
