using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _repository;

        public VotoService(IVotoRepository repository)
        {
            _repository = repository;
        }

        public async Task CrearVotoAsync(VotoDto dto)
        {
            await RegistrarVotoAsync(dto);
        }

        public async Task<int> CountCiudadanosVotaronAsync(int eleccionId)
        {
            var votos = await _repository.GetAllList();
            return votos.Count(x => x.EleccionId == eleccionId);
        }

        public async Task<List<VotoDto>> GetByEleccionIdAsync(int eleccionId)
        {
            var votos = await _repository.GetAllList();

            return votos
                .Where(x => x.EleccionId == eleccionId)
                .Select(x => new VotoDto
                {
                    Id = x.Id,
                    CiudadanoId = x.CiudadanoId,
                    EleccionId = x.EleccionId,
                    FechaVotacion = x.FechaVotacion
                })
                .ToList();
        }

        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {
            return await _repository.CiudadanoYaVotoAsync(ciudadanoId, eleccionId);
        }

        public async Task<bool> PuedeVotarAsync(int ciudadanoId, int eleccionId)
        {
            return !await _repository.CiudadanoYaVotoAsync(ciudadanoId, eleccionId);
        }

        public async Task RegistrarVotoAsync(VotoDto dto)
        {
            if (await _repository.CiudadanoYaVotoAsync(dto.CiudadanoId, dto.EleccionId))
                throw new Exception("El ciudadano ya votó.");

            await _repository.AddAsync(new Voto
            {
                CiudadanoId = dto.CiudadanoId,
                EleccionId = dto.EleccionId,
                FechaVotacion = DateTime.Now
            });
        }
    }
}