using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class CiudadanoService
        : GenericService<CiudadanoDto, Ciudadano>,
          ICiudadanoService
    {
        private readonly ICiudadanoRepository _repository;

        public CiudadanoService(
            ICiudadanoRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public override async Task AddAsync(CiudadanoDto dto)
        {
            if (await _repository.ExisteDocumentoAsync(dto.DocumentoIdentidad))
                throw new Exception("El documento ya existe.");

            if (await _repository.ExisteCorreoAsync(dto.CorreoElectronico))
                throw new Exception("El correo ya existe.");

            await base.AddAsync(dto);
        }
    }
}