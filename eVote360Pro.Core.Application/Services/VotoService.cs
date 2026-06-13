using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _votoRepository;

        public VotoService(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        public async Task<bool> RegistrarVotoAsync(VotoDTO dto)
        {
            bool yaVoto = await _votoRepository.CiudadanoYaVotoAsync(
                dto.CiudadanoId,
                dto.EleccionId);

            if (yaVoto)
            {
                return false;
            }

            Voto voto = new()
            {
                CiudadanoId = dto.CiudadanoId,
                EleccionId = dto.EleccionId,
                FechaVotacion = dto.FechaVoto,
            };

            foreach (var detalle in dto.VotoDetalles)
            {
                voto.VotoDetalles.Add(new VotoDetalle
                {
                    CandidatoId = detalle.CandidatoId,
                    PuestoElectivoId = detalle.PuestoElectivoId
                });
            }

            await _votoRepository.AddAsync(voto);

            return true;
        }

        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {
            return await _votoRepository.CiudadanoYaVotoAsync(
                ciudadanoId,
                eleccionId);
        }

        public async Task<int> ObtenerCantidadVotantesAsync(int eleccionId)
        {
            return await _votoRepository.CountCiudadanosVotaronAsync(eleccionId);
        }
    }
}