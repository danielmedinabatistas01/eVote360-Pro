using AutoMapper;
using eVote360Pro.Core.Application.DTOs;


namespace eVote360Pro.Core.Application.Mappings.EntitiesAndDtos
{
    public class VotoDetalleProfile : Profile
    {
        public VotoDetalleProfile()
        {
            CreateMap<VotoDetalle, VotoDetalleDTO>().ReverseMap();
        }
    }
}