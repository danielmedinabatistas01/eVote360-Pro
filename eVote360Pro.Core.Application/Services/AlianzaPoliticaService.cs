using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;

namespace eVote360Pro.Core.Application.Services
{
    public class AlianzaPoliticaService : IAlianzaPoliticaService
    {
        private readonly IAlianzaPoliticaRepository _repository;

        public AlianzaPoliticaService(
            IAlianzaPoliticaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AlianzaPoliticaDto>> GetAllAsync()
        {
            var alianzas = await _repository.GetAllList();

            return alianzas.Select(x => new AlianzaPoliticaDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Estado = x.Estado
            }).ToList();
        }

        public async Task<AlianzaPoliticaDto?> GetByIdAsync(int id)
        {
            var alianza = await _repository.GetById(id);

            if (alianza == null)
                return null;

            return new AlianzaPoliticaDto
            {
                Id = alianza.Id,
                Nombre = alianza.Nombre,
                Descripcion = alianza.Descripcion,
                Estado = alianza.Estado
            };
        }

        public async Task AddAsync(AlianzaPoliticaDto dto)
        {
            await _repository.AddAsync(new AlianzaPolitica
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado
            });
        }

        public async Task UpdateAsync(
            int id,
            AlianzaPoliticaDto dto)
        {
            await _repository.UpdateAsync(id,
                new AlianzaPolitica
                {
                    Id = id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Estado = dto.Estado
                });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task ActivarAsync(int id)
        {
            var alianza = await _repository.GetById(id);

            if (alianza == null)
                throw new Exception("Alianza no encontrada.");

            alianza.Estado = true;

            await _repository.UpdateAsync(id, alianza);
        }

        public async Task DesactivarAsync(int id)
        {
            var alianza = await _repository.GetById(id);

            if (alianza == null)
                throw new Exception("Alianza no encontrada.");

            alianza.Estado = false;

            await _repository.UpdateAsync(id, alianza);
        }

        public async Task<List<AlianzaPoliticaDto>> GetActivosAsync()
        {
            var alianzas = await _repository.GetActivosAsync();

            return alianzas.Select(x => new AlianzaPoliticaDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Estado = x.Estado
            }).ToList();
        }
    }
}