using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService
        : GenericService<VotoDto, Voto>, IVotoService
    {
        private readonly IVotoRepository _votoRepository;

        public VotoService(
            IVotoRepository votoRepository,
            IMapper mapper)
            : base(votoRepository, mapper)
        {
            _votoRepository = votoRepository;
        }

        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {
            return await _votoRepository.CiudadanoYaVotoAsync(ciudadanoId, eleccionId);
        }

        public async Task<bool> PuedeVotarAsync(int ciudadanoId, int eleccionId)
        {
            return !await CiudadanoYaVotoAsync(ciudadanoId, eleccionId);
        }

        public async Task RegistrarVotoAsync(VotoDto dto)
        {
            bool yaVoto = await CiudadanoYaVotoAsync(
                dto.CiudadanoId,
                dto.EleccionId);

            if (yaVoto)
            {
                throw new Exception("El ciudadano ya votó en esta elección.");
            }

            dto.FechaVotacion = DateTime.Now;

            await AddAsync(dto);
        }

        public async Task CrearVotoAsync(VotoDto dto)
        {
            await RegistrarVotoAsync(dto);
        }

        public async Task<int> CountCiudadanosVotaronAsync(int eleccionId)
        {
            return await _votoRepository.CountCiudadanosVotaronAsync(eleccionId);
        }

        public async Task<List<VotoDto>> GetByEleccionIdAsync(int eleccionId)
        {
            var votos = await _votoRepository.GetByEleccionIdAsync(eleccionId);

            return _mapper.Map<List<VotoDto>>(votos);
        }
    }
}