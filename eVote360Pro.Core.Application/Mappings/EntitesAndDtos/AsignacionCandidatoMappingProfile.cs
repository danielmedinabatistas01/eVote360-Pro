using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Domain.Common;
using eVote360Pro.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Mappings.EntitesAndDtos
{
    public class AsignacionCandidatoDtoMappingProfile : Profile
    {
        public AsignacionCandidatoDtoMappingProfile()
        {
            CreateMap<AsignacionCandidato, AsignacionCandidatoDto>()
                .ReverseMap();
        }
    }
}
