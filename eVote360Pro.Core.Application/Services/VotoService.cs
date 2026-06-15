using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using Org.BouncyCastle.Crypto;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService: GenericService<VotoDto, Voto>, IVotoService
    {
        private readonly IVotoRepository _repository;
        private readonly IMapper _mapper;

        public VotoService(
            IVotoRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CiudadanoYaVotoAsync(
            int ciudadanoId,
            int eleccionId)
        {
            return await _repository
                .CiudadanoYaVotoAsync(
                    ciudadanoId,
                    eleccionId);
        }

        public async Task<bool> PuedeVotarAsync(
            int ciudadanoId,
            int eleccionId)
        {
            return !await CiudadanoYaVotoAsync(
                ciudadanoId,
                eleccionId);
        }

        public async Task RegistrarVotoAsync(
            VotoDto dto)
        {
            bool yaVoto =
                await CiudadanoYaVotoAsync(
                    dto.CiudadanoId,
                    dto.EleccionId);

            if (yaVoto)
            {
                throw new Exception(
                    "El ciudadano ya votó en esta elección.");
            }

            var voto =
                _mapper.Map<Voto>(dto);

            voto.FechaVoto = DateTime.Now;

            await _repository.AddAsync(voto);
        }

        public async Task<int>
            CountCiudadanosVotaronAsync(
            int eleccionId)
        {
            return await _repository
                .CountCiudadanosVotaronAsync(
                    eleccionId);
        }
    }
}