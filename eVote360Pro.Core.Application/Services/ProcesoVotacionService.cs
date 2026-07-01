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
            if (string.IsNullOrWhiteSpace(
                numeroDocumento))
            {
                throw new Exception(
                    "Debe ingresar un número de cédula.");
            }

            var ciudadano =
                await _ciudadanoRepository
                    .GetByCedulaAsync(
                        numeroDocumento);

            if (ciudadano == null)
            {
                throw new Exception(
                    "La cédula ingresada no existe.");
            }

            if (!ciudadano.EsActivo)
            {
                throw new Exception(
                    "Este ciudadano se encuentra inactivo.");
            }

            return true;
        }

        public async Task<bool>
      ValidarIdentidadOcrAsync(
      string numeroDocumento,
      byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                throw new Exception(
                    "Debe cargar una imagen.");
            }

            string? cedulaExtraida =
                await _ocrService
                    .ExtraerCedulaAsync(
                        imageBytes);

            if (cedulaExtraida == null)
            {
                throw new Exception(
                    "No fue posible identificar una cédula en la imagen.");
            }

            if (cedulaExtraida !=
                numeroDocumento)
            {
                throw new Exception(
                    "La cédula detectada no coincide con la ingresada.");
            }

            return true;
        }

        public async Task<string> GenerarCodigoAsync(
            int ciudadanoId,
            int eleccionId)
        {
            string codigo =
                Random.Shared
                    .Next(100000, 999999)
                    .ToString();

            var ciudadano = await _ciudadanoRepository
        .GetById(ciudadanoId);

            if (ciudadano == null)
            {
                throw new Exception(
                    "Ciudadano no encontrado.");
            }

            if (!ciudadano.EsActivo)
            {
                throw new Exception(
                    "El ciudadano se encuentra inactivo.");
            }

            await _codigoRepository.AddAsync(
                new CodigoVerificacion
                {
                    CiudadanoId = ciudadanoId,
                    EleccionId = eleccionId,
                    Codigo = codigo,
                    FechaGeneracion = DateTime.Now,
                    FechaExpiracion =
                        DateTime.Now.AddMinutes(5),
                    Utilizado = false
                });

            return codigo;
        }

        public async Task<bool> ValidarCodigoAsync(
            int ciudadanoId,
            int eleccionId,
            string codigo)
        {
            if (string.IsNullOrWhiteSpace(
                codigo))
            {
                throw new Exception(
                    "Debe ingresar el código de verificación.");
            }

            var entity =
                await _codigoRepository
                    .GetCodigoAsync(
                        ciudadanoId,
                        eleccionId,
                        codigo);

            if (entity == null)
            {
                throw new Exception(
                    "Código inválido.");
            }

            if (entity.Utilizado)
            {
                throw new Exception(
                    "Este código ya fue utilizado.");
            }

            if (entity.FechaExpiracion <
                DateTime.Now)
            {
                throw new Exception(
                    "El código ha expirado.");
            }

            entity.Utilizado = true;
            await _codigoRepository.UpdateAsync(entity.Id, entity);

            return true;
        }
    }
}