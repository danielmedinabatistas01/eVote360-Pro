using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class AsignacionDirigenteService
        : GenericService<AsignacionDirigenteDto, AsignacionDirigente>,
          IAsignacionDirigenteService
    {
        private readonly IAsignacionDirigenteRepository _repository;

        public AsignacionDirigenteService(
            IAsignacionDirigenteRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public override async Task AddAsync(
            AsignacionDirigenteDto dto)
        {
            if (await _repository.PartidoTieneDirigenteAsync(dto.PartidoPoliticoId))
                throw new Exception("El partido ya tiene un dirigente.");

            if (await _repository.UsuarioTienePartidoAsync(dto.UsuarioId))
                throw new Exception("El dirigente ya pertenece a un partido.");

            await base.AddAsync(dto);
        }
    }
}