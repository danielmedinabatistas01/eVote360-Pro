using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Eleccion, EleccionDTO>().ReverseMap();
            CreateMap<Voto, VotoDto>().ReverseMap();
            CreateMap<VotoDetalle, VotoDetalleDTO>().ReverseMap();
            CreateMap<EleccionPuestoElectivo, EleccionPuestoElectivoDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}