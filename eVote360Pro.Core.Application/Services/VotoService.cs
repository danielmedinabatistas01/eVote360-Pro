using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService
        : GenericService<VotoDto, Voto>,
          IVotoService
    {
        private readonly IVotoRepository _votoRepository;

        private readonly IEleccionRepository _eleccionRepository;

        private readonly ICiudadanoRepository _ciudadanoRepository;

        public VotoService(
            IVotoRepository votoRepository,
            IEleccionRepository eleccionRepository,
            ICiudadanoRepository ciudadanoRepository,
            IMapper mapper)
            : base(votoRepository, mapper)
        {
            _votoRepository = votoRepository;

            _eleccionRepository = eleccionRepository;

            _ciudadanoRepository = ciudadanoRepository;
        }

        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {


            return await _votoRepository
                .CiudadanoYaVotoAsync(
                    ciudadanoId,
                    eleccionId);
        }

        public async Task<bool> PuedeVotarAsync(int ciudadanoId, int eleccionId)
        {
            var ciudadano =
                await _ciudadanoRepository
                    .GetById(ciudadanoId);

            if (ciudadano == null)
            {
                return false;
            }

            var eleccion =
                await _eleccionRepository
                    .GetById(eleccionId);

            if (eleccion == null)
            {
                return false;
            }

            bool yaVoto =
                await CiudadanoYaVotoAsync(
                    ciudadanoId,
                    eleccionId);

            return !yaVoto;
        }

        public async Task RegistrarVotoAsync(VotoDto dto)
        {
            var ciudadano =
                await _ciudadanoRepository
                    .GetById(dto.CiudadanoId);

            if (ciudadano == null)
            {
                throw new Exception(
                    "Ciudadano no encontrado.");
            }

            var eleccion =
                await _eleccionRepository
                    .GetById(dto.EleccionId);

            if (eleccion == null)
            {
                throw new Exception(
                    "La elección no existe.");
            }

            bool yaVoto =
                await CiudadanoYaVotoAsync(
                    dto.CiudadanoId,
                    dto.EleccionId);

            if (yaVoto)
            {
                throw new Exception("El ciudadano ya votó en esta elección.");
            }

            var voto =
                _mapper.Map<Voto>(dto);

            voto.FechaVoto =
                DateTime.Now;

            await _votoRepository
                .AddAsync(voto);
        }

        public async Task CrearVotoAsync(VotoDto dto)
        {
            await RegistrarVotoAsync(dto);
        }

        public async Task<int> CountCiudadanosVotaronAsync(int eleccionId)
        {
            return await _votoRepository.CountCiudadanosVotaronAsync(eleccionId);
        }

        public async Task CrearVotoAsync(
            VotoDto dto)
        {
            await RegistrarVotoAsync(dto);
        }
    }
}