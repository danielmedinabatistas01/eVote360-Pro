using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class userProfile : Profile
    {
        public userProfile() 
        {
            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();
        }
    }
}
