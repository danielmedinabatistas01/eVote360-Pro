using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class ProcesoVotacionService
        : IProcesoVotacionService
    {
        private readonly IOcrService _ocrService;
        private readonly ICiudadanoRepository _ciudadanoRepository;
        private readonly ICodigoVerificacionRepository _codigoRepository;

        public ProcesoVotacionService(
            IOcrService ocrService,
            ICiudadanoRepository ciudadanoRepository,
            ICodigoVerificacionRepository codigoRepository)
        {
            _ocrService = ocrService;
            _ciudadanoRepository = ciudadanoRepository;
            _codigoRepository = codigoRepository;
        }
        public async Task<bool> ValidarCedulaAsync(
            string numeroDocumento)
        {
            var ciudadano =
                await _ciudadanoRepository
                    .GetByCedulaAsync(numeroDocumento);

            return ciudadano != null;
        }

        public async Task<bool> ValidarIdentidadOcrAsync(
            string numeroDocumento, 
            string rutaImagen)
        {
            string? cedulaExtraida =
                await _ocrService.ExtraerCedulaAsync(rutaImagen);

            return cedulaExtraida == numeroDocumento;
        }

        public async Task<string> GenerarCodigoAsync(
            int ciudadanoId)
        {
            string codigo =
                Random.Shared
                    .Next(100000, 999999)
                    .ToString();

            await _codigoRepository.AddAsync(
                new CodigoVerificacion
                {
                    CiudadanoId = ciudadanoId,
                    Codigo = codigo,
                    FechaGeneracion = DateTime.Now,
                    FechaExpiracion = DateTime.Now.AddMinutes(5),
                    Utilizado = false
                });

            return codigo;
        }

        public async Task<bool> ValidarCodigoAsync(
            int ciudadanoId,
            string codigo)
        {
            var entity =
                await _codigoRepository
                    .GetCodigoAsync(
                        ciudadanoId,
                        codigo);

            if (entity == null)
                return false;

            if (entity.Utilizado)
                return false;

            if (entity.FechaExpiracion < DateTime.Now)
                return false;

            return true;
        }
    }
}