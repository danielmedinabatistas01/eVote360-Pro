using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _repository;

        public VotoService(
            IVotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VotoDto>> GetAllAsync()
        {
            var votos = await _repository.GetAllList();

            return votos.Select(x => new VotoDto
            {
                Id = x.Id,
                CiudadanoId = x.CiudadanoId,
                EleccionId = x.EleccionId,
                FechaVotacion = x.FechaVotacion

            }).ToList();
        }

        public async Task<VotoDto?> GetByIdAsync(int id)
        {
            var voto = await _repository.GetById(id);

            if (voto == null)
                return null;

            return new VotoDto
            {
                Id = voto.Id,
                CiudadanoId = voto.CiudadanoId,
                EleccionId = voto.EleccionId,
                FechaVotacion = voto.FechaVotacion
            };
        }

        public async Task AddAsync(VotoDto dto)
        {
            await _repository.AddAsync(new Voto
            {
                CiudadanoId = dto.CiudadanoId,
                EleccionId = dto.EleccionId,
                FechaVotacion = dto.FechaVotacion
            });
        }

        public async Task UpdateAsync(int id, VotoDto dto)
        {
            await _repository.UpdateAsync(id, new Voto
            {
                CiudadanoId = dto.CiudadanoId,
                EleccionId = dto.EleccionId,
                FechaVotacion = dto.FechaVotacion
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<bool>CiudadanoYaVotoAsync(int ciudadanoId,int eleccionId)
        {
            return await _repository
                .CiudadanoYaVotoAsync(
                    ciudadanoId,
                    eleccionId);
        }

        public async Task<bool>PuedeVotarAsync(int ciudadanoId,int eleccionId)
        {
            bool yaVoto =
                await _repository
                    .CiudadanoYaVotoAsync(
                        ciudadanoId,
                        eleccionId);

            return !yaVoto;
        }

        public async Task RegistrarVotoAsync(VotoDto dto)
        {
            bool yaVoto =
                await _repository
                    .CiudadanoYaVotoAsync(
                        dto.CiudadanoId,
                        dto.EleccionId);

            if (yaVoto)
            {
                throw new Exception(
                    "El ciudadano ya votó.");
            }

            await _repository.AddAsync(
                new Voto
                {
                    CiudadanoId = dto.CiudadanoId,
                    EleccionId = dto.EleccionId,
                    FechaVotacion = DateTime.Now
                });
        }

    }
}
