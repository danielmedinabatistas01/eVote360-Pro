using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class BasicDtoMappingProfile: Profile
    {
        public BasicDtoMappingProfile() {
        
            CreateMap(typeof(BasicDto<>), typeof(BaseEntity<>)).ReverseMap();
        }
    }
}
